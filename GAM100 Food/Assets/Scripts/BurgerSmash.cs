/*******************************************************************************
* File Name: BurgerSmash.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: This file contains logic for the Burger Smash minigame.
* The player must press the spacebar when a moving pointer enters a safe zone.
* Success is determined by timing and pointer position.
*******************************************************************************/

using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BurgerSmash : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move;

    [Header("Transforms")]
    [SerializeField] private Transform _pointa;              // Left boundary of pointer movement
    [SerializeField] private Transform _pointb;              // Right boundary of pointer movement
    [SerializeField] private RectTransform _safezone;        // Area where pointer must be when space is pressed
    [SerializeField] private RectTransform _pointertransform;// Pointer that moves between point A and B

    [Header("Movement")]
    [SerializeField] private float _movespeed;               // Speed of pointer movement

    [Header("GameObjects")]
    [SerializeField] private GameObject _parentobject;       // Parent object to show/hide based on camera

    [Header("UI Button")]
    [SerializeField] private Button _startbutton;            // Button to start the game

    [Header("Game State")]
    public bool BurgerSmashDone;                             // Flag to indicate game completion

    private float _direction = 1f;                           // Direction of pointer movement
    private Vector3 _targetposition;                         // Current target position for pointer

    // Called once before the first frame update
    private void Start()
    {
        // Set initial target to point B
        _targetposition = _pointb.position;
    }

    // Called once per frame
    private void Update()
    {
        // Show or hide the game based on camera position
        if (camera_Move.current_game == camera_Move.smash)
        {
            _parentobject.SetActive(true);
        }
        else
        {
            _parentobject.SetActive(false);
        }

        // Move the pointer toward the current target position
        _pointertransform.position = Vector3.MoveTowards(_pointertransform.position, _targetposition, _movespeed * Time.deltaTime);

        // Reverse direction when reaching either boundary
        if (Vector3.Distance(_pointertransform.position, _pointa.position) < 0.1f)
        {
            _targetposition = _pointb.position;
            _direction = 1f;
        }
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

        // Register button click to start the game
        _startbutton.onClick.AddListener(ButtonPress);
    }

    // Called when spacebar is pressed to check if pointer is in safe zone
    void CheckSuccess()
    {
        // Only check if pointer is moving
        if (_movespeed == 0f)
        {
            return;
        }

        // Check if pointer is inside the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(_safezone, _pointertransform.position, null))
        {
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

    // Called when start button is pressed to begin the game
    void ButtonPress()
    {
        // Set pointer speed to begin movement
        _movespeed = 2000f;
    }
}
