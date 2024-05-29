using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCity
{
    private int id;
    private string name;
    private List<int> scenarios;
    private int criminalId;
    private int totalTime;

    public FixedCity(int id, string name, List<int> scenarios, int criminalId, int totalTime)
    {
        this.id = id;
        this.name = name;
        this.scenarios = scenarios;
        this.criminalId = criminalId;
        this.totalTime = totalTime;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public List<int> Scenarios { get => scenarios; set => scenarios = value; }
    public int CriminalId { get => criminalId; set => criminalId = value; }
    public int TotalTime { get => totalTime; set => totalTime = value; }
}
