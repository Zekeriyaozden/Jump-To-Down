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
    private GameObject gm;
    public bool isGamePlay;
    void Start()
    {
        gm = GameObject.Find("GameManager");
        isSwipping = false;
        isGamePlay = true;
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

        if (gm.GetComponent<GameManager>().isGameOver)
        {
            isGamePlay = false;
        }
        
        if (isGamePlay)
        {
            inGameInputs();   
        }
    }
}
