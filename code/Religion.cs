using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Religion
{
    private int id;
    private string name;
    private string place;
    private string book;
    private string symbol;

    public Religion(int id, string name, string place, string book, string symbol)
    {
        this.id = id;
        this.name = name;
        this.place = place;
        this.book = book;
        this.symbol = symbol;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Place { get => place; set => place = value; }
    public string Book { get => book; set => book = value; }
    public string Symbol { get => symbol; set => symbol = value; }
}
