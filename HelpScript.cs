using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{
    public GameObject percentageButton, description;
    private GameObject GRAPH;
    private Graph graphScript;
    private CityUser nextCity;
    private List<int> enemyCity;
    private List<CityUser> neighbour;
    private int percentage;
    public TextMeshProUGUI descText;
    public AudioSource audio;
    public Button exitButton;
    void Start()
    {
        GRAPH = GameObject.Find("GRAPH");
        graphScript = GRAPH.GetComponent<Graph>();
        enemyCity = new List<int>(graphScript.getEnemyCity());
        description.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void get100()
    {
        showDescription(100);
    }

    public void get70()
    {
        showDescription(70);
    }

    public void get45()
    {
        showDescription(45);
    }

    public void resetState()
    {
        percentageButton.SetActive(true);
        description.SetActive(false);
    }

    private void showDescription(int per)
    {
        string str;
        int rand = Random.Range(0, 100);
        nextCity = graphScript.getNextCity();
        neighbour = graphScript.getNeighbour(graphScript.getCurrentCity());
        for (int i = 0; i < neighbour.Count; i++)
        {
            print(neighbour[i].Name);
        }
        percentageButton.SetActive(false);
        description.SetActive(true);
        if (rand <= per)
            str = nextCity.Name;
        else
        {
            do
            {
                str = neighbour[Random.Range(0, neighbour.Count - 1)].Name;
            } while (str == nextCity.Name);
        }
        string kalimat = "Dari info yang didapat, penjahat yang dicari melanjutkan perjalanannya ke kota " + str + ". Akurasi kebenaran informasi sebanyak " + per + "%.";
        StartCoroutine(PlayText(kalimat));
    }

    IEnumerator PlayText(string str)
    {
        descText.text = "";
        audio.Play();
        exitButton.enabled = false;
        foreach (char c in str)
        {
            descText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        audio.Stop();
        exitButton.enabled = true;
    }
}
