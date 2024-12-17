using DG.Tweening;
using UnityEngine;

public class ButtonTease : MonoBehaviour
{
    [SerializeField] private float _bounceStrength = 2;
    [SerializeField] private float _speed = 3;
    [SerializeField] private Ease _ease = Ease.InOutSine;
    

    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _rectTransform.DOScale(_bounceStrength, _speed)
            .SetEase(_ease)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        if (_rectTransform != null)
        {
            _rectTransform.DOKill();
        }
    }
}
