using UnityEngine;
using UnityEngine.UI;
using Manager;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public Button upgradeRange;
    public Button upgradeFireRate;
    public Button upgradeDamage;
    public Button buildTower;

    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI towerText;

    private GameData _data;
    private EventManager _eventManager;
    private UpgradeManager _upgradeManager;

    private void Awake()
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;
        _upgradeManager = UpgradeManager.Instance;
    }

    private void Start()
    {
        _eventManager.Upgrading += UpdateAllButtonText;
        _eventManager.CoinsUpdated += CoinsUpdate;
        _eventManager.CostsUpdating += UpdateAllButtonText;
        
        upgradeRange.onClick.AddListener(RangeClicked);
        upgradeDamage.onClick.AddListener(DamageClicked);
        upgradeFireRate.onClick.AddListener(FireRateClicked);
        buildTower.onClick.AddListener(BuildTowerClicked);
    }
    
    private void UpdateAllButtonText()
    {
        rangeText.SetText("Upgrade Range\n{0} Coins", _upgradeManager.CurrentRangeUpgradeCost);
        damageText.SetText("Upgrade Damage\n{0} Coins",  _upgradeManager.CurrentDamageUpgradeCost);
        fireRateText.SetText("Upgrade FireRate\n{0} Coins", _upgradeManager.CurrentFireRateUpgradeCost);
        towerText.SetText("Build Tower\n{0} Coins", _upgradeManager.CurrentSentryBuildCost);
    }

    private void CoinsUpdate()
    {
        upgradeRange.interactable = _data.Coins >= _upgradeManager.CurrentRangeUpgradeCost;
        upgradeDamage.interactable = _data.Coins >= _upgradeManager.CurrentDamageUpgradeCost;
        upgradeFireRate.interactable = _data.Coins >= _upgradeManager.CurrentFireRateUpgradeCost;
        buildTower.interactable = _data.Coins >= _upgradeManager.CurrentSentryBuildCost;
    }

    private void RangeClicked()
    {
        if (_data.Coins >= _upgradeManager.CurrentRangeUpgradeCost)
        {
            _upgradeManager.BuyRangeCost();
            _data.Upgrade.Range *= 1.05f;
        }
    }

    private void DamageClicked()
    {
        if (_data.Coins >= _upgradeManager.CurrentDamageUpgradeCost)
        {
            _upgradeManager.BuyDamageCost();
            _data.Upgrade.Damage *= 1.25f;
        }
    }

    private void FireRateClicked()
    {
        if (_data.Coins >= _upgradeManager.CurrentFireRateUpgradeCost)
        {
            _upgradeManager.BuyFireRateCost();
            _data.Upgrade.FireRate /= 1.25f;
        }
    }


    private void BuildTowerClicked()
    {
        if (_data.Coins >= _upgradeManager.CurrentSentryBuildCost)
        {
            _eventManager.PlaceSentry();
        }
    }
}