using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.SceneView;

public class SaladMix : MonoBehaviour
{
    [Header("Scripts")]
    public camera_move camera_Move;
    public SaladCircle5 _saladcircle5;
    public SaladCircle _saladcircle;

    [Header("Points")]
    [SerializeField]
    private GameObject _start;
    [SerializeField]
    private GameObject _position1;
    [SerializeField]
    private GameObject _position2;
    [SerializeField]
    private GameObject _position3;
    [SerializeField]
    private GameObject _position4;
    [SerializeField]
    private GameObject _position5;

    [Header("ParentObjects")]
    [SerializeField]
    private GameObject _parentobject;

    [Header("Mouse Cords")]
    [SerializeField]
    private Vector3 _screenposition;
    [SerializeField]
    private Vector3 _worldposition;

    [Header("Prefab")]
    [SerializeField]
    private GameObject _prefab;
    private GameObject _prefab2;

    [Header("Done")]
    public bool _saladmix_done = false;

    [Header("Loop")]
    public int _saladmix_fail_loop = 0;
    private int _saladmix_loops = 0;

    [Header("how many")]
    [SerializeField]
    private int _saladmix_loops_count = 5;
    [SerializeField]
    private int _saladmix_fail_loop_count = 1;


    private void Awake()
    {
        _prefab2 = Instantiate(_prefab, new Vector3(0, 0, 0), Quaternion.identity); // Create a game object
        _prefab2.SetActive(false);
    }

    private void Update()
    {
        // Get the Mouse position and the world mouse position
        _screenposition = Input.mousePosition;
        _screenposition.z = Camera.main.nearClipPlane + 1;
        _worldposition = Camera.main.ScreenToWorldPoint(_screenposition);
        if (camera_Move.current_game == camera_Move.salad)
        {
            _parentobject.SetActive(true); // if the camera is on salad area
            _prefab2.SetActive(true);
        }
        else
        {
            _parentobject.SetActive(false); // if camera is off of salad area
            _prefab2.SetActive(false);
            
        }
        _prefab2.transform.position = _worldposition;

        if (_saladcircle5._sixth == true)
        {
            if(_saladmix_loops == _saladmix_loops_count)
            {
                Debug.Log("Success");
                _saladmix_done = true;
                StopCoroutine(TimeDelay(0f));
                _saladmix_loops = 0;

            }
            _saladcircle._start = false;
            _saladmix_loops += 1;
        }

        if( _saladmix_fail_loop == _saladmix_fail_loop_count || _saladcircle._start == false)
        {
            StopCoroutine(TimeDelay(0f));
        }
        else
        {
            if(camera_Move.current_game == camera_Move.salad && _saladcircle._start == true)
            StartCoroutine(TimeDelay(3f));
        }

    }

    IEnumerator TimeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time();

    }

    private void Time()
    {
        Debug.Log("Failed");
        _saladcircle._start = false;
        _saladmix_fail_loop = 1;
    }
}
