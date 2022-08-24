using System;
using System.Collections.Generic;
using DG.Tweening;
using Sentry;
using UnityEngine;

public class ApplyUpgradeMenu : MonoBehaviour
{
    public RectTransform Container;
    
    public float MoveDuration = 0.3f;
    public Ease ShowEase = Ease.OutBack;
    public Ease HideEase = Ease.InBack;
    
    public void Show()
    {
        SentrySdk.AddBreadcrumb("Showing Upgrade Menu", "app.lifecycle");

        Container.DOAnchorPosY(0, MoveDuration)
            .SetEase(ShowEase);
    }

    public void Hide(Action finishCallback)
    {
        SentrySdk.AddBreadcrumb("Hiding Upgrade Menu", "app.lifecycle");

        Container.DOAnchorPosY(Container.rect.height, MoveDuration)
            .SetEase(HideEase)
            .OnStepComplete(() => finishCallback?.Invoke());
    }
}
