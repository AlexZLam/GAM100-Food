/*******************************************************************************
* File Name: HelpManager.cs
* Author: Alexander Lam
* DigiPen Email: alexander.lam@digipen.edu
* Course: GAM100
*
* Description:
*   This file manages help overlays for each minigame. When the player toggles
*   help, the script displays the appropriate help panel based on the currently
*   active minigame. Only one help panel can be active at a time.
*******************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public class HelpManager : MonoBehaviour
{
    /****************************************************************************
    * Section: Inspector References
    ****************************************************************************/
    [SerializeField] private GameObject helpBaking;
    [SerializeField] private GameObject helpFry;
    [SerializeField] private GameObject helpBurger;
    [SerializeField] private GameObject helpSmash;
    [SerializeField] private GameObject helpChopping;
    [SerializeField] private GameObject helpSalad;
    [SerializeField] private GameObject helpMilk;
    // Help panel prefabs for each minigame

    [SerializeField] private GameObject center; // Position where help panels appear
    [SerializeField] private Canvas canvas;     // Canvas used as parent for help panels

    /****************************************************************************
    * Section: Internal State
    ****************************************************************************/
    private camera_move cam;                   // Reference to camera movement script
    private GameObject activeHelp;             // Currently displayed help panel
    private bool isHelp = false;               // True when help is toggled on

    private Dictionary<GameObject, GameObject> helpMap; // Maps minigames to help panels

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Initializes references and constructs the mapping between minigame
    *   objects and their corresponding help panels.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Start()
    {
        // Locate the camera movement script in the scene
        cam = FindObjectOfType<camera_move>();

        // Create mapping between minigame objects and help panel prefabs
        helpMap = new Dictionary<GameObject, GameObject>
        {
            { cam.baking,    helpBaking },
            { cam.fries,     helpFry },
            { cam.burger,    helpBurger },
            { cam.smash,     helpSmash },
            { cam.chopping,  helpChopping },
            { cam.salad,     helpSalad },
            { cam.milkshake, helpMilk }
        };
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Ensures that if help is toggled off, any active help panel is removed.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        // If help is off but a panel is still active, remove it
        if (!isHelp && activeHelp != null)
        {
            Destroy(activeHelp);
            activeHelp = null;
        }
    }

    /****************************************************************************
    * Function: ToggleHelp
    *
    * Description:
    *   Toggles the help system on or off. When toggled on, displays the help
    *   panel corresponding to the currently active minigame. When toggled off,
    *   removes any active help panel.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    public void ToggleHelp()
    {
        // Flip help state
        isHelp = !isHelp;

        if (isHelp)
        {
            // Determine which minigame is currently active
            GameObject current = cam.current_game;

            // If a help panel exists for this minigame, instantiate it
            if (helpMap.TryGetValue(current, out GameObject helpPrefab) && helpPrefab != null)
            {
                activeHelp = Instantiate(
                    helpPrefab,
                    center.transform.position,
                    Quaternion.identity,
                    canvas.transform
                );
            }
        }
        else if (activeHelp != null)
        {
            // Remove active help panel when toggled off
            Destroy(activeHelp);
            activeHelp = null;
        }
    }
}
