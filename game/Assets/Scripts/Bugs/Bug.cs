using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Bug : MonoBehaviour
{
    public int HitPoints = 2;
    
    [SerializeField] private float _movementSpeed = 2;
    
    private Vector3 _targetPosition = Vector3.zero;

    private bool _canMove;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GetComponent<BugVisuals>().Spawn(() => _canMove = true);
    }
    
    private void Update()
    {
        if (!_canMove)
            return;
        
        var direction = (_targetPosition - transform.position).normalized;
        // transform.position += direction * (_movementSpeed * Time.deltaTime);

        _rigidbody.velocity = direction * _movementSpeed;
    }
}
