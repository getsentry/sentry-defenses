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
    
    public GameObject Container;
    
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
        Container.SetActive(true);
    }
    
    public void Hide(Action finishCallback)
    {
        StartButton.image.DOFade(0, FadeDuration);
        Background.DOFade(0, FadeDuration)
            .OnComplete(() =>
            {
                finishCallback?.Invoke();
                Container.SetActive(false);
            });
    }
}
