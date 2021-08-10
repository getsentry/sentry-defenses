using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace Manager
{
    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        public int BaseSentryBuildCost = 5;

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
            CurrentSentryBuildCost = BaseSentryBuildCost;
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
