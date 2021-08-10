using System;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button StartButton;
    public Image Background;

    public float FadeDuration = 0.3f;
    
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        StartButton.onClick.AddListener(OnStartClick);
    }

    private void OnStartClick()
    {
        _eventManager.Upgrade();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide(Action finishCallback)
    {
        StartButton.image.DOFade(0, FadeDuration);
        Background.DOFade(0, FadeDuration)
            .OnComplete(() =>
            {
                finishCallback?.Invoke();
                gameObject.SetActive(false);
            });
    }
}
