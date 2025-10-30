/*******************************************************************************
* File Name: HelpManager.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description: This file manages help overlays for different minigames. It
* displays context-specific help panels based on the current active game.
*******************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public class HelpManager : MonoBehaviour
{
    [SerializeField] private GameObject helpBaking, helpFry, helpBurger, helpSmash, helpChopping, helpSalad, helpMilk; // Help panels for each minigame
    [SerializeField] private GameObject center; // Position where help panels will appear
    [SerializeField] private Canvas canvas; // Canvas to parent help panels

    private camera_move cam; // Reference to camera movement script
    private GameObject activeHelp; // Currently active help panel
    private bool isHelp = false; // Flag to track if help is toggled on

    private Dictionary<GameObject, GameObject> helpMap; // Maps minigame objects to their help panels

    // Start is called before the first frame update
    void Start()
    {
        // Find camera movement script in the scene
        cam = FindObjectOfType<camera_move>();

        // Initialize mapping between minigame objects and their help panels
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

    // Update is called once per frame
    void Update()
    {
        // If help is toggled off and a help panel is active, destroy it
        if (!isHelp && activeHelp != null)
        {
            Destroy(activeHelp);
            activeHelp = null;
        }
    }

    // Toggles help panel visibility based on current minigame
    public void ToggleHelp()
    {
        // Flip help state
        isHelp = !isHelp;

        if (isHelp)
        {
            // Get currently active minigame
            GameObject current = cam.current_game;

            // If a help panel exists for the current game, instantiate it
            if (helpMap.TryGetValue(current, out GameObject helpPrefab) && helpPrefab != null)
            {
                activeHelp = Instantiate(helpPrefab, center.transform.position, Quaternion.identity, canvas.transform);
            }
        }
        else if (activeHelp != null)
        {
            // If help is toggled off, destroy the active help panel
            Destroy(activeHelp);
            activeHelp = null;
        }
    }
}
