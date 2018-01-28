using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tweet : MonoBehaviour {
    public Text usernameText;
    public Text channelText;
    public Text dateText;
    public Text message;
    List<string> channelNames;
    List<string> userNames;

    void Start () {
        channelNames = new List<string>(){
            "a", "b"
        };
        userNames = new List<string>(){
            "c", "d"
        };
        NewTweet("Another Earth TV show is about to start! So excited!", 0);
    }
	
	void Update () {
		
	}
    public void NewTweet(string text, int score)
    {
        message.text = text;
        if (score > 0)
        {

        }
        else if (score < 0)
        {

        }
        usernameText.text = userNames[Random.Range(0, userNames.Count)];
        channelText.text = channelNames[Random.Range(0, channelNames.Count)];
        dateText.text = System.DateTime.Now.ToShortDateString();
    }
}
