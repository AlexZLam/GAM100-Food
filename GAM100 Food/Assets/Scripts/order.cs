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
    
    void Start()
    {
        orderTxt = orderTextObject.GetComponent<TextMeshProUGUI>();
        updateOrderText(); // Show empty list initially
    }
    void Update()
    {
        if (activeOrders.Contains("Burger") && burgerScript.Burgerdone)
        {
            Debug.Log("Burger order completed!");
            burgerScript.Burgerdone = false;
            activeOrders.Remove("Burger");
            updateOrderText();
        }

        if (activeOrders.Contains("Salad") && saladScript._saladmix_done)
        {
            Debug.Log("Salad order completed!");
            saladScript._saladmix_done = false;
            activeOrders.Remove("Salad");
            updateOrderText();
        }

        if (activeOrders.Contains("Milkshake") && milkshakeScript.milkshake_done)
        {
            Debug.Log("Milkshake order completed!");
            milkshakeScript.milkshake_done = false;
            activeOrders.Remove("Milkshake");
            updateOrderText();
        }

        if (activeOrders.Contains("Chopping") && choppingScript.chopping_done)
        {
            Debug.Log("Chopping order completed!");
            choppingScript.chopping_done = false;
            activeOrders.Remove("Chopping");
            updateOrderText();
        }

        if (activeOrders.Contains("Burger Smash") && smashScript.BurgerSmashDone)
        {
            Debug.Log("Burger Smash order completed!");
            smashScript.BurgerSmashDone = false;
            activeOrders.Remove("Burger Smash");
            updateOrderText();
        }

        setOrderActive();
    }

    public void GenerateOrder()
    {
        activeOrders.Clear(); // Reset previous orders
        randOrderNum = Random.Range(1, 9);

        for (int i = 0; i < randOrderNum; i++)
        {
            int randOrder = Random.Range(1, 6);

            switch (randOrder)
            {
                case 1:
                    activeOrders.Add("Burger");
                    Debug.Log("Order " + (i + 1) + ": Burger");
                    break;
                case 2:
                    activeOrders.Add("Salad");
                    Debug.Log("Order " + (i + 1) + ": Salad");
                    break;
                case 3:
                    activeOrders.Add("Milkshake");
                    Debug.Log("Order " + (i + 1) + ": Milkshake");
                    break;
                case 4:
                    activeOrders.Add("Chopping");
                    Debug.Log("Order " + (i + 1) + ": Chopping");
                    break;
                case 5:
                    activeOrders.Add("Burger Smash");
                    Debug.Log("Order " + (i + 1) + ": Burger Smash");
                    break;
            }
        }

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
}
