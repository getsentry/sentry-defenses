using System;
using DG.Tweening;
using UnityEngine;

public class BugVisuals : MonoBehaviour
{
    public Transform BounceTransform;

    [Header("Spawn")] 
    public float StartScale = 0.3f;
    public float StartHeight = 0.25f;
    public float StartScaleDuration = 0.3f;
    public float HoldDuration = 0.1f;

    [Header("Drop")]
    public float FallDuration = 0.07f;
    public float SquashStretch = 0.3f;
    public float SquashDuration = 0.07f;
    
    private Sequence _spawnSequence;
    
    public void Spawn(Action finishCallback = null)
    {
        var endPosition = BounceTransform.position;
        BounceTransform.localScale = Vector3.one * StartScale;
        BounceTransform.localPosition = new Vector3(0, StartHeight, 0);

        _spawnSequence = DOTween.Sequence()
            .Append(BounceTransform.DOScale(1, StartScaleDuration).SetEase(Ease.OutBack))
            .AppendInterval(HoldDuration)
            .Append(BounceTransform.DOMoveY(endPosition.y, FallDuration).SetEase(Ease.OutQuad))
            .Insert(StartScaleDuration + HoldDuration,
                BounceTransform.DOScale(new Vector3(1 - SquashStretch, 1 + SquashStretch), FallDuration)
                    .SetEase(Ease.OutBack))
            .Append(BounceTransform.DOScale(new Vector3(1 + SquashStretch, 1 - SquashStretch), SquashDuration)
                .SetEase(Ease.OutBack))
            .Append(BounceTransform.DOScale(Vector3.one, SquashDuration).SetEase(Ease.OutBack))
            .AppendCallback(() =>
            {
                finishCallback?.Invoke();
            });
    }
}
