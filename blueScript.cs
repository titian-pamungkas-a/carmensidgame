using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class blueScript : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefabs;
    private GameObject parent, graphObject, buttonObject, eventObject, descObject;
    private Graph graphScript;
    private EventScript eventScript;
    private CityUser currentCity, nextCity;
    private List<CityUser> neighbour, cities;
    private List<string> description, placeName, clue;
    private TextMeshProUGUI buttonText, descText;
    private RectTransform rectTransform;
    private Button buttonAct;
    // Start is called before the first frame update
    void Start()
    {
        graphObject = GameObject.Find("GRAPH");
        eventObject = GameObject.Find("Events");
        eventScript = eventObject.GetComponent<EventScript>();
        graphScript = graphObject.GetComponent<Graph>();
        eventScript.setCityVisibility();
        eventScript.setPlaceVisibility();
        createNextPlaceButton();
    }

    private void findNextCity()
    {
        nextCity = graphScript.getNextCity();
    }
    private void createNextPlaceButton()
    {
        findNextCity();
        clue = new List<string>();
        if (graphScript.rightCurrentCity())
        {
            description = new List<string>(nextCity.Desc);
            for (int i = 0; i < 3; i++)
            {
                int randomValue = Random.Range(0, description.Count - 1);
                clue.Add(description[randomValue]);
                description.RemoveAt(randomValue);
            }
            print(graphScript.getCurrentVisit());
            print(graphScript.getCriminal().Diff);
            if (graphScript.getCurrentVisit() == 0 && graphScript.getCriminal().Diff - 1 >= 0)
            {
                int randrang = Random.Range(0, 2);
                clue[randrang] = graphScript.getCriminal().Desc[Random.Range(0, 2)];
                print(clue[randrang]);
            }
            if (graphScript.getCurrentVisit() == 1 && graphScript.getCriminal().Diff - 2 >= 0)
            {
                clue[Random.Range(0, 2)] = graphScript.getCriminal().Desc[3];
            }
            if (graphScript.getCurrentVisit() == 2 && graphScript.getCriminal().Diff - 3 >= 0)
            {
                clue[Random.Range(0, 2)] = graphScript.getCriminal().Desc[4];
            }
        }
        else
        {
            description = new List<string>(graphScript.getFalseDescription());
            for (int i = 0; i < 3; i++)
            {
                int randomValue = Random.Range(0, description.Count - 1);
                clue.Add(description[randomValue]);
                description.RemoveAt(randomValue);
            }
        }
            
        placeName = new List<string>(graphScript.getAllPlace());
        for (int i = 0; i < 3; i++)
        {
            createButton(new Vector3(260.0f, 0 - (45 * i), 0), placeName[i], i, clue[i]);
        }
    }

    private void createButton(Vector3 pos, string str, int i, string desc)
    {
        buttonObject = Object.Instantiate(buttonPrefabs, Vector3.zero, Quaternion.identity);
        buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        parent = this.gameObject;
        buttonAct = buttonObject.GetComponent<Button>();
        rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.SetParent(parent.transform);
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localPosition = pos;
        buttonText.text = str;
        buttonAct.onClick.AddListener(delegate { TaskOnClick(desc, str); });
    }

    private void TaskOnClick(string str, string descTitle)
    {
        eventScript.setTextVisibility(str, descTitle);
        /*descObject = GameObject.Find("Deskripsi");
        descText = descObject.GetComponent<TextMeshProUGUI>();
        //descText.text = str;*/
        graphScript.loseTime(3);
    }

    

    public void onMoveCity(CityUser newCity)
    {
        if (graphScript.getPlayerState() == 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            createNextPlaceButton();
            eventScript.setPlaceVisibility2();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPlace()
    {
        eventScript.setPlaceVisibility();
    }

    /*public void showDesc()
    {
        eventScript.setTextVisibility();
    }*/
}
