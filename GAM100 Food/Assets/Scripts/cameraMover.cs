/*******************************************************************************
* File Name: camera_move.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: GAM100
*
* Description:
*   This file handles camera movement between different minigame areas based on
*   user input. Pressing keys 1–9 moves the camera to the corresponding game
*   station. The script updates the current active game reference and positions
*   the camera accordingly.
*******************************************************************************/

using UnityEngine;

public class camera_move : MonoBehaviour
{


    [Header("Current Game Reference")]
    public GameObject current_game;
    // Tracks which game area is currently active

    [Header("Game Area References")]
    [SerializeField]
    public GameObject home, counter, burger, milkshake, salad,
                      chopping, baking, fries, smash;
    // References to all game area objects the camera can move to

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Called before the first frame update. Currently unused, but available for
    *   initialization logic if needed in the future.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Start()
    {
        // Reserved for future initialization
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Called once per frame. Checks for number key input (1–9) and updates the
    *   current game reference. After each key press, the camera is moved to the
    *   corresponding game area's position.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        if (Input.anyKey)
        {
            // Key 1 ? Home
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                current_game = home;
                moveCamera(current_game.transform);
            }

            // Key 2 ? Counter
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                current_game = counter;
                moveCamera(current_game.transform);
            }

            // Key 3 ? Burger
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                current_game = burger;
                moveCamera(current_game.transform);
            }

            // Key 4 ? Milkshake
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                current_game = milkshake;
                moveCamera(current_game.transform);
            }

            // Key 5 ? Salad
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                current_game = salad;
                moveCamera(current_game.transform);
            }

            // Key 6 ? Chopping
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                current_game = chopping;
                moveCamera(current_game.transform);
            }

            // Key 7 ? Baking
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                current_game = baking;
                moveCamera(current_game.transform);
            }

            // Key 8 ? Fries
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                current_game = fries;
                moveCamera(current_game.transform);
            }

            // Key 9 ? Smash
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                current_game = smash;
                moveCamera(current_game.transform);
            }
        }
    }

    /****************************************************************************
    * Function: moveCamera
    *
    * Description:
    *   Moves the camera to the position of the specified transform. This is used
    *   to reposition the camera whenever the player switches minigame areas.
    *
    * Inputs:
    *   Transform t - The transform of the target game area
    *
    * Outputs: None
    ****************************************************************************/
    void moveCamera(Transform t)
    {
        transform.position = t.position;
    }
}
