using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class GameData : MonoSingleton<GameData>
{
    public GameObject SentryPrefab;

    public int StartCoins = 25;
    public int Coins;
    
    public int Level = 1;
    public TowerUpgrade Upgrade = new TowerUpgrade();
    
    public List<GameObject> bugs = new List<GameObject>();

    public int StartHitPoints = 3;
    public int HitPoints;

    private EventManager _eventManager;
    
    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _eventManager.Resetting += OnResetting;
    }

    private void Start()
    {
        OnResetting();
    }

    private void OnResetting()
    {
        Coins = StartCoins;
        HitPoints = StartHitPoints;

        _eventManager.UpdateCoins();
        _eventManager.UpdateHitPoints();
    }
}


public class TowerUpgrade {
    public float Range = 0f;
    public float Damage = 1.0f;
    public float FireRate = 0f;

    public int CurrentRangeUpgradeCost = 1;
    public int CurrentDamageUpgradeCost = 1;
    public int CurrentFireRateUpgradeCost = 1;
}
