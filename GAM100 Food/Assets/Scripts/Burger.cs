using UnityEngine;
using static UnityEditor.SceneView;
using UnityEngine.UIElements.Experimental;

public class Burgere : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject burger;
    [SerializeField]
    private GameObject bun, bun1, tomato, lettuce, cheese, onion;

    public int fallSpeed = 2;
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
    void Update()
    {
        setBurgerActive();
        fallSpeed *= (int)Time.deltaTime;
        bun1.transform.position = new Vector3(rand, fallSpeed);
    }
    private void setBurgerActive()
    {
        burger.SetActive(camera_Move.current_game == camera_Move.burger);
    }
}
