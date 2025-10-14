using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public camera_move camera_Move;

    [Header ("Points")]
    [SerializeField]
    private Transform _pointa;
    [SerializeField] 
    private Transform _pointb;
    [SerializeField] 
    private RectTransform _safezone;
    [SerializeField]
    private RectTransform _pointertransform;

    [Header("MoveSpeed")]
    [SerializeField]
    private float _movespeed = 100f;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject parentobject;

    private float _direction = 1f;
    private Vector3 _targetposition;
 

    private void Start()
    {
        _targetposition = _pointb.position;
    }

    private void Update()
    {
        if( camera_Move.current_game == camera_Move.smash)
        {
            parentobject.SetActive (true);
        }
        else
        {
            parentobject.SetActive(false);
        }

            //Move the pointer towards the target position
            _pointertransform.position = Vector3.MoveTowards(_pointertransform.position, _targetposition, _movespeed * Time.deltaTime);

        // Change direction if the pointer reaches on of the points
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

        // Check for input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        // Check if the pointer is withing the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(_safezone, _pointertransform.position, null))
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }
}
