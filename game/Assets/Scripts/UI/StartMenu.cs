using System;
using DG.Tweening;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button StartButton;
    public Button SampleButton;
 
    [Header("Things that fade")]
    public TextMeshProUGUI Header;
    public Image Background;
    public Image LogoImage;
    public Animator Logo;

    public float FadeDuration = 0.3f;
    
    public GameObject Container;

    private EventManager _eventManager;
    private Action _hideFinishedCallback;
    
    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void Start()
    {
        StartButton.onClick.AddListener(OnStartClick);
        SampleButton.onClick.AddListener(OnSampleClick);
    }

    private void OnStartClick() => _eventManager.Upgrade();

    private void OnSampleClick() => SceneManager.LoadScene("1_Bugfarm");

    public void Show()
    {
        gameObject.SetActive(true);
        Container.SetActive(true);
    }
    
    public void Hide(Action finishCallback)
    {
        Logo.SetTrigger("Active");
        _hideFinishedCallback = finishCallback;
    }

    public void LogoFinished()
    {
        //  Hack: We want the next state to already do it's thing so the transition is smooth but we have to wait or
        // the logo animation to finish
        _hideFinishedCallback?.Invoke();

        Header.DOFade(0, FadeDuration);
        StartButton.image.DOFade(0, FadeDuration);
        Background.DOFade(0, FadeDuration);
        LogoImage.DOFade(0, FadeDuration).OnComplete(() => {
            Container.SetActive(false);
            gameObject.SetActive(false);
        });
    }
}
