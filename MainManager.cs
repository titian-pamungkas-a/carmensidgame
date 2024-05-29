using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public List<City> cities;
    public List<CityUser> cityUsers;
    public List<CityAttribute> cityAttribute;
    public List<string> allPlace;
    public List<Link> links;
    public List<Level> levels;
    public List<FixedCity> fixedCities;
    public List<Criminal> criminals1, criminals2, criminals3, allCriminals;
    public int currentLevel, fixedLevel, playerState = 0; // win=1, lose=-1
    public int[,] graphs;
    public List<AttributeCity> attributeCities;
    public List<Attribute> attributes;
    public int activeTrans;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    

    public void setCities(List<City> apiCities)
    {
        if (Instance != null)
        {
            MainManager.Instance.cities = new List<City>(apiCities);
            for (int i = 0; i < MainManager.Instance.cities.Count; i++)
            {
                print(MainManager.Instance.cities[i].Id);
                print(MainManager.Instance.cities[i].Name);
                print(MainManager.Instance.cities[i].House);
                print(MainManager.Instance.cities[i].Food);
                print(MainManager.Instance.cities[i].Art);
                print(MainManager.Instance.cities[i].Commodity);
            }
            SceneManager.LoadScene("Menu");
        }
    }

    public void setCityUsers(List<CityUser> apiCityUsers)
    {
        
        if (Instance != null)
        {
            MainManager.Instance.cityUsers = new List<CityUser>(apiCityUsers);
        }
    }

    public List<CityUser> getCityUsers()
    {
        return MainManager.Instance.cityUsers;
    }

    public void setCityAttribute(List<CityAttribute> apiCityUsers)
    {
        if (Instance != null)
        {
            MainManager.Instance.cityAttribute = new List<CityAttribute>(apiCityUsers);
        }
    }

    public List<CityAttribute> getCityAttribute()
    {
        return MainManager.Instance.cityAttribute;
    }

    public List<string> getAllPlace()
    {
        allPlace = new List<string>();
        allPlace.Add("Alun-alun");
        allPlace.Add("Taman Kota");
        allPlace.Add("Mall");
        return allPlace;
    }

    public void setLinks(List<Link> apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.links = new List<Link>(apiLink);
            graphs = new int[getCityUsers().Count+1, getCityUsers().Count + 1];
            for (int i = 0; i < getCityUsers().Count+1; i++)
            {
                for (int j = 0; j < getCityUsers().Count + 1; j++)
                {
                    graphs[i, j] = 0;
                }
            }
            for (int i = 0; i < apiLink.Count; i++)
            {
                graphs[apiLink[i].City1, apiLink[i].City2] = apiLink[i].DistanceHour;
                graphs[apiLink[i].City2, apiLink[i].City1] = apiLink[i].DistanceHour;
            }
        }
    }

    public List<Link> getLinks()
    {
        return MainManager.Instance.links;
    }

    public int[,] getGraphs()
    {
        return graphs;
    }

    public void setLevels(List<Level> apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.levels = new List<Level>(apiLink);
        }
    }

    public List<Level> getLevels()
    {
        return MainManager.Instance.levels;
    }

    public void setCurrentLevel(int apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.currentLevel = apiLink;
        }
    }

    public int getCurrentLevel()
    {
        return MainManager.Instance.currentLevel;
    }

    public void setFixedCities(List<FixedCity> apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.fixedCities = apiLink;
        }
    }

    public List<FixedCity> getFixedCities()
    {
        return MainManager.Instance.fixedCities;
    }

    public void setFixedLevel(int apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.fixedLevel = apiLink;
        }
    }

    public int getFixedLevel()
    {
        return MainManager.Instance.fixedLevel;
    }

    public void setCriminals(List<Criminal> apiLink1, List<Criminal> apiLink2, List<Criminal> apiLink3, List<Criminal> apiLink4)
    {
        if (Instance != null)
        {
            MainManager.Instance.criminals1 = new List<Criminal>(apiLink1);
            MainManager.Instance.criminals2 = new List<Criminal>(apiLink2);
            MainManager.Instance.criminals3 = new List<Criminal>(apiLink3);
            MainManager.Instance.allCriminals = new List<Criminal>(apiLink4);
        }
        SetActiveTrans(0);
    }

    public void setAllCriminals()
    {

    }

    public List<Criminal> getCriminals1()
    {
        return MainManager.Instance.criminals1;
    }

    public List<Criminal> getCriminals2()
    {
        return MainManager.Instance.criminals2;
    }

    public List<Criminal> getCriminals3()
    {
        return MainManager.Instance.criminals3;
    }

    public List<Criminal> getAllCriminal()
    {
        return MainManager.Instance.allCriminals;
    }

    public void setPlayerState(int apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.playerState = apiLink;
        }
    }

    public int getPlayerState()
    {
        return MainManager.Instance.playerState;
    }

    public void setAttributeCities(List<AttributeCity> apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.attributeCities = apiLink;
        }
    }

    public List<AttributeCity> GetAttributeCities()
    {
        return MainManager.Instance.attributeCities;
    }

    public void setAttributes(List<Attribute> apiLink)
    {
        if (Instance != null)
        {
            MainManager.Instance.attributes = apiLink;
        }
    }

    public List<Attribute> GetAttributes()
    {
        return MainManager.Instance.attributes;
    }

    public void SetActiveTrans(int num)
    {
        if (Instance != null)
            MainManager.Instance.activeTrans = num;
    }

    public int GetActiveTrans()
    {
        return MainManager.Instance.activeTrans;
    }



}
