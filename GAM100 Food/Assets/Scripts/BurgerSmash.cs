/*******************************************************************************
* File Name: BurgerSmash.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description:
*   This file implements the logic for the Burger Smash minigame. A pointer moves
*   back and forth between two boundary points, and the player must press the
*   spacebar when the pointer enters a designated safe zone. Success is based on
*   timing accuracy and pointer position.
*******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class BurgerSmash : MonoBehaviour
{
    /****************************************************************************
    * Section: Inspector References
    ****************************************************************************/
    [Header("Scripts")]
    public camera_move camera_Move;                 // Reference to camera movement script

    [Header("Transforms")]
    [SerializeField] private Transform _pointa;     // Left boundary of pointer movement
    [SerializeField] private Transform _pointb;     // Right boundary of pointer movement
    [SerializeField] private RectTransform _safezone;        // Safe zone for successful smash
    [SerializeField] private RectTransform _pointertransform; // Moving pointer UI element

    [Header("Movement")]
    [SerializeField] private float _movespeed;      // Pointer movement speed

    [Header("GameObjects")]
    [SerializeField] private GameObject _parentobject; // Parent object for enabling/disabling UI

    [Header("UI Button")]
    [SerializeField] private Button _startbutton;   // Button to start the minigame

    [Header("Game State")]
    public bool BurgerSmashDone;                   // True when the minigame is successfully completed

    /****************************************************************************
    * Section: Internal Variables
    ****************************************************************************/
    private float _direction = 1f;                 // Current movement direction (1 = right, -1 = left)
    private Vector3 _targetposition;               // Current target position for pointer movement

    /****************************************************************************
    * Function: Start
    *
    * Description:
    *   Initializes the pointer's first movement target. Called before the first
    *   frame update.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Start()
    {
        _targetposition = _pointb.position; // Begin by moving toward point B
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Called once per frame. Handles pointer movement, direction switching,
    *   input detection, and toggling the minigame UI based on camera position.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Update()
    {
        // Enable or disable the minigame UI depending on camera focus
        _parentobject.SetActive(camera_Move.current_game == camera_Move.smash);

        // Move pointer toward the current target
        _pointertransform.position =
            Vector3.MoveTowards(_pointertransform.position, _targetposition, _movespeed * Time.deltaTime);

        // Reverse direction when reaching the left boundary
        if (Vector3.Distance(_pointertransform.position, _pointa.position) < 0.1f)
        {
            _targetposition = _pointb.position;
            _direction = 1f;
        }
        // Reverse direction when reaching the right boundary
        else if (Vector3.Distance(_pointertransform.position, _pointb.position) < 0.1f)
        {
            _targetposition = _pointa.position;
            _direction = -1f;
        }

        // Check for spacebar input to attempt a smash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }

        // Register button click (note: ideally should be moved to Start)
        _startbutton.onClick.AddListener(ButtonPress);
    }

    /****************************************************************************
    * Function: CheckSuccess
    *
    * Description:
    *   Determines whether the pointer is inside the safe zone when the player
    *   presses the spacebar. If successful, the game ends.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void CheckSuccess()
    {
        // Ignore input if pointer is not moving
        if (_movespeed == 0f)
        {
            return;
        }

        // Check if pointer is inside the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(_safezone, _pointertransform.position, null))
        {
            // Only count success if the game is actively running
            if (_movespeed == 2000f)
            {
                _movespeed = 0f;
                Debug.Log("Success!");
                BurgerSmashDone = true;
            }
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    /****************************************************************************
    * Function: ButtonPress
    *
    * Description:
    *   Starts the minigame by setting the pointer's movement speed.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void ButtonPress()
    {
        _movespeed = 2000f; // Begin pointer movement
    }
}
