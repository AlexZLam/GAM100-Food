/*******************************************************************************
* File Name: BurgerDestroy.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description:
*   This script manages the behavior of individual burger ingredients in the
*   burger minigame. It handles snapping ingredients to the plate, detecting
*   when they fall, and notifying the Burger script when ingredients are lost
*   or when the burger is reset.
*******************************************************************************/

using UnityEngine;

public class BurgerDestroy : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject plate;   // Reference to the plate object
    public Burger burgereScript;                // Reference to the main Burger script


    [Header("Ingredient State Flags")]
    public bool bun1;
    public bool bun;
    public bool lettuce;
    public bool tomatto;
    public bool cheese;
    public bool onion;

    private bool onPlate = false;               // True if ingredient is snapped to the plate
    private bool fell = false;                  // True if ingredient has fallen off
    private bool isDestroyed = false;           // Reserved for future destruction logic
    private Vector3 platePos;                   // Cached plate position for snapping

    /****************************************************************************
    * Function: RemoveAllFromPlate 
    *
    * Description:
    *   Removes all burger ingredients from the plate by clearing their onPlate
    *   flags. Useful when resetting the entire burger.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    public static void RemoveAllFromPlate()
    {
        BurgerDestroy[] allIngredients = FindObjectsOfType<BurgerDestroy>();
        foreach (BurgerDestroy ingredient in allIngredients)
        {
            ingredient.RemoveFromPlate();
        }
    }

    public void RemoveFromPlate()//Removes Ingredients from plate
    {
        onPlate = false;
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Called once per frame. Handles snapping ingredients to the plate,
    *   detecting falls, and responding to burger completion events.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        // Cache the plate's current position
        platePos = plate.transform.position;

        // Snap ingredient to plate if placed correctly
        if (onPlate)
        {
            transform.position = platePos;
            // Optional: match rotation if desired
            // transform.rotation = plate.transform.rotation;
        }

        // Handle ingredient falling off the plate
        if (fell && !isDestroyed)
        {
            onPlate = false;
            burgereScript?.OnIngredientFell(); // Notify main burger script
            fell = false;
        }

        // If the burger is marked as done, simulate a fall and reset all ingredients
        if (burgereScript.Burgerdone == true)
        {
            RemoveAllFromPlate();
            burgereScript?.OnIngredientFell();
            burgereScript.Burgerdone = false;
        }
    }

    /****************************************************************************
    * Function: OnCollisionStay2D
    *
    * Description:
    *   Triggered while this ingredient remains in contact with another collider.
    *   Handles snapping to the plate and detecting when the ingredient hits
    *   the ground.
    *
    * Inputs:
    *   Collision2D collision - Collision information for the contact
    *
    * Outputs: None
    ****************************************************************************/
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Ignore plate-to-plate collisions
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Plate"))
        {
            return;
        }

        // If ingredient hits the ground, mark as fallen and reset all ingredients
        if (collision.gameObject.CompareTag("Ground"))
        {
            fell = true;
            RemoveAllFromPlate();
        }

        // Snap ingredient to plate if it is a valid burger component
        if (collision.gameObject.CompareTag("Plate"))
        {
            if (gameObject.CompareTag("Tomato") ||
                gameObject.CompareTag("Bun") ||
                gameObject.CompareTag("Bun1") ||
                gameObject.CompareTag("Cheese") ||
                gameObject.CompareTag("Lettuce") ||
                gameObject.CompareTag("Onion"))
            {
                onPlate = true;
            }
        }
    }
}
