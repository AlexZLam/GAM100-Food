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

    private int click_counter;
    private bool game_started = false;
    public bool milkshake_done = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spamclick_button.onClick.AddListener(OnSpamButtonClick);
        start_button.onClick.AddListener(OnStartButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_started)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                milkshake_done = false;
                game_started = false;
                Debug.Log("you lost.");
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
                Debug.Log(click_counter + " clicks , " + timer + " seconds left");
            }
            if (click_counter == click_goal)
            {
                milkshake_done = true;
                game_started = false;
                Debug.Log("you won!");
            }
        }
    }

    void OnStartButtonClick()
    {
        Debug.Log("Milkshake started: get " + click_goal + " clicks in " + timer + " seconds to win!");
        game_started = true;
        timer = 10f;
        milkshake_done = false;
    }

    private void setMilkshakeActive()
    {
        milkshake.SetActive(camera_Move.current_game == camera_Move.milkshake);
    }
}
