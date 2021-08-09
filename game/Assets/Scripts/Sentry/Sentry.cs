using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    [SerializeField] private float _fireRate = 1.0f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    
    
    private float _coolDown;
    private List<Transform> _targets;

    private bool _isActive;
    
    private void Awake()
    {
        _targets = new List<Transform>();
    }

    void Start()
    {
        
    }

    public void Activate()
    {
        _isActive = true;
    }
    
    void Update()
    {
        if (!_isActive)
        {
            return;
        }
        
        if (_targets.Count <= 0)
            return;
        
        _coolDown -= Time.deltaTime;
        if (_coolDown < 0.0f)
        {
            Fire();
            _coolDown = _fireRate;
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnTransform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(_targets[0]);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _targets.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _targets.Remove(other.transform);
    }
}
