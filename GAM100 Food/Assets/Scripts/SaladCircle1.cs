/*******************************************************************************
* File Name: SaladCircle1.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: Checks previous section to see if it had been passed over
*******************************************************************************/
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
        else if (_saladcircle._start == false)
        {
            _second = false;
        }
    }
}
