using DG.Tweening;
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
    
    private AsyncOperation _gameLoadOperation;
    
    private void Start()
    {
        StartButton.onClick.AddListener(OnStartClick);
        SampleButton.onClick.AddListener(OnSampleClick);
    }

    private void OnStartClick()
    {
        Logo.SetTrigger("Active");
        
        _gameLoadOperation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        _gameLoadOperation.allowSceneActivation = true;
    }

    private void OnSampleClick() => SceneManager.LoadScene("1_Bugfarm");
    
    public void Hide()
    {
        Logo.SetTrigger("Active");
    }

    public void LogoFinished()
    {
        Header.DOFade(0, FadeDuration);
        StartButton.image.DOFade(0, FadeDuration);
        Background.DOFade(0, FadeDuration);
        LogoImage.DOFade(0, FadeDuration).OnComplete(() => {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        });
    }
}
