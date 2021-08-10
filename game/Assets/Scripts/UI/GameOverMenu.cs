using Manager;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Button RestartButton;

    private EventManager _eventManager;
    
    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        Hide();
    }

    private void OnRestartButtonClick()
    {
        _eventManager.Upgrade();        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
