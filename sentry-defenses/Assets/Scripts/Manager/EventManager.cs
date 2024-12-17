using System;

namespace Manager
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action OnUpgrade;
        public event Action OnReset;
        public event Action OnUpdateXp;
        public event Action OnLevelUpXp;
        public event Action OnFight;
        public event Action OnUpdateHitPoints;


        public void PauseGame() => OnGamePause?.Invoke();
        public void ResumeGame() => OnGameResume?.Invoke();
        public void Upgrade() => OnUpgrade?.Invoke();
        public void Reset() => OnReset?.Invoke();
        public void StartFight() => OnFight?.Invoke();
        public void UpdateXp() => OnUpdateXp?.Invoke();
        public void LevelUpXp() => OnLevelUpXp?.Invoke();
        public void UpdateHitPoints() => OnUpdateHitPoints?.Invoke();
    }
}
