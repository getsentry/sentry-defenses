using UnityEngine;
using UnityEngine.UI;
using Manager;
using TMPro;

public class UpgradeButtons : MonoBehaviour
{
    public Button upgradeRange;
    public Button upgradeFireRate;
    public Button upgradeDamage;
    public Button buildTower;

    public TextMeshProUGUI RangeText;
    public TextMeshProUGUI RangeLevel;
    public TextMeshProUGUI RangeCost;
    
    public TextMeshProUGUI FireRateText;
    public TextMeshProUGUI FireRateLevel;
    public TextMeshProUGUI FireRateCost;
    
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI DamageLevel;
    public TextMeshProUGUI DamageCost;
    
    public TextMeshProUGUI TowerText;
    public TextMeshProUGUI TowerCost;

    private GameData _data;
    private EventManager _eventManager;
    private UpgradeManager _upgradeManager;

    private SentryTower _selectedSentryTower;

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
        
        CoinsUpdate();
    }

    public void SetSelectedSentry(SentryTower sentryTower)
    {
        _selectedSentryTower = sentryTower;
    }

    private void UpdateAllButtonText()
    {
        if (_selectedSentryTower != null)
        {
            RangeText.SetText("Upgrade Range");
            RangeLevel.SetText(_selectedSentryTower.upgrades.Range.ToString());                
            RangeCost.SetText(_selectedSentryTower.upgrades.CurrentRangeUpgradeCost.ToString());

            DamageText.SetText("Upgrade Damage");
            DamageLevel.SetText(_selectedSentryTower.upgrades.Damage.ToString());                
            DamageCost.SetText(_selectedSentryTower.upgrades.CurrentDamageUpgradeCost.ToString());
            
            FireRateText.SetText("Upgrade Fire Rate");
            FireRateLevel.SetText(_selectedSentryTower.upgrades.FireRate.ToString());                
            FireRateCost.SetText(_selectedSentryTower.upgrades.CurrentFireRateUpgradeCost.ToString());
        }
        else
        {
            RangeText.SetText("Select a Sentry");
            RangeLevel.SetText("?");                
            RangeCost.SetText("?");
            
            DamageText.SetText("Select a Sentry");
            DamageLevel.SetText("?");                
            DamageCost.SetText("?");
            
            FireRateText.SetText("Select a Sentry");
            FireRateLevel.SetText("?");                
            FireRateCost.SetText("?");
        }

        TowerText.SetText("Build new Sentry");
        TowerCost.SetText(_upgradeManager.CurrentSentryBuildCost.ToString());
    }

    private void CoinsUpdate()
    {
        upgradeRange.interactable = _selectedSentryTower != null && _data.Coins >= _selectedSentryTower.upgrades.CurrentRangeUpgradeCost;
        upgradeDamage.interactable = _selectedSentryTower != null && _data.Coins >= _selectedSentryTower.upgrades.CurrentDamageUpgradeCost;
        upgradeFireRate.interactable = _selectedSentryTower != null && _data.Coins >= _selectedSentryTower.upgrades.CurrentFireRateUpgradeCost;
        buildTower.interactable = _data.Coins >= _upgradeManager.CurrentSentryBuildCost;
    }

    private void RangeClicked()
    {
        Debug.Log("range clicked");
        
        if (_selectedSentryTower != null && _data.Coins >= _selectedSentryTower.upgrades.CurrentRangeUpgradeCost)
        {
            _data.Coins -= _selectedSentryTower.upgrades.CurrentRangeUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentryTower.upgrades.CurrentRangeUpgradeCost *= 2;
            _selectedSentryTower.upgrades.Range++;
            _selectedSentryTower.postUpgrade();
            _eventManager.UpdateCosts();
        }
    }

    private void DamageClicked()
    {
        if (_selectedSentryTower != null && _data.Coins >= _selectedSentryTower.upgrades.CurrentDamageUpgradeCost)
        {
            _data.Coins -= _selectedSentryTower.upgrades.CurrentDamageUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentryTower.upgrades.CurrentDamageUpgradeCost += 1;
            _selectedSentryTower.upgrades.Damage++;
            _selectedSentryTower.postUpgrade();
            _eventManager.UpdateCosts();
        }
    }

    private void FireRateClicked()
    {
        if (_selectedSentryTower != null && _data.Coins >= _selectedSentryTower.upgrades.CurrentFireRateUpgradeCost)
        {
            _data.Coins -= _selectedSentryTower.upgrades.CurrentFireRateUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentryTower.upgrades.CurrentFireRateUpgradeCost += 3;
            _selectedSentryTower.upgrades.FireRate++;
            _selectedSentryTower.postUpgrade();
            _eventManager.UpdateCosts();
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
