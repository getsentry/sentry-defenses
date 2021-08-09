using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/New Sentry")]
    public class SentryData : ScriptableObject
    {
        public string Name;
        public GameObject Prefab;
        
        
        
        public float FireRate;
    }
}
