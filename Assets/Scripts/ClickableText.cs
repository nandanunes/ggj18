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
    public Text actionText;
    public Text pickText;
    public Sprite[] buttonVisuals;

    private List<Button> buttons;

    void Start()
    {
        _text = GetComponent<Text>();

        _text.text = _text.text.Replace("<b>", "<b><color=#f1f1f2ff>").Replace("</b>", "</color></b>");

        list = new List<ActionBlock>();
        list.Add(new ActionBlock(-5, new Replacement[] { new Replacement("change to violet", "violet", 5), new Replacement("change to pink", "pink", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(0, new Replacement[] { new Replacement("change to stocking", "stocking", 0), new Replacement("change to skirt", "skirt", 0), new Replacement("change to skirt", "skirt", 0) }));
        list.Add(new ActionBlock(0, new Replacement[] { new Replacement("change to cries", "cries", 0), new Replacement("change to laughs", "laughs", -10), new Replacement("change to smiles awkwardly", "smiles awkwardly", 0) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("change to stocking", "stockings", -10), new Replacement("change to ", "change to skirts", 0), new Replacement("just remove the frigging vinyl", "no clothing", 5) }));
        list.Add(new ActionBlock(-10, new Replacement[] { new Replacement("change to violet", "violet", 0), new Replacement("change to pink", "pink", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(0, new Replacement[] { new Replacement("change to violet", "violet", 0), new Replacement("change to pink", "pink", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("change to screams", "screams", 0), new Replacement("change to cries", "cries", 0), new Replacement("change to nods in approval", "nods in approval", 10), new Replacement("change to chuckles", "chuckles", -10) }));
        list.Add(new ActionBlock(0, new Replacement[] { new Replacement("change to violet", "violet", 0), new Replacement("change to pink", "pink", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(-10, new Replacement[] { new Replacement("change to hamburguer", "change to stocking", -10), new Replacement("change to meaningless cloth", "piece of meaningless cloth", 0), new Replacement("change to vinyl jacket", "vinyl jacket", -10) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("change to screams", "screams", 0), new Replacement("change to cries", "cries", 0), new Replacement("change to nods in approval", "nods in approval", 10), new Replacement("change to chuckles", "chuckles", -10) }));
        list.Add(new ActionBlock(-10, new Replacement[] { new Replacement("change to violet", "violet", 0), new Replacement("change to golden", "golden", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("change to shoes", "", 0), new Replacement("change to cape", "cape", 0), new Replacement("change to vinyl pants", "vinyl pants", -10) }));
        list.Add(new ActionBlock(10, new Replacement[] { new Replacement("change to violet", "violet", 0), new Replacement("change to pink", "pink", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(0, new Replacement[] { new Replacement("change to violet", "violet", 0), new Replacement("change to pink", "pink", -10), new Replacement("change to cyan", "cyan", -10), new Replacement("change to purple", "purple", -10) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("0", "0", -10), new Replacement("0", "0", 0), new Replacement("0", "0", 0), new Replacement("0", "0", 0) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("change to screams", "screams", 0), new Replacement("change to cries", "cries", 0), new Replacement("change to nods in approval", "nods in approval", 10), new Replacement("change to chuckles", "chuckles", -10) }));
        list.Add(new ActionBlock(-15, new Replacement[] { new Replacement("change to screams", "screams", 0), new Replacement("change to cries", "cries", 0), new Replacement("change to nods in approval", "nods in approval", 10), new Replacement("change to chuckles", "chuckles", -10) }));

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
