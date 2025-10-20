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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBurger = burgerScript.Burgerdone;
        isChopping = choppingScript.chopping_done;
        isSmash = smashScript.burgersmash_done;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
