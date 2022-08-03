using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StickController : MonoBehaviour
{
    public GameObject mainChar;
    public GameObject gm;
    public float k;
    private bool flag;
    private float speed;
    private float speedOfHoriz;
    private float high;
    private float direction;
    private float progress;
    private float horizDir;
    public bool isYellow;
    public bool isBlue;
    public bool isPurple;
    // 0-purple  1-blue 2-yellow
    void Start()
    {
        flag = true;
        gm = GameObject.Find("GameManager");
        k = 0;
        horizDir = 0;
        high = gm.GetComponent<GameManager>().high;
        speed = gm.GetComponent<GameManager>().speed;
        speedOfHoriz = gm.GetComponent<GameManager>().speedOfHoriz;
        progress = high / speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (gm.GetComponent<GameManager>().jumperPurple && isPurple)
        {
            

            direction = Mathf.Lerp(1, -1, k);
            if (k < 1)
            {
                k += Time.deltaTime/progress;
            }
            

            if (k > .5f)
            {
                gm.GetComponent<GameManager>().charFall();
            }
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz);
        }
        
        //------------------------------------
        
        if (gm.GetComponent<GameManager>().jumperYellow && isYellow)
        {
            //215-145

            direction = Mathf.Lerp(1, -1, k);
            if (k < 1)
            {
                k += Time.deltaTime/progress;
            }
            if (k > .5f)
            {
                gm.GetComponent<GameManager>().charFall();
            }
            
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz);
        }
        
        //---------------------------------------------
        
        if (gm.GetComponent<GameManager>().jumperBlue && isBlue)
        {
            //215-145

            direction = Mathf.Lerp(1, -1, k);
            if (k < 1)
            {
                k += Time.deltaTime/progress;
            }
            if (k > .5f)
            {
                gm.GetComponent<GameManager>().charFall();
            }
            
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        k = 0;
        horizDir = 0f;
        gm.GetComponent<GameManager>().jumperYellow = false;
        gm.GetComponent<GameManager>().jumperBlue = false;
        gm.GetComponent<GameManager>().jumperPurple = false;
        if (other.tag == "Player" && gameObject.tag == "purple")
        {
            
            if (gm.GetComponent<GameManager>().currentColor != 0)
            {
                Time.timeScale = 0f;
                gm.GetComponent<GameManager>().HapticFail();
                gm.GetComponent<GameManager>().isGameOver = true;
            }else
            {
                gm.GetComponent<GameManager>().charJump();
                gm.GetComponent<GameManager>().HapticJump();
                gm.GetComponent<GameManager>().skor++;
            }
            
            int selectMaterial;
            do
            {
                selectMaterial = Random.Range(0,3);
            } while (selectMaterial == 0);

            Material[] s = new Material[1];
            s[0] = gm.GetComponent<GameManager>().materials[selectMaterial];
            mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = s;
            gm.GetComponent<GameManager>().currentColor = selectMaterial;
            
            
            gm.GetComponent<GameManager>().jumperPurple = true;
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
        //---------------------------------------------------------
        if (other.tag == "Player" && gameObject.tag == "blue")
        {
            
            if (gm.GetComponent<GameManager>().currentColor != 1)
            {
                Time.timeScale = 0f;
                gm.GetComponent<GameManager>().HapticFail();
                gm.GetComponent<GameManager>().isGameOver = true;
            }else
            {
                gm.GetComponent<GameManager>().charJump();
                gm.GetComponent<GameManager>().HapticJump();
                gm.GetComponent<GameManager>().skor++;
            }
            
            int selectMaterial;
            do
            {
                selectMaterial = Random.Range(0,3);
            } while (selectMaterial == 1);

            Material[] s = new Material[1];
            s[0] = gm.GetComponent<GameManager>().materials[selectMaterial];
            mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = s;
            gm.GetComponent<GameManager>().currentColor = selectMaterial;
            
            //25-95
            gm.GetComponent<GameManager>().jumperBlue = true;
            
            float eulerZ = transform.parent.transform.eulerAngles.z;
            if (eulerZ >= 95f)
            {
                horizDir = -1f;
            }else if (eulerZ <= 25f)
            {
                horizDir = 1f;
            }
            else
            {
                float rate = (2f) / (95f - 25f);
                horizDir = ((95f - eulerZ) * rate) - 1;
            }
        }
        //---------------------------------------------------------
        if (other.tag == "Player" && gameObject.tag == "yellow")
        {
            if (gm.GetComponent<GameManager>().currentColor != 2)
            {
                Time.timeScale = 0f;
                gm.GetComponent<GameManager>().HapticFail();
                gm.GetComponent<GameManager>().isGameOver = true;
            }
            else
            {
                gm.GetComponent<GameManager>().charJump();
                gm.GetComponent<GameManager>().HapticJump();
                gm.GetComponent<GameManager>().skor++;
            }
            int selectMaterial;
            do
            {
                selectMaterial = Random.Range(0,3);
            } while (selectMaterial == 2);

            Material[] s = new Material[1];
            s[0] = gm.GetComponent<GameManager>().materials[selectMaterial];
            mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = s;
            gm.GetComponent<GameManager>().currentColor = selectMaterial;
            
            //265-335
            gm.GetComponent<GameManager>().jumperYellow = true;
            
            float eulerZ = transform.parent.transform.eulerAngles.z;
            if (eulerZ >= 335f)
            {
                horizDir = -1f;
            }else if (eulerZ <= 265f)
            {
                horizDir = 1f;
            }
            else
            {
                float rate = (2f) / (335f - 265f);
                horizDir = ((335f - eulerZ) * rate) - 1;
            }
        }
        
        
    }

}
