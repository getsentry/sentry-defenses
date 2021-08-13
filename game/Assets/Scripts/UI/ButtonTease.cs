using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;

public class ButtonTease : MonoBehaviour
{
    public float Movement = 10;
    public float Speed;

    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        var targetY = _rectTransform.anchoredPosition.y + Movement;
        _rectTransform.DOAnchorPosY(targetY, Speed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        
    }
}
