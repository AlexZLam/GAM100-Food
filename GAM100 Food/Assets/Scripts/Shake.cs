/*******************************************************************************
* File Name: Shake.cs
* Author: Bishep Clous
* DigiPen Email: bishep.clous@digipen.edu
* Course: GAM100
*
* Description: Shakes gameobject when touching mouse
*******************************************************************************/
using UnityEngine;

public class ShakeOnHover : MonoBehaviour
{
    public float shakeAmount = 0.1f;   // How far left/right it moves
    public float shakeSpeed = 10f;     // How fast it shakes

    private bool isHovering = false;
    private Vector3 originalLocalPosition;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
    }

    void OnMouseEnter()
    {
        isHovering = true;
        Debug.Log("Mouse entered " + gameObject.name);
    }

    void OnMouseExit()
    {
        isHovering = false;
        transform.localPosition = originalLocalPosition;
        Debug.Log("Mouse exited " + gameObject.name);
    }

    void Update()
    {
        if (isHovering)
        {
            float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            transform.localPosition = originalLocalPosition + new Vector3(offset, 0f, 0f);
        }
    }
}
