using System;
using DG.Tweening;
using UnityEngine;

public class ApplyUpgradeMenu : MonoBehaviour
{
    public RectTransform Container;
    
    public float MoveDuration = 0.3f;
    public Ease ShowEase = Ease.OutBack;
    public Ease HideEase = Ease.InBack;
    
    public void Show()
    {
        Container.DOAnchorPosY(0, MoveDuration)
            .SetEase(ShowEase);
    }

    public void Hide(Action finishCallback)
    {
        Container.DOAnchorPosY(Container.rect.height, MoveDuration)
            .SetEase(HideEase)
            .OnStepComplete(() => finishCallback?.Invoke());
    }
}
