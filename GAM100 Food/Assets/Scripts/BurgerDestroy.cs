using UnityEngine;

public class BurgerDestroy : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    public Burgere burgereScript;

    public plate plateScript;

    private bool onPlate = false;
    private bool fell = false;
    private bool isDestroyed = false;
    private Vector3 platePos;

    public bool bun1;
    public bool bun;
    public bool lettuce;
    public bool tomatto;
    public bool cheese;
    public bool onion;


    public static void RemoveAllFromPlate()
    {
        BurgerDestroy[] allIngredients = FindObjectsOfType<BurgerDestroy>();
        foreach (BurgerDestroy ingredient in allIngredients)
        {
            ingredient.RemoveFromPlate();
        }
    }

    public void RemoveFromPlate()
    {
        onPlate = false;
    }


    void Update()
    {
        platePos = plate.transform.position;
        if (onPlate)
        {
            transform.position = platePos;
        }

        if (fell && !isDestroyed)
        {
            onPlate = false;
            isDestroyed = true;
            burgereScript?.OnIngredientFell(); // Notify Burgere script
            fell = false;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Plate"))
        {
            
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            fell = true;
            burgereScript.done = false;
        }

        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Tomato"))
        {
            onPlate = true;           
        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Bun"))
        {
            onPlate = true;
        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Bun1"))
        {
            onPlate = true;
            burgereScript.done = true;
        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Cheese"))
        {
            onPlate = true;
        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Lettuce"))
        {
            onPlate = true;
        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Onion"))
        {
            onPlate = true;
        }

    }
}
