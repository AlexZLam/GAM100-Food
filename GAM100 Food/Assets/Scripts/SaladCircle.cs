/*******************************************************************************
* File Name: SaladCircle.cs
* Author: (Your Name Here)
* DigiPen Email: (your.email@digipen.edu)
* Course: GAM100
*
* Description:
*   This file handles detection for the salad?mixing minigame. When the player
*   holds the mouse button while inside the trigger area, the mixing sequence
*   begins and the fail loop counter is reset.
*******************************************************************************/

using UnityEngine;

public class SaladCircle : MonoBehaviour
{
    
    [Header("Scripts")]
    public SaladMix _saladmix;     // Reference to the main SaladMix script

 
    [Header("Bools")]
    public bool _start = false;    // True when mixing has begun

    /****************************************************************************
    * Function: OnTriggerStay2D
    *
    * Description:
    *   If the player is holding the left mouse button, the salad mixing
    *   sequence begins and the fail loop counter is reset.
    *
    * Inputs:
    *   Collider2D collision - The collider currently inside the trigger
    *
    * Outputs: None
    ****************************************************************************/
    public void OnTriggerStay2D(Collider2D collision)
    {
        // Begin mixing when the player holds the left mouse button
        if (Input.GetMouseButton(0))
        {
            _start = true;

            // Reset fail loop counter in the SaladMix script
            _saladmix._saladmix_fail_loop = 0;
        }
    }
}
