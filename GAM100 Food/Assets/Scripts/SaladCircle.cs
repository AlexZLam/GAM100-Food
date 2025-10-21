using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle : MonoBehaviour
{
    [Header("Scripts")]
    public SaladMix _saladmix;

    [Header("Bools")]
    public bool _start = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            _start = true;
            _saladmix._saladmix_fail_loop = 0;
        }
    }
}
