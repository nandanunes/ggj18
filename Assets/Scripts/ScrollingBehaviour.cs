using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviour : MonoBehaviour {
    public float normalSpeed;
    public float fastSpeed;
    [HideInInspector]
    public float currentSpeed;
	// Use this for initialization
	void Start () {
        currentSpeed = normalSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, currentSpeed, 0);
	}
    public void SetSpeed(string speed)
    {
        switch (speed)
        {
            case "normal":
                currentSpeed = normalSpeed;
                break;
            case "fast":
                currentSpeed = fastSpeed;
                break;
        }
    }
}
