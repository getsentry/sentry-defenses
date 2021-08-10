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
    [SerializeField] private GameObject _circle;
    
    
    private GameData _data;
    private CircleCollider2D circleCollider;
    private float _coolDown;
    private List<Transform> _targets;

    private void Awake()
    {
        _targets = new List<Transform>();
    }

    void Start()
    {
        _data = GameData.Instance;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // TODO(wmak): Change on upgrade rather than on update
        this._circle.transform.localScale = new Vector2(_data.Upgrade.Range, _data.Upgrade.Range);
        this.circleCollider.radius = _data.Upgrade.Range * 3f;

        if (_targets.Count <= 0)
            return;
 
        _coolDown -= Time.deltaTime;
        if (_coolDown < 0.0f)
        {
            Fire();
            _coolDown = _data.Upgrade.FireRate;
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnTransform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(_targets[UnityEngine.Random.Range(0, _targets.Count - 1)], transform);
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
