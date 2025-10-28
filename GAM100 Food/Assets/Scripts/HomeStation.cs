using UnityEngine;
using static UnityEditor.SceneView;

public class HomeStation : MonoBehaviour
{
    [SerializeField]
    private GameObject home;

    public camera_move camera_Move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setHomeActive();
    }
    private void setHomeActive()
    {
        home.SetActive(camera_Move.current_game == camera_Move.home);
    }
}
