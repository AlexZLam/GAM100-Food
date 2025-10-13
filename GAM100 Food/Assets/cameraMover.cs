using UnityEngine;

public class camera_move : MonoBehaviour
{
    private GameObject current_game;

    [SerializeField]
    private GameObject minigame1, minigame2;

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
                current_game = minigame1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                current_game = minigame2;
            }
            moveCamera(current_game.transform);
        }

    }

    void moveCamera(Transform t)
    {
        gameObject.transform.position = t.position;
    }
}
