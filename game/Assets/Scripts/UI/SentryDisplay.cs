using System;
using Manager;
using UnityEngine;

public class SentryDisplay : MonoBehaviour
{
    public GameObject SentryUiPrefab;
    
    private GameData _data;
    private EventManager _eventManager;
    
    private void Awake()
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;

        _eventManager.Resetting += OnReset;
        _eventManager.SentryPlacing += OnSentryPlaced;
    }

    private void Start()
    {
        
    }

    private void OnReset()
    {
        ClearSentryDisplay();
        UpdateSentryDisplay();
    }

    private void OnSentryPlaced()
    {
        ClearSentryDisplay();
        UpdateSentryDisplay();

    }

    private void ClearSentryDisplay()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void UpdateSentryDisplay()
    {
        var sentryCount = _data.MaxSentryCount - _data.PlacedSentryCount;
        for (int i = 0; i < sentryCount; i++)
        {
            Instantiate(SentryUiPrefab, transform);
        }
    }
}
