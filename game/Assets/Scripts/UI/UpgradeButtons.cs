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

    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI towerText;

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
    }

    public void SetSelectedSentry(Sentry sentry)
    {
        _selectedSentry = sentry;
    }

    private void UpdateAllButtonText()
    {
        if (_selectedSentry != null)
        {
            rangeText.SetText("Upgrade Range\n(lvl {0})\n{1} Coins", _selectedSentry.upgrades.Range,
                _selectedSentry.upgrades.CurrentRangeUpgradeCost);
            damageText.SetText("Upgrade Damage\n(lvl {0})\n{1} Coins", _selectedSentry.upgrades.Damage,
                _selectedSentry.upgrades.CurrentDamageUpgradeCost);
            fireRateText.SetText("Upgrade FireRate\n(lvl {0})\n{1} Coins", _selectedSentry.upgrades.FireRate,
                _selectedSentry.upgrades.CurrentFireRateUpgradeCost);
        }
        else
        {
            rangeText.SetText("Select a Sentry");
            damageText.SetText("Select a Sentry");
            fireRateText.SetText("Select a Sentry");
        }

        towerText.SetText("Build Tower\n{0} Coins", _upgradeManager.CurrentSentryBuildCost);
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
        if (_selectedSentry != null && _data.Coins >= _selectedSentry.upgrades.CurrentRangeUpgradeCost)
        {
            _data.Coins -= _selectedSentry.upgrades.CurrentRangeUpgradeCost;
            _eventManager.UpdateCoins();

            _selectedSentry.upgrades.CurrentRangeUpgradeCost *= 2;
            _selectedSentry.upgrades.Range++;
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