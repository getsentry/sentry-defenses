using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private GameObject _circle;
    
    
    private GameData _data;
    private CircleCollider2D circleCollider;
    private float _coolDown;
    private List<Transform> _targets;
    private SpriteRenderer renderer;
    
    public TowerUpgrade upgrades = new TowerUpgrade();

    private void Awake()
    {
        _targets = new List<Transform>();
    }

    void Start()
    {
        _data = GameData.Instance;
        circleCollider = GetComponent<CircleCollider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // TODO(wmak): Change on upgrade rather than on update
        float radius = (float)(Math.Pow(1.10f, upgrades.Range));
        this._circle.transform.localScale = new Vector2(radius, radius);
        this.circleCollider.radius = 3f * radius;

        if (_targets.Count <= 0)
            return;
 
        _coolDown -= Time.deltaTime;
        if (_coolDown < 0.0f)
        {
            Fire();
            _coolDown = 1.0f / (float)Math.Pow(1.25f, upgrades.FireRate);
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

    public void onSelect()
    {
        renderer.color = Color.blue;
    }

    public void onDeSelect()
    {
        renderer.color = Color.white;
    }
}
