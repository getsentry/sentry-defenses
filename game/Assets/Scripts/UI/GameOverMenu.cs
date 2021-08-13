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
    public Image LogoImage;
    public GameObject Container;
    
    public TextMeshProUGUI Congratulations;
    public TextMeshProUGUI YouveMadeIt;
    public TextMeshProUGUI WaveText;
    
    
    public float FadeDuration = 0.3f;
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
 
        Container.SetActive(false);
    }

    private void OnRestartButtonClick()
    {
        _eventManager.Upgrade();        
    }

    public void Show(Action finishCallback)
    {
        Container.SetActive(true);

        Congratulations.DOFade(1, FadeDuration);
        YouveMadeIt.DOFade(1, FadeDuration);
        WaveText.DOFade(1, FadeDuration);
        RestartButton.image.DOFade(1, FadeDuration);
        Background.DOFade(1, FadeDuration);
        LogoImage.DOFade(1, FadeDuration);
        finishCallback?.Invoke();
    }

    public void Hide(Action finishCallback)
    {
        Congratulations.DOFade(0, FadeDuration);
        YouveMadeIt.DOFade(0, FadeDuration);
        WaveText.DOFade(0, FadeDuration);
        RestartButton.image.DOFade(0, FadeDuration);
        Background.DOFade(0, FadeDuration);
        LogoImage.DOFade(0, FadeDuration);
    }
}
