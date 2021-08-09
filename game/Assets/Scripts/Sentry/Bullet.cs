using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _movementSpeed = 3.0f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        // _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        var direction = (_target.position - transform.position).normalized;
        transform.position += direction * (_movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bug = other.gameObject.GetComponent<Bug>();
        bug.HitPoints -= GameData.Instance.Upgrade.Damage;
        
        if(bug.HitPoints <= 0) {
            GameData.Instance.bugs.Remove(bug.gameObject);
            Destroy(bug.gameObject);
        }
        
        Destroy(gameObject);
    }
}
