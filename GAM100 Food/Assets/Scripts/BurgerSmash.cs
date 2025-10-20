using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BurgerSmash : MonoBehaviour
{
    public camera_move camera_Move;

    [Header ("Transfroms")]
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
    private float _movespeed;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject _parentobject;

    [Header("Button")]
    [SerializeField]
    private Button _startbutton;

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
            _parentobject.SetActive (true);
        }
        else
        {
            _parentobject.SetActive(false);
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

        _startbutton.onClick.AddListener(ButtonPress);
    }

    void CheckSuccess()
    {
        // Check if the pointer is withing the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(_safezone, _pointertransform.position, null))
        {
           if(_movespeed == 0f)
            {
                // if move speed is 0 dont reaturn intill it is moving
                return;
            }
           if(_movespeed == 2000f)
            {
                _movespeed = 0f;
                // if move speed is 2000 say its a success
                Debug.Log("Success!");
            }
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    void ButtonPress()
    {
        // Set Move Speed
        _movespeed = 2000f;
    }
}
