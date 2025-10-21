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
    public bool Done;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            StartEnergizedEffect(duration);
        });
    }

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
            if (progressBar.radialProgressBar.fillAmount <= 2)
            {
                Done = true;
                EndEnergizedEffect(duration);
            }
        });
    }


    public void StartEnergizedEffect(float duration)
    {


        isEnergized = true;
        energizedEffect.SetActive(true);
        energizedEffect.transform.Find("RadialProgressBar").GetComponent<CircularProgressBar>().ActivateCountdown(duration);

        StartCoroutine(EndEnergizedEffect(duration));
    }

    IEnumerator EndEnergizedEffect(float delay)
    {
        yield return new WaitForSeconds(delay);

        isEnergized = false;
    }
}
