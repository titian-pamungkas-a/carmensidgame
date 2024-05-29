using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityUser
{
    private int id;
    private string name;
    private string imagePath;
    private List<string> desc;
    private string treasure;
    private string treasurePlace;

    public CityUser(int id, string name, string imagePath, List<string> desc, string treasure, string treasurePlace)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.imagePath = imagePath;
        this.treasure = treasure;
        this.treasurePlace = treasurePlace;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public List<string> Desc { get => desc; set => desc = value; }
    public string ImagePath { get => imagePath; set => imagePath = value; }
    public string Treasure { get => treasure; set => treasure = value; }
    public string TreasurePlace { get => treasurePlace; set => treasurePlace = value; }
}
