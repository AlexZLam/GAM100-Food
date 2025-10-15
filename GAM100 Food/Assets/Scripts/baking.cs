using UnityEngine;
using UnityEngine.UI;

public class Baking : MonoBehaviour
{
    public camera_move camera_Move;
    public GameObject slider;
    public GameObject baking;

    [SerializeField]
    private Button button;
    [SerializeField]
    private RawImage dark;

    private const float time = 5.0f;
    private const float successThreshold = 0.9f;

    private float countdown;
    private bool isCountingDown = false;
    private float alpha;

    private Color newColor;
    private CanvasGroup sliderGroup;
    private Slider sliderComponent;
 

    void Start()
    {
        sliderComponent = slider.GetComponent<Slider>();
        sliderGroup = slider.GetComponent<CanvasGroup>();

        newColor = dark.color;
        newColor.a = 0;

        sliderComponent.maxValue = time;
        countdown = time;
        sliderComponent.value = countdown;

        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (isCountingDown)
        {
            Timer();
        }

        alpha = Mathf.Clamp01(1.0f - Mathf.Pow(countdown / time, 7));
        sliderComponent.value = countdown;

        setBakingActive();
    }

    private void Timer()
    {
        countdown -= Time.deltaTime;

        // Update dark overlay alpha
        newColor.a = alpha;
        dark.color = newColor;

        // Fade out slider
        if (sliderGroup != null)
        {
            sliderGroup.alpha = 1.0f - alpha;
        }

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

        float sliderValue = sliderComponent.value;

        if (sliderValue > 0 && sliderValue <= successThreshold)
        {
            isCountingDown = false;

            Finished();

            Debug.Log("Button clicked while slider is between 0 and 0.9!");
        }
    }

    private void setBakingActive()
    {
        baking.SetActive(camera_Move.current_game == camera_Move.baking);
    }

    public void Finished()
    {
        sliderGroup.alpha = 1;
        dark.color = new Color(newColor.r, newColor.g, newColor.b, 0);
    }
}
