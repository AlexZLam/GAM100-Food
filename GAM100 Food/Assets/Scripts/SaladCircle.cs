using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle : MonoBehaviour
{
    [Header("Bools")]
    public bool _start = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            _start = true;
        }
    }
}
