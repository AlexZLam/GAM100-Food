/*******************************************************************************
* File Name: Baking.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description: This file contains logic for the baking minigame, including
* countdown timer, UI feedback, and win condition based on timing.
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class Baking : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move; // Reference to camera movement script

    [Header("Game Objects")]
    public GameObject slider; // Slider UI element for countdown
    public GameObject baking; // Parent object for baking minigame

    [Header("UI Elements")]
    [SerializeField]
    private Button button; // Button to start baking interaction
    [SerializeField]
    private RawImage dark; // Overlay image that darkens during countdown

    [Header("Time")]
    private const float time = 5.0f; // Total time for countdown
    private const float successThreshold = 0.9f; // Time threshold for success

    [Header("Countdowns")]
    private float countdown; // Current countdown value
    private bool isCountingDown = false; // Flag to track if countdown is active
    private float alpha; // Alpha value for dark overlay

    [Header("Color Swap")]
    private Color newColor; // Color used to fade overlay
    private CanvasGroup sliderGroup; // Canvas group for fading slider
    private Slider sliderComponent; // Reference to slider component

    public bool BakingDone; // Flag to indicate if baking was successful

    // Start is called before the first frame update
    void Start()
    {
        // Get references to slider and canvas group components
        sliderComponent = slider.GetComponent<Slider>();
        sliderGroup = slider.GetComponent<CanvasGroup>();

        // Initialize overlay color with alpha 0
        newColor = dark.color;
        newColor.a = 0;

        // Set slider max value and initialize countdown
        sliderComponent.maxValue = time;
        countdown = time;
        sliderComponent.value = countdown;

        // Add listener to button click
        button.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        // If countdown is active, update timer
        if (isCountingDown)
        {
            Timer();
        }

        // Calculate overlay alpha based on countdown curve
        alpha = Mathf.Clamp01(1.0f - Mathf.Pow(countdown / time, 7));
        sliderComponent.value = countdown;

        // Show or hide baking UI based on camera focus
        setBakingActive();
    }

    // Handles countdown logic and visual feedback
    private void Timer()
    {
        // Decrease countdown by time passed
        countdown -= Time.deltaTime;

        // Update overlay transparency
        newColor.a = alpha;
        dark.color = newColor;

        // Fade out slider UI
        if (sliderGroup != null)
        {
            sliderGroup.alpha = 1.0f - alpha;
        }

        // If countdown ends, reset timer and continue counting
        if (countdown <= 0f)
        {
            countdown = time;
            isCountingDown = true;
            Debug.Log("Button clicked, countdown started.");

            float sliderValue = sliderComponent.value;
            Debug.Log("Timer finished!");
        }
    }

    // Triggered when the button is clicked
    private void OnButtonClick()
    {
        // Reset game state
        BakingDone = false;
        countdown = time;
        isCountingDown = true;
        Debug.Log("Button clicked, countdown started.");

        float sliderValue = sliderComponent.value;

        // If clicked within success threshold, win the game
        if (sliderValue > 0 && sliderValue <= successThreshold)
        {
            isCountingDown = false;
            Finished();
            Debug.Log("Button clicked while slider is between 0 and 0.9!");
            BakingDone = true;
        }
    }

    // Show baking UI only if camera is focused on this game
    private void setBakingActive()
    {
        baking.SetActive(camera_Move.current_game == camera_Move.baking);
    }

    // Reset visual elements when baking is finished
    public void Finished()
    {
        sliderGroup.alpha = 1;
        dark.color = new Color(newColor.r, newColor.g, newColor.b, 0);
    }
}
