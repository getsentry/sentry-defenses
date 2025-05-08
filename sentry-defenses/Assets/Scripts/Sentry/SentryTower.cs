using System.Collections.Generic;
using System.Runtime.InteropServices;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class SentryTower : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform ArrowSpawnTransform;
    public Transform CircleTransform;
    public SpriteRenderer CircleRenderer;
    
    public CircleCollider2D AttackRangeCollider;

    public GameObject WoodSentry;

    private EventManager _eventManager;
    private SentryVisuals _visuals;

    private float _coolDown;
    private List<Transform> _targets;
    
    public TowerUpgrade Upgrades = new TowerUpgrade();

    [SerializeField] private int _baseDamage = 1;
    [SerializeField] private float _baseFireRate = 1.0f;
    
    private bool _isPaused = true; // True by default to avoid shooting while building a tower
    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _eventManager.OnReset += OnReset;
        
        _targets = new List<Transform>();
        _visuals = GetComponent<SentryVisuals>();
    }

    private void Start()
    {
        _eventManager.OnGamePause += OnPause;
        _eventManager.OnGameResume += OnResume;
    }

    private void OnPause()
    {
        CircleRenderer.enabled = true;
        _isPaused = true;
    }

    private void OnResume()
    {
        CircleRenderer.enabled = false;
        _isPaused = false;
    }

    private void OnDestroy()
    {
        _eventManager.OnGamePause -= OnPause;
        _eventManager.OnGameResume -= OnResume;
    }

    private void OnReset()
    {
        if (gameObject == null)
        {
            return;
        }
        
        if (gameObject.CompareTag("Turd"))
        {
            Upgrades = new TowerUpgrade();
            gameObject.transform.localScale = Vector3.one;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (_isPaused) { return; }

        if (_targets.Count <= 0) { return; }
 
        _coolDown -= Time.deltaTime;
        if (_coolDown <= 0.0f)
        {
            _coolDown = 1 / (_baseFireRate + Upgrades.FireRate);
            Fire();
        }
    }

    public void PostUpgrade()
    {
        float radius = Mathf.Pow(1.10f, Upgrades.Range);
        CircleTransform.transform.localScale = new Vector2(radius, radius);
        AttackRangeCollider.radius = 1.35f * radius;
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
                var position = ArrowSpawnTransform.position;
                var direction = (target.position + new Vector3(0, 0.25f, 0) - position).normalized; 
                var arrowObject = Instantiate(ArrowPrefab, position, Quaternion.identity);
                arrowObject.GetComponent<Arrow>().Fire(_baseDamage + Upgrades.Damage, direction);
                
                _visuals.Fire();
                break;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            _targets.Add(other.transform);    
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _targets.Remove(other.transform);
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
