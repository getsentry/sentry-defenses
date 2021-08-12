using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using TMPro;

public class Coins : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay;
    private GameData _data;
    private EventManager _eventManager;

    private void Awake()
    {
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;
        coinDisplay.text = _data.Coins.ToString();
        _eventManager.CoinsUpdated += CoinsUpdate;
    }

    private void CoinsUpdate() {
        coinDisplay.text = _data.Coins.ToString();
    }
}
