/*******************************************************************************
* File Name: Baking.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description:
*   This file implements the logic for the baking miniggame. It manages the
*   countdown timer, UI fading effects, player interaction through a button,
*   and determines success based on timing accuracy. The script also handles
*   enabling/disabling the minigame UI depending on camera focus.
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class Baking : MonoBehaviour
{


    [Header("Scripts")]
    public camera_move camera_Move;   // Reference to camera movement script

    [Header("Game Objects")]
    public GameObject slider;         // Slider UI element for countdown
    public GameObject baking;         // Parent object for baking minigame

    [Header("UI Elements")]
    [SerializeField]
    private Button button;            // Button used to start the baking interaction
    [SerializeField]
    private RawImage dark;            // Overlay image that darkens during countdown



    [Header("Time")]
    private const float time = 5.0f;            // Total countdown duration
    private const float successThreshold = 0.9f; // Time threshold for success

    [Header("Countdowns")]
    private float countdown;          // Current countdown value
    private bool isCountingDown = false; // Tracks whether countdown is active
    private float alpha;              // Alpha value for dark overlay



    [Header("Color Swap")]
    private Color newColor;           // Color used to fade overlay
    private CanvasGroup sliderGroup;  // CanvasGroup for fading slider UI
    private Slider sliderComponent;   // Reference to slider component

    public bool BakingDone;           // Indicates whether baking was successful

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Initializes references, sets up UI values, and registers the button
    *   click event. Called once before the first frame update.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Start()
    {
        // Retrieve slider and canvas group components
        sliderComponent = slider.GetComponent<Slider>();
        sliderGroup = slider.GetComponent<CanvasGroup>();

        // Initialize overlay color with zero alpha
        newColor = dark.color;
        newColor.a = 0;

        // Configure slider values
        sliderComponent.maxValue = time;
        countdown = time;
        sliderComponent.value = countdown;

        // Register button click event
        button.onClick.AddListener(OnButtonClick);
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Called once per frame. Updates countdown logic, UI fading, and determines
    *   whether the baking UI should be visible based on camera focus.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        // Update countdown if active
        if (isCountingDown)
        {
            Timer();
        }

        // Compute overlay alpha using a steep curve for dramatic fade
        alpha = Mathf.Clamp01(1.0f - Mathf.Pow(countdown / time, 7));
        sliderComponent.value = countdown;

        // Enable or disable baking UI depending on camera state
        setBakingActive();
    }

    /****************************************************************************
    * Function: Timer
    *
    * Description:
    *   Handles countdown progression, overlay fading, slider fading, and timer
    *   reset when the countdown reaches zero.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Timer()
    {
        // Reduce countdown by elapsed time
        countdown -= Time.deltaTime;

        // Update overlay transparency
        newColor.a = alpha;
        dark.color = newColor;

        // Fade slider UI opposite of overlay
        if (sliderGroup != null)
        {
            sliderGroup.alpha = 1.0f - alpha;
        }

        // Reset timer when countdown finishes
        if (countdown <= 0f)
        {
            countdown = time;
            isCountingDown = true;

            Debug.Log("Timer finished!");
        }
    }

    /****************************************************************************
    * Function: OnButtonClick
    *
    * Description:
    *   Triggered when the player presses the baking button. Resets the timer,
    *   starts the countdown, and checks whether the click occurred within the
    *   success threshold.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void OnButtonClick()
    {
        // Reset state
        BakingDone = false;
        countdown = time;
        isCountingDown = true;

        float sliderValue = sliderComponent.value;

        // Check for success condition
        if (sliderValue > 0 && sliderValue <= successThreshold)
        {
            isCountingDown = false;
            Finished();
            BakingDone = true;

            Debug.Log("Successful timing! Baking completed.");
        }
    }

    /****************************************************************************
    * Function: setBakingActive
    *
    * Description:
    *   Shows or hides the baking UI depending on whether the camera is focused
    *   on the baking minigame.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void setBakingActive()
    {
        baking.SetActive(camera_Move.current_game == camera_Move.baking);
    }

    /****************************************************************************
    * Function: Finished
    *
    * Description:
    *   Resets UI elements after the baking minigame is completed, restoring
    *   slider visibility and clearing the dark overlay.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    public void Finished()
    {
        sliderGroup.alpha = 1;
        dark.color = new Color(newColor.r, newColor.g, newColor.b, 0);
    }
}
