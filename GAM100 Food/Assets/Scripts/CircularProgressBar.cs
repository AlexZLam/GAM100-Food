using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    private bool isActive = false;

    private float indicatorTimer;
    private float maxIndicatorTimer;

    [Header("Image")]
    public Image radialProgressBar;

    private void Awake()
    {
        radialProgressBar = GetComponent<Image>();
    }

    private void Update()
    {
        if (isActive)
        {
            indicatorTimer -= Time.deltaTime;
            radialProgressBar.fillAmount = (indicatorTimer / maxIndicatorTimer);

            if (indicatorTimer <= 0)
            {
                StopCountdown();
            }
        }
    }


    public void ActivateCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    public void StopCountdown()
    {
        radialProgressBar.fillAmount = 100;
        isActive = false;
        ActivateCountdown(10);
    }
}
