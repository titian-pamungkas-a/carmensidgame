using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityUser2 : MonoBehaviour
{
    private int id;
    private string name;
    private List<string> desc;

    public CityUser2(int id, string name, List<string> desc)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public List<string> Desc { get => desc; set => desc = value; }
}
