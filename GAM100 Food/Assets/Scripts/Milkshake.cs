using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Milkshake : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move;
    [Header("Buttons")]
    public Button spamclick_button;
    public Button start_button;
    [Header("Parent Object")]
    public GameObject milkshake;
    [Header("Time to beat")]
    public float timer = 10f;
    public int click_goal = 100;


    public Animator milkshake_animator;

    private int click_counter;
    private float time_counter;
    private bool game_started = false;
    public bool milkshake_done = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time_counter = timer;
        spamclick_button.onClick.AddListener(OnSpamButtonClick);
        start_button.onClick.AddListener(OnStartButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_started)
        {
            time_counter -= Time.deltaTime;
            if (time_counter <= 0)
            {
                milkshake_done = false;
                game_started = false;
                Debug.Log("you lost.");
                //animate
                milkshake_animator.SetTrigger("done_mixing");
            }
        }
        setMilkshakeActive();
    }

    void OnSpamButtonClick()
    {
        if (game_started)
        {
            click_counter++;
            if(click_counter % 10 == 0)
            {
                Debug.Log(click_counter + " clicks , " + time_counter + " seconds left");
            }
            if (click_counter == click_goal)
            {
                milkshake_done = true;
                game_started = false;
                Debug.Log("you won!");
                milkshake_animator.SetTrigger("done_mixing");
            }
        }
    }

    void OnStartButtonClick()
    {
        //animate
        milkshake_animator.ResetTrigger("done_mixing");
        milkshake_animator.ResetTrigger("start_mixing");
        milkshake_animator.SetTrigger("start_mixing");
        milkshake_animator.SetTrigger("start_mixing");
        game_started = true;
        time_counter = timer;
        click_counter = 0;
        milkshake_done = false;
        Debug.Log("Milkshake started: get " + click_goal + " clicks in " + time_counter + " seconds to win!");
    }

    private void setMilkshakeActive()
    {
        milkshake.SetActive(camera_Move.current_game == camera_Move.milkshake);
    }
}
