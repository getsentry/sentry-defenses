using UnityEngine;

public class TitleScaler : MonoBehaviour
{
    [SerializeField] private float _upScaledHeight = 200;
    
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        var aspectRatio = (float)Screen.width / Screen.height;
        if (aspectRatio < 2)
        {
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _upScaledHeight);    
        }
    }
}
