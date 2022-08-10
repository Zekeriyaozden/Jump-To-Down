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
    public float horizDir;
    private float horizDirFlt;
    public bool isYellow;
    public bool isBlue;
    public bool isPurple;
    private float charAngle;
    // 0-purple  1-blue 2-yellow
    void Start()
    {
        charAngle = 0;
        flag = true;
        gm = GameObject.Find("GameManager");
        k = 0;
        horizDir = 0;
        high = gm.GetComponent<GameManager>().high;
        speed = gm.GetComponent<GameManager>().speed;
        progress = high / speed;
        speedOfHoriz = gm.GetComponent<GameManager>().speedOfHoriz;
    }

    // Update is called once per frame
    void Update()
    {
        high = gm.GetComponent<GameManager>().high;
        speed = gm.GetComponent<GameManager>().speed;
        progress = high / speed;
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

            float ang = Mathf.Lerp(charAngle, 0, k);
            mainChar.transform.eulerAngles = new Vector3(0,0,ang);
            
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed , Space.World);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz , Space.World);
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
            
            float ang = Mathf.Lerp(charAngle, 0, k);
            mainChar.transform.eulerAngles = new Vector3(0,0,ang);
            
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed , Space.World);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz , Space.World);
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
            
            float ang = Mathf.Lerp(charAngle, 0, k);
            mainChar.transform.eulerAngles = new Vector3(0,0,ang);
            
            
            mainChar.transform.Translate(new Vector3(0,direction,0) * Time.deltaTime * speed , Space.World);
            mainChar.transform.Translate(new Vector3(horizDir,0,0) * Time.deltaTime * speedOfHoriz , Space.World);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        k = 0;
        horizDir = 0f;
        horizDirFlt = gm.GetComponent<GameManager>().horizDir;
        gm.GetComponent<GameManager>().jumperYellow = false;
        gm.GetComponent<GameManager>().jumperBlue = false;
        gm.GetComponent<GameManager>().jumperPurple = false;
        if (other.tag == "Player" && gameObject.tag == "purple")
        {

            
            if (gm.GetComponent<GameManager>().currentColor != 0)
            {
                gm.GetComponent<GameManager>().isGameOver = true;
                gm.GetComponent<GameManager>().HapticFail();
                gm.GetComponent<GameManager>().hitParticle(gm.GetComponent<GameManager>().currentColor);
                mainChar.GetComponent<BoxCollider>().enabled = false;
                mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                //gm.GetComponent<GameManager>().isGameOver = true;
            }else
            {
                gm.GetComponent<GameManager>().hitParticle(0);
                gm.GetComponent<GameManager>().speed += gm.GetComponent<GameManager>().speed * (2f / 100f);
                gm.GetComponent<GameManager>().charJump();
                gm.GetComponent<GameManager>().HapticJump();
                gm.GetComponent<GameManager>().ChangeTheColor();
                gm.GetComponent<GameManager>().fillTheObject();
                gm.GetComponent<GameManager>().cloudProgress();
            }


            
            
            gm.GetComponent<GameManager>().jumperPurple = true;
            float eulerZ = transform.eulerAngles.z;
            if (eulerZ > 270)
            {
                eulerZ = eulerZ - 360f;
            }
            if (eulerZ >= 90f)
            {
                horizDir = -1f;
            }else if (eulerZ <= -90f)
            {
                horizDir = 1f;
            }
            else
            {
                float rate = (2f) / (90f - -90f);
                horizDir = ((90f - eulerZ) * rate) - 1;
            }
        }
        //---------------------------------------------------------
        if (other.tag == "Player" && gameObject.tag == "blue" )
        {

            if (gm.GetComponent<GameManager>().currentColor != 1)
            {
                gm.GetComponent<GameManager>().isGameOver = true;
                gm.GetComponent<GameManager>().HapticFail();
                gm.GetComponent<GameManager>().hitParticle(gm.GetComponent<GameManager>().currentColor);
                mainChar.GetComponent<BoxCollider>().enabled = false;
                mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                //gm.GetComponent<GameManager>().isGameOver = true;
            }else
            {
                gm.GetComponent<GameManager>().hitParticle(1);
                gm.GetComponent<GameManager>().speed += gm.GetComponent<GameManager>().speed * (2f / 100f);
                gm.GetComponent<GameManager>().charJump();
                gm.GetComponent<GameManager>().HapticJump();
                gm.GetComponent<GameManager>().ChangeTheColor();
                gm.GetComponent<GameManager>().fillTheObject();
                gm.GetComponent<GameManager>().cloudProgress();
            }
            

            
            //25-95
            gm.GetComponent<GameManager>().jumperBlue = true;
            
            float eulerZ = transform.eulerAngles.z;
            if (eulerZ > 270)
            {
                eulerZ = eulerZ - 360f;
            }
            if (eulerZ >= 90f)
            {
                horizDir = -1f;
            }else if (eulerZ <= -90f)
            {
                horizDir = 1f;
            }
            else
            {
                float rate = (2f) / (90f - -90f);
                horizDir = ((90f - eulerZ) * rate) - 1;
            }

        }
        //---------------------------------------------------------
        if (other.tag == "Player" && gameObject.tag == "yellow" )
        {

            if (gm.GetComponent<GameManager>().currentColor != 2)
            {
                gm.GetComponent<GameManager>().isGameOver = true;
                gm.GetComponent<GameManager>().HapticFail();
                gm.GetComponent<GameManager>().hitParticle(gm.GetComponent<GameManager>().currentColor);
                mainChar.GetComponent<BoxCollider>().enabled = false;
                mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
            else
            {
                gm.GetComponent<GameManager>().hitParticle(2);
                gm.GetComponent<GameManager>().speed += gm.GetComponent<GameManager>().speed * (2f / 100f);
                gm.GetComponent<GameManager>().charJump();
                gm.GetComponent<GameManager>().HapticJump();
                gm.GetComponent<GameManager>().ChangeTheColor();
                gm.GetComponent<GameManager>().fillTheObject();
                gm.GetComponent<GameManager>().cloudProgress();
            }
            

            
            //265-335
            gm.GetComponent<GameManager>().jumperYellow = true;
            
            float eulerZ = transform.eulerAngles.z;
            if (eulerZ > 270)
            {
                eulerZ = eulerZ - 360f;
            }
            if (eulerZ >= 90f)
            {
                horizDir = -1f;
            }else if (eulerZ <= -90f)
            {
                horizDir = 1f;
            }
            else
            {
                float rate = (2f) / (90f - -90f);
                horizDir = ((90f - eulerZ) * rate) - 1;
            }

        }

        charAngle = horizDir * 35f * -1f;
        mainChar.transform.eulerAngles = new Vector3(0, 0, charAngle);
        horizDir = horizDir * 2f;
        horizDir = horizDir + horizDirFlt;
        gm.GetComponent<GameManager>().horizDir = horizDir;

    }

}
