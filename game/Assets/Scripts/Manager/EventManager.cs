using System;

namespace Manager
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public event Action Upgrading;
        public event Action Resetting;
        public event Action SentryPlacing;
        public event Action CoinsUpdated;
        public event Action Fighting;
        public event Action CostsUpdating;
        public event Action UpdatingHitPoints;

        public void Upgrade() => Upgrading?.Invoke();
        public void Reset() => Resetting?.Invoke();
        public void StartFight() => Fighting?.Invoke();
        public void PlaceSentry() => SentryPlacing?.Invoke();
        public void UpdateCoins() => CoinsUpdated?.Invoke();
        public void UpdateCosts() => CostsUpdating?.Invoke();
        public void UpdateHitPoints() => UpdatingHitPoints?.Invoke();
    }
}
