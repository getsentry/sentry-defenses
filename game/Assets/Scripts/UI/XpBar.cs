using System;
using DG.Tweening;
using Manager;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    public RectTransform RectTransform;
    public float ScaleEffectDuration = 0.1f;
    public Ease ScaleEase = Ease.OutBack;
    
    private GameData _data;
    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        
        _data = GameData.Instance;

        var eventManager = EventManager.Instance;
        eventManager.OnUpdateXp += OnUpdateXp;
        eventManager.OnLevelUpXp += Reset;
    }

    private void Start()
    {
        Reset();
    }

    private void OnUpdateXp()
    {
        RectTransform.DOScaleX((float)_data.CurrentXp / _data.LevelUpRequirement, ScaleEffectDuration)
            .SetEase(ScaleEase);
    }

    private void Reset()
    {
        RectTransform.DOKill();
        RectTransform.localScale = new Vector3(0.03f, 1, 1);
    }
}
