using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Graph : MonoBehaviour
{
    private int[,] graph, graphs;
    private CityUser currentCity;
    private List<CityUser> cities, neighbour;
    private List<int> enemyCity;
    private GameObject mainManager, titleObject, timeLeftObject, eventObject;
    private TextMeshProUGUI titleText, timeLeftText;
    private MainManager script;
    private List<string> allPlace, falseDescription;
    private List<Level> levels;
    private List<Link> links;
    private List<FixedCity> fixes;
    private Criminal criminal;
    private string c1, c2, c3;
    public int timeLeft, currentVisit, enemyLevel, baseTime=0;
    public Image bgImage;
    private Sprite sprite;
    private EventScript eventScript;
    // Start is called before the first frame update
    void Start()
    {
        eventObject = GameObject.Find("Events");
        eventScript = eventObject.GetComponent<EventScript>();
        mainManager = GameObject.Find("MainManager");
        titleObject = GameObject.Find("Title");
        timeLeftObject = GameObject.Find("TimeLeft");
        timeLeftText = timeLeftObject.GetComponent<TextMeshProUGUI>();
        script = mainManager.GetComponent<MainManager>();
        titleText = titleObject.GetComponent<TextMeshProUGUI>();
        timeLeft = 60;
        currentVisit = 0;
        bgImage.sprite = Resources.Load<Sprite>("Background/16");
        initGraph2();
        //initGraph();
    }

    private void initGraph()
    {
        cities = new List<CityUser>(script.getCityUsers());
        allPlace = new List<string>(script.getAllPlace());
        graph = new int[cities.Count, cities.Count];
        for (int i = 0; i < cities.Count; i++)
        {
            for (int j = 0; j < cities.Count; j++)
            {
                graph[i, j] = 0;
            }
        }
        createLink(cities.Count);
        createEnemy();
        initFalseDescription();
    }
    
    public int getCurrentLevel()
    {
        return script.getCurrentLevel();
    }

    public void setCurrentLevel(int lvl)
    {
        script.setCurrentLevel(lvl);
    }

    private void initGraph2()
    {
        cities = new List<CityUser>(script.getCityUsers());
        allPlace = new List<string>(script.getAllPlace());
        graphs = script.getGraphs().Clone() as int[,];
        links = new List<Link>(script.getLinks());
        /*for (int i = 0; i < cities.Count+1; i++)
        {
            for (int j = 0; j < cities.Count+1; j++)
            {
                graphs[i, j] = 0;
            }
        }
        for (int i = 0; i < links.Count; i++)
        {
            createLink2(links[i].City1, links[i].City2, links[i].DistanceHour);
        }*/
        createEnemy();
        initFalseDescription();
    }

    private void initFalseDescription()
    {
        falseDescription = new List<string>();
        falseDescription.Add("Tidak ditemukan tersangka pada tempat ini");
        falseDescription.Add("Sumber terpercaya tidak melihat tersangka di sekitar sini");
        falseDescription.Add("Tidak ditemukan orang dengan ciri-ciri tersebut");
        falseDescription.Add("Orang yang dicari tidak terlihat");
        falseDescription.Add("Tersangka tidak tampak di sekitar sini");
        falseDescription.Add("Orang sekitar tidak yakin melihat orang dengan ciri-ciri tersebut");
        falseDescription.Add("Polisi patrol tidak menemukan orang yang dicari");
        falseDescription.Add("Ciri-ciri orang yang dicari tidak sesuai dengan orang yang lewat");
        falseDescription.Add("Orang yang dicari tidak ada");
        falseDescription.Add("Tidak ada bukti kehadiran tersangka di sini");
        falseDescription.Add("Orang yang dicari tidak ada");
        falseDescription.Add("Pihak kepolisian melaporkan kemungkinan tersangka di tempat lain");
    }

    private void createEnemy()
    {
        levels = new List<Level>(script.getLevels());
        enemyCity = new List<int>();
        if (script.getCurrentLevel() != 0)
        {
            currentCity = cities[Random.Range(0, cities.Count)];
            print("Level saat ini adalah : " + script.getCurrentLevel());
            enemyCity.Add(currentCity.Id);
            CityUser now = new CityUser(currentCity.Id, currentCity.Name, currentCity.ImagePath, currentCity.Desc, currentCity.Treasure, currentCity.TreasurePlace);
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].LevelCount == script.getCurrentLevel())
                {
                    timeLeft = levels[i].TotalTime;
                    int pNum = 0;
                    for (int j = 0; j < levels[i].CityCount; j++)
                    {
                        List<CityUser> neigh = new List<CityUser>(getNeighbour(now));
                        int next = Random.Range(0, neigh.Count);
                        if (pNum > neigh.Count * 3 && enemyCity.Contains(neigh[next].Id))
                        {
                            pNum++;
                            j--;
                            continue;
                        }pNum = 0;
                        enemyCity.Add(neigh[next].Id);
                        now = neigh[next];
                        //print(cities[next].Name + " " + cities[next].Id);
                    }
                    for (int j = 1; j < enemyCity.Count; j++)
                    {
                        print("Enemi1: " + enemyCity[j]);
                        baseTime += distanceOnMove(cities[enemyCity[j]-1].Id, cities[enemyCity[j-1]-1].Id);
                        print(cities[enemyCity[j-1]-1].Name + " dan " + cities[enemyCity[j]-1].Name);
                    }
                    print("Basetime adalah " + baseTime);
                }
            }
        }
        else
        {
            int temp = script.getFixedLevel();
            fixes = new List<FixedCity>(script.getFixedCities());
            for (int i = 0; i < fixes[temp].Scenarios.Count; i++)
            {
                enemyCity.Add(fixes[temp].Scenarios[i]);
            }
            currentCity = cities[fixes[temp].Scenarios[0] - 1];
            timeLeft = fixes[temp].TotalTime;
            //bgImage.sprite = Resources.Load<Sprite>("Background/" + currentCity.ImagePath);
            /*enemyCity.Add(fixes[temp].City1 );
            enemyCity.Add(fixes[temp].City2 );
            enemyCity.Add(fixes[temp].City3 );
            enemyCity.Add(fixes[temp].City4 );
            enemyCity.Add(fixes[temp].City5 );
            enemyCity.Add(fixes[temp].City6 );*/
        }
        
        setImage(currentCity.ImagePath);
        /*enemyCity = new List<int>();
        enemyCity.Add(3);
        enemyCity.Add(5);
        enemyCity.Add(4);
        enemyCity.Add(6);
        enemyCity.Add(8);
        currentCity = cities[5];*/
        titleText.text = currentCity.Name;
    }

    public List<Level> getLevels()
    {
        return levels;
    }

    public int getCurrentHour()
    {
        return timeLeft;
    }

    private void setImage(string imagePath)
    {
        sprite = Resources.Load<Sprite>("Background/" + imagePath);
        if (sprite != null)
        {
            bgImage.sprite = sprite;
        }
        else
        {
            StartCoroutine(getImage(imagePath));
        }
    }

    IEnumerator getImage(string URL)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL))
        {
            print("Masuk korotin");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil");
            else
            {
                print("Masuk teksture");
                Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite spriteUrl = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                bgImage.sprite = spriteUrl;
                print("Masuk sprite");
            }
        }
    }

    public List<int> getEnemyCity()
    {
        return enemyCity;
    }

    private void createLink(int d)
    {
        int i = 0;
        do
        {
            link(i, i + 1);
            link(i, i + 2);
            link(i + 1, i + 3);
            i += 2;
        } while (i < d - 2);
        link(d - 2, d - 1);
        link(0, d - 2);
        link(1, d - 1);
    }

    private void createLink2(int id1, int id2, int dh)
    {
        graphs[id1, id2] = dh;
        graphs[id2, id1] = dh;
    }

    private void link(int i, int j)
    {
        graph[i, j] = 1;
        graph[j, i] = 1;
    }

    public List<string> getAllPlace()
    {
        allPlace = new List<string>(script.getAllPlace());
        return allPlace;
    }

    public List<CityUser> getNeighbour(CityUser currentCity)
    {
        
        //this.currentCity = currentCity;
        neighbour = new List<CityUser>();
        //neighbour.Clear();
        /*for (int i = 0; i < cities.Count; i++)
        {
            if (graph[currentCity.Id-1, i] == 1)
            {
                neighbour.Add(cities[i]);
            }
        }*/
        for (int i = 1; i <= cities.Count; i++)
        {
            //print(graphs[currentCity.Id, i]);\
            
            if (graphs[currentCity.Id, i] != 0)
            {
                /*print("Current: " + currentCity.Name);
                print("NEighbour: " + cities[i - 1].Id + " " + cities[i - 1].Name);*/
                neighbour.Add(cities[i-1]);
            }
        }
        //print(currentCity.Name);
        return neighbour;
    }

    public List<string> getFalseDescription()
    {
        return falseDescription;
    }

    public bool rightInNeighbour()
    {
        neighbour = new List<CityUser>(getNeighbour(this.currentCity));
        //print(neighbour.Count);
        /*for (int j = 0; j < enemyCity.Count; j++)
        {
            print("Enemi2: " + enemyCity[j]);
        }*/
        print(cities[0].Name);
        for (int i = 0; i < neighbour.Count; i++)
        {
            print("KIRI: " +  getNextCity().Id + ", Kanan: " + neighbour[i].Id);
            //print(cities[enemyCity[0]-1].Name + " " + cities[enemyCity[1]-1].Name + " " + cities[enemyCity[2]-1].Name + " " + cities[enemyCity[3]-1].Name + " " + cities[enemyCity[4]].Name);
            if (getNextCity().Id == neighbour[i].Id)
                return true;
        }
        return false;
    }

    public bool rightCurrentCity()
    {
        if (this.currentCity == cities[enemyCity[0] - 1])
            return true;
        else
            return false;
    }

    public CityUser getNextCity()
    {

        //print(enemyCity.Count);
        print(enemyCity[1] - 1);
        return cities[enemyCity[1]-1];
    }

    public CityUser getCurrentCity()
    {
        return this.currentCity;
    }

    public void setCurrentCity(CityUser city)
    {
        this.currentCity = city;
        setImage(currentCity.ImagePath);
        //bgImage.sprite = Resources.Load<Sprite>("Background/" + currentCity.ImagePath);
        titleText.text = this.currentCity.Name;
        if (this.currentCity.Id == cities[enemyCity[1] - 1].Id)
        {
            enemyCity.RemoveAt(0);
            setCurrentVisit();
            print(enemyCity.Count);
            if (enemyCity.Count == 1)
            {
                checkSuspect();
            }
            else
                StartCoroutine(eventScript.rightCityAnimation());
        }
        else
            StartCoroutine(waitToPlay());
        
    }

    IEnumerator waitToPlay()
    {
        yield return new WaitForSeconds(5);
        eventScript.playMusic();
    }

    public List<CityUser> getCities()
    {
        return this.cities;
    }

    public void loseTime(int num)
    {
        timeLeft -= num;
        if (timeLeft <= 0)
            script.setPlayerState(-2);
    }

    public int distanceOnMove(int id1, int id2)
    {
        return graphs[id1, id2];
    }

    public int getPlayerState()
    {
        return script.getPlayerState();
    }

    public void setPlayerState(int num)
    {
        script.setPlayerState(num);
    }

    public void setCriminal(Criminal cr)
    {
        criminal = cr;
        print(criminal.Name);
    }

    public Criminal getCriminal()
    {
        //print(criminal.Name);
        return criminal;
    }



    public void setSuspect(string r, string h, string o)
    {
        c1 = r;
        c2 = h;
        c3 = o;
        print(c1 + " " + c2 + " " + c3);
    }


    private void checkSuspect()
    {
        if (criminal.Diff == 1)
        {
            if (criminal.HobbyName == c1)
            {
                script.setPlayerState(1);
            }
            else
                script.setPlayerState(-1);
        }
        if (criminal.Diff == 2)
        {
            if (criminal.HobbyName == c1 && criminal.Hair == c2)
            {
                script.setPlayerState(1);
            }
            else
                script.setPlayerState(-1);
        }
        if (criminal.Diff == 3)
        {
            if (criminal.HobbyName == c1 && criminal.Hair == c2 && criminal.Outfit == c3)
            {
                script.setPlayerState(1);
            }
            else
                script.setPlayerState(-1);
        }
    }

    public void setCurrentVisit()
    {
        currentVisit = currentVisit + 1;
    }

    public int getCurrentVisit()
    {
        return currentVisit;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftText.text = timeLeft.ToString();
    }
}
