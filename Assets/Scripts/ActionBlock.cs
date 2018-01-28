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
    public ActionBlock(int s, Replacement[] o) { score = s; options = o; }
    public int score = -5;
    public Replacement[] options;
    public bool done = false;
}


