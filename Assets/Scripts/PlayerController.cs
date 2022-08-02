using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sens;
    private Vector3 firstPosMouse;
    private float mouseDrag;
    private Vector3 firstRotationTriangle;
    private bool isSwipping;
    void Start()
    {
        isSwipping = false;

    }

    private void inGameInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwipping = true;
            firstPosMouse = Input.mousePosition;
            firstRotationTriangle = transform.eulerAngles;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwipping = false;
            firstPosMouse = new Vector3(0, 0, 0);
            firstRotationTriangle = transform.eulerAngles;
        }

        if (isSwipping)
        {
            mouseDrag = Input.mousePosition.x - firstPosMouse.x;
        }
        else
        {
            mouseDrag = 0;
        }

        if (isSwipping)
        {
            transform.eulerAngles = new Vector3(0, 0, firstRotationTriangle.z + (mouseDrag * sens));
        }
        
    }
    
    void Update()
    {
        inGameInputs();
    }
}
