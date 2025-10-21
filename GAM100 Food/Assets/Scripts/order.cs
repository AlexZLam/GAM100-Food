using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class order : MonoBehaviour
{
    public Burger burgerScript;
    public Milkshake milkshakeScript;
    public Chopping choppingScript;
    public BurgerSmash smashScript;
    public SaladMix saladScript;
    public camera_move camera_Move;

    [SerializeField]
    private GameObject orderTextObject;
    [SerializeField]
    private GameObject orderObj;

    private TextMeshProUGUI orderTxt;

    private int randOrderNum;
    private List<string> activeOrders = new List<string>();

    private float orderStartTime;
    private bool timerRunning = false;

    void Start()
    {
        orderTxt = orderTextObject.GetComponent<TextMeshProUGUI>();
        updateOrderText(); // Show empty list initially
    }
    void Update()
    {
        CheckOrderCompletion();

        if (timerRunning && AllOrdersCompleted())
        {
            float timeTaken = Time.time - orderStartTime;
            int score = CalculateScore(timeTaken, randOrderNum);
            Debug.Log($"Order completed in {timeTaken:F2} seconds. Score: {score}");
            timerRunning = false;
        }

        setOrderActive();
    }


    public void GenerateOrder()
    {
        if (!AllOrdersCompleted())
        {
            Debug.Log("Finish current order first!");
            return;
        }

        activeOrders.Clear();
        randOrderNum = Random.Range(1, 9);

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

        orderStartTime = Time.time;
        timerRunning = true;
        updateOrderText();
    }



    private void updateOrderText()
    {
        orderTxt.text = "Orders:\n";
        foreach (string order in activeOrders)
        {
            orderTxt.text += order + "\n";
        }
    }

    private void setOrderActive()
    {
        orderObj.SetActive(camera_Move.current_game == camera_Move.counter);
    }
    private bool AllOrdersCompleted()
    {
        return activeOrders.Count == 0;
    }
    private void CheckOrderCompletion()
    {
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
    private int CalculateScore(float timeTaken, int orderCount)
    {
        float baseScore = 100f;
        float timePenalty = timeTaken * 2f; // 2 points lost per second
        float complexityBonus = orderCount * 10f;

        float finalScore = baseScore + complexityBonus - timePenalty;
        return Mathf.Max(0, Mathf.RoundToInt(finalScore));
    }



}
