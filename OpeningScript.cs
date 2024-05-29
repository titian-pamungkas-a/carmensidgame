using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpeningScript : MonoBehaviour
{
    public string[] text;
    private int textOrder;
    public TextMeshProUGUI textUI;
    public Button nextButton;
    private CityUser city;
    private string cityName = "Jakarta", cityTreasure="Super Semar", cityTreasurePlace="Museum Nasional";
    private int cityHour=60, caseHour=0;
    private float waitTrans = 1f;
    private CityUser firstCity;
    private GameObject GRAPH, closingObject, eventObjevt;
    private ClosingScript closingScript;
    private Graph graphScript;
    public GameObject obj;
    private CanvasGroup cg;
    public AudioSource typingSound;
    private EventScript eventScript;
    void Start()
    {
        GRAPH = GameObject.Find("GRAPH");
        eventObjevt = GameObject.Find("Events");
        eventScript = eventObjevt.GetComponent<EventScript>();
        closingObject = GameObject.Find("Closing");
        cg = this.GetComponent<CanvasGroup>();
        closingScript = closingObject.GetComponent<ClosingScript>();
        graphScript = GRAPH.GetComponent<Graph>();
        SetButtonState(0);
        textOrder = 0;
        text = new string[6];
        text[0] = "Kasus baru telah terbit!!!";
        
        StartCoroutine(PlayText(text[textOrder]));
    }

    
    void Update()
    {
        
    }

    private void SetButtonState(int i)
    {
        Color temp = nextButton.image.color;
        if (i == 0)
        {
            nextButton.enabled = false;
            temp.a = 0.2f;
            nextButton.image.color = temp;
        }
        else
        {
            nextButton.enabled = true;
            temp.a = 1f;
            nextButton.image.color = temp;
        }
    }

    IEnumerator PlayText(string str)
    {
        textUI.text = "";
        yield return new WaitForSeconds(waitTrans);
        typingSound.Play();
        foreach (char c in str)
        {
            textUI.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        typingSound.Stop();
        waitTrans = 0f;
        SetButtonState(1);
    }

    public void NextButton()
    {
        textOrder += 1;
        firstCity = graphScript.getCurrentCity();
        cityTreasure = firstCity.Treasure;
        cityHour = graphScript.getCurrentHour();
        cityName = firstCity.Name;
        cityTreasurePlace = firstCity.TreasurePlace;
        text[1] = "Peninggalan sejarah berupa " + cityTreasure + " dicuri dari " + cityTreasurePlace + " di " + cityName + ".";
        text[2] = "Tersangka dilaporkan terlihat di sekitar tempat kejadian.";
        text[3] = "Tugasmu adalah melacak pelaku pencurian dari " + cityName + " ke tempat persembunyiannya dan menangkapnya!!";
        text[4] = "Pelaku harus ditangkap dalam " + cityHour + " jam sebelum dia lari ke luar negeri.";
        text[5] = "Semoga Beruntung, Detektif !";
        if (textOrder == text.Length)
        {
            StartCoroutine(exitOpening());
            closingScript.setName(cityTreasure);
        }
            
        else
        {
            SetButtonState(0);
            StartCoroutine(PlayText(text[textOrder]));
        }
        

    }

    IEnumerator exitOpening()
    {
        //this.gameObject.SetActive(false);
        obj.transform.localScale = Vector2.one;
        obj.transform.LeanScale(Vector2.zero, 0.75f).setEaseInBack();
        yield return new WaitForSeconds(0.75f);
        cg.alpha = 1;
        cg.LeanAlpha(0, 0.25f);
        eventScript.playMusic();
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }

    
}
