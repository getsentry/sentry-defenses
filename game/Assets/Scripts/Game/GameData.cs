using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoSingleton<GameData>
{
    public GameObject SentryPrefab;


    public int MaxSentryCount = 3;
    public TowerUpgrade Upgrade = new TowerUpgrade();
    public int PlacedSentryCount;
    public List<GameObject> bugs = new List<GameObject>();
}


public class TowerUpgrade {
    public float Range = 1.0f;
    public float Damage = 1.0f;
    public float FireRate = 1.0f;
}
