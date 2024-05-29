using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class orangeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefabs;
    private GameObject parent, graphObject, buttonObject, eventObject;
    private Graph graphScript;
    private EventScript eventScript;
    private CityUser currentCity, nextCity, rightCity;
    private List<CityUser> neighbour, cities;
    private TextMeshProUGUI buttonText;
    private RectTransform rectTransform;
    private Button buttonAct;
    // Start is called before the first frame update
    void Start()
    {
        graphObject = GameObject.Find("GRAPH");
        eventObject = GameObject.Find("Events");
        eventScript = eventObject.GetComponent<EventScript>();
        graphScript = graphObject.GetComponent<Graph>();
        createNextCityButton();
    }

    private void createNextCityButton()
    {
        currentCity = graphScript.getCurrentCity();
        neighbour = new List<CityUser>(graphScript.getNeighbour(currentCity));
        //print(currentCity.Id);
        for (int i = 0; i < neighbour.Count; i++)
        {
            //print(neighbour[i].Id);
            createButton(new Vector3(260.0f, 10 - (40 * i), 0), neighbour[i]);
        }
    }

    private void createButton(Vector3 pos, CityUser city)
    {
        buttonObject = Object.Instantiate(buttonPrefabs, Vector3.zero, Quaternion.identity);
        buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        parent = this.gameObject;
        buttonAct = buttonObject.GetComponent<Button>();
        rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.SetParent(parent.transform);
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localPosition = pos;
        buttonText.text = city.Name;
        buttonAct.onClick.AddListener(delegate { TaskOnClick(city); });
    }

    private void TaskOnClick(CityUser city)
    {
        eventScript.moveCity(city);
    }

    public void onMoveCity(CityUser newCity)
    {
        graphScript.loseTime(graphScript.distanceOnMove(currentCity.Id, newCity.Id));
        if (graphScript.getPlayerState() == 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            createNextCityButton();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showCity()
    {
        eventObject = GameObject.Find("Events");
        eventScript = eventObject.GetComponent<EventScript>();
        eventScript.setCityVisibility();
    }
}
