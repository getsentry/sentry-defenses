using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class SentryVisuals : MonoBehaviour
{
    public SpriteRenderer WoodRenderer;
    public SpriteRenderer BrickRenderer;
    
    public Transform BounceTransform;

    [Header("Spawn")] 
    public float StartScale = 0.3f;
    public float StartHeight = 0.25f;
    public float FallDuration = 0.07f;
    public float SquashStretch = 0.3f;
    public float SquashDuration = 0.07f;

    [Header("Wiggle")]
    public float WiggleSpeed = 0.5f;
    public float WiggleStrength = 10.0f;

    private Sequence _wiggleSequence;
    
    [Header("Selection")]
    public float PunchStrength = 0.15f;
    public float PunchDuration = 0.1f;
    public Material OutlineMaterial;
    private Material _defaultMaterial;

    private void Awake()
    {
        _defaultMaterial = WoodRenderer.material;
    }

    public void Wiggle()
    {
        BounceTransform.localPosition = new Vector3(0, StartHeight, 0);
        
        _wiggleSequence = DOTween.Sequence()
            .Append(BounceTransform.DOLocalRotate(new Vector3(0, 0, WiggleStrength), WiggleSpeed)
                .SetEase(Ease.InOutSine))
            .Append(BounceTransform.DOLocalRotate(new Vector3(0, 0, -WiggleStrength), WiggleSpeed)
                .SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Drop()
    {
        var endPosition = Vector3.zero;
        BounceTransform.localScale = Vector3.one * StartScale;
        BounceTransform.localPosition = new Vector3(0, StartHeight, 0);

        _wiggleSequence.Kill();
        BounceTransform.rotation = Quaternion.identity;

        DOTween.Sequence()
            .Append(BounceTransform.DOLocalMoveY(endPosition.y, FallDuration)
                .SetEase(Ease.OutQuad))
            .Insert(0, BounceTransform.DOScale(new Vector3(1 - SquashStretch, 1 + SquashStretch), FallDuration)
                .SetEase(Ease.OutBack))
            .Append(BounceTransform.DOScale(new Vector3(1 + SquashStretch, 1 - SquashStretch), SquashDuration)
                .SetEase(Ease.OutBack))
            .Append(BounceTransform.DOScale(Vector3.one, SquashDuration)
                .SetEase(Ease.OutBack))
            ;
    }
    
    public void Select()
    {
        WoodRenderer.material = OutlineMaterial;
        BrickRenderer.material = OutlineMaterial;

        transform.DOKill();
        transform.DOPunchScale(Vector3.one * PunchStrength, PunchDuration);
    }
    
    public void Deselect()
    {
        WoodRenderer.material = _defaultMaterial;
        BrickRenderer.material = _defaultMaterial;
    }
}