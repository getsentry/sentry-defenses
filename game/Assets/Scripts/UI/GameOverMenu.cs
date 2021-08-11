using System;
using DG.Tweening;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Button RestartButton;
    public Image Background;
    public TextMeshProUGUI Text;
    
    public float FadeDuration = 0.3f;
    
    public GameObject Container;
    
    private EventManager _eventManager;
    
    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        RestartButton.onClick.AddListener(OnRestartButtonClick);

        var buttonColor = RestartButton.image.color;
        buttonColor.a = 0;
        RestartButton.image.color = buttonColor;

        var backgroundColor = Background.color;
        backgroundColor.a = 0;
        Background.color = backgroundColor;

        var textColor = Text.color;
        textColor.a = 0;
        Text.color = textColor;
        
        Container.SetActive(false);
    }

    private void OnRestartButtonClick()
    {
        _eventManager.Upgrade();        
    }

    public void Show(Action finishCallback)
    {
        Container.SetActive(true);
        
        RestartButton.image.DOFade(1, FadeDuration);
        Background.DOFade(1, FadeDuration);
        Text.DOFade(1, FadeDuration)
            .OnComplete(() =>
            {
                finishCallback?.Invoke();
            });
    }

    public void Hide(Action finishCallback)
    {
        RestartButton.image.DOFade(0, FadeDuration);
        Background.DOFade(0, FadeDuration);
        Text.DOFade(0, FadeDuration)
            .OnComplete(() =>
            {
                finishCallback?.Invoke();
                Container.SetActive(false);
            });
    }
}
