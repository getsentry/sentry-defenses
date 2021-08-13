using System;
using System.ComponentModel;
using Bugs;
using Manager;
using UnityEngine;
using Utility.StateMachine;

public class BugStateMachine : StateMachine<BugStates>
{
    public float HitPoints;
    public float HitPointsTotal;
    
    public BugVisuals Visuals;
    public Rigidbody2D Rigidbody;
    public CircleCollider2D Collider;
    public float MovementSpeed;

    public AnimationCurve HitReactionCurve;
    public float HitReactionDuration;
    public float HitPushBackForce;
    public Vector3 PushBackDirection;

    public int CoinDrop = 1;
    
    public float DamageTaken;

    public Action OnHit;
    public Action OnTargetReached;

    protected override void Initialize()
    {
        base.Initialize();
        
        _states.Add(BugStates.Spawn, new BugStateSpawn(this));
        _states.Add(BugStates.Move, new BugStateMove(this));
        _states.Add(BugStates.Hit, new BugStateHit(this));
        _states.Add(BugStates.Despawn, new BugStateDespawn(this));
        _states.Add(BugStates.Attack, new BugStateAttack(this));

        _currentState = _states[BugStates.Spawn];
    }

    public void Hit(float damage, Vector3 direction)
    {
        DamageTaken = damage;
        PushBackDirection = direction;
        
        OnHit?.Invoke();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Turd"))
        {
            OnTargetReached?.Invoke();            
        }
    }
}
