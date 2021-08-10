using System;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public RectTransform Top;
    public RectTransform Bottom;

    public int StartOffset = 50;
    public float MoveDuration = 0.3f;
    public Ease MoveEase = Ease.OutBack;
    
    public Button StartButton;
    
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        StartButton.onClick.AddListener(_eventManager.StartFight);

        Top.anchoredPosition = new Vector2(0, -StartOffset);
        Bottom.anchoredPosition = new Vector2(0, StartOffset);
    }

    public void Show()
    {
        Top.DOAnchorPosY(0, MoveDuration)
            .SetEase(MoveEase);
        
        Bottom.DOAnchorPosY(0, MoveDuration)
            .SetEase(MoveEase);
    }

    public void Hide(Action finishCallback)
    {
        Top.DOAnchorPosY(Top.rect.height, MoveDuration)
            .SetEase(MoveEase);
        
        Bottom.DOAnchorPosY(-Bottom.rect.height, MoveDuration)
            .SetEase(MoveEase)
            .OnStepComplete(() => finishCallback?.Invoke());
    }
}
