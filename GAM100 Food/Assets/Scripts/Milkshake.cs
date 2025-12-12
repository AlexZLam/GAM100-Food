/*******************************************************************************
* File Name: Milkshake.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: GAM100
*
* Description:
*   This file implements the milkshake minigame. The player must click rapidly
*   within a time limit to reach a click goal. The script manages timing,
*   click tracking, animation control, and win/lose conditions.
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class Milkshake : MonoBehaviour
{

    [Header("Scripts")]
    public camera_move camera_Move;          // Reference to camera movement script

    [Header("Buttons")]
    public Button spamclick_button;          // Button used for rapid clicking
    public Button start_button;              // Button used to start the minigame

    [Header("Parent Object")]
    public GameObject milkshake;             // Parent object for enabling/disabling UI

    [Header("Time to beat")]
    public float timer = 10f;                // Time limit for the minigame

    [Header("Clicks to get")]
    public int click_goal = 100;             // Number of clicks required to win

    [Header("Blender sprite animator")]
    public Animator milkshake_animator;      // Animator controlling blender animation


    private int click_counter;               // Number of clicks so far
    private float time_counter;              // Remaining time
    private bool game_started = false;       // True when the game is active
    public bool milkshake_done = false;      // True when the minigame is successfully completed

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Initializes the timer and registers button listeners for starting the
    *   game and counting clicks.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Start()
    {
        // Initialize countdown timer
        time_counter = timer;

        // Register button click events
        spamclick_button.onClick.AddListener(OnSpamButtonClick);
        start_button.onClick.AddListener(OnStartButtonClick);
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Called once per frame. Handles countdown logic, loss condition, and
    *   toggles visibility based on camera position.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        if (game_started)
        {
            // Decrease remaining time
            time_counter -= Time.deltaTime;

            // If time runs out, player loses
            if (time_counter <= 0)
            {
                milkshake_done = false;
                game_started = false;
                Debug.Log("You lost.");

                // Stop blender animation
                milkshake_animator.SetTrigger("done_mixing");
            }
        }

        // Show or hide the minigame UI
        setMilkshakeActive();
    }

    /****************************************************************************
    * Function: OnSpamButtonClick
    *
    * Description:
    *   Called whenever the spam-click button is pressed. Increments the click
    *   counter and checks for win condition.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void OnSpamButtonClick()
    {
        if (game_started)
        {
            click_counter++;

            // Optional debug output every 10 clicks
            if (click_counter % 10 == 0)
            {
                Debug.Log(click_counter + " clicks, " + time_counter + " seconds left");
            }

            // Win condition
            if (click_counter == click_goal)
            {
                milkshake_done = true;
                game_started = false;
                Debug.Log("You won!");

                // Stop blender animation
                milkshake_animator.SetTrigger("done_mixing");
            }
        }
    }

    /****************************************************************************
    * Function: OnStartButtonClick
    *
    * Description:
    *   Starts the minigame by resetting counters, enabling animation, and
    *   preparing the timer.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void OnStartButtonClick()
    {
        // Start blender animation
        milkshake_animator.SetTrigger("start_mixing");

        // Reset game state
        game_started = true;
        time_counter = timer;
        click_counter = 0;
        milkshake_done = false;

        Debug.Log("Milkshake started: get " + click_goal +
                  " clicks in " + time_counter + " seconds to win!");
    }

    /****************************************************************************
    * Function: setMilkshakeActive
    *
    * Description:
    *   Shows or hides the milkshake minigame UI depending on whether the camera
    *   is currently focused on the milkshake station.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void setMilkshakeActive()
    {
        milkshake.SetActive(camera_Move.current_game == camera_Move.milkshake);
    }
}
