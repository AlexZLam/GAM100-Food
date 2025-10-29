/*******************************************************************************
* File Name: order.cs
* Author: Alex Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description: This file manages the order system for the cooking game.
* It generates randomized orders, tracks their completion, updates the UI,
* and calculates scores based on performance.
*******************************************************************************/

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class order : MonoBehaviour
{
    [Header("Minigame Scripts")]
    public Burger burgerScript;
    public Milkshake milkshakeScript;
    public Chopping choppingScript;
    public BurgerSmash smashScript;
    public SaladMix saladScript;
    public camera_move camera_Move;

    [Header("UI Elements")]
    [SerializeField] private GameObject orderTextObject;
    [SerializeField] private GameObject orderObj;

    private TextMeshProUGUI orderTxt;

    // Number of tasks in the current order
    private int randOrderNum;
    // List of active tasks to complete
    private List<string> activeOrders = new List<string>();

    // Timer tracking
    private float orderStartTime;
    private bool timerRunning = false;

    // Called once at the start of the game
    void Start()
    {
        // Get reference to the UI text component
        orderTxt = orderTextObject.GetComponent<TextMeshProUGUI>();
        // Display empty order list initially
        updateOrderText();
    }

    // Called once per frame
    void Update()
    {
        // Check if any tasks have been completed
        CheckOrderCompletion();

        // If all tasks are done and timer is running, calculate score
        if (timerRunning && AllOrdersCompleted())
        {
            float timeTaken = Time.time - orderStartTime;
            int score = CalculateScore(timeTaken, randOrderNum);
            Debug.Log($"Order completed in {timeTaken:F2} seconds. Score: {score}");
            timerRunning = false;
        }

        // Show or hide the order UI based on camera position
        setOrderActive();
    }

    // Generates a new randomized order
    public void GenerateOrder()
    {
        // Prevent generating a new order if current one isn't finished
        if (!AllOrdersCompleted())
        {
            Debug.Log("Finish current order first!");
            return;
        }

        // Clear previous order and generate new one
        activeOrders.Clear();
        randOrderNum = Random.Range(1, 9); // Number of tasks in the order

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

        // Start the timer
        orderStartTime = Time.time;
        timerRunning = true;
        updateOrderText();
    }

    // Updates the order list text in the UI
    private void updateOrderText()
    {
        orderTxt.text = "Orders:\n";
        foreach (string order in activeOrders)
        {
            orderTxt.text += order + "\n";
        }
    }

    // Activates the order UI only when the camera is at the counter
    private void setOrderActive()
    {
        orderObj.SetActive(camera_Move.current_game == camera_Move.counter);
    }

    // Checks if all tasks have been completed
    private bool AllOrdersCompleted()
    {
        return activeOrders.Count == 0;
    }

    // Checks each task's completion flag and removes it from the list
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

    // Calculates score based on time taken and number of tasks
    private int CalculateScore(float timeTaken, int orderCount)
    {
        float baseScore = 100f;
        float timePenalty = timeTaken * 2f; // 2 points lost per second
        float complexityBonus = orderCount * 10f;

        float finalScore = baseScore + complexityBonus - timePenalty;
        return Mathf.Max(0, Mathf.RoundToInt(finalScore));
    }
}
