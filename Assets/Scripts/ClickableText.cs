using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct NiceWord
{
    public NiceWord(int i, string w, int b, int l) { word = w; index = i; beginIndex = b; lastIndex = l; }
    public string word;
    public int index, beginIndex, lastIndex;
}

public class ClickableText : MonoBehaviour, IPointerDownHandler
{
    public Camera _camera;
    private Text _text;
    private List<ActionBlock> list;
    public Text score;
    private int _score = 0;
    public GameObject optionsPanel;
    public Button actionButton;
    public Tweet tweetWindow;
    public Text actionText;
    public Text pickText;
    public Sprite[] buttonVisuals;

    private List<Button> buttons;

    void Start()
    {
        _text = GetComponent<Text>();

        _text.text = _text.text.Replace("<b>", "<b><color=#f1f1f2ff>").Replace("</b>", "</color></b>");

        list = new List<ActionBlock>();
        list.Add(new ActionBlock(-5, "What was this 'red' nonsense?", new Replacement[] { new Replacement("violet", "violet", 5, ""), new Replacement("pink", "pink", -10, "Look at how that men is dressed, disgusting"), new Replacement("change to cyan", "cyan", -10, "Look at how that men is dressed, disgusting"), new Replacement("purple", "purple", -10, "Look at how that men is dressed, disgusting") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("stocking", "stocking", 0, ""), new Replacement("skirt", "skirt", 0, ""), new Replacement("change to skirt", "skirt", 0, ""), new Replacement("strip that suit away", "thong", 0, "That thong is gonna trend #trend #trending") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("slopes", "Slopes", 0, ""), new Replacement("stockings", "Stockings", -10, "That's just offensive."), new Replacement("", "", 0, "") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("cries", "cries", 0, ""), new Replacement("change to laughs", "laughs", -10, "WHAT? Why would the audience laugh?? #disgusted"), new Replacement("smiles awkwardly", "smiles awkwardly", 0, "") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("boredom", "boredom", -10, "Oh great! Now bored people can be freely shown on TV? #BoycottAlienCable"), new Replacement("joy", "joy", 0, ""), new Replacement("pressure", "pressure", 0, "") }));
        list.Add(new ActionBlock(-15, "For the big bang’s sake! i’m watching this show with my infants. #StopVinyl", new Replacement[] { new Replacement("stocking", "stockings", -20, "WHAT! IS THAT A STOCKING ON LIVE TV? #offended"), new Replacement("salmon skin", "salmon skin", 10, "That salmon skin is amazing. #want #need #salmon #skin #noFilter #waitWhat"), new Replacement("just remove the frigging vinyl", "no clothing", 5, "YAY! #goNudeOrGoHome") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("arms", "arms", 0, ""), new Replacement("hands", "hands", 0, ""), new Replacement("face", "face", 0, "") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("legs", "legs", 0, ""), new Replacement("food", "food", 0, ""), new Replacement("butts", "butts", 0, "I'm ok with this #loveButts") }));
        list.Add(new ActionBlock(-10, "Why gotta be green?", new Replacement[] { new Replacement("violet", "violet", 0, ""), new Replacement("pink", "pink", -10, "PINK on TV? #boycottAlienCable"), new Replacement("cyan", "cyan", -10, "Ugh. This color hurts my eyes"), new Replacement("purple", "purple", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("violet", "violet", 0, ""), new Replacement("pink", "pink", -10, "What the hell is this color."), new Replacement("cyan", "cyan", -10, "Ugh. This color hurts my eyes"), new Replacement("purple", "purple", -10, "terrible!! almost feels like a h*man show") }));
        list.Add(new ActionBlock(-15, "Look at the way they open their mouths and make those horrible noises #stopLaughter #AlienRightsNow", new Replacement[] { new Replacement("screams", "screams", 0, ""), new Replacement("cries", "cries", 0, ""), new Replacement("nods", "nods in approval", 10, ""), new Replacement("chuckles", "chuckles", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("violet", "violet", 0, ""), new Replacement("pink", "pink", -10, "PINK on TV? #boycottAlienCable"), new Replacement("cyan", "cyan", -10, "Ugh. This color hurts my eyes"), new Replacement("purple", "purple", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(-10, "That's offensive! We are #OneNation! [b]NO FLAGS[/b]", new Replacement[] { new Replacement("hamburger", "hambuger?", 0, ""), new Replacement("regular meaningless cloth", "regular meaningless cloth", 0, ""), new Replacement("vinyl jacket", "vinyl jacket", -10, "") }));
        list.Add(new ActionBlock(-15, "Laughing is disgusting. #ProtectOurChildren #StopLaughin", new Replacement[] { new Replacement("screams", "screams", 0, ""), new Replacement("cries", "cries", 0, ""), new Replacement("nods", "nods in approval", 10, ""), new Replacement("chuckles", "chuckles", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("ladder", "ladder", 0, ""), new Replacement("slope", "slope", 0, ""), new Replacement("stockings", "stockings", -15, "That makes absolutely no sense. #WTF") }));
        list.Add(new ActionBlock(-10, "PINK on TV? #boycottAlienCable", new Replacement[] { new Replacement("violet", "violet", 0, ""), new Replacement("golden", "golden", -10, "Is [b]golden[/b] even a color?"), new Replacement("cyan", "cyan", -10, "Ugh. This color hurts my eyes"), new Replacement("purple", "purple", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(-15, "WHY DO HUMANS USE THIS DISGUSTING STOCKINGS #WHY", new Replacement[] { new Replacement("shoes", "shoes", 0, ""), new Replacement("cape", "cape", 0, ""), new Replacement("vinyl pants", "vinyl pants", -10, "VINYL PANTS? Adding insult to injury!") }));
        list.Add(new ActionBlock(10, "AlienCable cares for my baby. Support #babyBlue ! It's the #bestColor", new Replacement[] { new Replacement("violet", "violet", 0, ""), new Replacement("pink", "pink", -10, "PINK on TV? #boycottAlienCable"), new Replacement("cyan", "cyan", -10, "Ugh. This color hurts my eyes #StopAlienCable"), new Replacement("change to purple", "purple", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(0, "", new Replacement[] { new Replacement("violet", "violet", 0, ""), new Replacement("pink", "pink", -10, "PINK on TV? #boycottAlienCable"), new Replacement("cyan", "cyan", -10, "Ugh. This color hurts my eyes"), new Replacement("purple", "purple", -10, "Ugh. This color hurts my eyes") }));
        list.Add(new ActionBlock(-15, "For the big bang’s sake! i’m watching this show with my infants. #StopVinyl", new Replacement[] { new Replacement("stocking", "stockings", -20, "WHAT! IS THAT A STOCKING ON LIVE TV? #offended"), new Replacement("salmon skin", "salmon skin", 10, "That salmon skin is amazing. #want #need #salmon #skin #noFilter #waitWhat"), new Replacement("just remove the frigging vinyl", "no clothing", 5, "YAY! #goNudeOrGoHome") }));
        list.Add(new ActionBlock(-15, "Laughing is disgusting. #ProtectOurChildren #StopLaughin", new Replacement[] { new Replacement("screams", "screams", 0, ""), new Replacement("cries", "cries", 0, "Weird, but at least they're not laughing!"), new Replacement("nods", "nods in approval", 5, "What a civilized audience."), new Replacement("chuckles", "chuckles", -10, "That's even worse than laughing! ") }));
        list.Add(new ActionBlock(-15, "Those bizarre mouth sounds. My child will never recover from the horror. #callMyLawyer", new Replacement[] { new Replacement("screams", "screams", 0, ""), new Replacement("cries", "cries", 0, ""), new Replacement("nods", "nods in approval", 5, "#That's it, nice and polite"), new Replacement("chuckles", "chuckles", -10, "Oh great! Now CHUCKLING can be freely shown on TV? #BoycottAlienCable") }));

        score.text = _score.ToString();

        actionText.text = "CHOOSE A WORD";
        pickText.enabled = false;

        buttons = new List<Button>();
    }

    public string ReplaceAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }

    void Update()
    {
        var textGen = _text.cachedTextGenerator;
        for (int i = 1; i < textGen.characterCount - 1; ++i)
        {
            try
            {
                if (i == 0 || i == _text.text.Length - 2) continue;
                if (_text.text[i] != '<' || _text.text[i + 1] != '/' || _text.text[i + 2] != 'c') continue;
            }
            catch (Exception) { continue; }

            Vector2 worldBottomRight = transform.TransformPoint(new Vector2(textGen.verts[i * 4 + 2].position.x, textGen.verts[i * 4 + 2].position.y));

            if (worldBottomRight.y > 215)
            {
                var word = GetWordAtIndex(i - 1);
                var block = list[word.index];
                if (block.done) continue;
                block.done = true;
                _score += block.score;
                score.text = _score.ToString();
                if (block.feedback != "") {
                    tweetWindow.NewTweet(block.feedback, block.score);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (buttons.Count > 0) buttons[0].onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (buttons.Count > 1) buttons[1].onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (buttons.Count > 2) buttons[2].onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (buttons.Count > 3) buttons[3].onClick.Invoke();
        }

    }

    void OnDrawGizmos()
    {
        var text = GetComponent<Text>();
        var textGen = text.cachedTextGenerator;
        var prevMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        for (int i = 0; i < textGen.characterCount; ++i)
        {
            Vector2 locUpperLeft = new Vector2(textGen.verts[i * 4].position.x, textGen.verts[i * 4].position.y);
            Vector2 locBottomRight = new Vector2(textGen.verts[i * 4 + 2].position.x, textGen.verts[i * 4 + 2].position.y);

            Vector3 mid = (locUpperLeft + locBottomRight) / 2.0f;
            Vector3 size = locBottomRight - locUpperLeft;

            Gizmos.DrawWireCube(mid, size);
        }
        Gizmos.matrix = prevMatrix;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.Use();
        int index = GetIndexOfClick(_camera.ScreenPointToRay(eventData.position));
        if (index != -1)
        {
            var niceWord = GetWordAtIndex(index);
            if (niceWord.index >= 0)
            {
                var block = list[niceWord.index];
                buttons.Clear();
                foreach (Transform child in optionsPanel.transform) GameObject.Destroy(child.gameObject);
                actionText.text = niceWord.word.ToUpper();
                pickText.enabled = true;
                int i = 0;
                foreach (var l in block.options)
                {
                    var b = GameObject.Instantiate(actionButton);
                    buttons.Add(b);
                    b.transform.SetParent(optionsPanel.transform);
                    b.name = l.param;
                    b.GetComponentInChildren<Text>().text = l.name;
                    b.GetComponentInChildren<Image>().sprite = buttonVisuals[i++];
                    b.onClick.AddListener(
                        () => {
                            buttons.Clear();
                            foreach (Transform child in optionsPanel.transform) GameObject.Destroy(child.gameObject);
                            actionText.text = "CHOOSE A WORD";
                            pickText.enabled = false;
                            _text.text= _text.text.Remove(niceWord.beginIndex - 20, niceWord.lastIndex - niceWord.beginIndex + 20).Insert(niceWord.beginIndex - 20, "<b><color=#00ff00ff>" + l.param);
                            block.score = l.deltaScore;
                            block.feedback = l.feedback;
                            }
                        );
                }
            }
        }
    }

    int GetIndexOfClick(Ray ray)
    {
        Ray localRay = new Ray(
          transform.InverseTransformPoint(ray.origin),
          transform.InverseTransformDirection(ray.direction));

        Vector3 localClickPos =
          localRay.origin +
          localRay.direction / localRay.direction.z * (transform.localPosition.z - localRay.origin.z);

        Debug.DrawRay(transform.TransformPoint(localClickPos), Vector3.up / 10, Color.red, 20.0f);

        var textGen = _text.cachedTextGenerator;
        for (int i = 0; i < textGen.characterCount; ++i)
        {
            Vector2 locUpperLeft = new Vector2(textGen.verts[i * 4].position.x, textGen.verts[i * 4].position.y);
            Vector2 locBottomRight = new Vector2(textGen.verts[i * 4 + 2].position.x, textGen.verts[i * 4 + 2].position.y);

            if (localClickPos.x >= locUpperLeft.x &&
             localClickPos.x <= locBottomRight.x &&
             localClickPos.y <= locUpperLeft.y &&
             localClickPos.y >= locBottomRight.y
             )
            {
                return i;
            }
        }

        return -1;
    }

    NiceWord GetWordAtIndex(int index)
    {
        int begIndex = -1;
        int marker = index;
        while (begIndex == -1)
        {
            marker--;
            if (marker < 0 || (_text.text[marker] == '>' && _text.text[marker-1] == 'b'))
            {
                return new NiceWord(-1, "", -1, -1);
            }
            else if (_text.text[marker] == '>' && _text.text[marker-1] == 'f')
            {
                begIndex = marker;
                break;
            }
        }

        int lastIndex = -1;
        marker = index;
        while (lastIndex == -1)
        {
            marker++;
            if (marker > _text.text.Length - 1)
            {
                return new NiceWord(-1, "", -1, -1);
            }
            else if (_text.text[marker] == '<')
            {
                lastIndex = marker;
                break;
            }
        }

        string source = _text.text.Substring(0, lastIndex);
        int count = -2;
        foreach (char c in source) if (c == '<') count++;

        return new NiceWord(count/4, _text.text.Substring(begIndex + 1, lastIndex - begIndex - 1), begIndex + 1, lastIndex);
    }
}
