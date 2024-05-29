using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAttribute
{
    private int id;
    private string name;
    private List<string> arts;
    private List<string> clothes;
    private List<string> destinations;
    private List<string> foods;
    private List<string> histories;
    private List<string> houses;
    private List<string> news;
    private List<string> songs;
    //private List<string> commodities;
    private List<string> towns;
    private string imagePath;
    

    public CityAttribute(int id, string name, List<string> arts, List<string> clothes, List<string> destinations, List<string> histories, List<string> news, List<string> songs, List<string> foods, List<string> towns, List<string> houses, string imagePath)
    {
        this.id = id;
        this.name = name;
        this.arts = arts;
        this.clothes = clothes;
        this.destinations = destinations;
        this.histories = histories;
        this.news = news;
        this.songs = songs;
        this.towns = towns;
        this.houses = houses;
        this.foods = foods;
        //this.commodities = commodities;
        this.imagePath = imagePath;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public List<string> Arts { get => arts; set => arts = value; }
    public List<string> Clothes { get => clothes; set => clothes = value; }
    public List<string> Destinations { get => destinations; set => destinations = value; }
    public List<string> Foods { get => foods; set => foods = value; }
    public List<string> Histories { get => histories; set => histories = value; }
    public List<string> Houses { get => houses; set => houses = value; }
    public List<string> News { get => news; set => news = value; }
    public List<string> Songs { get => songs; set => songs = value; }
    public List<string> Towns { get => towns; set => towns = value; }
    public string ImagePath { get => imagePath; set => imagePath = value; }
    //public List<string> Commodities { get => commodities; set => commodities = value; }
}
