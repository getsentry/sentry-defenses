using UnityEngine;
using Manager;

public class Arrow : MonoBehaviour
{
    public float MovementSpeed = 20.0f;
    
    private Transform _target;
    private Transform _origin;
    private float _damage;
    
    private GameData _gameData;
    private EventManager _eventManager;

    private void Awake()
    {
        _gameData = GameData.Instance;
        _eventManager = EventManager.Instance;
    }

    public void SetTarget(float Damage, Transform target, Transform origin)
    {
        _target = target;
        _origin = origin;
        _damage = Damage;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        var targetPosition = _target.position + new Vector3(0, 0.25f, 0);
        var direction = (targetPosition - transform.position).normalized;
        transform.position += direction * (MovementSpeed * Time.deltaTime);
        transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bug"))
        {
            return;
        }
        
        var bug = other.GetComponent<BugStateMachine>();

        var hitDirection = (other.transform.position - _origin.position).normalized;
        bug.Hit(_damage, hitDirection);
        
        Destroy(gameObject);
    }
}
