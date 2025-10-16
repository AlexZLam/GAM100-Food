using UnityEngine;

public class Chopping : MonoBehaviour
{
    public GameObject knife;
    public int[] slice_positions;
    public int slice_tolerance;

    private int chops_current = 0;
    private bool[] slice_bools;
    private int chops_goal = 7;
    private bool game_won;
    private bool game_started;

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
        slice_positions = new int[chops_goal];
        slice_bools = new bool[chops_goal];
        for(int i = 0; i < slice_bools.Length; i++)
        {
            slice_bools[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
