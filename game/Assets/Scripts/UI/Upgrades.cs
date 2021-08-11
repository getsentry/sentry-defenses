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
    private PlayerInput _input;

    private Sentry _tower = null;

    private void Awake()
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;
        _upgradeManager = UpgradeManager.Instance;
        _input = PlayerInput.Instance;
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

    private void Update()
    {
        if (_input.GetMouseDown() && !Helpers.IsMouseOverUI()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (!hit.collider.isTrigger) {
                    if (_tower != null)
                        _tower.onDeSelect();
                    _tower = hit.collider.gameObject.GetComponent<Sentry>();
                    _tower.onSelect();
                    break;
                } else if (_tower != null) {
                    _tower.onDeSelect();
                    _tower = null;
                }
            }
            _eventManager.UpdateCosts();
            _eventManager.UpdateCoins();
        }
    }
    
    private void UpdateAllButtonText()
    {
        if (_tower != null)
        {
            rangeText.SetText("Upgrade Range\n(lvl {0})\n{1} Coins", _tower.upgrades.Range, _tower.upgrades.CurrentRangeUpgradeCost);
            damageText.SetText("Upgrade Damage\n(lvl {0})\n{1} Coins", _tower.upgrades.Damage, _tower.upgrades.CurrentDamageUpgradeCost);
            fireRateText.SetText("Upgrade FireRate\n(lvl {0})\n{1} Coins", _tower.upgrades.FireRate, _tower.upgrades.CurrentFireRateUpgradeCost);
        } else {
            rangeText.SetText("Select a Sentry");
            damageText.SetText("Select a Sentry");
            fireRateText.SetText("Select a Sentry");
        }
        towerText.SetText("Build Tower\n{0} Coins", _upgradeManager.CurrentSentryBuildCost);
    }

    private void CoinsUpdate()
    {
        upgradeRange.interactable = _tower != null && _data.Coins >= _tower.upgrades.CurrentRangeUpgradeCost;
        upgradeDamage.interactable = _tower != null && _data.Coins >= _tower.upgrades.CurrentDamageUpgradeCost;
        upgradeFireRate.interactable = _tower != null && _data.Coins >= _tower.upgrades.CurrentFireRateUpgradeCost;
        buildTower.interactable = _data.Coins >= _upgradeManager.CurrentSentryBuildCost;
    }

    private void RangeClicked()
    {
        if (_tower != null && _data.Coins >= _tower.upgrades.CurrentRangeUpgradeCost)
        {
            _data.Coins -= _tower.upgrades.CurrentRangeUpgradeCost;
            _eventManager.UpdateCoins();

            _tower.upgrades.CurrentRangeUpgradeCost *= 2;
            _tower.upgrades.Range++;
            _eventManager.UpdateCosts();
        }
    }

    private void DamageClicked()
    {
        if (_tower != null && _data.Coins >= _tower.upgrades.CurrentDamageUpgradeCost)
        {
            _data.Coins -= _tower.upgrades.CurrentDamageUpgradeCost;
            _eventManager.UpdateCoins();

            _tower.upgrades.CurrentDamageUpgradeCost += 1;
            _tower.upgrades.Damage++;
            _eventManager.UpdateCosts();
        }
    }

    private void FireRateClicked()
    {
        if (_tower != null && _data.Coins >= _tower.upgrades.CurrentFireRateUpgradeCost)
        {
            _data.Coins -= _tower.upgrades.CurrentFireRateUpgradeCost;
            _eventManager.UpdateCoins();

            _tower.upgrades.CurrentFireRateUpgradeCost += 3;
            _tower.upgrades.FireRate++;
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
