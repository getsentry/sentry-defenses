using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    public TextMeshProUGUI HitPointText;

    private GameData _gameData;
    private EventManager _eventManager;

    private void Awake()
    {
        _gameData = GameData.Instance;
        _eventManager = EventManager.Instance;
        _eventManager.UpdatingHitPoints += OnUpdatingHitPoints;
    }
    
    private void OnUpdatingHitPoints()
    {
        HitPointText.SetText("HitPoints: {0}", _gameData.HitPoints); 
    }

    private void Start()
    {
        OnUpdatingHitPoints();
    }
}
