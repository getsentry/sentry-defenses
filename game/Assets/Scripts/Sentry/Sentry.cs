using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform ArrowSpawnTransform;
    public GameObject CircleVisual;
    public CircleCollider2D AttackRangeCollider;
    
    private GameData _data;
    private SentryVisuals _visuals;
    
    private float _coolDown;
    private List<Transform> _targets;
    
    public TowerUpgrade upgrades = new TowerUpgrade();

    private void Awake()
    {
        _targets = new List<Transform>();
        _visuals = GetComponent<SentryVisuals>();
    }

    void Start()
    {
        _data = GameData.Instance;
    }

    void Update()
    {
        // TODO(wmak): Change on upgrade rather than on update
        float radius = Mathf.Pow(1.10f, upgrades.Range);
        CircleVisual.transform.localScale = new Vector2(radius, radius);
        AttackRangeCollider.radius = 1.35f * radius;

        if (_targets.Count <= 0)
            return;
 
        _coolDown -= Time.deltaTime;
        if (_coolDown < 0.0f)
        {
            Fire();
            _coolDown = 1.0f / Mathf.Pow(1.25f, upgrades.FireRate);
        }
    }

    private void Fire()
    {
        while (_targets.Count > 0)
        {
            var targetIndex = Random.Range(0, _targets.Count - 1);
            var target =_targets[targetIndex];
            if(target == null)
            {
                _targets.RemoveAt(targetIndex);
            }
            else
            {
                var bullet = Instantiate(ArrowPrefab, ArrowSpawnTransform.position, Quaternion.identity);
                bullet.GetComponent<Arrow>().SetTarget(Mathf.Pow(1.5f, upgrades.Damage), target, transform);
                break;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _targets.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _targets.Remove(other.transform);
    }

    public void Select()
    {
        _visuals.Select();
    }

    public void Deselect()
    {
        _visuals.Deselect();
    }

    public void Wiggle()
    {
        _visuals.Wiggle();
    }
    
    public void Drop()
    {
        _visuals.Drop();
    }
}
