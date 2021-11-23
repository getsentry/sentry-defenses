using System;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class HitPoints : MonoBehaviour
{
    public List<Sprite> HitPointSprites;
    public Image HitPointRenderer;

    [Header("Effects")]
    public float HitStrength = 0.5f;
    public float HitDuration = 0.1f;
    
    private GameData _gameData;
    private EventManager _eventManager;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _gameData = GameData.Instance;
        _rectTransform = GetComponent<RectTransform>();
        _eventManager = EventManager.Instance;
        _eventManager.UpdatingHitPoints += OnUpdatingHitPoints;
    }
    
    [ContextMenu("HitIt")]
    private void OnUpdatingHitPoints()
    {
        if (_rectTransform)
        {
            _rectTransform.DOKill();
            _rectTransform.localScale = Vector3.one;
            _rectTransform.DOPunchScale(Vector3.one * HitStrength, HitDuration);    
        }

        var index = _gameData.HitPoints - 1;
        if (index > 0 && index < HitPointSprites.Count)
        {
            HitPointRenderer.sprite = HitPointSprites[index];
        }
    }

    private void OnDestroy()
    {
        _rectTransform?.DOKill();
    }
}
