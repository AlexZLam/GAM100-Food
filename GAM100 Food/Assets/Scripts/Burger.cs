using UnityEngine;
using static UnityEditor.SceneView;
using UnityEngine.UIElements.Experimental;

public class Burgere : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject burger;
    [SerializeField]
    private GameObject bun, bun1, tomato, lettuce, cheese, onion, plate;

    [SerializeField]
    private float plateSpeed = 5f;

    [SerializeField]
    private float plateMinX = 9.68f;  // Left boundary
    [SerializeField]
    private float plateMaxX = 26.46f; // Right boundary


    float rand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        Vector3 startPos = new Vector3(18, 16);
        GameObject[] ingredients = { bun, bun1, tomato, lettuce, cheese, onion };

        foreach (GameObject ingredient in ingredients)
        {

            ingredient.transform.position = startPos;

        }

        rand = Random.Range(9.68f, 26.46f);

    }

    // Update is called once per frame
    private float fallSpeed = 0f;
    private float startY = 16f;

    void Update()
    {
        setBurgerActive();
        MovePlate();
        ingredientFall();

      
    }
    private void MovePlate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 platePos = plate.transform.position;

        platePos.x += horizontalInput * plateSpeed * Time.deltaTime;
        platePos.x = Mathf.Clamp(platePos.x, plateMinX, plateMaxX);

        plate.transform.position = platePos;
    }


    private void setBurgerActive()
    {
        burger.SetActive(camera_Move.current_game == camera_Move.burger);
    }
    
    public void ingredientFall()
    {
        if (burger.activeSelf && startY - fallSpeed >= 8)
        {
            startY = 16f;
            fallSpeed = 0f;
            fallSpeed += 2f * Time.deltaTime;
            bun.transform.position = new Vector3(rand, startY - fallSpeed);
        }
        else if (startY - fallSpeed >= 8)
        {
            startY = 16f;
            fallSpeed = 0f;
            fallSpeed += 2f * Time.deltaTime;
            lettuce.transform.position = new Vector3(rand, startY - fallSpeed);
        }
        else if (startY - fallSpeed >= 8)
        {
            startY = 16f;
            fallSpeed = 0f;
            fallSpeed += 2f * Time.deltaTime;
            tomato.transform.position = new Vector3(rand, startY - fallSpeed);
        }
        else if (startY - fallSpeed >= 8)
        {
            startY = 16f;
            fallSpeed = 0f;
            fallSpeed += 2f * Time.deltaTime;
            onion.transform.position = new Vector3(rand, startY - fallSpeed);
        }
        else if (startY - fallSpeed >= 8)
        {
            startY = 16f;
            fallSpeed = 0f;
            fallSpeed += 2f * Time.deltaTime;
            cheese.transform.position = new Vector3(rand, startY - fallSpeed);
        }
        else if (startY - fallSpeed >= 8)
        {
            startY = 16f;
            fallSpeed = 0f;
            fallSpeed += 2f * Time.deltaTime;
            bun1.transform.position = new Vector3(rand, startY - fallSpeed);
        }
    }
}
