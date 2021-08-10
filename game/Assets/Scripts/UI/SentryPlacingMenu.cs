using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class SentryPlacingMenu : MonoBehaviour
{
    public Button ReturnButton;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        ReturnButton.gameObject.SetActive(false);
        ReturnButton.onClick.AddListener(OnReturnClick);
    }

    private void OnReturnClick()
    {
        _eventManager.Upgrade();
    }

    public void Show()
    {
        ReturnButton.gameObject.SetActive(true);
    }

    public void Hide()
    {
        ReturnButton.gameObject.SetActive(false);
    }
}