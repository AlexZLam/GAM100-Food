/*******************************************************************************
* File Name: Burger.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description: This script controls the Burger stacking minigame. Ingredients
* fall one by one, and the player must move the plate to catch them. If all
* ingredients are successfully stacked, the burger is marked as complete.
*******************************************************************************/

using UnityEngine;

public class Burger : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move; // Reference to camera movement controller

    [Header("Game Objects")]
    public GameObject burger;       // Parent object for burger minigame
    [SerializeField] private GameObject bun, bun1, tomato, lettuce, cheese, onion, plate;

    [Header("Speed")]
    [SerializeField] private float plateSpeed = 10f; // Speed of plate movement

    [Header("Plate Movement Bounds")]
    private float plateMinX = 5f;                    // Left boundary for plate
    private float plateMaxX = 30f;                   // Right boundary for plate
    private float ingredientMaxX = 26f;              // Max X for falling ingredient
    private float ingredientMinX = 9f;               // Min X for falling ingredient

    [Header("Ingredient Drop Logic")]
    private GameObject[] ingredients;                // Array of ingredients to drop
    private int currentIngredientIndex = 0;          // Index of current falling ingredient
    private float fallSpeed = 0f;                    // Speed of falling ingredient
    private float startY = 16f;                      // Starting Y position for drops
    private float rand;                              // Random X position for each drop

    [Header("Game State")]
    public bool done;                                // Reserved for future use
    public bool Burgerdone;                          // Flag to indicate burger completion

// Called once at the start of the game

    void Start()//intializes ingredients, gives each ingredient the BurgerDestroy script
    {
        // Initialize ingredient array
        ingredients = new GameObject[] { bun, lettuce, tomato, onion, cheese, bun1 };
        Vector3 startPos = new Vector3(18, 16);

        // Set initial position and link BurgerDestroy script
        foreach (GameObject ingredient in ingredients)
        {
            ingredient.transform.position = startPos;

            var destroyScript = ingredient.GetComponent<BurgerDestroy>();
            if (destroyScript != null)
            {
                destroyScript.burgereScript = this;
            }
        }

        // Set initial random X position for first drop
        rand = Random.Range(ingredientMinX, ingredientMaxX);
    }

/****************************************************************************
* Function: Update
*
* Description: calls functons and checks for win
*   
*
* Inputs:  None
* Outputs: None
****************************************************************************/
    void Update()
    {
        // Show/hide burger minigame based on camera position
        setBurgerActive();

        // Move plate based on player input
        MovePlate();

        // Handle ingredient falling logic
        if (burger.activeSelf && currentIngredientIndex < ingredients.Length)
        {
            fallSpeed += 7f * Time.deltaTime;
            float newY = startY - fallSpeed;

            // Drop ingredient until it reaches plate level
            if (newY >= 5f)
            {
                ingredients[currentIngredientIndex].transform.position = new Vector3(rand, newY);
            }
            else
            {
                // Reset for next ingredient
                fallSpeed = 0f;
                startY = 16f;
                currentIngredientIndex++;

                if (currentIngredientIndex < ingredients.Length)
                {
                    rand = Random.Range(ingredientMinX, ingredientMaxX);
                }
                else
                {
                    Debug.Log("Complete");
                    Burgerdone = true;
                    OnIngredientFell();
                }
            }
        }
    }

/****************************************************************************
* Function: MovePlate
*
* Description: Moves plate horizontally
* 
*
* Inputs:  None
* Outputs: None
****************************************************************************/
    private void MovePlate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 platePos = plate.transform.position;

        platePos.x += horizontalInput * plateSpeed * Time.deltaTime;
        platePos.x = Mathf.Clamp(platePos.x, plateMinX, plateMaxX);

        plate.transform.position = platePos;
    }

/****************************************************************************
* Function: setBurgerActive
*
* Description: Controls the visibility of the burger station
* 
*
* Inputs:  None
* Outputs: None
****************************************************************************/
    private void setBurgerActive()
    {
        burger.SetActive(camera_Move.current_game == camera_Move.burger);
    }

    // Called by BurgerDestroy when an ingredient falls off the plate
    public void OnIngredientFell()//Resets Ingredients to be ready to fall again
    {
        // Reset ingredient drop sequence
        currentIngredientIndex = 0;
        fallSpeed = 0f;
        startY = 16f;
        rand = Random.Range(ingredientMinX, ingredientMaxX);

        Vector3 resetPos = new Vector3(rand, startY);

        // Reset all ingredient positions
        foreach (GameObject ingredient in ingredients)
        {
            ingredient.transform.position = resetPos;
        }
    }
}
