using UnityEngine;
using System.Collections.Generic;

public class HelpManager : MonoBehaviour
{
    [SerializeField] private GameObject helpBaking, helpFry, helpBurger, helpSmash, helpChopping, helpSalad, helpMilk;
    [SerializeField] private GameObject center;
    [SerializeField] private Canvas canvas;

    private camera_move cam;
    private GameObject activeHelp;
    private bool isHelp = false;

    private Dictionary<GameObject, GameObject> helpMap;

    void Start()
    {
        cam = FindObjectOfType<camera_move>();

        helpMap = new Dictionary<GameObject, GameObject>
        {
            { cam.baking, helpBaking },
            { cam.fries, helpFry },
            { cam.burger, helpBurger },
            { cam.smash, helpSmash },
            { cam.chopping, helpChopping },
            { cam.salad, helpSalad },
            { cam.milkshake, helpMilk }
        };
    }

    void Update()
    {
        if (!isHelp && activeHelp != null)
        {
            Destroy(activeHelp);
            activeHelp = null;
        }
    }

    public void ToggleHelp()
    {
        isHelp = !isHelp;

        if (isHelp)
        {
            GameObject current = cam.current_game;

            if (helpMap.TryGetValue(current, out GameObject helpPrefab) && helpPrefab != null)
            {
                activeHelp = Instantiate(helpPrefab, center.transform.position, Quaternion.identity, canvas.transform);
            }
        }
        else if (activeHelp != null)
        {
            Destroy(activeHelp);
            activeHelp = null;
        }
    }
}
