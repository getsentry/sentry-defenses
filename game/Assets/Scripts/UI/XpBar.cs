using System;
using DG.Tweening;
using Manager;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    public RectTransform RectTransform;
    [SerializeField] private float ScaleEffectDuration = 0.25f;
    public Ease ScaleEase = Ease.OutBack;

    private bool _beReadyToReset;
    
    private GameData _data;
    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        
        _data = GameData.Instance;

        var eventManager = EventManager.Instance;
        eventManager.OnUpdateXp += OnUpdateXp;
        eventManager.OnFight += OnFight;
        eventManager.OnReset += Reset;
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

    private void OnFight()
    {
        Reset();
    }

    private void Reset()
    {
        RectTransform.DOKill();
        RectTransform.localScale = new Vector3(0.05f, 1, 1);
    }
}
