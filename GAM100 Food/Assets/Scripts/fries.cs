/*******************************************************************************
* File Name: fries.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: This file contains logic for the fries minigame, including
* progress tracking, visual feedback, and success/failure conditions based
* on timing and player interaction.
*******************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class fries : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move; // Reference to camera movement script
    public CircularProgressBar progressBar; // Progress bar for fry timer

    [Header("Game Object")]
    public GameObject energizedEffect; // Visual effect when energized
    public GameObject ParentObject; // Parent object for fries minigame

    [Header("Is Active")]
    public bool isEnergized; // Flag to track if energized effect is active

    [Header("Button")]
    public Button button; // Button to interact with fries

    [Header("Duration")]
    public float duration; // Duration for energized effect

    [Header("Finished")]
    public bool friesDone; // Flag to track if fries are successfully completed
    private bool itStarted = false; // Flag to track if countdown has started

    [SerializeField]
    private Sprite perfect, burnt, under, empty; // Sprites for fry states

    [SerializeField]
    private GameObject basket; // Basket to display fry sprite
    private bool wasBurnt; // Unused flag for burnt state

    // Update is called once per frame
    private void Update()
    {
        // Show or hide fries minigame based on camera focus
        if (camera_Move.current_game == camera_Move.fries)
        {
            ParentObject.SetActive(true);
        }
        else
        {
            ParentObject.SetActive(false);
        }

        // Add listener to button click (executed every frame — should be moved to Start)
        button.onClick.AddListener(() =>
        {
            // If progress bar is nearly empty, mark as success
            if (progressBar.radialProgressBar.fillAmount <= 0.096f)
            {
                basket.GetComponent<SpriteRenderer>().sprite = empty;
                Debug.Log("Success");
                friesDone = true;
                EndEnergizedEffect(duration);
                itStarted = false;
                basket.GetComponent<SpriteRenderer>().sprite = perfect;
            }

            // If countdown hasn't started, start it
            if (itStarted == false)
            {
                StartEnergizedEffect(duration);
                itStarted = true;
            }

            // If clicked during undercooked range, mark as fail
            if (progressBar.radialProgressBar.fillAmount > 0.195f && itStarted == true && progressBar.radialProgressBar.fillAmount < 0.95f)
            {
                Debug.Log("Fail");
                StartEnergizedEffect(duration);
                basket.GetComponent<SpriteRenderer>().sprite = under;
            }

            // If fries were done but overcooked, reset and restart
            if (friesDone == true && progressBar.radialProgressBar.fillAmount >= 0.95f)
            {
                friesDone = false;
                itStarted = true;
                StartEnergizedEffect(duration);
            }
        });

        // If progress bar is completely empty, mark as burnt
        if (progressBar.radialProgressBar.fillAmount == 0)
        {
            StartEnergizedEffect(duration);
            Debug.Log("Fail");
            basket.GetComponent<SpriteRenderer>().sprite = burnt;
        }
    }

    // Starts the energized effect and begins countdown
    public void StartEnergizedEffect(float duration)
    {
        isEnergized = true;
        progressBar.ActivateCountdown(duration);
        StartCoroutine(EndEnergizedEffect(duration));
    }

    // Ends the energized effect after delay
    IEnumerator EndEnergizedEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        isEnergized = false;
    }
}
