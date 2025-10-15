using UnityEngine;
using static UnityEditor.SceneView;
using UnityEngine.UIElements.Experimental;

public class Burgere : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject burger;
    [SerializeField]
    private GameObject bun, bun1, tomato, lettuce, cheese, onion;

    private bool startGame = false; 

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

        if (burger.activeSelf)
        {
            fallSpeed += 2f * Time.deltaTime;
            bun1.transform.position = new Vector3(rand, startY - fallSpeed);
        }
    }

    private void setBurgerActive()
    {
       burger.SetActive(camera_Move.current_game == camera_Move.burger);

    }
}
