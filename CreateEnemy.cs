using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    private List<Criminal> criminals1, criminals2, criminals3, allCriminals;
    private GameObject mainManager;
    private MainManager script;

    void Start()
    {
        mainManager = GameObject.Find("MainManager");
        script = mainManager.GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createEnemeyDesc(List<Criminal> c1, List<Criminal> c2, List<Criminal> c3, List<Criminal> c)
    {
        criminals1 = new List<Criminal>(c1);
        criminals2 = new List<Criminal>(c2);
        criminals3 = new List<Criminal>(c3);
        allCriminals = new List<Criminal>(c);
        for (int i = 0; i < criminals1.Count; i++)
        {
            criminals1[i].Desc.Add(religion1(criminals1[i].HobbyPlace));
            criminals1[i].Desc.Add(religion2(criminals1[i].HobbyEquipment));
            criminals1[i].Desc.Add(religion3(criminals1[i].HobbyFigure));
        }
        for (int i = 0; i < criminals2.Count; i++)
        {
            criminals2[i].Desc.Add(religion1(criminals2[i].HobbyPlace));
            criminals2[i].Desc.Add(religion2(criminals2[i].HobbyEquipment));
            criminals2[i].Desc.Add(religion3(criminals2[i].HobbyFigure));
            criminals2[i].Desc.Add(hair1(criminals2[i].Hair));
        }
        for (int i = 0; i < criminals3.Count; i++)
        {
            criminals3[i].Desc.Add(religion1(criminals3[i].HobbyPlace));
            criminals3[i].Desc.Add(religion2(criminals3[i].HobbyEquipment));
            criminals3[i].Desc.Add(religion3(criminals3[i].HobbyFigure));
            criminals3[i].Desc.Add(hair1(criminals3[i].Hair));
            criminals3[i].Desc.Add(outfit1(criminals3[i].Outfit));
        }
        for (int i = 0; i < allCriminals.Count; i++)
        {
            allCriminals[i].Desc.Add(religion1(allCriminals[i].HobbyPlace));
            allCriminals[i].Desc.Add(religion2(allCriminals[i].HobbyEquipment));
            allCriminals[i].Desc.Add(religion3(allCriminals[i].HobbyFigure));
            if (allCriminals[i].Hair != null)
                allCriminals[i].Desc.Add(hair1(allCriminals[i].Hair));
            if (allCriminals[i].Outfit != null)
                allCriminals[i].Desc.Add(outfit1(allCriminals[i].Outfit));
        }
        
        script.setCriminals(criminals1, criminals2, criminals3, allCriminals);
    }
    private string religion1(string str)
    {
        return "Tersangka bertanya mengenai lokasi " + str + " di dalam kota.";
    }

    private string religion2(string str)
    {
        return "Saksi setempat melihat tersangka membeli " + str + " di pusat kota.";
    }

    private string religion3(string str)
    {
        return "Kasir toko buku menyebutkan bahwa tersangka membeli biografi " + str + " beberapa saat yang lalu";
    }

    private string hair1(string str)
    {
        return "Warga sekitar melihat orang berambut " + str + " melewati daerah ini";
    }

    private string outfit1(string str)
    {
        return "Tersangka terlihat memakai atasan " + str + " oleh masayarakat";
    }
}
