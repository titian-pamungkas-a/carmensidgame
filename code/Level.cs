using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private int levelCount;
    private int cityCount;
    private int totalTime;
    private int criminalLevel;

    public Level(int levelCount, int cityCount, int totalTime, int criminalLevel)
    {
        this.levelCount = levelCount;
        this.cityCount = cityCount;
        this.totalTime = totalTime;
        this.criminalLevel = criminalLevel;
    }

    public int LevelCount { get => levelCount; set => levelCount = value; }
    public int CityCount { get => cityCount; set => cityCount = value; }
    public int TotalTime { get => totalTime; set => totalTime = value; }
    public int CriminalLevel { get => criminalLevel; set => criminalLevel = value; }
}
