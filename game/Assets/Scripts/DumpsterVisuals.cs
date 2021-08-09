using System;
using DG.Tweening;
using UnityEngine;

public class DumpsterVisuals : MonoBehaviour
{
    public float Increase = 0.3f;
    public float IncreaseDuration = 0.35f;
    public Ease IncreaseEase = Ease.OutBack;
    
    public float HoldIncreaseDuration = 0.15f;
    
    public float Shrink = 0.3f;
    public float ShrinkDuration = 0.5f;
    public Ease ShrinkEase = Ease.OutBounce;
    
    public float HoldShrinkDuration = 0.15f;
    
    public Ease ResetEase = Ease.InOutSine;
    public float ResetDuration = 0.3f;
    
    public void Spawn(Action finishCallback)
    {
        DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1 - Increase, 1 + Increase, 1), IncreaseDuration)
                .SetEase(IncreaseEase))
            .AppendInterval(HoldIncreaseDuration)
            .Append(transform.DOScale(new Vector3(1 + Shrink, 1 - Shrink, 1), ShrinkDuration)
                .SetEase(ShrinkEase))
            .AppendCallback(() => finishCallback?.Invoke())
            .AppendInterval(HoldShrinkDuration)
            .Append(transform.DOScale(Vector3.one, ResetDuration)
                .SetEase(ResetEase));
    }
}
