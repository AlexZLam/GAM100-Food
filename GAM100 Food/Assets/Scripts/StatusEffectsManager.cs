using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectsManager : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move;
    public CircularProgressBar progressBar;
    
    [Header("Game Object")]
    public GameObject energizedEffect;
    public GameObject ParentObject;

    [Header("Is Active")]
    public bool isEnergized;

    [Header("Button")]
    public Button button;

    [Header("Duration")]
    public float duration;

    [Header("Finished")]
    public bool friesDone;
    private bool itStarted = false;

    private void Update()
    {
        if (camera_Move.current_game == camera_Move.fries)
        {
            ParentObject.SetActive(true);
        }
        else
        {
            ParentObject.SetActive(false);
        }

        button.onClick.AddListener(() =>
        {
            if (progressBar.radialProgressBar.fillAmount <= 0.096f)
            {
                Debug.Log("Success");
                friesDone = true;
                EndEnergizedEffect(duration);
                itStarted = false;

            }
            if(itStarted == false)
            {
                StartEnergizedEffect(duration);
                itStarted = true;
            }
            if(progressBar.radialProgressBar.fillAmount > 0.195f && itStarted == true && progressBar.radialProgressBar.fillAmount < 0.95f)
            {
                Debug.Log("Fail");
                StartEnergizedEffect(duration);
            }
            if(friesDone == true && progressBar.radialProgressBar.fillAmount >= 0.95f)
            {
                friesDone = false;
                itStarted = true;
                StartEnergizedEffect(duration);
            }

        });
        if (progressBar.radialProgressBar.fillAmount == 0)
        {
            StartEnergizedEffect(duration);
        }
    }


    public void StartEnergizedEffect(float duration)
    {


        isEnergized = true;
        progressBar.ActivateCountdown(duration);

        StartCoroutine(EndEnergizedEffect(duration));
    }

    IEnumerator EndEnergizedEffect(float delay)
    {
        yield return new WaitForSeconds(delay);

        isEnergized = false;
    }
}
