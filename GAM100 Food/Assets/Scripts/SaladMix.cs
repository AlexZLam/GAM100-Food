using UnityEngine;
using static UnityEditor.SceneView;

public class SaladMix : MonoBehaviour
{
    public camera_move camera_Move;

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

    private void Update()
    {
        if (camera_Move.current_game == camera_Move.salad)
        {
            _parentobject.SetActive(true);
        }
        else
        {
            _parentobject.SetActive(false);
        }

        _screenposition = Input.mousePosition;
        _screenposition.z = Camera.main.nearClipPlane + 1;

        _worldposition = Camera.main.ScreenToWorldPoint(_screenposition);

        transform.position = _worldposition;
    }
}
