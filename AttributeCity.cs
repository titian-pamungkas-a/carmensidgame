using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeCity
{
    private int id;
    private string name;
    private List<string>[] clue;
    private string imagePath;
    private string treasure;
    private string treasurePlace;
    private string detail;

    public AttributeCity(int id, string name, List<string>[] clue, string imagePath, string treasure, string treasurePlace, string detail)
    {
        this.id = id;
        this.name = name;
        this.clue = clue;
        this.imagePath = imagePath;
        this.treasure = treasure;
        this.treasurePlace = treasurePlace;
        this.detail = detail;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public List<string>[] Clue { get => clue; set => clue = value; }
    public string ImagePath { get => imagePath; set => imagePath = value; }
    public string Detail { get => detail; set => detail = value; }
    public string Treasure { get => treasure; set => treasure = value; }
    public string TreasurePlace { get => treasurePlace; set => treasurePlace = value; }
}
