using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {

    private Transform arrow;
    private Vector3 neutralPos;
    private int score;
    private Animator anim;
    public Threshold[] thresholds;
    public bool hitZero = false;

    // Use this for initialization
    void Start() {
        arrow = transform.GetChild(0).transform;
        neutralPos = arrow.position;
        anim = GetComponent<Animator>();
        score = thresholds[2].score;
    }

    // Update is called once per frame
    void Update() {
        arrow.position = Vector3.Lerp(arrow.position, neutralPos + new Vector3(score * 10, 0, 0), 0.1f);
        if (score < thresholds[1].score)
        {
            anim.SetBool("alert", score < thresholds[1].score);
            anim.speed = 1 - score / thresholds[1].score;
        }

        if (arrow.position == neutralPos)
        {
            hitZero = true;
        }
    }

    public void SetScore(int s)
    {
        score = s;
        score = Mathf.Clamp(score, thresholds[0].score, thresholds[2].score);
    }
}

[System.Serializable]
public struct Threshold
{
    public int score;
    public GameObject marker;
}
