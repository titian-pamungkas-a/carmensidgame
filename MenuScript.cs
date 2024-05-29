using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    private GameObject mainManager, buttonObject;
    private MainManager script;
    public GameObject menu1, mode1, buttonPrefabs, gameMode, tutor1, changeScene1;
    private List<FixedCity> fixes;
    private TextMeshProUGUI buttonText;
    private RectTransform rectTransform;
    private Button buttonAct;
    public AudioSource buttonSound;
    private int tutorPage;
    public GameObject[] tutorObjects;
    public Button leftTutorButton, rightTutorButton;
    public Transform changeScene2;
    
    // Start is called before the first frame update
    void Start()
    {
        changeScene1.SetActive(false);
        mainManager = GameObject.Find("MainManager");
        menu1 = GameObject.Find("menu1");
        mode1 = GameObject.Find("mode1");
        gameMode = GameObject.Find("Play");
        script = mainManager.GetComponent<MainManager>();
        fixes = new List<FixedCity>(script.getFixedCities());
        for (int i = 0; i < fixes.Count; i++)
        {
            createLevelButtton(new Vector3(-200f + ((i%4)*125f), 100f - (Mathf.Floor(i/4)*125), 0f), fixes[i].Id.ToString(), fixes[i]);
        }
        mode1.SetActive(false);
        gameMode.SetActive(false);
        tutor1.SetActive(false);
        tutorPage = 0;
        for (int i = 0; i < tutorObjects.Length; i++)
        {
            tutorObjects[i].SetActive(false);
        }
        if (script.GetActiveTrans() == 1)
        {
            StartCoroutine(changeInTrans());
        }
    }

    void createLevelButtton(Vector3 pos, string str, FixedCity fix)
    {
        buttonObject = Object.Instantiate(buttonPrefabs, Vector3.zero, Quaternion.identity);
        buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = buttonObject.GetComponent<RectTransform>();
        buttonAct = buttonObject.GetComponent<Button>();
        rectTransform.SetParent(mode1.transform);
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localPosition = pos;
        buttonText.text = str;
        buttonAct.onClick.AddListener(delegate { TaskOnClick(fix); });
    }

    private void TaskOnClick(FixedCity fix)
    {
        buttonSound.Play();
        script.setCurrentLevel(0);
        script.setFixedLevel(fix.Id-1);
        StartCoroutine(changeOutTrans());
        //SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        gameMode.SetActive(true);
        menu1.SetActive(false);
        buttonSound.Play();
    }

    public void randomMode()
    {
        buttonSound.Play();
        script.setCurrentLevel(1);
        StartCoroutine(changeOutTrans());
        //SceneManager.LoadScene("Game");
    }

    IEnumerator changeOutTrans()
    {
        changeScene1.SetActive(true);
        CanvasGroup cg = changeScene1.GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        changeScene2.localPosition = Vector2.zero;
        cg.LeanAlpha(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        changeScene2.LeanScale(Vector2.one, 0.5f);
        yield return new WaitForSeconds(0.5f);
        script.SetActiveTrans(1);
        SceneManager.LoadScene("Game");
    }

    IEnumerator changeInTrans()
    {
        changeScene1.SetActive(true);
        CanvasGroup cg = changeScene1.GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        changeScene2.localPosition = Vector2.one;
        changeScene2.LeanScale(Vector2.zero, 0.5f); 
        yield return new WaitForSeconds(0.5f);
        cg.LeanAlpha(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        changeScene1.SetActive(false);
    }

    public void backFromChoosing()
    {
        gameMode.SetActive(true);
        mode1.SetActive(false);
        buttonSound.Play();
    }

    public void backFromModeing()
    {
        gameMode.SetActive(false);
        menu1.SetActive(true);
        buttonSound.Play();
    }

    public void backFromTutoring()
    {

        tutor1.SetActive(false);
        menu1.SetActive(true);
        buttonSound.Play();
        tutorObjects[tutorPage].SetActive(false);
    }

    public void fixedMode()
    {
        gameMode.SetActive(false);
        mode1.SetActive(true);
        buttonSound.Play();
    }

    public void learn()
    {
        buttonSound.Play();
        script.SetActiveTrans(0);
        SceneManager.LoadScene("Learn");
    }

    public void tutor()
    {
        menu1.SetActive(false);
        tutor1.SetActive(true);
        tutorPage = 0;
        tutorObjects[tutorPage].SetActive(true);
        leftTutorButton.enabled = false;
        rightTutorButton.enabled = true;
        Color tempc = rightTutorButton.image.color;
        tempc.a = 0.2f;
        leftTutorButton.image.color = tempc;
        tempc.a = 1f;
        rightTutorButton.image.color = tempc;
        buttonSound.Play();
    }
    public void leftTutor()
    {
        tutorObjects[tutorPage].SetActive(false);
        tutorPage -= 1;
        tutorObjects[tutorPage].SetActive(true);
        checkRightLeftButton();
    }

    public void rightTutor()
    {
        tutorObjects[tutorPage].SetActive(false);
        tutorPage += 1;
        tutorObjects[tutorPage].SetActive(true);
        checkRightLeftButton();
    }

    private void checkRightLeftButton()
    {
        buttonSound.Play();
        if (tutorPage == 0)
        {
            leftTutorButton.enabled = false;
            Color tempc = leftTutorButton.image.color;
            tempc.a = 0.2f;
            leftTutorButton.image.color = tempc;
        }
        else
        {
            leftTutorButton.enabled = true;
            Color tempc = leftTutorButton.image.color;
            tempc.a = 1f;
            leftTutorButton.image.color = tempc;
        }   
        if (tutorPage == tutorObjects.Length - 1)
        {
            rightTutorButton.enabled = false;
            Color tempc = rightTutorButton.image.color;
            tempc.a = 0.2f;
            rightTutorButton.image.color = tempc;
        }
        else
        {
            rightTutorButton.enabled = true;
            Color tempc = rightTutorButton.image.color;
            tempc.a = 1f;
            rightTutorButton.image.color = tempc;
        }
            
    }

}
