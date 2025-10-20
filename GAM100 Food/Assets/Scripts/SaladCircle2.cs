using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle2 : MonoBehaviour
{
    [Header("Scripts")]
    public SaladCircle _saladcircle;
    public SaladCircle1 _saladcircle1;

    [Header("Bools")]
    public bool _third = false;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (_saladcircle._start == true && _saladcircle1._second == true)
        {
            _third = true;
        }
    }
}
