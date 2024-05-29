using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class APIScript : MonoBehaviour
{
    private string URL1 = "https://64f0149b8a8b66ecf7792137.mockapi.io/api/getCity";
    //private string URL = "http://127.0.0.1:8000/api/";
    private string URL = "http://carmensid.my.id/api/";
    private List<City> cities;
    private List<CityAttribute> cityAttributes;
    private List<Link> links;
    private List<Level> levels;
    private List<FixedCity> fixedCities;
    private List<Religion> religions;
    private List<Criminal> criminals1, criminals2, criminals3, allCriminals;
    private CreateCity createCity;
    private CreateEnemy createEnemy;
    private int[] gotoMenu, idforCity, idforAttribute, idforScenario;
    [SerializeField]
    private GameObject mainManager;
    private MainManager script;
    private List<Attribute> attributes;
    private List<AttributeCity> attributeCities;
    public GameObject mulaiButton, loadingText, retryButton;
    private int total, change=0;
    //private City city;
    void Start()
    {
        total = 0;
        mulaiButton.SetActive(false);
        retryButton.SetActive(false);
        loadingText.SetActive(true);
        gotoMenu = new int[] { 0, 0, 0, 0, 0};
        cities = new List<City>();
        cityAttributes = new List<CityAttribute>();
        attributeCities = new List<AttributeCity>();
        attributes = new List<Attribute>();
        script = mainManager.GetComponent<MainManager>();
        createCity = GetComponent<CreateCity>();
        createEnemy = GetComponent<CreateEnemy>();
        getCourotine();
    }

    private void getCourotine()
    {
        /*StartCoroutine(GetDatas());
        StartCoroutine(GetUserCities());*/
        StartCoroutine(GetAttribute());
    }

    IEnumerator GetAttribute()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "getAttribute"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                failed();
            else
            {
                print("atribute kota berhasil diambil");
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                idforAttribute = new int[nodes[nodes.Count-1]["id"]+1];
                for (int i = 0; i < nodes.Count; i++)
                {
                    idforAttribute[nodes[i]["id"]] = i+1;
                    attributes.Add(new Attribute(i+1, nodes[i]["name"], nodes[i]["sentence"]));
                }
                StartCoroutine(GetAttributeCities());
            }
        }
    }

    IEnumerator GetAttributeCities()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "getusercity"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("kota tidak berhasil diambil");
            else
            {
                print("Kota Berhasil Diambil");
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                idforCity = new int[nodes[nodes.Count - 1]["id"] + 1];
                for (int i = 0; i < nodes.Count; i++)
                {
                    idforCity[nodes[i]["id"]] = i + 1;
                    attributeCities.Add(new AttributeCity(i+1, nodes[i]["name"], new List<string>[attributes.Count], nodes[i]["imagePath"], nodes[i]["treasure"], nodes[i]["treasurePlace"], nodes[i]["detail"]));
                    for (int j = 0; j < attributes.Count; j++)
                    {
                        attributeCities[i].Clue[j] = new List<string>();
                    }
                }
                StartCoroutine(GetAttributePerCities());
                StartCoroutine(GetLink());
                StartCoroutine(GetFix());
                StartCoroutine(GetReligion());
                StartCoroutine(GetLevel());
            }

        }
    }

    IEnumerator GetAttributePerCities()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "getCityAttribute"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("kota attribute tidak berhasil diambil");
            else
            {
                print("kota attribute berhasil diambil");
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    //attributeCities[nodes[i]["cityId"] - 1].Clue[nodes[i]["attributeId"] - 1].Add(nodes[i]["attributeName"]);
                    attributeCities[idforCity[nodes[i]["cityId"]]-1].Clue[idforAttribute[nodes[i]["attributeId"]]-1].Add(nodes[i]["attributeName"]);
                }

                gotoMenu[0] = 1;
            }
        }
    }

    IEnumerator GetDatas()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL+"getcity"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil");
            else
            {
                print("Berhasil");
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    cities.Add(new City(nodes[i]["id"], nodes[i]["name"], nodes[i]["house"], nodes[i]["food"], nodes[i]["art"], nodes[i]["song"], nodes[i]["clothes"], nodes[i]["destination"], nodes[i]["history"], nodes[i]["commodity"]));
                }
                //script.getCities(cities);
                createCity.getCities(cities);
            }
                
        }
    }

    IEnumerator GetUserCities()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getusercity"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil2");
            else
            {
                print("Berhasil2");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    cityAttributes.Add(new CityAttribute(nodes[i]["id"], nodes[i]["name"], null, null, null, null, null, null, null, null, null, nodes[i]["imagePath"]));
                }
                /*StartCoroutine(GetReligion());
                StartCoroutine(GetLevel());*/
                /*StartCoroutine(GetFood());
                StartCoroutine(GetHouse());
                StartCoroutine(GetArt());
                StartCoroutine(GetDestination());
                StartCoroutine(GetHistory());
                StartCoroutine(GetNew());
                StartCoroutine(GetClothe());
                StartCoroutine(GetSong());
                StartCoroutine(GetTown());*/
            }
        }
    }

    IEnumerator GetFood()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getFood"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil3");
            else
            {
                print("Berhasil3");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Foods = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Foods.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }
                            
                    }
                }
                /*for (int i = 0; i < cityAttributes.Count; i++)
                {
                    for (int j = 0; j < cityAttributes[i].Foods.Count; j++)
                    {
                        print(cityAttributes[i].Id + " " + cityAttributes[i].Name + " " + cityAttributes[i].Foods[j]);
                    }
                }*/
                gotoMenu[0] = 1;
            }
        }
    }

    IEnumerator GetHouse()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getHouses"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil4");
            else
            {
                print("Berhasil4");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Houses = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Houses.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[1] = 1;
            }
        }
    }

    IEnumerator GetArt()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getArt"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil5");
            else
            {
                print("Berhasil5");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Arts = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Arts.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[2] = 1;
            }
        }
    }

    IEnumerator GetClothe()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getClothe"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil6");
            else
            {
                print("Berhasil6");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Clothes = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Clothes.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[3] = 1;
            }
        }
    }

    IEnumerator GetDestination()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getDestination"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil7");
            else
            {
                print("Berhasil7");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Destinations = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Destinations.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[4] = 1;
            }
        }
    }

    IEnumerator GetHistory()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getHistory"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil8");
            else
            {
                print("Berhasil8");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Histories = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Histories.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[5] = 1;
            }
        }
    }

    IEnumerator GetNew()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getNew"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil9");
            else
            {
                print("Berhasil9");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].News = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].News.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[6] = 1;
            }
        }
    }

    IEnumerator GetTown()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getTown"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil10");
            else
            {
                print("Berhasil10");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Towns = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Towns.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[7] = 1;
            }
        }
    }

    IEnumerator GetSong()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getSong"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasil11");
            else
            {
                print("Berhasil11");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < cityAttributes.Count; i++)
                {
                    cityAttributes[i].Songs = new List<string>();
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (cityAttributes[i].Id == nodes[j]["cityId"])
                        {
                            cityAttributes[i].Songs.Add(nodes[j]["name"]);
                            //print(cityAttributes[i].Foods[j]);
                        }

                    }
                }
                gotoMenu[8] = 1;
            }
        }
    }

    IEnumerator GetLink()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getLink"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasilLink");
            else
            {
                print("BerhasilLink");
                links = new List<Link>();
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    //links.Add(new Link(nodes[i]["city1"], nodes[i]["city2"], nodes[i]["distanceHour"]));
                    //print(nodes[i]["city1"] + "__" + nodes[i]["city2"]);
                    links.Add(new Link(idforCity[nodes[i]["city1"]], idforCity[nodes[i]["city2"]], nodes[i]["distanceHour"]));
                }


                gotoMenu[1] = 1;
            }
        }
    }

    IEnumerator GetLevel()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getLevel"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasilLevel");
            else
            {
                print("BerhasilLevel");
                levels = new List<Level>();
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    levels.Add(new Level(nodes[i]["levelCount"], nodes[i]["cityCount"], nodes[i]["totalTime"], nodes[i]["criminalLevel"]));
                }
                for (int i = 0; i < levels.Count; i++)
                {
                    print(levels[i].LevelCount + " " + levels[i].CityCount + " " + levels[i].TotalTime);
                }

                gotoMenu[2] = 1;
            }
        }
    }

    IEnumerator GetFix()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getFix"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasilFix");
            else
            {
                print("BerhasilFix");
                fixedCities = new List<FixedCity>();
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                idforScenario = new int[nodes[nodes.Count - 1]["id"] + 1];
                for (int i = 0; i < nodes.Count; i++)
                {
                    idforScenario[nodes[i]["id"]] = i+1;
                    fixedCities.Add(new FixedCity(i+1, nodes[i]["name"], new List<int>(), nodes[i]["criminalId"], nodes[i]["totalTime"]));
                    //fixedCities[i].Scenarios = new List<int>();
                }
                print("AASDASDASDDSA" + fixedCities.Count);
                StartCoroutine(GetScenario());
                print("AASDASDASDDSA" + fixedCities.Count);
                
            }
        }
    }

    IEnumerator GetScenario()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getScenario"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasilScenario");
            else
            {
                print("BerhasilScenario");
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    //print(i + "__" + nodes[i]["fixesId"] + "__" + nodes[i]["cityId"]);
                    //fixedCities[nodes[i]["fixesId"] - 1].Scenarios.Add(nodes[i]["cityId"]);
                    //fixedCities[nodes[i]["fixesId"] - 1].Scenarios.Add(idforCity[nodes[i]["cityId"]]);
                    fixedCities[idforScenario[nodes[i]["fixesId"]] - 1].Scenarios.Add(idforCity[nodes[i]["cityId"]]);
                }
                for (int i = 0; i < fixedCities.Count; i++)
                {
                    for (int j = 0; j < fixedCities[i].Scenarios.Count; j++)
                    {
                        int temp = j + 1;
                        print("Urutan " + temp + " adalah " + fixedCities[i].Scenarios[j]);
                    }
                }
                gotoMenu[3] = 1;
                //gotoMenu[0] = 1;
            }
        }
    }

    IEnumerator GetReligion()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getReligion"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasilReligion");
            else
            {
                print("BerhasilReligion");
                religions = new List<Religion>();
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    religions.Add(new Religion(nodes[i]["id"], nodes[i]["name"], nodes[i]["place"], nodes[i]["equipment"], nodes[i]["figure"]));
                }
                /*for (int i = 0; i < religions.Count; i++)
                {
                    print(religions[i].Name);
                    print(religions[i].Place);
                    print(religions[i].Book);
                    print(religions[i].Symbol);
                }*/
                /*for (int i = 0; i < religions.Count; i++)
                {
                    print(religions[i].Id + " " + religions[i].Name + " " + religions[i].Symbol);
                }*/
                StartCoroutine(GetCriminal());
                //gotoMenu[0] = 1;
                
            }
        }
    }

    IEnumerator GetCriminal()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(URL + "getCriminal"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
                print("Tidak berhasilCriminal");
            else
            {
                print("BerhasilCriminal");
                criminals1 = new List<Criminal>();
                criminals2 = new List<Criminal>();
                criminals3 = new List<Criminal>();
                allCriminals = new List<Criminal>();
                string json = req.downloadHandler.text;
                SimpleJSON.JSONNode nodes = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < nodes.Count; i++)
                {
                    print(nodes[i]["id"] + " " + nodes[i]["name"] + " " + nodes[i]["religionId"] + " DIFFNYA " + nodes[i]["diff"]);
                    print(nodes[i]["id"].GetType() + " " + nodes[i]["name"].GetType() + " " + nodes[i]["religionId"].GetType() + " DIFFNYA " + nodes[i]["diff"].GetType());
                    allCriminals.Add(new Criminal(nodes[i]["id"], nodes[i]["name"], religions[nodes[i]["religionId"] - 1].Name, nodes[i]["hair"], nodes[i]["outfit"], religions[nodes[i]["religionId"] - 1].Place, religions[nodes[i]["religionId"] - 1].Book, religions[nodes[i]["religionId"] - 1].Symbol, nodes[i]["diff"], new List<string>()));
                    if (nodes[i]["diff"].AsInt == 1)
                        criminals1.Add(new Criminal(nodes[i]["id"], nodes[i]["name"], religions[nodes[i]["religionId"]-1].Name, nodes[i]["hair"], nodes[i]["outfit"], religions[nodes[i]["religionId"] - 1].Place, religions[nodes[i]["religionId"] - 1].Book, religions[nodes[i]["religionId"] - 1].Symbol, nodes[i]["diff"], new List<string>()));
                    else if (nodes[i]["diff"].AsInt == 2)
                        criminals2.Add(new Criminal(nodes[i]["id"], nodes[i]["name"], religions[nodes[i]["religionId"] - 1].Name, nodes[i]["hair"], nodes[i]["outfit"], religions[nodes[i]["religionId"] - 1].Place, religions[nodes[i]["religionId"] - 1].Book, religions[nodes[i]["religionId"] - 1].Symbol, nodes[i]["diff"], new List<string>()));
                    else if(nodes[i]["diff"].AsInt == 3)
                        criminals3.Add(new Criminal(nodes[i]["id"], nodes[i]["name"], religions[nodes[i]["religionId"] - 1].Name, nodes[i]["hair"], nodes[i]["outfit"], religions[nodes[i]["religionId"] - 1].Place, religions[nodes[i]["religionId"] - 1].Book, religions[nodes[i]["religionId"] - 1].Symbol, nodes[i]["diff"], new List<string>()));
                    
                }
                print("Criminal berhasil diambil dengan data " + allCriminals.Count + " " + criminals1.Count + " " + criminals2.Count + " " + criminals3.Count); 

                gotoMenu[4] = 1;
            }
        }
    }

    private void Update()
    {
        total = 0;
        for (int i = 0; i < gotoMenu.Length;  i++)
        {
            total += gotoMenu[i];
        }
        if (total == gotoMenu.Length && change == 0)
        {
            //createCity.createCityAttribute(cityAttributes);
            createCity.createAttributeCity(attributes, attributeCities);
            createEnemy.createEnemeyDesc(criminals1, criminals2, criminals3, allCriminals);
            script.setLinks(links);
            script.setLevels(levels);
            script.setFixedCities(fixedCities);
            script.setPlayerState(0);
            //SceneManager.LoadScene("Menu");
            mulaiButton.SetActive(true);
            loadingText.SetActive(false);
            change = 1;
        }
            
    }

    public List<City> getCities()
    {
        return cities;
    }

    public void start()
    {
        SceneManager.LoadScene("Menu");
    }

    public void retry()
    {
        getCourotine();
        retryButton.SetActive(false);
        loadingText.SetActive(true);
    }

    private void failed()
    {
        retryButton.SetActive(true);
        loadingText.SetActive(false);
    }
}
