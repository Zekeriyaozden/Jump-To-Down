using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public GameObject mainChar;
    public int indexOfStick;
    public Vector3 mainCharPos;
    public GameObject gm;
    public float k;
    private bool flag;
    public float speed;
    public float speedOfHoriz;
    private float direction;
    private float progress;
    private float horizDir;
    void Start()
    {
        flag = true;
        gm = GameObject.Find("GameManager");
        k = 0;
        horizDir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        progress = 30f / speed;
        if (gm.GetComponent<GameManager>().jumperBlue)
        {
            //215-145

            direction = Mathf.Lerp(1, -1, k);
            if (k < 1 && flag)
            {
                k += Time.deltaTime/progress;
            }
            
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        k = 0;
        flag = true;
        Debug.Log("EnterPlayerTrigger");
        if (other.tag == "Player")
        {
            gm.GetComponent<GameManager>().jumperBlue = true;
            
            float eulerZ = transform.parent.transform.eulerAngles.z;
            if (eulerZ >= 215f)
            {
                horizDir = -1f;
            }else if (eulerZ <= 145f)
            {
                horizDir = 1f;
            }
            else
            {
                float rate = (2f) / (215f - 145f);
                horizDir = ((215f - eulerZ) * rate) - 1;
            }
        }
    }


    
}
