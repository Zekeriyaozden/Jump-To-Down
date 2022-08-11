using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
public class GameManager : MonoBehaviour
{
    public List<int> materialList;
    public List<Material> shaderMaterialList;
    public GameObject charts;
    public bool isSoundOn;
    public bool isHapticOn;
    public GameObject mainChar;
    public bool jumperBlue;
    public bool jumperYellow;
    public bool jumperPurple;
    public bool flag;
    public int currentColor;
    public bool isGameOver;
    // 0-purple  1-blue 2-yellow
    public float speed;
    public float speedOfHoriz;
    public float high;
    public Material[] materials;
    //public GameObject UIGameOver;
    public GameObject particleObj;
    public GameObject FailparticleObj;
    private bool hapticFailFlag;
    private bool cloudFlag;
    public GameObject clouds;
    public float cloudSpeed;
    public float cloudDuration;
    public AudioSource audioSrc;
    public GameObject CanvasSkor;
    private bool particleFlag;
    public float horizDir;
    public int screenWidth;
    public GameObject[] gObjSticks;
    //public float screenDevideTo828;
    private int countFillObject;
    public GameObject fillObject;
    //UI
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;
    public GameObject hapticOnBtn;
    public GameObject hapticOffBtn;
    //
    public GameObject filledObject;
    private Slider slider;
    private float sliderMaxValue;
    private float sliderValue;
    public List<GameObject> stars;
    private bool chartsBool;
    public bool stickBool;
    void Start()
    {
        int temp = 0;
        PlayerPrefs.SetInt("Level",level);
        if (level == 0)
        {
            temp = PlayerPrefs.GetInt("Level", 0);
            if (temp != 0)
            {
                SceneManager.LoadScene(temp);
            }
        }
        screenWidth = Screen.width;
        stickBool = true;
        chartsBool = true;
        slider = gameObject.GetComponent<GamePlayController>().slider.GetComponent<Slider>();
        sliderMaxValue = slider.maxValue;
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            soundOnBtn.SetActive(false);
            soundOffBtn.SetActive(true);
            isSoundOn = true;
        }
        else
        {
            soundOnBtn.SetActive(true);
            soundOffBtn.SetActive(false);
            isSoundOn = false;
        }

        if (PlayerPrefs.GetInt("Haptic", 1) == 1)
        {
            hapticOnBtn.SetActive(false);
            hapticOffBtn.SetActive(true);
            isHapticOn = true;
        }
        else
        {
            hapticOnBtn.SetActive(true);
            hapticOffBtn.SetActive(false);
            isHapticOn = false;
        }
        countFillObject = 0;

        horizDir = 0;
        particleFlag = true;
        audioSrc = GetComponent<AudioSource>();
        Application.targetFrameRate = 240;
        cloudFlag = false;
        hapticFailFlag = true;
        Time.timeScale = 1;
        isGameOver = false;
        flag = false;
        jumperBlue = false;
    }
    //-------------------------------------------------------------------------------------
/*
 *
 *             int selectMaterial;
            do
            {
                selectMaterial = Random.Range(0,3);
            } while (selectMaterial == 0);

            Material[] s = new Material[1];
            s[0] = gm.GetComponent<GameManager>().materials[selectMaterial];
            if (!gm.GetComponent<GameManager>().isGameOver)
            {
                mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = s;   
            }
            gm.GetComponent<GameManager>().currentColor = selectMaterial;
 */
//-------------------------------------------------------------
//UI
    public void soundOff()
    {
        isSoundOn = false;
        soundOffBtn.SetActive(false);
        soundOnBtn.SetActive(true);
        PlayerPrefs.SetInt("Sound",0);
    }

    public void soundOn()
    {
        isSoundOn = true;
        soundOffBtn.SetActive(true);
        soundOnBtn.SetActive(false);
        PlayerPrefs.SetInt("Sound",1);
    }

    public void hapticOn()
    {
        isHapticOn = true;
        hapticOnBtn.SetActive(false);
        hapticOffBtn.SetActive(true);
        PlayerPrefs.SetInt("Haptic",1);
    }

    public void hapticOff()
    {
        isHapticOn = false;
        hapticOffBtn.SetActive(false);
        hapticOnBtn.SetActive(true);
        PlayerPrefs.SetInt("Haptic",0);
    }
