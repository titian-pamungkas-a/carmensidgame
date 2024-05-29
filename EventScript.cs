using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventScript : MonoBehaviour
{
    private bool isText, isPlace, isCity;
    private string con; // showText, showCity, showPlace
    public GameObject textui, placeui, cityui, enemyui, enemyui1, winPanel, losePanel, helpui, helpui1, changeAnim, CloseEvent;
    public CanvasGroup cgEnemy, cgHelp;
    private GameObject blueObject, orangeObject, graphObject, closeObject;
    private orangeScript cityScript;
    private blueScript placeScript;
    private Graph graphScript;
    private HelpScript helpScript;
    private EnemyScript enemyScript;
    private TextMeshProUGUI descText;
    public TextMeshProUGUI descTitleText, descRightPath;
    public Image descBG;
    public CanvasGroup cg, cgRightPath, cgTrans;
    public Transform cloud, cloud1, cloud2, cloud3, plane, trans2;
    public TextMeshProUGUI conButton;
    public Transform carImage;
    public GameObject rightPath1, rightPath2, rightPath, rightPathObj, trans1;
    private ClosingScript closeScript;
    public AudioSource music, planeSound, rightSound, buttonSound, typingSound;
    public Button rightCityButton;

    private void Start()
    {
        changeAnim.SetActive(false);
        con = "showCity";
        blueObject = GameObject.Find("nextPlace");
        orangeObject = GameObject.Find("nextCity");
        graphObject = GameObject.Find("GRAPH");
        closeObject = GameObject.Find("Closing");
        closeScript = closeObject.GetComponent<ClosingScript>();
        placeScript = blueObject.GetComponent<blueScript>();
        cityScript = orangeObject.GetComponent<orangeScript>();
        graphScript = graphObject.GetComponent<Graph>();
        cgTrans = trans1.GetComponent<CanvasGroup>();
        enemyui.SetActive(false);
        helpui.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        rightPath.SetActive(false);
        CloseEvent.SetActive(false);
        StartCoroutine(TransIn());
    }

    IEnumerator TransIn()
    {
        cgTrans.alpha = 1f;
        trans2.localScale = Vector2.one;
        yield return new WaitForSeconds(0.5f);
        trans2.LeanScale(Vector2.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);
        cgTrans.LeanAlpha(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        trans1.SetActive(false);
    }

    IEnumerator TransOut(string str)
    {
        trans1.SetActive(true);
        cgTrans.alpha = 0f;
        trans2.localScale = Vector2.zero;
        cgTrans.LeanAlpha(1f, 0.5f); 
        yield return new WaitForSeconds(0.5f);
        trans2.LeanScale(Vector2.one, 0.5f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(str);
    }

    private void Update()
    {
        if (graphScript.getPlayerState() == 1)
        {
            graphScript.setPlayerState(0);
            CloseEvent.SetActive(true);
            closeScript.Closing(1);

        }else if (graphScript.getPlayerState() == -1)
        {
            graphScript.setPlayerState(0);
            CloseEvent.SetActive(true);
            closeScript.Closing(-1);
        }
        else if (graphScript.getPlayerState() == -2)
        {
            graphScript.setPlayerState(0);
            CloseEvent.SetActive(true);
            closeScript.Closing(-2); 
        }


    }

    public void retryButton()
    {
        graphScript.setPlayerState(0);
        StartCoroutine(TransOut("Game"));
        
    }

    public void backMenuButton()
    {
        //buttonSound.Play();
        graphScript.setPlayerState(0);
        StartCoroutine(TransOut("Menu"));
        
    }

    public void continueButton()
    {
        buttonSound.Play();
        if (graphScript.getCurrentLevel() == 0 || closeScript.getIsNext() == 0)
        {
            graphScript.setPlayerState(0);
            //SceneManager.LoadScene("Game");
        }
        else
        {
            graphScript.setPlayerState(0);
            graphScript.setCurrentLevel(graphScript.getCurrentLevel()+1);
            if (graphScript.getLevels().Count == graphScript.getCurrentLevel())
                graphScript.setCurrentLevel(graphScript.getCurrentLevel() - 1);
            //SceneManager.LoadScene("Game");
        }
        StartCoroutine(TransOut("Game"));
    }

    private void continueText()
    {
        if (graphScript.getCurrentLevel() == 0)
        {
            conButton.text = "Retry";
        }
        else
        {
            conButton.text = "Continue";
        }
    }

    private void winEvent()
    {
        winPanel.SetActive(true);
        continueText();
    }

    private void loseEvent()
    {
        losePanel.SetActive(true);
    }

    public void setTextVisibility(string str, string descTitle)
    {
        
        textui.SetActive(true);
        placeui.SetActive(false);
        cityui.SetActive(false);
        GameObject descObject = GameObject.Find("Deskripsi");
        descText = descObject.GetComponent<TextMeshProUGUI>();
        descText.text = "";
        StartCoroutine(PlayText(str));
        setTitleDescription(descTitle, "blue");
    }

    IEnumerator PlayText(string str)
    {
        typingSound.Play();
        foreach (char c in str)
        {
            descText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        typingSound.Stop();
    }

    public void setPlaceVisibility()
    {
        buttonSound.Play();
        textui.SetActive(false);
        placeui.SetActive(true);
        cityui.SetActive(false);
        setTitleDescription("Petunjuk", "blue");
        //return isPlace;
    }

    public void setPlaceVisibility2()
    {
        textui.SetActive(false);
        placeui.SetActive(true);
        cityui.SetActive(false);
        setTitleDescription("Petunjuk", "blue");
        //return isPlace;
    }

    public void setCityVisibility()
    {
        buttonSound.Play();
        textui.SetActive(false);
        placeui.SetActive(false);
        cityui.SetActive(true);
        setTitleDescription("Bandara", "orange");
        //return isCity;
    }

    public void moveCity(CityUser nextCity)
    {
        
        StartCoroutine(onChangeCity(nextCity));
        //graphScript.loseTime(5);
    }

    public void openEnemy()
    {
        buttonSound.Play();
        enemyui.SetActive(true);
        enemyui1.transform.localScale = Vector2.zero;
        cgEnemy.alpha = 0;
        cgEnemy.LeanAlpha(1, 0.5f);
        enemyui1.transform.LeanScale(Vector2.one, 0.75f).setEaseOutBack();
    }

    public void closeEnemy()
    {
        buttonSound.Play();
        enemyScript = enemyui.GetComponent<EnemyScript>();
        enemyScript.resetState();
        enemyui.SetActive(false);
    }

    public void openHelp()
    {
        buttonSound.Play();
        helpui.SetActive(true);
        helpui1.transform.localScale = Vector2.zero;
        cgHelp.alpha = 0;
        cgHelp.LeanAlpha(1, 0.5f);
        helpui1.transform.LeanScale(Vector2.one, 0.75f).setEaseOutBack();
    }

    public void closeHelp()
    {
        buttonSound.Play();
        helpScript = helpui.GetComponent<HelpScript>();
        helpScript.resetState();
        helpui.SetActive(false);
    }

    private void setTitleDescription(string descTitle, string rgbColor)
    {
        if (rgbColor == "orange")
            descBG.color = new Color32(255, 94, 0, 255);
        else
            descBG.color = new Color32(0, 153, 255, 255);
        descTitleText.text = descTitle;
    }

    IEnumerator onChangeCity(CityUser nextCity)
    {
        changeAnim.SetActive(true);
        music.Stop();
        planeSound.Play();
        cloud.localPosition = new Vector2(Screen.width/ 1.5f, 0);
        cloud1.localPosition = new Vector2(-Screen.width/ 1.5f, -30);
        cloud2.localPosition = new Vector2(-Screen.width/ 1.5f, 100);
        cloud3.localPosition = new Vector2(Screen.width/1.5f, 125);
        plane.localPosition = new Vector2(Screen.width/2, -Screen.height);
        cg.alpha = 0;
        cg.LeanAlpha(1, 1f);
        cloud.LeanMoveLocalX(300, 1f).setEaseOutBack();
        cloud1.LeanMoveLocalX(-300, 1f).setEaseOutBack();
        cloud2.LeanMoveLocalX(-200, 1f).setEaseOutBack();
        cloud3.LeanMoveLocalX(175, 1f).setEaseOutBack();
        plane.LeanMoveLocalX(40, 2f);
        plane.LeanMoveLocalY(0, 2f).setEaseOutSine();
        yield return new WaitForSeconds(2.0f);
        graphScript.setCurrentCity(nextCity);
        placeScript.onMoveCity(nextCity);
        cityScript.onMoveCity(nextCity);
        plane.LeanMoveLocalX(-40, 3f);
        yield return new WaitForSeconds(3.0f);
        cg.LeanAlpha(0, 1.5f);
        cloud.LeanMoveLocalX(Screen.width, 2f).setEaseInBack();
        cloud1.LeanMoveLocalX(-Screen.width, 2f).setEaseInBack();
        cloud2.LeanMoveLocalX(-Screen.width, 2f).setEaseInBack();
        cloud3.LeanMoveLocalX(Screen.width, 2f).setEaseInBack();
        plane.LeanMoveLocalX(-Screen.width, 2f).setEaseInSine();
        plane.LeanMoveLocalY(-Screen.height, 2f).setEaseInSine();
        //rightPath.SetActive(true);
        yield return new WaitForSeconds(2f);
        changeAnim.SetActive(false);
        
    }

    public IEnumerator rightCityAnimation()
    {
        //music.Stop();
        rightPath.SetActive(true);
        rightPathObj.transform.LeanScale(Vector3.one, 0.75f).setEaseOutBack();
        rightPath.transform.localScale = Vector2.one;
        rightPath1.SetActive(true);
        rightPath2.SetActive(false);
        //rightPathObj.SetActive(true);
        rightPathObj.transform.localPosition = Vector2.one;
        cgRightPath.alpha = 1;
        carImage.transform.localPosition = new Vector2(200, 20);
        yield return new WaitForSeconds(5f);
        carImage.transform.LeanMoveLocalX(-200, 1.3f);
        rightSound.Play();
        yield return new WaitForSeconds(2.0f);
        rightPath1.SetActive(false);
        rightPath2.SetActive(true);
        descRightPath.text = "";
        string keterangan = "Kau berada pada jalur yang benar!! \nLanjutkan misi pencarianmu !";
        StartCoroutine(PlayText2(keterangan));
    }

    IEnumerator PlayText2(string str)
    {
        typingSound.Play();
        rightCityButton.enabled = false;
        foreach(char c in str)
        {
            descRightPath.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        typingSound.Stop();
        rightCityButton.enabled = true;
    }

    public void exitRightPath()
    {
        buttonSound.Play();
        StartCoroutine(ExitRightPath());
    }

    IEnumerator ExitRightPath()
    {
        rightPathObj.transform.localPosition = Vector2.one;
        cgRightPath.alpha = 1;
        rightPathObj.transform.LeanScale(Vector3.zero, 0.75f).setEaseInBack();
        yield return new WaitForSeconds(0.75f);
        cgRightPath.LeanAlpha(0, 0.25f);
        music.Play();
        yield return new WaitForSeconds(0.25f);
        rightPathObj.transform.LeanScale(Vector3.one, 0.75f).setEaseOutBack();
        rightPath.SetActive(false);
        
    }

    public void playMusic()
    {
        music.Play();
    }

}
