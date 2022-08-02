using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainChar;
    public bool jumperBlue;
    public bool jumperYellow;
    public bool jumperPurple;
    private bool flag;
    public int currentColor;
    // 0-purple  1-blue 2-yellow
    public Material[] materials;
    void Start()
    {
        flag = true;
        jumperBlue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumperBlue && flag)
        {
            mainChar.transform.Translate(new Vector3(0,-1f,0) * Time.deltaTime * 6f);
        }

        if (jumperBlue || jumperPurple || jumperYellow)
        {
            flag = false;
        }

    }
}
