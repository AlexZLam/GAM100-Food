using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    [Header("Scripts")]
    public fries manager;

    private bool isActive = false;

    private float indicatorTimer;
    private float maxIndicatorTimer;

    [Header("Image")]
    public Image radialProgressBar;

    private void Awake()
    {
        radialProgressBar = GetComponent<Image>();
    }

    public void ActivateCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    private void Update()
    {
        if (isActive)
        {
            indicatorTimer -= Time.deltaTime;
            radialProgressBar.fillAmount = (indicatorTimer / maxIndicatorTimer);

            if (manager.friesDone == true)
            {
                StopCountdown();
            }
        }
    }




    public void StopCountdown()
    {
        radialProgressBar.fillAmount = 100;
        isActive = false;
    }
}
