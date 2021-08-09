using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using Utility.StateMachine;

public class Upgrades : MonoBehaviour
{
    public Button upgradeRange;
    public Button upgradeDamage;
    public Button upgradeFireRate;
    
    private GameData _data;
    private EventManager _eventManager;

    private void Awake()
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;

    } 
    
    private void Start()
    {
        _eventManager.SentryUpgrading += IsUpdating;
        upgradeRange.onClick.AddListener(RangeClicked);
        upgradeDamage.onClick.AddListener(DamageClicked);
        upgradeFireRate.onClick.AddListener(FireRateClicked);
        gameObject.SetActive(false);
    }

    private void IsUpdating() {
        Debug.Log("isUpdating");
        gameObject.SetActive(true);
    }

    private void Update()
    {
        
    }

    private void RangeClicked() {
        _data.Upgrade.Range *= 1.05f;
        OnUpgraded();
    }

    private void DamageClicked() {
        _data.Upgrade.Damage *= 1.25f;
        OnUpgraded();
    }

    private void FireRateClicked() {
        _data.Upgrade.FireRate /= 1.25f;
        OnUpgraded();
    }

    private void OnUpgraded() {
        gameObject.SetActive(false);
        _eventManager.Upgraded();
    }
}
