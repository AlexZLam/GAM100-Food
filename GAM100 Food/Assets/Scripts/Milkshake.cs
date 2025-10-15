using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Milkshake : MonoBehaviour
{
    public camera_move camera_Move;
    public Button spamclick_button;
    public Button start_button;
    public float timer = 10f;
    public int click_goal = 100;

    private int click_counter;
    private bool game_started = false;
    private bool game_won = false;


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
                game_won = false;
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
            if (click_counter == click_goal)
            {
                game_won = true;
                game_started = false;
                Debug.Log("you won!");
            }
        }
    }

    void OnStartButtonClick()
    {
        game_started = true;
        timer = 10f;
        game_won = false;
    }

    private void setMilkshakeActive()
    {
        gameObject.SetActive(camera_Move.current_game == camera_Move.milkshake);
    }
}
