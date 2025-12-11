/*******************************************************************************
* File Name: Chopping.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: GAM100
*
* Description:
*   This file implements the chopping minigame. The player moves a knife along
*   the x-axis using the mouse and clicks to chop at specific target positions.
*   The game checks whether the chop is within tolerance of a valid slice point,
*   tracks progress, and resets on mistakes.
*******************************************************************************/

using UnityEngine;

public class Chopping : MonoBehaviour
{
    /****************************************************************************
    * Section: Inspector References
    ****************************************************************************/
    [Header("Scripts")]
    public camera_move camera_Move;              // Reference to camera movement script

    [Header("Parent Object")]
    public GameObject chopping;                  // Parent object for enabling/disabling UI

    [Header("Game Done")]
    public bool chopping_done;                   // True when all slices are completed

    [Header("Game Objects")]
    public GameObject knife;                     // Knife object controlled by the player
    public GameObject[] slice_x_position_objs;   // Slice markers representing valid chop positions

    [Header("Tolerance")]
    public float slice_tolerance = 15f;          // Allowed distance from slice point for a valid chop

    /****************************************************************************
    * Section: Internal State
    ****************************************************************************/
    private int chops_current = 0;               // Number of successful chops so far
    private bool[] slice_bools;                  // Tracks which slice positions have been chopped
    private float[] slice_positions;             // X positions of valid slice points
    private int chops_goal;                      // Total number of required chops
    private float knife_start_height;            // Starting Y position of the knife

    private bool game_started;                   // True when the minigame is active
    private bool knife_currently_chopping = false; // True while knife is animating downward

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Initializes slice positions, tolerance, and game state. Called before the
    *   first frame update.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Start()
    {
        knife_start_height = knife.transform.position.y;
        chops_goal = slice_x_position_objs.Length;

        slice_positions = new float[chops_goal];
        slice_bools = new bool[chops_goal];

        // Determine tolerance based on slice sprite width
        slice_tolerance = slice_x_position_objs[0].GetComponent<SpriteRenderer>().bounds.max.x -
                          slice_x_position_objs[0].GetComponent<SpriteRenderer>().bounds.min.x;

        // Cache slice x-positions
        for (int i = 0; i < chops_goal; i++)
        {
            slice_positions[i] = slice_x_position_objs[i].transform.position.x;
            slice_bools[i] = false;
        }
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Handles knife movement, chop input, and activation of the minigame based
    *   on camera position.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Move knife horizontally with mouse unless chopping animation is active
        if (!knife_currently_chopping)
        {
            knife.transform.position = new Vector3(mouse_position.x, knife.transform.position.y);
        }
        else
        {
            // Animate knife downward
            knife.transform.position = new Vector3(
                knife.transform.position.x,
                knife.transform.position.y - (30f * Time.deltaTime)
            );

            // Reset knife after animation completes
            if (knife.transform.position.y <= -3f)
            {
                knife_currently_chopping = false;
                knife.transform.position = new Vector3(mouse_position.x, knife_start_height);
            }
        }

        // Detect chop input
        if (Input.GetMouseButtonDown(0))
        {
            myMouseLeftClick(mouse_position);
        }

        // Enable/disable minigame based on camera position
        setChoppingActive();
    }

    /****************************************************************************
    * Function: setChoppingActive
    *
    * Description:
    *   Activates the chopping minigame when the camera is focused on it. Starts
    *   or stops the game appropriately.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void setChoppingActive()
    {
        bool camera_on_game = camera_Move.current_game == camera_Move.chopping;

        if (camera_on_game && !game_started)
        {
            restartGame();
        }
        else if (!camera_on_game && game_started)
        {
            game_started = false;
        }

        chopping.SetActive(camera_on_game);
    }

    /****************************************************************************
    * Function: myMouseLeftClick
    *
    * Description:
    *   Handles chop attempts. Checks whether the chop is within tolerance of a
    *   valid slice point, updates progress, and resets the game on mistakes.
    *
    * Inputs:
    *   Vector3 mouse_position - The world position of the mouse click
    *
    * Outputs: None
    ****************************************************************************/
    private void myMouseLeftClick(Vector3 mouse_position)
    {
        if (!game_started)
        {
            return;
        }

        // Begin chop animation
        knife_currently_chopping = true;

        float chop_x = mouse_position.x;
        bool successful_chop = false;
        int arr_pos = 0;

        // Check chop against all slice positions
        for (int i = 0; i < slice_positions.Length; i++)
        {
            float upper_limit = slice_positions[i] + slice_tolerance;
            float lower_limit = slice_positions[i] - slice_tolerance;

            if (chop_x > lower_limit && chop_x < upper_limit)
            {
                successful_chop = true;
                break;
            }

            arr_pos++;
        }

        if (successful_chop)
        {
            // Only count chop if this slice hasn't been hit before
            if (!slice_bools[arr_pos])
            {
                chops_current++;
                slice_bools[arr_pos] = true;

                // Check win condition
                if (chops_current == chops_goal)
                {
                    Debug.Log("You won!");
                    chopping_done = true;
                }
            }

            // Hide slice marker
            slice_x_position_objs[arr_pos].GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Debug.Log("Bad chop, you lost.");
            restartGame();
        }
    }

    /****************************************************************************
    * Function: restartGame
    *
    * Description:
    *   Resets all slice markers, progress counters, and game state to allow the
    *   player to try again.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void restartGame()
    {
        game_started = true;
        chops_current = 0;

        // Reset slice flags
        for (int i = 0; i < slice_bools.Length; i++)
        {
            slice_bools[i] = false;
        }

        // Re-enable slice markers
        for (int i = 0; i < slice_x_position_objs.Length; i++)
        {
            slice_x_position_objs[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        chopping_done = false;
    }
}
