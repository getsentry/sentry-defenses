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

    private Sentry _selectedSentry;

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

    public void SetSelectedSentry(Sentry sentry)
    {
        _selectedSentry = sentry;
    }

    private void UpdateAllButtonText()
    {
        if (_selectedSentry != null)
        {
            RangeText.SetText("Upgrade Range");
            RangeLevel.SetText(_selectedSentry.upgrades.Range.ToString());                
            RangeCost.SetText(_selectedSentry.upgrades.CurrentRangeUpgradeCost.ToString());

            DamageText.SetText("Upgrade Damage");
            DamageLevel.SetText(_selectedSentry.upgrades.Damage.ToString());                
            DamageCost.SetText(_selectedSentry.upgrades.CurrentDamageUpgradeCost.ToString());
            
            FireRateText.SetText("Upgrade Fire Rate");
            FireRateLevel.SetText(_selectedSentry.upgrades.FireRate.ToString());                
            FireRateCost.SetText(_selectedSentry.upgrades.CurrentFireRateUpgradeCost.ToString());
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
        upgradeRange.interactable = _selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentRangeUpgradeCost;
        upgradeDamage.interactable = _selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentDamageUpgradeCost;
        upgradeFireRate.interactable = _selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentFireRateUpgradeCost;
        buildTower.interactable = _data.Coins >= _upgradeManager.CurrentSentryBuildCost;
    }

    private void RangeClicked()
    {
        Debug.Log("range clicked");
        
        if (_selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentRangeUpgradeCost)
        {
            _data.Coins -= _selectedSentry.upgrades.CurrentRangeUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentry.upgrades.CurrentRangeUpgradeCost *= 2;
            _selectedSentry.upgrades.Range++;
            _selectedSentry.postUpgrade();
            _eventManager.UpdateCosts();
        }
    }

    private void DamageClicked()
    {
        if (_selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentDamageUpgradeCost)
        {
            _data.Coins -= _selectedSentry.upgrades.CurrentDamageUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentry.upgrades.CurrentDamageUpgradeCost += 1;
            _selectedSentry.upgrades.Damage++;
            _selectedSentry.postUpgrade();
            _eventManager.UpdateCosts();
        }
    }

    private void FireRateClicked()
    {
        if (_selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentFireRateUpgradeCost)
        {
            _data.Coins -= _selectedSentry.upgrades.CurrentFireRateUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentry.upgrades.CurrentFireRateUpgradeCost += 3;
            _selectedSentry.upgrades.FireRate++;
            _selectedSentry.postUpgrade();
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
