/*******************************************************************************
* File Name: order.cs
* Author: Alex Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description:
*   This file manages the order system for the cooking game. It generates
*   randomized orders, tracks their completion across multiple minigames,
*   updates the UI, and calculates a score based on player performance.
*******************************************************************************/

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class order : MonoBehaviour
{
    /****************************************************************************
    * Section: Minigame Script References
    ****************************************************************************/
    [Header("Minigame Scripts")]
    public Burger burgerScript;            // Reference to burger minigame
    public Milkshake milkshakeScript;      // Reference to milkshake minigame
    public Chopping choppingScript;        // Reference to chopping minigame
    public BurgerSmash smashScript;        // Reference to burger smash minigame
    public SaladMix saladScript;           // Reference to salad mixing minigame
    public camera_move camera_Move;        // Reference to camera movement script

    /****************************************************************************
    * Section: UI Elements
    ****************************************************************************/
    [Header("UI Elements")]
    [SerializeField] private GameObject orderTextObject; // UI text container
    [SerializeField] private GameObject orderObj;        // Order UI parent

    private TextMeshProUGUI orderTxt;      // Text component for displaying orders

    /****************************************************************************
    * Section: Order Tracking
    ****************************************************************************/
    private int randOrderNum;              // Number of tasks in the current order
    private List<string> activeOrders = new List<string>(); // List of tasks to complete

    /****************************************************************************
    * Section: Timer Tracking
    ****************************************************************************/
    private float orderStartTime;          // Time when the order began
    private bool timerRunning = false;     // Tracks whether the timer is active

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Initializes the order UI by retrieving the text component and displaying
    *   an empty order list.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Start()
    {
        orderTxt = orderTextObject.GetComponent<TextMeshProUGUI>();
        updateOrderText();
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Continuously checks for task completion, updates the score when all
    *   tasks are done, and toggles the visibility of the order UI based on
    *   camera position.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        // Check if any minigame tasks have been completed
        CheckOrderCompletion();

        // If all tasks are done, calculate score once
        if (timerRunning && AllOrdersCompleted())
        {
            float timeTaken = Time.time - orderStartTime;
            int score = CalculateScore(timeTaken, randOrderNum);

            Debug.Log($"Order completed in {timeTaken:F2} seconds. Score: {score}");
            timerRunning = false;
        }

        // Show or hide order UI depending on camera location
        setOrderActive();
    }

    /****************************************************************************
    * Function: GenerateOrder
    *
    * Description:
    *   Creates a new randomized order consisting of 1–8 tasks. Prevents
    *   generating a new order if the current one is not yet completed.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    public void GenerateOrder()
    {
        // Prevent generating a new order prematurely
        if (!AllOrdersCompleted())
        {
            Debug.Log("Finish current order first!");
            return;
        }

        // Reset previous order
        activeOrders.Clear();
        randOrderNum = Random.Range(1, 9);

        // Generate random tasks
        for (int i = 0; i < randOrderNum; i++)
        {
            int randOrder = Random.Range(1, 6);
            switch (randOrder)
            {
                case 1: activeOrders.Add("Burger"); break;
                case 2: activeOrders.Add("Salad"); break;
                case 3: activeOrders.Add("Milkshake"); break;
                case 4: activeOrders.Add("Chopping"); break;
                case 5: activeOrders.Add("Burger Smash"); break;
            }
        }

        // Start timing the order
        orderStartTime = Time.time;
        timerRunning = true;

        updateOrderText();
    }

    /****************************************************************************
    * Function: updateOrderText
    *
    * Description:
    *   Updates the UI text to display the current list of active tasks.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void updateOrderText()
    {
        orderTxt.text = "Orders:\n";
        foreach (string order in activeOrders)
        {
            orderTxt.text += order + "\n";
        }
    }

    /****************************************************************************
    * Function: setOrderActive
    *
    * Description:
    *   Shows the order UI only when the camera is positioned at the counter.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void setOrderActive()
    {
        orderObj.SetActive(camera_Move.current_game == camera_Move.counter);
    }

    /****************************************************************************
    * Function: AllOrdersCompleted
    *
    * Description:
    *   Checks whether all tasks in the current order have been completed.
    *
    * Inputs:  None
    * Outputs: Returns true if no tasks remain.
    ****************************************************************************/
    private bool AllOrdersCompleted()
    {
        return activeOrders.Count == 0;
    }

    /****************************************************************************
    * Function: CheckOrderCompletion
    *
    * Description:
    *   Checks each minigame script for completion flags. When a task is
    *   completed, it is removed from the active order list and the UI updates.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void CheckOrderCompletion()
    {
        if (activeOrders.Contains("Burger") && burgerScript.Burgerdone)
        {
            burgerScript.Burgerdone = false;
            activeOrders.Remove("Burger");
            updateOrderText();
        }
        if (activeOrders.Contains("Salad") && saladScript._saladmix_done)
        {
            saladScript._saladmix_done = false;
            activeOrders.Remove("Salad");
            updateOrderText();
        }
        if (activeOrders.Contains("Milkshake") && milkshakeScript.milkshake_done)
        {
            milkshakeScript.milkshake_done = false;
            activeOrders.Remove("Milkshake");
            updateOrderText();
        }
        if (activeOrders.Contains("Chopping") && choppingScript.chopping_done)
        {
            choppingScript.chopping_done = false;
            activeOrders.Remove("Chopping");
            updateOrderText();
        }
        if (activeOrders.Contains("Burger Smash") && smashScript.BurgerSmashDone)
        {
            smashScript.BurgerSmashDone = false;
            activeOrders.Remove("Burger Smash");
            updateOrderText();
        }
    }

    /****************************************************************************
    * Function: CalculateScore
    *
    * Description:
    *   Computes a score based on time taken and number of tasks. Faster
    *   completion and more complex orders yield higher scores.
    *
    * Inputs:
    *   float timeTaken   - Time elapsed since order generation
    *   int orderCount    - Number of tasks in the order
    *
    * Outputs:
    *   Returns an integer score (minimum 0)
    ****************************************************************************/
    private int CalculateScore(float timeTaken, int orderCount)
    {
        float baseScore = 100f;
        float timePenalty = timeTaken * 0.5f;     // 0.5 points lost per second
        float complexityBonus = orderCount * 10f; // Bonus per task

        float finalScore = baseScore + complexityBonus - timePenalty;

        // Edge case: ensure complexity bonus is not lost
        if (finalScore == 0 && complexityBonus >= 10)
            finalScore += complexityBonus;

        return Mathf.Max(0, Mathf.RoundToInt(finalScore));
    }
}
