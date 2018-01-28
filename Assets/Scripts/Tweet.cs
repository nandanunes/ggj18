﻿using System.Collections;
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
            "@theRealHuman", "@BlorpJohnson3000", "@EqualityForAllAliens_", "@theGoodAlien99", "@OnlyVibez", "@MillaliensKilledPluto", "@SwipeMeLeftPlease", "@MrsCatsfire", "@SubscribeMyChannel", "@FamilyGal007", "@IHateAliens_x", "@JessicasGirlfriend", "@freeAlienBlorpNow", "@CoverYourMouth028", "@boredAlienJason", "@NeverFinishWatching", "@ChannelCriticMagazine", "@SalamuraUzaylıHuseyin", "@BigGreenHead23", "@ETRowling", "@lostInTransmission", "@endlessGreenVoid", "@spaceWonderer", "@BbCita"
        };
        userNames = new List<string>(){
            "JoTy Zurg", "Blorpee KeeToo", "OinkDeWoo", "Qwarp Letu", "Ptilm Zy", "John Smith", "Rjuk Rjuk", "Zlorpee Yipp", "Platee Groolp", "YobYob Pee", "Kippz Tipz", "Wlept Tlepsorp", "Rre Flixbus", "Xyud Mamo", "Dobidob Grop", "Fanama Plut", "Quba Tuba", "Oliq Rot"
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
            message.color = Color.green;
        }
        else if (score < 0)
        {
            message.color = Color.red;
        }
        else
        {
            message.color = Color.white;
        }
        usernameText.text = userNames[Random.Range(0, userNames.Count)];
        channelText.text = channelNames[Random.Range(0, channelNames.Count)];
        dateText.text = System.DateTime.Now.ToShortDateString();
    }
}
