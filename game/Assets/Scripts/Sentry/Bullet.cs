using UnityEngine;
using Manager;

public class Bullet : MonoBehaviour
{
    public float MovementSpeed = 20.0f;
    
    private Transform _target;
    private Transform _origin;
    
    private GameData _gameData;
    private EventManager _eventManager;

    private void Awake()
    {
        _gameData = GameData.Instance;
        _eventManager = EventManager.Instance;
    }

    public void SetTarget(Transform target, Transform origin)
    {
        _target = target;
        _origin = origin;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var direction = (_target.position - transform.position).normalized;
        transform.position += direction * (MovementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bug"))
        {
            return;
        }
        
        var bug = other.GetComponent<BugStateMachine>();

        var hitDirection = (other.transform.position - _origin.position).normalized;
        bug.Hit(_gameData.Upgrade.Damage, hitDirection);
        
        Destroy(gameObject);
    }
}