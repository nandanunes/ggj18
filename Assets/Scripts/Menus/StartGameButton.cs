using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(GoToTutorial());
        }
	}

    IEnumerator GoToTutorial()
    {
        SceneManager.LoadScene("TutorialScreen");
        yield return null;
    }
}
