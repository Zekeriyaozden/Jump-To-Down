using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    private bool flag;
    public GameObject gObj;
    private float UILastIndex;
    private float UITargetIndex;
    public float slideSpeed;
    public GameObject slider;
    void Start()
    {
        UILastIndex = 0;
        UITargetIndex = 0;
        flag = true;
    }

    public void incramentUI()
    {
        UITargetIndex++;
    }

    // Update is called once per frame
    void Update()
    {


        if (UILastIndex < UITargetIndex)
        {
            UILastIndex += (slideSpeed * Time.deltaTime);
        }
        slider.GetComponent<Slider>().value = UILastIndex;
        if (flag && Input.GetMouseButtonDown(0))
        {
            flag = false;
            gObj.SetActive(false);
            gameObject.GetComponent<GameManager>().flag = true;
        }
        
    }
}
