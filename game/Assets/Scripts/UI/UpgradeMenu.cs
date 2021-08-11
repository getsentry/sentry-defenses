using System;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public UpgradeButtons UpgradeButtons;
    
    public RectTransform Top;
    public RectTransform Bottom;
    private List<RectTransform> _bottomButtons;

    public int StartOffset = 50;
    public float MoveDuration = 0.3f;
    public float ButtonDelay = 0.1f;
    public Ease ShowEase = Ease.OutBack;
    public Ease HideEase = Ease.InBack;
    
    public Button StartButton;
    
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;

        _bottomButtons = new List<RectTransform>();
        for (int i = 0; i < Bottom.childCount; ++i)
        {
            _bottomButtons.Add(Bottom.GetChild(i).GetComponent<RectTransform>());
        }
    }

    private void Start()
    {
        StartButton.onClick.AddListener(_eventManager.StartFight);

        Top.anchoredPosition = new Vector2(0, -StartOffset);
        // Bottom.anchoredPosition = new Vector2(0, StartOffset);
        for (int i = 0; i < _bottomButtons.Count; i++)
        {
            var button = _bottomButtons[i];
            button.anchoredPosition = new Vector2(button.anchoredPosition.x, StartOffset);
        }
    }

    public void Show()
    {
        Top.DOAnchorPosY(0, MoveDuration)
            .SetEase(ShowEase);
        
        // Bottom.DOAnchorPosY(0, MoveDuration)
        //     .SetEase(ShowEase);
        
        for (int i = 0; i < _bottomButtons.Count; i++)
        {
            var button = _bottomButtons[i];
            button.DOAnchorPosY(50, MoveDuration)
                .SetDelay(i * ButtonDelay)
                .SetEase(ShowEase);
        }
    }

    public void Hide(Action finishCallback)
    {
        Top.DOAnchorPosY(Top.rect.height, MoveDuration)
            .SetEase(HideEase);

        // Bottom.DOAnchorPosY(-Bottom.rect.height, MoveDuration)
        //     .SetEase(HideEase)
        //     .OnStepComplete(() => finishCallback?.Invoke());
        
        for (int i = 0; i < _bottomButtons.Count; i++)
        {
            var button = _bottomButtons[i];
            button.DOAnchorPosY(-button.rect.height, MoveDuration)
                .SetDelay(i * ButtonDelay)
                .SetEase(HideEase);
        }
    }
}
