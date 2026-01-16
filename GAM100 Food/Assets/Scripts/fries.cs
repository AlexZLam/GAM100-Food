/*******************************************************************************
* File Name: fries.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description:
*   This file implements the logic for the fries minigame. It manages the fry
*   timer, visual feedback for cooking states (perfect, burnt, undercooked),
*   and success/failure conditions based on player timing and interaction.
*******************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class fries : MonoBehaviour
{

    [Header("Scripts")]
    public camera_move camera_Move;            // Reference to camera movement script
    public CircularProgressBar progressBar;    // Circular progress bar for fry timer


    [Header("Game Object")]
    public GameObject energizedEffect;         // Visual effect when energized
    public GameObject ParentObject;            // Parent object for fries minigame UI


    [Header("Is Active")]
    public bool isEnergized;                   // True while energized effect is active

    [Header("Button")]
    public Button button;                      // Button used to interact with fries

    [Header("Duration")]
    public float duration;                     // Duration of energized effect

    [Header("Finished")]
    public bool friesDone;                     // True when fries are successfully completed
    private bool itStarted = false;            // True when countdown has begun


    [SerializeField] private Sprite perfect;   // Perfectly cooked fries
    [SerializeField] private Sprite burnt;     // Burnt fries
    [SerializeField] private Sprite under;     // Undercooked fries
    [SerializeField] private Sprite empty;     // Empty basket sprite

    [SerializeField] private GameObject basket; // Basket object displaying fry sprite
    private bool wasBurnt;                     // Unused flag (reserved for future use)

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Handles UI visibility based on camera position,
    *   processes button interactions, and checks for burnt fries.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Update()
    {
        // Show or hide the fries minigame depending on camera focus
        ParentObject.SetActive(camera_Move.current_game == camera_Move.fries);


        button.onClick.AddListener(() =>
        {
            // SUCCESS: Fries are perfectly cooked (very low fill amount)
            if (progressBar.radialProgressBar.fillAmount <= 0.096f)
            {
                basket.GetComponent<SpriteRenderer>().sprite = empty;
                Debug.Log("Success");
                friesDone = true;
                EndEnergizedEffect(duration);
                itStarted = false;
                basket.GetComponent<SpriteRenderer>().sprite = perfect;
            }

            // Start countdown if not already started
            if (!itStarted)
            {
                StartEnergizedEffect(duration);
                itStarted = true;
            }

            // FAIL: Undercooked range
            if (progressBar.radialProgressBar.fillAmount > 0.195f &&
                itStarted &&
                progressBar.radialProgressBar.fillAmount < 0.95f)
            {
                Debug.Log("Fail");
                StartEnergizedEffect(duration);
                basket.GetComponent<SpriteRenderer>().sprite = under;
            }

            // FAIL: Overcooked after being done
            if (friesDone && progressBar.radialProgressBar.fillAmount >= 0.95f)
            {
                friesDone = false;
                itStarted = true;
                StartEnergizedEffect(duration);
            }
        });

        // FAIL: Completely burnt
        if (progressBar.radialProgressBar.fillAmount == 0)
        {
            StartEnergizedEffect(duration);
            Debug.Log("Fail");
            basket.GetComponent<SpriteRenderer>().sprite = burnt;
        }
    }

    /****************************************************************************
    * Function: StartEnergizedEffect
    *
    * Description:
    *   Begins the energized effect and starts the countdown timer.
    *
    * Inputs:
    *   float duration - Duration of energized effect
    *
    * Outputs: None
    ****************************************************************************/
    public void StartEnergizedEffect(float duration)
    { 
        isEnergized = true;
        progressBar.ActivateCountdown(duration);
        StartCoroutine(EndEnergizedEffect(duration));
    }

    /****************************************************************************
    * Function: EndEnergizedEffect
    *
    * Description:
    *   Ends the energized effect after a delay.
    *
    * Inputs:
    *   float delay - Time to wait before ending effect
    *
    * Outputs: None
    ****************************************************************************/
    IEnumerator EndEnergizedEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        isEnergized = false;
    }
}
