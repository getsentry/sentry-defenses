using System.Collections.Generic;
using Manager;
using UnityEngine;

public class GameData : MonoSingleton<GameData>
{
    public GameObject SentryPrefab;
    public TowerUpgrade Upgrade = new TowerUpgrade();
    public List<GameObject> bugs = new List<GameObject>();

    public int StartHitPoints = 3;

    private int _hitPoints;
    public int HitPoints
    {
        get => _hitPoints;
        set
        {
            _hitPoints = value;
            _eventManager.UpdateHitPoints();
        }
    }

    private int _currentXp;
    public int CurrentXp
    {
        get => _currentXp;
        set
        {
            _currentXp = value;
            _eventManager.UpdateXp();
            
            BugCount++;
            
            if (_currentXp >= LevelUpRequirement)
            {
                _currentXp = 0;
                _eventManager.LevelUpXp();

                RequirementIncrease++;
                LevelUpRequirement += RequirementIncrease;
            }
        }
    }
    
    public int LevelUpRequirement = 2;
    public int RequirementIncrease = 2;
    
    public int BugCount;
    
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _eventManager.OnReset += OnReset;
    }

    private void Start()
    {
        HitPoints = StartHitPoints;
        bugs = new List<GameObject>();
        
        _eventManager.UpdateHitPoints();
    }

    private void OnReset()
    {
        bugs = new List<GameObject>();
        
        HitPoints = StartHitPoints;
        CurrentXp = 0;
        BugCount = 0;

        LevelUpRequirement = 2;
        RequirementIncrease = 2;
        
        _eventManager.UpdateHitPoints();
        _eventManager.UpdateXp();
    }
}


public class TowerUpgrade {
    public float Range = 0f;
    public float Damage = 0f;
    public float FireRate = 0f;
}
