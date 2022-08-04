using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
public class GameManager : MonoBehaviour
{
    public GameObject mainChar;
    public bool jumperBlue;
    public bool jumperYellow;
    public bool jumperPurple;
    private bool flag;
    public int currentColor;
    public bool isGameOver;
    // 0-purple  1-blue 2-yellow
    public float speed;
    public float speedOfHoriz;
    public float high;
    public Material[] materials;
    public GameObject UIGameOver;
    public GameObject UISkor;
    public int skor;
    public GameObject UIBestScore;
    public GameObject particleObj;
    private bool hapticFailFlag;
    void Start()
    {
        hapticFailFlag = true;
        skor = 0;
        Time.timeScale = 1;
        isGameOver = false;
        flag = true;
        jumperBlue = false;
    }

    public void charFall()
    {
        mainChar.GetComponent<Animator>().SetBool("Fall",true);
        mainChar.GetComponent<Animator>().SetBool("Jumping",false);
        mainChar.GetComponent<Animator>().SetBool("Press",false);
    }

    public void charJump()
    {
        mainChar.GetComponent<Animator>().SetBool("Fall",false);
        mainChar.GetComponent<Animator>().SetBool("Jumping",true);
        mainChar.GetComponent<Animator>().SetBool("Press",true);
    }

    public void charPress()
    {
        mainChar.GetComponent<Animator>().SetBool("Fall",true);
        mainChar.GetComponent<Animator>().SetBool("Jumping",false);
        mainChar.GetComponent<Animator>().SetBool("Press",false);
    }

    public void hitParticle(int material)
    {
        GameObject partic = Instantiate(particleObj);
        partic.transform.position = mainChar.transform.position;
        partic.GetComponent<ParticleSystemRenderer>().material = materials[material];
    }
    
    public void HapticJump()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
    }

    public void HapticFail()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
    }

    private void gameOver()
    {
        if (mainChar.transform.position.x < -5.9f || mainChar.transform.position.x > 5.9 || mainChar.transform.position.y < -12)
        {
            if (hapticFailFlag)
            {
                HapticFail();
                hapticFailFlag = false;
            }
            Time.timeScale = 0;
            UIGameOver.SetActive(true);
            isGameOver = true;
        }
    }

    public void tryAgain()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs.SetInt("BestScore",0);
        if (skor > PlayerPrefs.GetInt("BestScore",0))
        {
            PlayerPrefs.SetInt("BestScore",skor);
        }
        UIBestScore.GetComponent<Text>().text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        UISkor.GetComponent<Text>().text = skor.ToString();
        gameOver();
        if (isGameOver)
        {
            UIGameOver.SetActive(true);
        }
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
