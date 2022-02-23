using System;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class PickUpgradeMenu : MonoBehaviour
{
    public Button UpgradeRange;
    public Button UpgradeFireRate;
    public Button UpgradeDamage;
    public Button BuildTower;
    
    public RectTransform Bottom;
    private List<RectTransform> _bottomButtons;

    public int StartOffset = 50;
    public float MoveDuration = 0.3f;
    public float ButtonDelay = 0.1f;
    public Ease ShowEase = Ease.OutBack;
    public Ease HideEase = Ease.InBack;
    
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
        Bottom.anchoredPosition = new Vector2(0, StartOffset);
    }
    
    public void Show()
    {
        Bottom.DOAnchorPosY(0, MoveDuration)
            .SetEase(ShowEase);
    }

    public void Hide(Action finishCallback)
    {
        Bottom.DOAnchorPosY(-Bottom.rect.height, MoveDuration)
            .SetEase(HideEase)
            .OnStepComplete(() => finishCallback?.Invoke());
    }
}
