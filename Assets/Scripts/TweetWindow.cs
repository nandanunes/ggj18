using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweetWindow : MonoBehaviour {
    public Text usernameText;
    public Text channelText;
    public Text dateText;
    public Text message;
    private List<string> channelNames;
    private List<string> userNames;
    private Animator anim;
    private List<Tweet> tweetList;

    void Start () {
        anim = GetComponent<Animator>();
        channelNames = new List<string>(){
            "@theRealHuman", "@BlorpJohnson3000", "@EqualityForAllAliens_", "@theGoodAlien99", "@OnlyVibez", "@MillaliensKilledPluto", "@SwipeMeLeftPlease", "@MrsCatsfire", "@SubscribeMyChannel", "@FamilyGal007", "@IHateAliens_x", "@JessicasGirlfriend", "@freeAlienBlorpNow", "@CoverYourMouth028", "@boredAlienJason", "@NeverFinishWatching", "@ChannelCriticMagazine", "@SalamuraUzaylıHuseyin", "@BigGreenHead23", "@ETRowling", "@lostInTransmission", "@endlessGreenVoid", "@spaceWonderer", "@BbCita"
        };
        userNames = new List<string>(){
            "JoTy Zurg", "Blorpee KeeToo", "OinkDeWoo", "Qwarp Letu", "Ptilm Zy", "John Smith", "Rjuk Rjuk", "Zlorpee Yipp", "Platee Groolp", "YobYob Pee", "Kippz Tipz", "Wlept Tlepsorp", "Rre Flixbus", "Xyud Mamo", "Dobidob Grop", "Fanama Plut", "Quba Tuba", "Oliq Rot"
        };
        tweetList = new List<Tweet>();
        tweetList.Add(new Tweet("Another Earth TV show is about to start! So excited!", 0));
        StartCoroutine(ScheduleTweetList());
    }

    IEnumerator ScheduleTweetList()
    {
        while (true)
        {
            if(tweetList.Count > 0)
            {
                NewTweet(tweetList[0]);
                tweetList.RemoveAt(0);
                yield return new WaitForSeconds(1);
            }
            yield return null;
            

        }
    }
	
	void Update () {
		
	}
    public void NewTweet(Tweet t)
    {
        message.text = t.text;
        anim.SetTrigger("newTweet");
        anim.SetBool("bad", false);
        if (t.score > 0)
        {
            message.color = Color.green;
        }
        else if (t.score < 0)
        {
            message.color = Color.red;
            anim.SetBool("bad", true);
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

[System.Serializable]
public struct Tweet
{
    public string text;
    public int score;
    public Tweet(string t, int s)
    {
        text = t;
        score = s;
    }

}
