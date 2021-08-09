using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TargetVisuals : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    [SerializeField] private float _rotation = 30.0f;
    [SerializeField] private float _rotationSpeed = 2.0f;
    [SerializeField] private Ease _rotationEase = Ease.InOutSine;
    
    
    // Start is called before the first frame update
    void Start()
    {
        var sequence = DOTween.Sequence()
            .Append(_targetTransform.DORotate(new Vector3(0, 0, _rotation), _rotationSpeed)
                .SetEase(_rotationEase))
            .Append(_targetTransform.DORotate(new Vector3(0, 0, -_rotation), _rotationSpeed)
                .SetEase(_rotationEase))
            .SetLoops(-1, LoopType.Yoyo)
            ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
