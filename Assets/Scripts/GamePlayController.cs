using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    private bool flag;
    public GameObject gObj;
    void Start()
    {
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag && Input.GetMouseButtonDown(0))
        {
            flag = false;
            gObj.SetActive(false);
            gameObject.GetComponent<GameManager>().flag = true;
            gameObject.GetComponent<GamePlayController>().enabled = false;
        }
        
    }
}
