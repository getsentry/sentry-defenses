using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace Manager
{
    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        public int BaseRangeUpgradeCost = 1;
        public int BaseDamageUpgradeCost = 1;
        public int BaseFireRateUpgradeCost = 1;
        public int BaseSentryBuildCost = 5;

        public int CurrentRangeUpgradeCost;
        public int CurrentDamageUpgradeCost;
        public int CurrentFireRateUpgradeCost;
        public int CurrentSentryBuildCost;

        private GameData _data;
        private EventManager _eventManager;
        
        private void Awake()
        {
            _data = GameData.Instance;
            _eventManager = EventManager.Instance;
            _eventManager.Resetting += OnReset;
        }

        private void Start()
        {
            OnReset();
        }
        
        private void OnReset()
        {
            CurrentRangeUpgradeCost = BaseRangeUpgradeCost;
            CurrentDamageUpgradeCost = BaseDamageUpgradeCost;
            CurrentFireRateUpgradeCost = BaseFireRateUpgradeCost;
            CurrentSentryBuildCost = BaseSentryBuildCost;
        }
        
        public void BuyRangeCost()
        {
            _data.Coins -= CurrentRangeUpgradeCost;
            _eventManager.UpdateCoins();

            CurrentRangeUpgradeCost *= 2;
            _eventManager.UpdateCosts();
        }
        
        public void BuyDamageCost()
        {
            _data.Coins -= CurrentDamageUpgradeCost;
            _eventManager.UpdateCoins();

            CurrentDamageUpgradeCost += 1;
            _eventManager.UpdateCosts();
        }

        public void BuyFireRateCost()
        {
            _data.Coins -= CurrentFireRateUpgradeCost;
            _eventManager.UpdateCoins();
            
            CurrentFireRateUpgradeCost += 3;
            _eventManager.UpdateCosts();
        }

        public void BuySentryBuildCost()
        {
            _data.Coins -= CurrentSentryBuildCost;
            _eventManager.UpdateCoins();
            
            CurrentSentryBuildCost *= 2;
            _eventManager.UpdateCosts();
        }
    }
}