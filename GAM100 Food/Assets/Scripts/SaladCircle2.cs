/*******************************************************************************
* File Name: SaladCircle2.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: Checks previous section to see if it had been passed over
*******************************************************************************/
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
        else if (_saladcircle._start == false)
        {
            _third = false;
        }
    }
}
