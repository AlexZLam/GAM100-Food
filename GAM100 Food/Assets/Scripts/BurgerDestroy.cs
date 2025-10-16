using UnityEngine;

public class BurgerDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject plate;

    private bool onPlate = false;
    public bool fell = false;
    Vector3 platePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platePos = plate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(onPlate)
        {
            gameObject.transform.position = platePos;
        }
        if(fell)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Plate"))
        {
            onPlate = true;
        }
        else
        {
            fell = true;
        }
    }
}
