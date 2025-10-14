using UnityEngine;
using UnityEngine.UI;

public class Baking : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject slider;
    private float time = 5.0f;
    private float countdown;
   
    [SerializeField]
    private Button button;

    private bool isCountingDown = false;

    void Start()
    {

        slider.GetComponent<Slider>().maxValue = 5;
        slider.GetComponent<Slider>().value = countdown;
        slider.SetActive(false);
        button.enabled = false;
        slider.GetComponent<Slider>().enabled = false;
        button.GetComponent<Image>().enabled = false;
        countdown = time;
        button.onClick.AddListener(OnButtonClick);
    }
    
    void Update()
    {
        slider.GetComponent<Slider>().value = countdown;

        setBakingActive();
            
        if (isCountingDown)
        {
            Timer();
        }
        //Debug.Log(countdown);
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
        float sliderValue = slider.GetComponent<Slider>().value;

        // Check if slider is between 0.5 and 2.0
        if (sliderValue >= 0.5f && sliderValue <= 2.0f)
        {
            isCountingDown = false;
            button.enabled = false;

            Debug.Log("Button clicked while slider is between 0.5 and 2.0!");
            // You can add any special behavior here
        }       
    }

    private void setBakingActive()
    {
        if (camera_Move.current_game == camera_Move.baking)
        {
            button.enabled = true;
            button.GetComponent<Image>().enabled = true;
        }

        else
        {
            button.enabled = false;
            button.GetComponent<Image>().enabled = false;
        }
        if (camera_Move.current_game == camera_Move.baking)
        {
            slider.SetActive(true);
            slider.GetComponent<Slider>().enabled = true;
        }

        else
        {
            slider.SetActive(false);
            slider.GetComponent<Slider>().enabled = false;
        }
    }
   

}
