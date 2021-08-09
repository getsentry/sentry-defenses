using Data;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtons : MonoBehaviour
{
    public GameObject ButtonPrefab;
    
    private EventManager _eventManager;
    
    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {

    }

    private void OnButtonClick(SentryData sentry)
    {
        _eventManager.Placing(sentry);
    }
}
