using UnityEngine;
using UnityEngine.UI;

public class Baking : MonoBehaviour
{
    public camera_move camera_Move;
    [SerializeField]
    private float time = 5.0f;
    private float countdown;

    [SerializeField]
    private Button button;

    private bool isCountingDown = false;

    void Start()
    {
        button.enabled = false;
        button.GetComponent<Image>().enabled = false;
        countdown = time;
        button.onClick.AddListener(OnButtonClick);
    }
    
    void Update()
    {
        if(camera_Move. current_game == camera_Move.baking)
        {
             button.enabled = true;
             button.GetComponent<Image>().enabled = true;
        }
           
        else
        {
            button.enabled = false;
            button.GetComponent<Image>().enabled = false;
        }
            
        if (isCountingDown)
        {
            Timer();
        }
    }

    private void Timer()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            isCountingDown = false;
            Debug.Log("Timer finished!");
        }
    }

    private void OnButtonClick()
    {
        countdown = time;
        isCountingDown = true;
        Debug.Log("Button clicked, countdown started.");
    }
}
