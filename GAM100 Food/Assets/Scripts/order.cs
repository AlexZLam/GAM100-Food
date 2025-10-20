using UnityEngine;
using UnityEngine.UI;

public class order : MonoBehaviour
{
    public Burger burgerScript;
    public Baking bakingScript;
    public Milkshake milkshakeScript;
    public Chopping choppingScript;
    public BurgerSmash smashScript;

    private bool isBurger;
    private bool isChopping;
    private bool isBaking;
    private bool isSmash;
    private bool isMilkshake;

    private int randOrderNum;
    private int randOrder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
<<<<<<< Updated upstream
        isBurger = burgerScript.Burgerdone;
        isChopping = choppingScript.chopping_done;
        isSmash = smashScript.burgersmash_done;
=======
        randOrderNum = Random.Range(1, 9); // Generate between 1 and 8 meals
        startOrder();
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        // Check if each order type is done and reset the flag
        if (isBurger && burgerScript.Burgerdone)
        {
            Debug.Log("Burger order completed!");
            burgerScript.Burgerdone = false;
            isBurger = false;
        }

        if (isBaking && bakingScript.BakingDone)
        {
            Debug.Log("Baking order completed!");
            bakingScript.BakingDone = false;
            isBaking = false;
        }

        if (isMilkshake && milkshakeScript.milkshake_done)
        {
            Debug.Log("Milkshake order completed!");
            milkshakeScript.milkshake_done = false;
            isMilkshake = false;
        }

        if (isChopping && choppingScript.chopping_done)
        {
            Debug.Log("Chopping order completed!");
            choppingScript.chopping_done = false;
            isChopping = false;
        }

        if (isSmash && smashScript.BurgerSmashDone)
        {
            Debug.Log("Burger Smash order completed!");
            smashScript.BurgerSmashDone = false;
            isSmash = false;
        }
    }

    private void startOrder()
    {
        for (int i = 0; i < randOrderNum; i++)
        {
            randOrder = Random.Range(1, 6); // Random meal type between 1 and 5

            switch (randOrder)
            {
                case 1:
                    isBurger = true;
                    Debug.Log("Order " + (i + 1) + ": Burger");
                    break;
                case 2:
                    isBaking = true;
                    Debug.Log("Order " + (i + 1) + ": Baking");
                    break;
                case 3:
                    isMilkshake = true;
                    Debug.Log("Order " + (i + 1) + ": Milkshake");
                    break;
                case 4:
                    isChopping = true;
                    Debug.Log("Order " + (i + 1) + ": Chopping");
                    break;
                case 5:
                    isSmash = true;
                    Debug.Log("Order " + (i + 1) + ": Burger Smash");
                    break;
                default:
                    Debug.Log("Order " + (i + 1) + ": Invalid order type");
                    break;
            }
        }
    }
}