//-------------------------------------------------------------
    public bool ChangeTheColor()
    {
        if (materialList.Count > 2)
        {
            Material[] s = new Material[1];
            s[0] = materials[materialList[1]];
            if (!isGameOver)
            {
                mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = s;   
            }
            currentColor = materialList[1];
            materialList.RemoveAt(0);
            return true;
        }
        else
        {
            hitParticle(1);
            mainChar.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            charts.SetActive(false);
            StartCoroutine(FilledObject());
            return false;
        }
    }

    private IEnumerator FilledObject()
    {
        Vector3 pos;
        Vector3 scale;
        float k = 0;
        pos = filledObject.transform.position;
        scale = filledObject.transform.localScale;
        for (int i = 0; i <= 200; i++)
        {
            yield return new WaitForSeconds(.4f / 200f);
            k += 1 / 200f;
            filledObject.transform.position = Vector3.Lerp(pos, new Vector3(-0.9f, -25.3f, pos.z),k);
            filledObject.transform.localScale = Vector3.Lerp(scale, new Vector3(9.5f, 9.5f, 9.5f),k);
        }
        StartCoroutine(FilledObject2());
    }
    
    private IEnumerator FilledObject2()
    {
        Vector3 pos;
        Vector3 scale;
        float k = 0;
        pos = filledObject.transform.position;
        scale = filledObject.transform.localScale;
        for (int i = 0; i <= 200; i++)
        {
            yield return new WaitForSeconds(.4f / 200f);
            k += 1 / 200f;
            filledObject.transform.position = Vector3.Lerp(pos, new Vector3(-0.9f, -1f, pos.z),k);
            filledObject.transform.localScale = Vector3.Lerp(scale, new Vector3(4.5f, 4.5f, 4.5f),k);
        }
        sliderControll();
    }
    
    //-------------------------------------------------------------------------------------

    private IEnumerator shaderCor(Material mt)
    {
        float k = 0;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(.6f/100f);
            k += 1f / 100f;
            float s = Mathf.Lerp(-1f, .15f, k);
            mt.SetFloat("_Ring",s);
        }
    }
    
    public void fillTheObject()
    {
        if (1 < materialList.Count)
        {
            gameObject.GetComponent<GamePlayController>().incramentUI();
            int s = fillObject.transform.GetChild(countFillObject).childCount;
            Material[] ms = new Material[s];

            
//            fillObject.transform.GetChild(countFillObject).gameObject.GetComponent<MeshRenderer>().materials = ms;
            for (int i = 0; i < s; i++)
            {
                Material[] mTemp = new Material[1];
                mTemp[0] = shaderMaterialList[countFillObject];
                fillObject.transform.GetChild(countFillObject).GetChild(i).gameObject.GetComponent<MeshRenderer>()
                    .materials = mTemp;
                StartCoroutine(shaderCor(mTemp[0]));
            }

            countFillObject++;
        }
        else
        {
            
        }
    }

    //-------------------------------------------------------------------------------------
    private IEnumerator cloudCor()
    {
        cloudFlag = true;
        yield return new WaitForSeconds(cloudDuration);
        cloudFlag = false;
    }

    public void cloudProgress()
    {
        StartCoroutine(cloudCor());
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
        if (isGameOver)
        {
            GameObject partics = Instantiate(FailparticleObj);
            partics.transform.position = mainChar.transform.position;
            partics.GetComponent<ParticleSystemRenderer>().material = materials[material];
            StartCoroutine(particleDestroyer(partics));
        }
        else
        {
            GameObject partic = Instantiate(particleObj);
            partic.transform.position = mainChar.transform.position;
            partic.GetComponent<ParticleSystemRenderer>().material = materials[material]; 
            StartCoroutine(particleDestroyer(partic));
        }
    }
    
    //------------------------------------------------------------------------------------

    private IEnumerator particleDestroyer(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        Destroy(go.gameObject);
    }

    //------------------------------------------------------------------------------------
    private IEnumerator gmParticle(int material)
    {
        GameObject partics = Instantiate(FailparticleObj);
        partics.transform.position = mainChar.transform.position;
        partics.GetComponent<ParticleSystemRenderer>().material = materials[material];
        StartCoroutine(particleDestroyer(partics));
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.1f);
            /*GameObject partic = Instantiate(FailparticleObj);
            partic.transform.position = mainChar.transform.position;
            partic.GetComponent<ParticleSystemRenderer>().material = materials[material];
            StartCoroutine(particleDestroyer(partic));*/
        }
    }
    
    public void hitParticleGM(int material)
    {
        if (particleFlag)
        {
            particleFlag = false;
            mainChar.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            StartCoroutine(gmParticle(material));
        }

    }
    
    //------------------------------------------------------------------------------------

    public void HapticSuccess()
    {
        if (isHapticOn)
        {
            MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);   
        }
    }
    public void HapticJump()
    {
        if (isHapticOn)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);   
        }

        if (isSoundOn)
        {
            audioSrc.Play();
        }
    }

    public void HapticFail()
    {
        CanvasSkor.SetActive(false);
        isGameOver = true;
        if (isHapticOn)
        {
            MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
        }

        if (isSoundOn)
        {
            
        }
    }

    private void gameOver()
    {
        if (mainChar.transform.position.y < -12)
        {
            isGameOver = true;
            hitParticleGM(currentColor);
            if (hapticFailFlag)
            {
                HapticFail();
                hapticFailFlag = false;
            }
            else
            {
                CanvasSkor.SetActive(false);
            }
            
            isGameOver = true;
        }
    }

    public void tryAgainBtn()
    {
        SceneManager.LoadScene(level);
        HapticSuccess();
    }
    
    //TODO nextLevel check

    public void nextLevelBtn()
    {
        if (level == 2)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(level+1);
        }
        HapticSuccess();
    }
    
    private void sliderControll()
    {
        if (sliderValue/sliderMaxValue < .5f)
        {
            stars[3].SetActive(true);
            stars[7].SetActive(true);
            stars[9].SetActive(true);
        }
        else if (sliderValue / sliderMaxValue < .75f)
        {
            stars[0].SetActive(true);   
            stars[4].SetActive(true);
            stars[8].SetActive(true);
        }else if (sliderValue / sliderMaxValue < .99f)
        {
            stars[1].SetActive(true);
            stars[5].SetActive(true);
            stars[8].SetActive(true);
        }else
        {
            stars[2].SetActive(true);
            stars[6].SetActive(true);
            stars[8].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = slider.value;
        //deneme = Camera.main.WorldToScreenPoint(mainChar.transform.position);
        float s = Camera.main.WorldToScreenPoint(mainChar.transform.position).x;

        if (s < 11)
        {
            foreach (var stick in gObjSticks)
            {
                if (stick.GetComponent<StickController>().horizDir < 0)
                {
                    stick.GetComponent<StickController>().horizDir = stick.GetComponent<StickController>().horizDir * -1f;
                    horizDir = stick.GetComponent<StickController>().horizDir;
                }
            }   
        }
        if (s > screenWidth - 11)
        {
            foreach (var stick in gObjSticks)
            {
                if (stick.GetComponent<StickController>().horizDir > 0)
                {
                    stick.GetComponent<StickController>().horizDir = stick.GetComponent<StickController>().horizDir * -1f;   
                    horizDir = stick.GetComponent<StickController>().horizDir;
                }
            }
        }
        
        
        if (cloudFlag)
        {
            clouds.transform.Translate(new Vector3(0,-1,0) * Time.deltaTime * cloudSpeed);
        }
        gameOver();
        if (isGameOver && chartsBool)
        {
            charts.SetActive(false);
            StartCoroutine(FilledObject());
            chartsBool = false;
        }
        /*if (isGameOver)
        {
            sliderControll();
        }*/

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
