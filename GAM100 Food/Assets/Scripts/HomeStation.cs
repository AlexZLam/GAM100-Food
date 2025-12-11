/*******************************************************************************
* File Name: HomeStation.cs
* Author: Alexander Lam
* DigiPen Email: alexader.lam@digipen.edu
* Course: GAM100
*
* Description:
*   This file manages the visibility of the Home station UI. It checks the
*   camera's current focus each frame and activates the Home station only when
*   the camera is positioned at the Home area.
*******************************************************************************/

using UnityEngine;

public class HomeStation : MonoBehaviour
{
    /****************************************************************************
    * Section: Inspector References
    ****************************************************************************/

    [Header("Home Station Object")]
    [SerializeField]
    private GameObject home;   // Reference to the Home station GameObject

    [Header("Camera Controller")]
    public camera_move camera_Move; // Reference to the camera movement script

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Called before the first frame update. Currently unused, but available
    *   for initialization logic if needed in the future.
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
    *   Called once per frame. Checks whether the camera is focused on the Home
    *   station and updates the station's visibility accordingly.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    void Update()
    {
        setHomeActive();
    }

    /****************************************************************************
    * Function: setHomeActive
    *
    * Description:
    *   Activates or deactivates the Home station UI depending on whether the
    *   camera is currently focused on the Home area.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void setHomeActive()
    {
        home.SetActive(camera_Move.current_game == camera_Move.home);
    }
}
