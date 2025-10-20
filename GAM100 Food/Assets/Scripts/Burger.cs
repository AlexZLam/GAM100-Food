using UnityEngine;

public class Burgere : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject burger;
    [SerializeField] private GameObject bun, bun1, tomato, lettuce, cheese, onion, plate;

    [SerializeField] private float plateSpeed = 10f;
    private float plateMinX = 5;
    private float plateMaxX = 30f;
    private float ingredientMaxX = 26f;
    private float ingredientMinX = 9f;


    private GameObject[] ingredients;
    private int currentIngredientIndex = 0;
    private float fallSpeed = 0f;
    private float startY = 16f;
    private float rand;
    public bool done;

    void Start()
    {
        ingredients = new GameObject[] { bun, lettuce, tomato, onion, cheese, bun1 };
        Vector3 startPos = new Vector3(18, 16);

        foreach (GameObject ingredient in ingredients)
        {
            ingredient.transform.position = startPos;

            // Add BurgerDestroy reference
            var destroyScript = ingredient.GetComponent<BurgerDestroy>();
            if (destroyScript != null)
            {
                destroyScript.burgereScript = this;
            }
        }

        rand = Random.Range(ingredientMinX, ingredientMaxX);

    }

    void Update()
    {
        
        setBurgerActive();
        MovePlate();

        if (burger.activeSelf && currentIngredientIndex < ingredients.Length)
        {
            fallSpeed += 5f * Time.deltaTime;
            float newY = startY - fallSpeed;

            if (newY >= 5f)
            {
                ingredients[currentIngredientIndex].transform.position = new Vector3(rand, newY);
                Debug.Log(rand);
                Debug.Log(ingredients[currentIngredientIndex].transform.position.x);
                Debug.Log(" MinX: " + plateMinX + " MaxX: " + plateMaxX);
            }
            else
            {
                fallSpeed = 0f;
                startY = 16f;
                currentIngredientIndex++;
                if (currentIngredientIndex < ingredients.Length)
                {
                    rand = Random.Range(ingredientMinX, ingredientMaxX);
                }
                else if(done == true)
                {
                    Debug.Log("Complete");
                }
            }
        }
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

    // Called by BurgerDestroy when an ingredient falls off
    public void OnIngredientFell()
    {
        currentIngredientIndex = 0;
        fallSpeed = 0f;
        startY = 16f;
        rand = Random.Range(ingredientMinX, ingredientMaxX);

        Vector3 resetPos = new Vector3(rand, startY);

        foreach (GameObject ingredient in ingredients)
        {
            ingredient.transform.position = resetPos;
        }
    }

}
