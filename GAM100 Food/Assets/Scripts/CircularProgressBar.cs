/*******************************************************************************
* File Name: CircularProgressBar.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: This file manages the circular progress bar UI element used in the
*              fries minigame. It handles countdown visualization and stops when
*              the fries are done.
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    [Header("Scripts")]
    public fries manager; // Reference to the fries minigame manager script

    private bool isActive = false; // Tracks whether the countdown is active

    private float indicatorTimer;     // Current countdown time
    private float maxIndicatorTimer;  // Maximum countdown duration

    [Header("Image")]
    public Image radialProgressBar; // UI image used to display radial fill progress

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Get the Image component attached to this GameObject
        radialProgressBar = GetComponent<Image>();
    }

    // Activates the countdown and sets the timer
    public void ActivateCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    // Called once per frame
    private void Update()
    {
        if (isActive)
        {
            // Decrease the timer based on time elapsed
            indicatorTimer -= Time.deltaTime;

            // Update the radial fill amount based on remaining time
            radialProgressBar.fillAmount = (indicatorTimer / maxIndicatorTimer);

            // If the fries are done, stop the countdown
            if (manager.friesDone == true)
            {
                StopCountdown();
            }
        }
    }

    // Stops the countdown and resets the progress bar
    public void StopCountdown()
    {
        radialProgressBar.fillAmount = 100; // Reset fill to full
        isActive = false;
    }
}
