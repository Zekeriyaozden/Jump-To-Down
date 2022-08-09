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
    public GameObject UIGameOver;
    public GameObject UISkor;
    public int skor;
    public GameObject UIBestScore;
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
    // public float screenDevideTo828;
    private int countFillObject;
    public GameObject fillObject;

    void Start()
    {
        //fillObject.transform.GetChild(10).gameObject.transform.position = new Vector3(0, 0, 0);
        countFillObject = 0;
        screenWidth = Screen.width;
        /*screenDevideTo828 = (float) screenWidth / 828f;
        mainChar.transform.localScale *= screenDevideTo828;
        speed *= screenDevideTo828;
        speedOfHoriz *= screenDevideTo828;
        charts.transform.localScale *= screenDevideTo828;
        high *= screenDevideTo828;*/
        horizDir = 0;
        particleFlag = true;
        audioSrc = GetComponent<AudioSource>();
        Application.targetFrameRate = 240;
        cloudFlag = false;
        hapticFailFlag = true;
        skor = 0;
        Time.timeScale = 1;
        isGameOver = false;
        flag = false;
        jumperBlue = false;
        isHapticOn = true;
        isSoundOn = true;
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
            return false;
        }
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
        Debug.Log(countFillObject);
        if (1 < materialList.Count)
        {
            Material[] ms = new Material[1];
            ms[0] = shaderMaterialList[countFillObject];
            fillObject.transform.GetChild(countFillObject).gameObject.GetComponent<MeshRenderer>().materials = ms;
            StartCoroutine(shaderCor(shaderMaterialList[countFillObject]));
            countFillObject++;
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
            GameObject partic = Instantiate(FailparticleObj);
            partic.transform.position = mainChar.transform.position;
            partic.GetComponent<ParticleSystemRenderer>().material = materials[material];
            StartCoroutine(particleDestroyer(partic));
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
        UIGameOver.SetActive(true);
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
        

        if (isGameOver && Input.GetMouseButtonDown(0))
        {
                tryAgain();
        }
        
        if (cloudFlag)
        {
            clouds.transform.Translate(new Vector3(0,-1,0) * Time.deltaTime * cloudSpeed);
        }
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
