using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Material[] materials;
    public GameObject UIGameOver;
    public GameObject UISkor;
    public int skor;
    public GameObject UIBestScore;
    void Start()
    {
        skor = 0;
        Time.timeScale = 1;
        isGameOver = false;
        flag = true;
        jumperBlue = false;
    }

    private void gameOver()
    {
        if (mainChar.transform.position.x < -5.9f || mainChar.transform.position.x > 5.9 || mainChar.transform.position.y < -12)
        {
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
