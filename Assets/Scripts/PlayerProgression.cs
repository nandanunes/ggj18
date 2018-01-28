using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour {
    public static int currentLevel = 0;
    public static int currentLevelScore = 0;
    public static int totalScore = 0;
    public static int totalLevels = 1;
	// Use this for initialization

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
