using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle5 : MonoBehaviour
{
    [Header("Scripts")]
    public SaladCircle _saladcircle;
    public SaladCircle1 _saladcircle1;
    public SaladCircle2 _saladcircle2;
    public SaladCircle3 _saladcircle3;
    public SaladCircle4 _saladcircle4;

    [Header("Bools")]
    public bool _sixth = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(_saladcircle._start == true && _saladcircle1._second == true && _saladcircle2._third == true && _saladcircle3._fourth == true && _saladcircle4._fifth == true)
        {
            _sixth = true;
        }
        else if(_saladcircle._start == false)
        {
            _sixth = false;
        }
    }
}
