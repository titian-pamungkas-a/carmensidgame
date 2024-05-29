using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    private int id;
    private string name;
    private string? house;
    private string? food;
    private string? art;
    private string? song;
    private string? clothes;
    private string? destination;
    private string? history;
    private string? commodity;

    public City(int id, string name, string house, string food, string art, string song, string clothes, string destination, string history, string commodity)
    {
        this.id = id;
        this.name = name;
        this.house = house;
        this.food = food;
        this.art = art;
        this.song = song;
        this.clothes = clothes;
        this.destination = destination;
        this.history = history;
        this.commodity = commodity;
    }


    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string House { get => house; set => house = value; }
    public string Food { get => food; set => food = value; }
    public string Art { get => art; set => art = value; }
    public string Clothes { get => clothes; set => clothes = value; }
    public string Destination { get => destination; set => destination = value; }
    public string History { get => history; set => history = value; }
    public string Commodity { get => commodity; set => commodity = value; }
    public string Song { get => song; set => song = value; }
}
