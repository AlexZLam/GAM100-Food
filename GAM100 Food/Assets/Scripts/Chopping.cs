using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.SceneView;
using static UnityEngine.Rendering.HableCurve;

public class Chopping : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject chopping;

    public GameObject knife;
    public GameObject[] slice_x_position_objs;
    public float slice_tolerance = 15f;

    private int chops_current = 0;
    private bool[] slice_bools;
    private float[] slice_positions;
    private int chops_goal;
    private bool game_won;
    private bool game_started;
    private bool knife_currently_chopping = false;

    /*
    chopping pseudocode
    - make knife follow mouse (on update, knife position = mouse position)
    - click = chop = knife sprite goes down (have knife slip into two segments, front and back )
    - if chopped wrong, lose/restart
    - if chopped correctly, draw a line at that spot
       - draw line: sprite for each line 
    - to check if chopped correctly, see if x of mouse is in the right spot (array of x coords for 
      where the chops should be, check if mouse is within [tolerance] pixels left/right of that)  
      AND if i've chopped here already, dont count it toward the total (have array of bools that aligns 
       w positions, true if been chopped before)
    - if all chops done, win
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        chops_goal = slice_x_position_objs.Length;
        slice_positions = new float[chops_goal];
        Debug.Log("Chopping slice positions: " );
        for (int i = 0; i < slice_positions.Length; i++)
        {
            slice_positions[i] = slice_x_position_objs[i].transform.position.x;
            Debug.Log(slice_positions[i]);
        }
        
        slice_bools = new bool[chops_goal];
        for(int i = 0; i < slice_bools.Length; i++)
        {
            slice_bools[i] = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!knife_currently_chopping)
        {
            knife.transform.position = new Vector3 (mouse_position.x, knife.transform.position.y);
        }
        if (Input.GetMouseButtonDown(0))
        {
            myMouseLeftClick(mouse_position);
        }
        setChoppingActive();
    }

    private void setChoppingActive()
    {
        //bool camera_on_game = camera_Move.current_game == camera_Move.chopping;
        //if (camera_on_game && game_started == false)
        //{
        //    restartGame();
        //}
        //else if(!camera_on_game && game_started == true)
        //{
        //    game_started = false;
        //}
        chopping.SetActive(camera_Move.current_game == camera_Move.chopping);
    }

    private void myMouseLeftClick(Vector3 mouse_position)
    {/*
        click = chop = knife sprite goes down(have knife slip into two segments, front and back)
        - if chopped wrong, lose / restart
    - if chopped correctly, draw a line at that spot
       -draw line: sprite for each line
    -to check if chopped correctly, see if x of mouse is in the right spot(array of x coords for
      where the chops should be, check if mouse is within[tolerance] pixels left / right of that)  
      AND if i've chopped here already, dont count it toward the total (have array of bools that aligns 
       w positions, true if been chopped before)
    - if all chops done, win*/

        //ADD KNIFE ANIMATION
        //check if chopped correctly
        float chop_x = mouse_position.x;
        Debug.Log("chop position: " + chop_x + ", total chops: " + chops_current);
        bool successful_chop = false;
        int arr_pos = 0;
        float upper_limit = 0;
        float lower_limit = 0;
        Debug.Log("loop: ");
        for (int i = 0; i < slice_positions.Length; i++)
        {
            upper_limit = slice_positions[i] + slice_tolerance;
            lower_limit = slice_positions[i] - slice_tolerance;
            Debug.Log("upper_limit = " + upper_limit + " lower_limit = " + lower_limit);
            if (chop_x > lower_limit && chop_x < upper_limit)
            {
                successful_chop = true;
                arr_pos = i;
                Debug.Log("break");
                break;
                
            }
        }

        if (successful_chop)
        {
            Debug.Log("arr_pos = " + arr_pos + "successful chop: slice_bools[arr_pos] = " + slice_bools[arr_pos]);
            //if ive already chopped here
            if (slice_bools[arr_pos] == false)
            {
                chops_current += 1;
                Debug.Log("chops_current incremented: " + chops_current);
                slice_bools[arr_pos] = true;
                if (chops_current == chops_goal)
                {
                    Debug.Log("you won!");
                    game_won = true;
                }
            }
            //ADD DRAW LINE
            
        }
        else
        {
            Debug.Log("bad chop, you lost.");
            restartGame();
        }
    }

    private void restartGame()
    {
        game_started = true;
        chops_current = 0;
        for (int i = 0; i < slice_bools.Length; i++)
        {
            slice_bools[i] = false;
        }
        game_won = false;
        Debug.Log("Chopping restarted");
    }
}
