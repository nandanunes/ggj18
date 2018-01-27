using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action
{
    public Action(string t, string p) { type = t; param = p; }
    public string type;
    public string param;
}

public class ActionBlock {
    public int score = -5;
    public List<Action> list;
    public bool done = false;
}
