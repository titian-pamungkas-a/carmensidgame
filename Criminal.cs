using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal
{
    private int id;
    private string name;
    private string hobbyName;
    private string hair;
    private string outfit;
    private string hobbyPlace;
    private string hobbyEquipment;
    private string hobbyFigure;
    private int diff;
    private List<string> desc;
    public Criminal(int id, string name, string religionName, string hair, string outfit, string religionPlace, string religionBook, string religionSymbol, int diff, List<string> desc)
    {
        this.id = id;
        this.name = name;
        this.hobbyName = religionName;
        this.hair = hair;
        this.outfit = outfit;
        this.hobbyPlace = religionPlace;
        this.hobbyEquipment = religionBook;
        this.hobbyFigure = religionSymbol;
        this.diff = diff;
        this.desc = desc;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    
    public string Hair { get => hair; set => hair = value; }
    public string Outfit { get => outfit; set => outfit = value; }
    
    public int Diff { get => diff; set => diff = value; }
    public List<string> Desc { get => desc; set => desc = value; }
    public string HobbyName { get => hobbyName; set => hobbyName = value; }
    public string HobbyPlace { get => hobbyPlace; set => hobbyPlace = value; }
    public string HobbyEquipment { get => hobbyEquipment; set => hobbyEquipment = value; }
    public string HobbyFigure { get => hobbyFigure; set => hobbyFigure = value; }
}
