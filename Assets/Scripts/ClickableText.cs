using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableText : MonoBehaviour, IPointerDownHandler
{
    public Camera camera;
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
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
        Debug.DrawRay(Vector3.zero, Vector3.left, Color.red, 10);
        eventData.Use();
        int index = GetIndexOfClick(camera.ScreenPointToRay(eventData.position));
        if (index != -1) Debug.Log(GetWordAtIndex(index));
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

    string GetWordAtIndex(int index)
    {
        int begIndex = -1;
        int marker = index;
        while (begIndex == -1)
        {
            marker--;
            if (marker < 0 || _text.text[marker] == '}')
            {
                return "";
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
                return "";
            }
            else if (_text.text[marker] == '}')
            {
                lastIndex = marker;
            }
        }

        return _text.text.Substring(begIndex + 1, lastIndex - begIndex - 1);
    }

}
