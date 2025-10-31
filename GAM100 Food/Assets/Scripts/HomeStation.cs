/*******************************************************************************
* File Name: HomeStation.cs
* Author: Alexander Lam
* DigiPen Email: alexader.lam@digipen.edu
* Course: GAM100
*
* Description: This file controls the visibility of the Home station UI based on
*              the camera's current focus. It activates the Home station only
*              when the camera is set to the Home area.
*******************************************************************************/

using UnityEngine;
using static UnityEditor.SceneView;

public class HomeStation : MonoBehaviour
{
    [Header("Home Station Object")]
    [SerializeField]
    private GameObject home; // Reference to the Home station GameObject

    [Header("Camera Controller")]
    public camera_move camera_Move; // Reference to the camera movement script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialization logic can be added here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the camera is focused on the Home station and update visibility
        setHomeActive();
    }

    // Activates the Home station UI only if the camera is currently focused on it
    private void setHomeActive()
    {
        home.SetActive(camera_Move.current_game == camera_Move.home);
    }
}
