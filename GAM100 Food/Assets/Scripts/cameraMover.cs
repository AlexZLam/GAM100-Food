using UnityEngine;

public class camera_move : MonoBehaviour
{
    private GameObject current_game;

    [SerializeField]
    private GameObject home, counter, burger, milkshake, salad, chopping, baking, fries, chopping2;

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                current_game = home;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                current_game = counter;
            }
            moveCamera(current_game.transform);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                current_game = burger;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                current_game = milkshake;
            }
            moveCamera(current_game.transform);
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                current_game = salad;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                current_game = chopping;
            }
            moveCamera(current_game.transform);
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                current_game = baking;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                current_game = fries;
            }
            moveCamera(current_game.transform);
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                current_game = chopping2;
            }
            
        }

    }

    void moveCamera(Transform t)
    {
        gameObject.transform.position = t.position;
    }
}
