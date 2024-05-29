using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public TextMeshProUGUI choice1, choice2, choice3, criminalDescText1, criminalDescText2;
    public GameObject enter, criminalDesc, criminalInput;
    public Button right1, left1, right2, left2;
    private List<string> item1, item2, item3;
    private int[] currentItem;
    private List<FixedCity> fixes;
    private GameObject mainManager;
    private MainManager script;
    private Criminal criminal;
    private List<Criminal> criminals, allCriminals;
    private List<Level> levels;
    private GameObject GRAPH;
    private Graph graphScript;
    public AudioSource typingSound;
    void Awake()
    {
        criminalDesc.SetActive(false);
        mainManager = GameObject.Find("MainManager");
        GRAPH = GameObject.Find("GRAPH");
        graphScript = GRAPH.GetComponent<Graph>();
        script = mainManager.GetComponent<MainManager>();
        //criminal = script.getCriminals()[Random.Range(0, 5)];
        levels = new List<Level>(script.getLevels());
        print(levels.Count);
        int temp = script.getFixedLevel();
        fixes = new List<FixedCity>(script.getFixedCities());
        allCriminals = new List<Criminal>(script.getAllCriminal());
        if (script.getCurrentLevel() == 0)
        {
            
            print(fixes[temp].CriminalId + " Adalah id criminal");
            
            for (int i = 0; i < allCriminals.Count; i++)
            {
                if (fixes[temp].CriminalId == allCriminals[i].Id)
                {
                    criminal = allCriminals[i];
                    print(criminal.Name + " " + criminal.Hair);
                    break;
                }
            }
            /*criminals = new List<Criminal>(script.getCriminals3());
            criminal = criminals[Random.Range(0, criminals.Count - 1)];*/
            graphScript.setCriminal(criminal);
            print(criminal.Name + " " + criminal.Hair);
        }
        else
        {
            for (int i = 0; i < levels.Count; i++)
            {
                print(script.getCurrentLevel() + " " + levels[i].LevelCount);
                if (script.getCurrentLevel() == levels[i].LevelCount)
                {
                    print(levels[i].CriminalLevel);
                    if (levels[i].CriminalLevel == 1)
                    {
                        criminals = new List<Criminal>(script.getCriminals1());
                        for (int j = 0; j < criminals.Count; j++)
                        {
                            print(criminals[j].Name + " " + criminals[j].HobbyName + " " + Random.Range(0, criminals.Count));
                        }
                        criminal = criminals[Random.Range(0, criminals.Count)];
                        print(criminal.Name + " " + criminal.Hair);
                    }
                    else if (levels[i].CriminalLevel == 2)
                    {
                        criminals = new List<Criminal>(script.getCriminals2());
                        criminal = criminals[Random.Range(0, criminals.Count)];
                        print(criminal.Name + " " + criminal.Hair);
                    }
                    else if (levels[i].CriminalLevel == 3)
                    {
                        criminals = new List<Criminal>(script.getCriminals3());
                        criminal = criminals[Random.Range(0, criminals.Count)];
                        print(criminal.Name + " " + criminal.Hair);
                    }
                }
            }
        }
        
        graphScript.setCriminal(criminal);
        item1 = new List<string>() { 
            "-", "Olahraga", "Kuliner", "Membaca", "Melukis", "Film", "Musik"
        };
        item2 = new List<string>()
        {
            "-", "Hitam", "Pirang"
        };
        item3 = new List<string>()
        {
            "-", "Kemeja", "Jas"
        };
        choice1.text = item1[0];
        choice2.text = item2[0];
        choice3.text = item3[0];
        currentItem = new int[3];
        currentItem[0] = 0;
        currentItem[1] = 0;
        currentItem[2] = 0;
        if (criminal.Hair == null)
        {
            right1.enabled = false;
            left1.enabled = false;
            Color tempc = right1.image.color;
            tempc.a = 0.1f;
            right1.image.color = tempc;
            left1.image.color = tempc;
            choice2.color = tempc;
        }
        if (criminal.Outfit == null)
        {
            right2.enabled = false;
            left2.enabled = false;
            Color tempc = right2.image.color;
            tempc.a = 0.1f;
            right2.image.color = tempc;
            left2.image.color = tempc;
            choice3.color = tempc;
        }
        /*rightButton.enabled = false;
        Color temp = rightButton.image.color;
        temp.a = 0.1f;
        rightButton.image.color = temp;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void after1()
    {
        currentItem[0] += 1;
        if (currentItem[0] == item1.Count)
        {
            currentItem[0] = 0;
        }
        choice1.text = item1[currentItem[0]];
    }

    public void before1()
    {
        currentItem[0] -= 1;
        if (currentItem[0] < 0)
        {
            currentItem[0] = item1.Count - 1;
        }
        choice1.text = item1[currentItem[0]];
    }

    public void after2()
    {
        currentItem[1] += 1;
        if (currentItem[1] == item2.Count)
        {
            currentItem[1] = 0;
        }
        choice2.text = item2[currentItem[1]];
    }

    public void before2()
    {
        currentItem[1] -= 1;
        if (currentItem[1] < 0)
        {
            currentItem[1] = item2.Count - 1;
        }
        choice2.text = item2[currentItem[1]];
    }

    public void after3()
    {
        currentItem[2] += 1;
        if (currentItem[2] == item3.Count)
        {
            currentItem[2] = 0;
        }
        choice3.text = item3[currentItem[2]];
    }

    public void before3()
    {
        currentItem[2] -= 1;
        if (currentItem[2] < 0)
        {
            currentItem[2] = item3.Count - 1;
        }
        choice3.text = item3[currentItem[2]];
    }

    public void setSuspect()
    {
        graphScript.setSuspect(choice1.text, choice2.text, choice3.text);
        criminalInput.SetActive(false);
        criminalDesc.SetActive(true);
        int crimCount = 0;
        string criminalName = "ASD";
        for (int i = 0; i < allCriminals.Count; i++)
        {
            if (choice2.text != null && choice3.text != null)
            {
                if ((allCriminals[i].HobbyName == choice1.text || choice1.text == "-") && (allCriminals[i].Hair == choice2.text || choice2.text == "-") && (allCriminals[i].Outfit == choice3.text || choice3.text == "-"))
                {
                    crimCount++;
                    criminalName = allCriminals[i].Name;
                    print(criminalName);
                } 
            }
            else if (choice2.text != null && choice3.text == null)
            {
                if ((allCriminals[i].HobbyName == choice1.text || choice1.text == "-") && (allCriminals[i].Hair == choice2.text || choice2.text == "-"))
                {
                    crimCount++;
                    criminalName = allCriminals[i].Name;
                    print(criminalName);
                }            
            }
            else if (choice2.text == null)
            {
                if (allCriminals[i].HobbyName == choice1.text || choice1.text == "-")
                {
                    crimCount++;
                    criminalName = allCriminals[i].Name;
                    print(criminalName);
                }
            }
        }
        StartCoroutine(SetText1(crimCount, criminalName));
    }

    IEnumerator SetText1(int crimCount, string criminalName)
    {
        string str = "Berdasarkan ciri-ciri yang disebutkan, terdapat " + crimCount + " yang ditemukan.";
        print(crimCount);
        criminalDescText1.text = "";
        criminalDescText2.text = "";
        typingSound.Play();
        foreach (char c in str)
        {
            criminalDescText1.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        typingSound.Stop();
        string text2 = "";
        if (crimCount == 1)
            text2 = "Orang tersebut bernama <b><u>" + criminalName + "</u></b>";
        else
            text2 = "Cari ciri lain dari tersangka sebagai bukti untuk menghindari salah tangkap!";
        yield return new WaitForSeconds(1f);
        typingSound.Play();
        foreach (char c in text2)
        {
            criminalDescText2.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        typingSound.Stop();
        //StartCoroutine(SetText2(text2));
    }

    

    public void resetState()
    {
        criminalInput.SetActive(true);
        criminalDesc.SetActive(false);
        typingSound.Stop();
    }
}
