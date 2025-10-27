using UnityEngine;
using UnityEngine.UI;
public class helpButton : MonoBehaviour
{

    [SerializeField]
    public GameObject helpBake, helpFry, helpSmash, helpSalad, helpMilk, helpBurger, helpChopping;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    public GameObject helpSpawn;

    public camera_move camMove;

    private GameObject currentGame;
    private bool isClicked = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        currentGame = camMove.current_game;
        if (isClicked == false)
        {
            Destroy(helpChopping);
            Destroy(helpBake);
            Destroy(helpMilk);
            Destroy(helpBurger);
            Destroy(helpSmash);
            Destroy(helpSalad);
            Destroy(helpFry);
        }
    }
    public void getHelp()
    {
        isClicked = true;
        if(currentGame == camMove.baking)
        {
             Instantiate(helpBake, helpSpawn.transform.position, Quaternion.identity, currentGame.transform);

        }
        else if (currentGame == camMove.fries)
        {
            Instantiate(helpFry, helpSpawn.transform.position, Quaternion.identity);
        }
        else if (currentGame == camMove.smash)
        {
            Instantiate(helpSmash, helpSpawn.transform.position, Quaternion.identity);
        }
        else if (currentGame == camMove.salad)
        {
            Instantiate(helpSalad, helpSpawn.transform.position, Quaternion.identity);
        }
        else if (currentGame == camMove.milkshake)
        {
            Instantiate(helpMilk, helpSpawn.transform.position, Quaternion.identity);
        }
        else if (currentGame == camMove.burger)
        {
            Instantiate(helpBurger, helpSpawn.transform.position, Quaternion.identity);
        }
        else if (currentGame == camMove.chopping)
        {
            Instantiate(helpChopping, helpSpawn.transform.position, Quaternion.identity);
        }
    }
}
