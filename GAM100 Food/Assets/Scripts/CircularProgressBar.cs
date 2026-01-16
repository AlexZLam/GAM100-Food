/*******************************************************************************
* File Name: CircularProgressBar.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description:
*   This file manages the circular progress bar UI element used in the fries
*   minigame. It visualizes a countdown timer by adjusting the radial fill
*   amount of a UI Image. The countdown stops automatically when the fries
*   minigame reports completion.
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{

    [Header("Scripts")]
    public fries manager;                 // Reference to the fries minigame manager

    private bool isActive = false;        // True when the countdown is running
    private float indicatorTimer;         // Current countdown time
    private float maxIndicatorTimer;      // Maximum countdown duration


    [Header("Image")]
    public Image radialProgressBar;       // UI Image used for radial fill display

    private void Awake()
    {
        radialProgressBar = GetComponent<Image>();
    }

    public void ActivateCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *    Updates the countdown timer, adjusts the radial
    *   fill amount, and stops the countdown when the fries are finished.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Update()
    {
        if (isActive)
        {
            // Reduce timer based on elapsed time
            indicatorTimer -= Time.deltaTime;

            // Update radial fill (value between 0 and 1)
            radialProgressBar.fillAmount = indicatorTimer / maxIndicatorTimer;

            // Stop countdown if fries are done
            if (manager.friesDone == true)
            {
                StopCountdown();
            }
        }
    }

    public void StopCountdown()
    {
        radialProgressBar.fillAmount = 1f; // Reset fill to full
        isActive = false;
    }
}
