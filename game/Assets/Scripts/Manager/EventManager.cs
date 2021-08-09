using System;
using Data;

namespace Manager
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public event Action Resetting;
        public event Action SentryPlacing;

        
        public void Reset()
        {
            Resetting?.Invoke();
        }
        
        public void SentryPlaced()
        {
            SentryPlacing?.Invoke();
        }
    }
}
