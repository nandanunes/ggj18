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

    void Start()
    {
        _text = GetComponent<Text>();

        list = new List<ActionBlock>();
        list.Add(new ActionBlock());
        list.Add(new ActionBlock());

        score.text = _score.ToString();
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
        for (int i = 0; i < textGen.characterCount; ++i)
        {
            if (_text.text[i] != '}') continue;

            Vector2 worldBottomRight = transform.TransformPoint(new Vector2(textGen.verts[i * 4 + 2].position.x, textGen.verts[i * 4 + 2].position.y));

            if (worldBottomRight.y > 230)
            {
                var word = GetWordAtIndex(i - 1);
                var block = list[word.index];
                if (block.done) continue;
                block.done = true;
                _score += block.score;
                
                score.text = _score.ToString();
            }
        }

    }

    private void AddScore()
    {

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
                foreach (Transform child in optionsPanel.transform) GameObject.Destroy(child.gameObject);
                foreach (var l in block.options)
                {
                    var b = GameObject.Instantiate(actionButton);
                    b.transform.SetParent(optionsPanel.transform);
                    b.name = l.param;
                    b.GetComponentInChildren<Text>().text = l.name;
                    b.onClick.AddListener(
                        () => {
                            foreach (Transform child in optionsPanel.transform) GameObject.Destroy(child.gameObject);
                            _text.text = _text.text.Remove(niceWord.beginIndex, niceWord.lastIndex).Insert(niceWord.beginIndex, l.param);
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
            if (marker < 0 || _text.text[marker] == '}')
            {
                return new NiceWord(-1, "", -1, -1);
            }
            else if (_text.text[marker] == '{')
            {
                begIndex = marker;
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
            else if (_text.text[marker] == '}')
            {
                lastIndex = marker;
            }
        }

        string source = _text.text.Substring(0, lastIndex - 1);
        int count = 0;
        foreach (char c in source) if (c == '}') count++;

        return new NiceWord(count, _text.text.Substring(begIndex + 1, lastIndex - begIndex - 1), begIndex + 1, lastIndex - begIndex - 1);
    }
}
