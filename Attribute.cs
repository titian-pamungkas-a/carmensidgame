using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute
{
    private int id;
    private string name;
    private string sentence;

    public Attribute(int id, string name, string sentence)
    {
        this.id = id;
        this.name = name;
        this.sentence = sentence;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Sentence { get => sentence; set => sentence = value; }
}
