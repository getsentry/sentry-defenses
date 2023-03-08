using System;
using DG.Tweening;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Image _background;
    [SerializeField] private Image _logoImage;

    [SerializeField] private TextMeshProUGUI _congratulations;
    [SerializeField] private TextMeshProUGUI _youSentryd;

    [SerializeField] private float _fadeDuration = 0.3f;
    private EventManager _eventManager;
    
    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);

        var buttonColor = _restartButton.image.color;
        buttonColor.a = 0;
        _restartButton.image.color = buttonColor;

        var backgroundColor = _background.color;
        backgroundColor.a = 0;
        _background.color = backgroundColor;
 
        _container.SetActive(false);
    }

    public void SetBugCount(int count)
    {
        _youSentryd.text = $"You've senrty'd {count} bugs!";
    }

    private void OnRestartButtonClick() => _eventManager.StartFight();

    public void Show(Action finishCallback)
    {
        _container.SetActive(true);

        _congratulations.DOFade(1, _fadeDuration);
        _youSentryd.DOFade(1, _fadeDuration);
        _restartButton.image.DOFade(1, _fadeDuration);
        _background.DOFade(1, _fadeDuration);
        _logoImage.DOFade(1, _fadeDuration);
        
        finishCallback?.Invoke();
    }

    public void Hide(Action finishCallback)
    {
        _congratulations.DOFade(0, _fadeDuration);
        _youSentryd.DOFade(0, _fadeDuration);
        _restartButton.image.DOFade(0, _fadeDuration);
        _background.DOFade(0, _fadeDuration);
        _logoImage.DOFade(0, _fadeDuration)
            .OnComplete(() =>
            {
                _container.SetActive(false);
                finishCallback?.Invoke();
            });
    }
}
