using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCity : MonoBehaviour
{
    private List<City> cities;
    private List<CityAttribute> cityAttributes;
    private List<CityUser> cityUsers, cityUsers2;
    //private List<CityUser2> cityUsers2;
    [SerializeField]
    private GameObject mainManager;
    private MainManager script;
    //private List<string> allPlace;
    
    void Start()
    {
        script = mainManager.GetComponent<MainManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getCities(List<City> apiCities)
    {
        cities = new List<City>(apiCities);
        cityUsers = new List<CityUser>();
        for (int i = 0; i < cities.Count; i++)
        {
            /*cityUsers.Add(new CityUser(cities[i].Id, cities[i].Name, "1", new List<string>(){ 
                createHouse(cities[i].House),
                createFood(cities[i].Food),
                createArt(cities[i].Art),
                createSong(cities[i].Song),
                createClothes(cities[i].Clothes),
                createDestination(cities[i].Destination),
                createHistory(cities[i].History),
                createCommodity(cities[i].Commodity),
            }));*/
            cityUsers[i].Desc.RemoveAll(item => item == null);
        }
        //script.setCityUsers(cityUsers);
    }

    public void createCityAttribute(List<CityAttribute> apiCities)
    {
        cityAttributes = new List<CityAttribute>(apiCities);
        cityUsers2 = new List<CityUser>();
        for (int i = 0; i < cityAttributes.Count; i++)
        {
            /*cityUsers2.Add(new CityUser(cityAttributes[i].Id, cityAttributes[i].Name, cityAttributes[i].ImagePath, new List<string>()));
            createHouse2(cityAttributes[i].Houses, i);
            createFood2(cityAttributes[i].Foods, i);
            createArt2(cityAttributes[i].Arts, i);
            createSong2(cityAttributes[i].Songs, i);
            createClothes2(cityAttributes[i].Clothes, i);
            createDestination2(cityAttributes[i].Destinations, i);
            createHistory2(cityAttributes[i].Histories, i);
            //createCommodity2(cityAttributes[i].Commodities, i);
            createNews2(cityAttributes[i].News, i);
            createTown2(cityAttributes[i].Towns, i);*/
        }
        
        //script.setCityUsers(cityUsers2);
    }

    public void createAttributeCity(List<Attribute> attributes, List<AttributeCity> attributeCities)
    {
        List<CityUser> cityUsers3 = new List<CityUser>();
        for (int i = 0; i < attributeCities.Count; i++)
        {
            cityUsers3.Add(new CityUser(attributeCities[i].Id, attributeCities[i].Name, attributeCities[i].ImagePath, new List<string>(), attributeCities[i].Treasure, attributeCities[i].TreasurePlace));
            for (int j = 0; j < attributes.Count; j++)
            {
                for (int k = 0; k < attributeCities[i].Clue[j].Count; k++)
                {
                    cityUsers3[i].Desc.Add(attributes[j].Sentence.Replace("%", "<b><u>" + attributeCities[i].Clue[j][k] + "</u></b>"));
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            int temp = 0;
            for (int j = 0; j < attributes.Count; j++)
            {
                for (int k = 0; k < attributeCities[i].Clue[j].Count; k++)
                {
                    print(cityUsers3[i].Name + " " + attributes[j].Name + " " + cityUsers3[i].Desc[temp]);
                    temp++;
                }
            }
        }
        script.setCityUsers(cityUsers3);
        script.setAttributeCities(attributeCities);
        script.setAttributes(attributes);
    }

    

    private string createHouse(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Tersangka ingin melihat " + str + " secara langsung";
        return sen;
    }

    private void createHouse2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Tersangka ingin melihat " + strs[i] + " secara langsung";
            cityUsers2[num].Desc.Add(str);
        }  
    }

    private string createFood(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Sumber terpercaya melihat tersangka mampir di warung " + str;
        return sen;
    }

    private void createFood2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Sumber terpercaya melihat tersangka mampir di warung " + strs[i];
            cityUsers2[num].Desc.Add(str);
        }
    }

    private string createArt(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Beberapa saksi melihat tersangka membawa buku tentang " + str;
        return sen;
    }

    private void createArt2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Beberapa saksi melihat tersangka membawa buku tentang " + strs[i];
            cityUsers2[num].Desc.Add(str);
        }
    }

    private string createSong(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Saksi mendengar lagu '" + str + "' saat melewati tersangka";
        return sen;
    }

    private void createSong2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Saksi mendengar lagu '" + strs[i] + "' saat melewati tersangka";
            cityUsers2[num].Desc.Add(str);
        }
    }

    private string createClothes(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Tersangka terlihat membawa pakaian " + str + " saat pergi ke Bandara";
        return sen;
    }

    private void createClothes2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Tersangka terlihat membawa pakaian " + strs[i] + " saat pergi ke Bandara";
            cityUsers2[num].Desc.Add(str);
        }
    }

    private string createDestination(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Orang setempat mendapati pamflet " + str + " jatuh di tengah jalan. Dicurigai pamflet merupakan milik tersangka";
        return sen;
    }

    private void createDestination2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Orang setempat mendapati pamflet " + strs[i] + " jatuh di tengah jalan. Dicurigai pamflet merupakan milik tersangka";
            cityUsers2[num].Desc.Add(str);
        }
    }

    private string createHistory(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Sejarawan setempat mengaku bahwa tersangka bertanya kepadanya tentang " + str;
        return sen;
    }

    private void createHistory2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Sejarawan setempat mengaku bahwa tersangka bertanya kepadanya tentang " + strs[i];
            cityUsers2[num].Desc.Add(str);
        }
    }

    private string createCommodity(string str)
    {
        if (str == "" || str == null)
        {
            return null;
        }
        string sen = "Orang yang dicari ingin mendapatkan " + str + " langsung dari tempatnya";
        return sen;
    }

    private void createCommodity2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Orang yang dicari ingin mendapatkan " + strs[i] + " langsung dari tempatnya";
            cityUsers2[num].Desc.Add(str);
        }
    }

    private void createNews2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Orang yang dicari ingin memastikan berita tentang " + strs[i];
            cityUsers2[num].Desc.Add(str);
        }
    }

    private void createTown2(List<string> strs, int num)
    {
        for (int i = 0; i < strs.Count; i++)
        {
            string str = "Orang yang dicari ingin pergi ke kota " + strs[i];
            cityUsers2[num].Desc.Add(str);
        }
    }
}
