using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link
{
    private int city1;
    private int city2;
    private int distanceHour;

    public Link(int city1, int city2, int distanceHour)
    {
        this.city1 = city1;
        this.city2 = city2;
        this.distanceHour = distanceHour;
    }

    public int City1 { get => city1; set => city1 = value; }
    public int City2 { get => city2; set => city2 = value; }
    public int DistanceHour { get => distanceHour; set => distanceHour = value; }
}
