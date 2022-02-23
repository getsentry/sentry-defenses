using System;
using Bugs;
using Manager;
using TMPro;
using UnityEngine;
using Utility.StateMachine;

public class BugStateMachine : StateMachine<BugStates>
{
    public Transform Transform;
    
    public float HitPoints;
    public float HitPointsTotal;

    public BugVisuals Visuals;
    public CircleCollider2D Collider;
    public float MovementSpeed;
    
    public float SlowdownMultiplier = 0.3f;
    public float ActiveSlowdownDuration;
    public float MaxSlowdownDuration = 0.15f;

    public bool IsPaused;
    public event Action<float> OnHit;

    protected override void Awake()
    {
        base.Awake();
        Transform = transform;
    }

    protected override void Start()
    {
        base.Start();

        var eventManager = EventManager.Instance;
        eventManager.OnGamePause += () =>
        {
            Visuals.Pause();
            IsPaused = true;
        };
        
        eventManager.OnGameResume += () =>
        {
            Visuals.Play();
            IsPaused = false;
        };
        
    }

    protected override void Initialize()
    {
        base.Initialize();

        _states.Add(BugStates.Spawn, new BugStateSpawn(this));
        _states.Add(BugStates.Move, new BugStateMove(this));
        _states.Add(BugStates.Despawn, new BugStateDespawn(this));
        _states.Add(BugStates.Attack, new BugStateAttack(this));

        _currentState = _states[BugStates.Spawn];
    }

    public void Hit(float damage) => OnHit?.Invoke(damage);
}