using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle : MonoBehaviour
{
    [Header("Bools")]
    private bool _first = false;
    private bool _second = false;
    private bool _third = false;
    private bool _fourth = false;
    private bool _fifth = false;
    private bool _sixth = false;
    public bool _finish = false;

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetMouseButton(0))
        {
            _first = true;
            if (_first == true)
            {
                _second = true;
                if(_second == true)
                {
                    _third = true;
                    if(_third == true)
                    {
                        _fourth = true;
                        if(_fourth == true)
                        {
                            _fifth = true;
                            if(_fifth == true)
                            {
                                _sixth = true;
                                if(_sixth == true)
                                {
                                    _finish = true;
                                    if(_finish == false)
                                    {
                                        _first = false;
                                        _second = false;
                                        _third = false;
                                        _fourth = false;
                                        _fifth = false;
                                        _sixth = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
