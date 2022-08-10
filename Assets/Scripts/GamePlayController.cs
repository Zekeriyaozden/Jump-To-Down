using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    private bool flag;
    public GameObject gObj;
    private int UILastIndex;
    private int UITargetIndex;
    void Start()
    {
        flag = true;
    }

    public void incramentUI()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UILastIndex < UITargetIndex)
        {
            
        }
        if (flag && Input.GetMouseButtonDown(0))
        {
            flag = false;
            gObj.SetActive(false);
            gameObject.GetComponent<GameManager>().flag = true;
            gameObject.GetComponent<GamePlayController>().enabled = false;
        }
        
    }
}
