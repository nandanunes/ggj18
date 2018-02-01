using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {

    private Transform arrow;
    private Vector3 neutralPos;
    private int score = 0;

    // Use this for initialization
    void Start() {
        arrow = transform.GetChild(0).transform;
        neutralPos = arrow.position;
    }

    // Update is called once per frame
    void Update() {
        arrow.position = Vector3.Lerp(arrow.position, neutralPos + new Vector3(score * 10, 0, 0), 0.1f);
    }

    public void SetScore(int s)
    {
        score = s;
        score = Mathf.Clamp(score, -30, 30);
    }
}
