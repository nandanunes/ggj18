using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelClear : MonoBehaviour {
    public Text buttonText;

    private void Start()
    {
        if (PlayerProgression.currentLevel + 1 <= PlayerProgression.totalLevels)
        {
            buttonText.text = "Game finished!\nBack to Title";
        }
    }

    public void NextLevel () {
        PlayerProgression.currentLevel++;
        SceneManager.LoadScene("Gameplay");
	}
}
