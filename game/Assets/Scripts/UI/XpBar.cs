using DG.Tweening;
using Manager;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    public RectTransform RectTransform;
    [SerializeField] private float ScaleEffectDuration = 0.25f;
    public Ease ScaleEase = Ease.OutBack;

    private bool _beReadyToReset;
    
    private GameData _data;
    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        
        _data = GameData.Instance;

        var eventManager = EventManager.Instance;
        eventManager.OnUpdateXp += OnUpdateXp;
        eventManager.OnLevelUpXp += Reset;
        eventManager.OnReset += Reset;
    }

    private void Start()
    {
        Reset();
    }

    private void OnUpdateXp()
    {
        var targetSize = 160 * (float)_data.CurrentXp / _data.LevelUpRequirement;
        RectTransform.DOSizeDelta(new Vector2(targetSize, RectTransform.sizeDelta.y), ScaleEffectDuration)
            .OnUpdate(() =>
            {
                if (RectTransform.sizeDelta.x > 160)
                {
                    RectTransform.sizeDelta = new Vector2(160, RectTransform.sizeDelta.y);
                }
            })
            .SetEase(ScaleEase);
    }

    private void Reset()
    {
        RectTransform.DOKill();
        RectTransform.sizeDelta = new Vector3(10, RectTransform.sizeDelta.y);
    }
}
