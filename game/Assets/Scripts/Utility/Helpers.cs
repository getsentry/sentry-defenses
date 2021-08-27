using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static Camera _mainCamera;
        
    public static Vector3 GetMouseWorldPosition()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                return Vector3.zero;    
            }
        }
        
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    public static bool IsMouseOverUI()
    {
        // return EventSystem.current.IsPointerOverGameObject();
        
        if (EventSystem.current.IsPointerOverGameObject())
            return true;
 
        for (int touchIndex = 0; touchIndex < Input.touchCount; touchIndex++)
        {
            Touch touch = Input.GetTouch(touchIndex);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return true;
        }
 
        return false;
    }

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default,
        int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.MiddleCenter,
        TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 0)
    {
        if (color == null)
            color = Color.white;

        return CreateWorldText(parent, text, localPosition, fontSize, (Color) color, textAnchor, textAlignment,
            sortingOrder);
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize,
        Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        var gameObject = new GameObject("World_Text", typeof(TextMesh));
        var transform = gameObject.transform;
        transform.localScale = Vector3.one * 0.1f;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        var textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}