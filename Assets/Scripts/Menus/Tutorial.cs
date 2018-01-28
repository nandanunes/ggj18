using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
    public Sprite[] images;
    public Text pageText;
    public int currentPage = 0;
    public GameObject previousButton;
    public Image displayImage;

	// Use this for initialization
	void Start () {
        updateTutorialImage();
    }
	
	// Update is called once per frame
	void Update () {
        pageText.text = (currentPage + 1) + "/" + images.Length;
	}

    public void AdvancePage(int increment)
    {
        currentPage += increment;
        currentPage = Mathf.Max(0, currentPage);
        if (currentPage == 0)
        {
            previousButton.SetActive(false);
        }
        else if (!previousButton.activeSelf)
        {
            previousButton.SetActive(true);
        }
        if (currentPage >= images.Length)
        {
            StartGame();
        } else updateTutorialImage();
    }

    void updateTutorialImage()
    {
        displayImage.sprite = images[currentPage];
    }

    void StartGame()
    {
        SceneManager.LoadScene("TextTest");
    }
}
