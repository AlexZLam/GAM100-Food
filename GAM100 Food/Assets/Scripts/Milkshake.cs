/*******************************************************************************
* File Name: Milkshake.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: GAM100
*
* Description: This file contains functions for the milkshake minigame.
*******************************************************************************/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Milkshake : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move;
    [Header("Buttons")]
    public Button spamclick_button;
    public Button start_button;
    [Header("Parent Object")]
    public GameObject milkshake;
    [Header("Time to beat")]
    public float timer = 10f;
    [Header("Clicks to get")]
    public int click_goal = 100;
    [Header("Blender sprite animator")]
    public Animator milkshake_animator;

    private int click_counter;
    private float time_counter;
    private bool game_started = false;
    public bool milkshake_done = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //timer stores the time limit so it does not change, thus time_counter starts at this value and counts down to 0.
        time_counter = timer;
        //when the start button and spam button are clicked, trigger the corresponding function in this script
        spamclick_button.onClick.AddListener(OnSpamButtonClick);
        start_button.onClick.AddListener(OnStartButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_started)
        {
            //decrease the countdown tracker by how much time has passed since the last frame
            time_counter -= Time.deltaTime;
            //if the countdown ends, lose the game
            if (time_counter <= 0)
            {
                milkshake_done = false;
                game_started = false;
                Debug.Log("you lost.");
                //stop animating
                milkshake_animator.SetTrigger("done_mixing");
            }
        }
        //if the camera is on this game, show it, if not, hide it
        setMilkshakeActive();
    }

    //when the spam button is clicked, count it and check for win condition
    void OnSpamButtonClick()
    {
        if (game_started)
        {   
            //increment the counter of clicks and win the game if it reaches the goal
            click_counter++;
            if (click_counter % 10 == 0) { Debug.Log(click_counter + " clicks , " + time_counter + " seconds left"); }
            if (click_counter == click_goal)
            {
                milkshake_done = true;
                game_started = false;
                Debug.Log("you won!");
                //stop animating
                milkshake_animator.SetTrigger("done_mixing");
            }
        }
    }

    //when the start button is clicked, start the game
    void OnStartButtonClick()
    {
        //start animating
        milkshake_animator.SetTrigger("start_mixing");
        //reset fields
        game_started = true;
        time_counter = timer;
        click_counter = 0;
        milkshake_done = false;
        Debug.Log("Milkshake started: get " + click_goal + " clicks in " + time_counter + " seconds to win!");
    }

    //if the camera is on this game, show it, if not, hide it
    private void setMilkshakeActive()
    {
        milkshake.SetActive(camera_Move.current_game == camera_Move.milkshake);
    }
}
