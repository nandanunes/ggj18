using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Replacement
{
    public Replacement(string n, string p, int d) { name = n; param = p; deltaScore = d; }
    public string name;
    public string param;
    public int deltaScore;
}

public class ActionBlock {
    public int score = -5;
    public List<Replacement> options = new List<Replacement>(new Replacement[]{ new Replacement("Trocar pra nome alien","XYZ567", 2), new Replacement("Troca pra doggo", "dog", 3) });
    public bool done = false;
}
