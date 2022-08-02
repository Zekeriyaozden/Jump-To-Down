using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainChar;
    public bool jumperBlue;
    void Start()
    {
        jumperBlue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumperBlue)
        {
            mainChar.transform.Translate(new Vector3(0,-1f,0) * Time.deltaTime);
        }

    }
}
