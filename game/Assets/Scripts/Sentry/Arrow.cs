using System;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float MovementSpeed = 20.0f;
    
    private float _damage;
    private Vector3 _direction;

    public float MaxLifeTime = 5.0f;
    private Coroutine _selfDestructRoutine;

    private void Start()
    {
        _selfDestructRoutine = StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(MaxLifeTime);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void Fire(float Damage, Vector3 direction)
    {
        _damage = Damage;
        _direction = direction;
    }

    private void Update()
    {
        transform.position += _direction * (MovementSpeed * Time.deltaTime);
        transform.right = _direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Bug"))
        {
            return;
        }
        
        var bug = collider.gameObject.GetComponent<BugStateMachine>();
        bug.Hit(_damage);
        
        StopCoroutine(_selfDestructRoutine);
        Destroy(gameObject);
    }
}
