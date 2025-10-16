using UnityEngine;

public class BurgerDestroy : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    public Burgere burgereScript;

    private bool onPlate = false;
    private bool fell = false;
    private bool isDestroyed = false;
    private Vector3 platePos;

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
            isDestroyed = true;
            burgereScript?.OnIngredientFell(); // Notify Burgere script

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            onPlate = true;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            fell = true;
        }
    }
}
