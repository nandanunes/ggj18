using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Replacement
{
    public Replacement(string n, string p, int d, string f) { name = n; param = p; deltaScore = d; feedback = f; }
    public string name;
    public string param;
    public int deltaScore;
    public string feedback;
}

public class ActionBlock {
    public ActionBlock(int s, string f, Replacement[] o) { score = s; feedback = f;  options = o; }
    public int score = 0;
    public string feedback = "";
    public Replacement[] options;
    public bool done = false;
}


