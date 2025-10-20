using UnityEngine;
using TMPro; // ? Required for TextMeshPro
using System.Collections.Generic;
using static UnityEditor.SceneView;
using UnityEngine.UIElements.Experimental;

public class order : MonoBehaviour
{
    public Burger burgerScript;
    public Baking bakingScript;
    public Milkshake milkshakeScript;
    public Chopping choppingScript;
    public BurgerSmash smashScript;
<<<<<<< HEAD
    public camera_move camera_Move;

    [SerializeField]
    private GameObject orderTextObject;
    [SerializeField]
    private GameObject orderObj;

    private TextMeshProUGUI orderTxt; // ? Use TMP component
=======
    public SaladMix saladScript;

    private bool isBurger;
    private bool isChopping;
    private bool isBaking;
    private bool isSmash;
    private bool isMilkshake;
    private bool isMixed;
>>>>>>> cef6db37dcbafebdfc5eec0ed374d66360cbb6eb

    private int randOrderNum;
    private List<string> activeOrders = new List<string>();

    void Start()
    {
<<<<<<< HEAD
        orderTxt = orderTextObject.GetComponent<TextMeshProUGUI>(); // ? Correct TMP component
        randOrderNum = Random.Range(1, 9); // Generate between 1 and 8 meals
        startOrder();
        updateOrderText();
=======
        isBurger = burgerScript.Burgerdone;
        isChopping = choppingScript.chopping_done;
        isSmash = smashScript.BurgerSmashDone;
        isMixed = saladScript._saladmix_done;

        randOrderNum = Random.Range(1, 9); // Generate between 1 and 8 meals
        startOrder();
>>>>>>> cef6db37dcbafebdfc5eec0ed374d66360cbb6eb
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

        if (activeOrders.Contains("Baking") && bakingScript.BakingDone)
        {
            Debug.Log("Baking order completed!");
            bakingScript.BakingDone = false;
            activeOrders.Remove("Baking");
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

    private void startOrder()
    {
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
                    activeOrders.Add("Baking");
                    Debug.Log("Order " + (i + 1) + ": Baking");
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
                default:
                    Debug.Log("Order " + (i + 1) + ": Invalid order type");
                    break;
            }
        }
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
