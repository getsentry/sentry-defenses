using System;
using Data;

namespace Manager
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public event Action<SentryData> OnPlacing;

        public void Placing(SentryData sentry)
        {
            OnPlacing?.Invoke(sentry);
        }
    }
}
