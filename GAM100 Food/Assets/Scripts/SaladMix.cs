/*******************************************************************************
* File Name: SaladMix.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description:
*   This file implements the salad mixing minigame. The player must move the
*   mouse through a sequence of circular checkpoints. Completing all loops
*   results in success, while stopping early or failing a loop results in a loss.
*******************************************************************************/

using System.Collections;
using UnityEngine;

public class SaladMix : MonoBehaviour
{
    /****************************************************************************
    * Section: Script References
    ****************************************************************************/
    [Header("Scripts")]
    public camera_move camera_Move;      // Reference to camera movement script
    public SaladCircle5 _saladcircle5;   // Reference to final circle trigger
    public SaladCircle _saladcircle;     // Reference to initial circle trigger

    /****************************************************************************
    * Section: Checkpoint Objects
    ****************************************************************************/
    [Header("Points")]
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _position1;
    [SerializeField] private GameObject _position2;
    [SerializeField] private GameObject _position3;
    [SerializeField] private GameObject _position4;
    [SerializeField] private GameObject _position5;

    /****************************************************************************
    * Section: Parent Object
    ****************************************************************************/
    [Header("ParentObjects")]
    [SerializeField] private GameObject _parentobject; // UI container for minigame

    /****************************************************************************
    * Section: Mouse Tracking
    ****************************************************************************/
    [Header("Mouse Cords")]
    [SerializeField] private Vector3 _screenposition;
    [SerializeField] private Vector3 _worldposition;

    /****************************************************************************
    * Section: Prefab Hitbox
    ****************************************************************************/
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab; // Prefab used as mouse hitbox
    private GameObject _prefab2;                // Instantiated hitbox

    /****************************************************************************
    * Section: Game State
    ****************************************************************************/
    [Header("Done")]
    public bool _saladmix_done = false;         // True when minigame is completed

    [Header("Loop")]
    public int _saladmix_fail_loop = 0;         // Tracks failure loops
    private int _saladmix_loops = 0;            // Tracks successful loops

    [Header("how many")]
    [SerializeField] private int _saladmix_loops_count = 5;       // Required loops to win
    [SerializeField] private int _saladmix_fail_loop_count = 1;   // Fail threshold

    /****************************************************************************
    * Function: Awake
    *
    * Description:
    *   Instantiates a hitbox prefab used to detect mouse collisions with the
    *   salad circles. The hitbox follows the mouse each frame.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Awake()
    {
        _prefab2 = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        _prefab2.SetActive(false);
    }

    /****************************************************************************
    * Function: Update
    *
    * Description:
    *   Handles mouse tracking, minigame activation, loop progression, and
    *   success/failure conditions.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Update()
    {
        // Convert mouse position to world coordinates
        _screenposition = Input.mousePosition;
        _screenposition.z = Camera.main.nearClipPlane + 1;
        _worldposition = Camera.main.ScreenToWorldPoint(_screenposition);

        // Show or hide minigame based on camera focus
        if (camera_Move.current_game == camera_Move.salad)
        {
            _parentobject.SetActive(true);
            _prefab2.SetActive(true);
        }
        else
        {
            _parentobject.SetActive(false);
            _prefab2.SetActive(false);
        }

        // Move hitbox to mouse position
        _prefab2.transform.position = _worldposition;

        // If the final circle is reached
        if (_saladcircle5._sixth == true)
        {
            // Win condition
            if (_saladmix_loops == _saladmix_loops_count)
            {
                Debug.Log("Success");
                _saladmix_done = true;
                StopCoroutine(TimeDelay(0f));
                _saladmix_loops = 0;
            }

            // Reset start flag and increment loop count
            _saladcircle._start = false;
            _saladmix_loops += 1;
        }

        // Failure conditions
        if (_saladmix_fail_loop == _saladmix_fail_loop_count || _saladcircle._start == false)
        {
            StopCoroutine(TimeDelay(0f));
        }
        else
        {
            // Start failure timer if actively mixing
            if (camera_Move.current_game == camera_Move.salad && _saladcircle._start == true)
            {
                StartCoroutine(TimeDelay(3f));
            }
        }
    }

    /****************************************************************************
    * Function: TimeDelay
    *
    * Description:
    *   Waits for a specified delay before triggering a failure event.
    *
    * Inputs:
    *   float delay - Time to wait before calling Time()
    *
    * Outputs: None
    ****************************************************************************/
    IEnumerator TimeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time();
    }

    /****************************************************************************
    * Function: Time
    *
    * Description:
    *   Handles failure logic when the player stops mixing or takes too long.
    *
    * Inputs:  None
    * Outputs: None
    ****************************************************************************/
    private void Time()
    {
        Debug.Log("Failed");
        _saladcircle._start = false;
        _saladmix_fail_loop = 1;
    }
}
