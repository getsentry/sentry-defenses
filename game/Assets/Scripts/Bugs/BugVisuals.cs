using System;
using DG.Tweening;
using UnityEngine;

public class BugVisuals : MonoBehaviour
{
    public Transform BounceTransform;
    public SpriteRenderer Body;
    public SpriteRenderer Shadow;
    
    [Header("Health")]
    public Transform HealthPointBar;
    public float HealthLossSpeed = 0.25f;

    [Header("Spawn")] 
    public float StartScale = 0.3f;
    public float StartHeight = 0.25f;
    public float StartScaleDuration = 0.3f;
    public float HoldDuration = 0.1f;

    [Header("Drop")]
    public float FallDuration = 0.07f;
    public float SquashStretch = 0.3f;
    public float SquashDuration = 0.07f;

    [Header("Hit")] 
    public SpriteRenderer Renderer;
    public float FlashDuration = 0.05f;
    
    [Header("Despawn")]
    public ParticleSystem DespawnPrefab;
    
    private Sequence _spawnSequence;
    
    public void Spawn(Action finishCallback = null)
    {
        var directionToTarget = (Vector3.zero - transform.position).normalized;
        if (directionToTarget.x < 0)
        {
            Body.flipX = true;
        }
        
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
    
    public void Hit(float HitPoints, float HitPointsTotal)
    {
        HealthPointBar.DOScaleX(Mathf.Max(HitPoints, 0) / HitPointsTotal, HealthLossSpeed)
            .SetEase(Ease.OutSine);
        
        if (DOTween.IsTweening(Renderer.material))
        {
            return;
        }
        
        Renderer.material.DOFloat(1.0f, "_Flash", FlashDuration)
            .SetLoops(2, LoopType.Yoyo);
    }

    public void Despawn()
    {
        Instantiate(DespawnPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
