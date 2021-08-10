using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using TMPro;
using Utility.StateMachine;

public class Upgrades : MonoBehaviour
{
    public Button upgradeRange;
    public Button upgradeFireRate;
    public Button upgradeDamage;
    public Button buildTower;
    public Button startWave;

    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI towerText;
    
    private GameData _data;
    private EventManager _eventManager;

    private int CostRange = 1;
    private int CostDamage = 1;
    private int CostFireRate = 1;
    private int CostTower = 5;

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
        buildTower.onClick.AddListener(BuildTowerClicked);
        startWave.onClick.AddListener(() => _eventManager.Fighting());
        _eventManager.CoinsUpdated += CoinsUpdate;
    }

    private void IsUpdating() {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        
    }

    private void CoinsUpdate() {
        upgradeRange.interactable = _data.Coins >= CostRange;
        upgradeDamage.interactable = _data.Coins >= CostDamage;
        upgradeFireRate.interactable = _data.Coins >= CostFireRate;
        buildTower.interactable = _data.Coins >= CostTower;
    }

    private void RangeClicked() {
        if (_data.Coins >= CostRange)
        {
            _eventManager.UpdateCoins(-CostRange);
            CostRange *= 2;
            rangeText.SetText("Upgrade Range\n{0} Coins", CostRange);
            _data.Upgrade.Range *= 1.05f;
            OnUpgraded();
        }
    }

    private void DamageClicked() {
        if (_data.Coins >= CostDamage)
        {
            _eventManager.UpdateCoins(-CostDamage);
            CostDamage += 1;
            damageText.SetText("Upgrade Damage\n{0} Coins", CostDamage);
            _data.Upgrade.Damage *= 1.25f;
            OnUpgraded();
        }
    }

    private void FireRateClicked() {
        if (_data.Coins >= CostFireRate)
        {
            _eventManager.UpdateCoins(-CostFireRate);
            CostFireRate += 3;
            fireRateText.SetText("Upgrade FireRate\n{0} Coins", CostFireRate);
            _data.Upgrade.FireRate /= 1.25f;
            OnUpgraded();
        }
    }

    private void BuildTowerClicked() {
        if (_data.Coins >= CostTower)
            _eventManager.UpdateCoins(-CostTower);
            CostTower *= 2;
            towerText.SetText("Build Tower\n{0} Coins", CostTower);
            gameObject.SetActive(false);
            _eventManager.Idle();
    }

    private void OnUpgraded() {
        if (_data.Coins <= 0) {
            gameObject.SetActive(false);
            _eventManager.Upgraded();
        }
    }
}
