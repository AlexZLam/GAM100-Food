/*******************************************************************************
* File Name: SaladCircle4.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: Checks previous section to see if it had been passed over
*******************************************************************************/
using Unity.VisualScripting;
using UnityEngine;

public class SaladCircle4 : MonoBehaviour
{
    [Header("Scirpts")]
    public SaladCircle _saladcircle;
    public SaladCircle1 _saladcircle1;
    public SaladCircle2 _saladcircle2;
    public SaladCircle3 _saladcircle3;

    [Header("Bools")]
    public bool _fifth = false;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (_saladcircle._start == true && _saladcircle1._second == true && _saladcircle2._third == true && _saladcircle3._fourth == true)
        {
            _fifth = true;
        }
        else if (_saladcircle._start == false)
        {
            _fifth = false;
        }
    }
}
