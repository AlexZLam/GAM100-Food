/*******************************************************************************
* File Name: camera_move.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: GAM100
*
* Description: This file handles camera movement between different minigame areas
*              based on user input. Each number key (1–9) moves the camera to a
*              corresponding game object.
*******************************************************************************/

using UnityEngine;

public class camera_move : MonoBehaviour
{
    [Header("Current Game Reference")]
    public GameObject current_game; // Tracks which game area is currently active

    [Header("Game Area References")]
    [SerializeField]
    public GameObject home, counter, burger, milkshake, salad, chopping, baking, fries, smash;
    // References to all game area objects the camera can move to

    // Start is called before the first frame update
    void Start()
    {
        // Initialization logic can be added here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKey)
        {
            // If key 1 is pressed, move to home area
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                current_game = home;
            }

            // If key 2 is pressed, move to counter area
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                current_game = counter;
            }

            // Move camera to the newly selected game area
            moveCamera(current_game.transform);

            // If key 3 is pressed, move to burger area
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                current_game = burger;
            }

            // If key 4 is pressed, move to milkshake area
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                current_game = milkshake;
            }

            // Move camera to the newly selected game area
            moveCamera(current_game.transform);

            // If key 5 is pressed, move to salad area
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                current_game = salad;
            }

            // If key 6 is pressed, move to chopping area
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                current_game = chopping;
            }

            // Move camera to the newly selected game area
            moveCamera(current_game.transform);

            // If key 7 is pressed, move to baking area
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                current_game = baking;
            }

            // If key 8 is pressed, move to fries area
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                current_game = fries;
            }

            // Move camera to the newly selected game area
            moveCamera(current_game.transform);

            // If key 9 is pressed, move to smash area
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                current_game = smash;
            }
        }
    }

    // Moves the camera to the position of the specified transform
    void moveCamera(Transform t)
    {
        gameObject.transform.position = t.position;
    }
}
