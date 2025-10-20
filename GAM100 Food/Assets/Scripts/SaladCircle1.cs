using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle1 : MonoBehaviour
{
    [Header("Scripts")]
    public SaladCircle _saladcircle;

    [Header("Bools")]
    public bool _second = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(_saladcircle == true)
        {
            _second = true;
        }
    }
}
