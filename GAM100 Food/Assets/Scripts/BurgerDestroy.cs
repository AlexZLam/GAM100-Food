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
  
    void Start()
    {
        
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

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Plate"))
        {
            onPlate = true;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            fell = true;
        }

        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Tomato"))
        {

        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Bun"))
        {

        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Bun1"))
        {

        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Cheese"))
        {

        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Lettuce"))
        {

        }
        if (collision.gameObject.CompareTag("Plate") && gameObject.CompareTag("Onion"))
        {

        }

    }
}
