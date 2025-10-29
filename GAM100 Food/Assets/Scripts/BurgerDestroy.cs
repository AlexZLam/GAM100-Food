/*******************************************************************************
* File Name: BurgerDestroy.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description: This script manages burger ingredient behavior in the burger
* minigame. It handles positioning on the plate, falling detection, and 
* communication with the Burger script when ingredients are lost or reset.
*******************************************************************************/

using UnityEngine;

public class BurgerDestroy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject plate;          // Reference to the plate object
    public Burger burgereScript;                        // Reference to the Burger script

    [Header("Ingredient State Flags")]
    public bool bun1;
    public bool bun;
    public bool lettuce;
    public bool tomatto;
    public bool cheese;
    public bool onion;

    private bool onPlate = false;                       // Whether the ingredient is currently on the plate
    private bool fell = false;                          // Whether the ingredient has fallen
    private bool isDestroyed = false;                   // Reserved for future use (e.g., visual destruction)
    private Vector3 platePos;                           // Cached plate position for snapping

    // Static method to remove all ingredients from the plate
    public static void RemoveAllFromPlate()
    {
        BurgerDestroy[] allIngredients = FindObjectsOfType<BurgerDestroy>();
        foreach (BurgerDestroy ingredient in allIngredients)
        {
            ingredient.RemoveFromPlate();
        }
    }

    // Removes this ingredient from the plate
    public void RemoveFromPlate()
    {
        onPlate = false;
    }

    // Called once per frame
    void Update()
    {
        // Cache the plate's current position
        platePos = plate.transform.position;

        // If the ingredient is on the plate, snap its position to the plate
        if (onPlate)
        {
            transform.position = platePos;
            // Optional: match rotation if needed
            // transform.rotation = plate.transform.rotation;
        }

        // If the ingredient has fallen and hasn't been handled yet
        if (fell && !isDestroyed)
        {
            onPlate = false;
            burgereScript?.OnIngredientFell(); // Notify the Burger script
            fell = false;
        }

        // If the burger is marked as done, simulate a fall and reset
        if (burgereScript.Burgerdone == true)
        {
            fell = true;
            RemoveAllFromPlate();
            burgereScript?.OnIngredientFell();
            burgereScript.Burgerdone = false;
        }
    }

    // Called when the ingredient stays in contact with another collider
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Ignore plate-to-plate collisions
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Plate"))
        {
            return;
        }

        // If the ingredient hits the ground, mark it as fallen and reset all
        if (collision.gameObject.CompareTag("Ground"))
        {
            fell = true;
            RemoveAllFromPlate();
        }

        // If the ingredient touches the plate and is a valid burger component, snap it
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
