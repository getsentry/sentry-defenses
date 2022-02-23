using Manager;
using UnityEngine;

public class BugStateSpawn : BugState
{
    private readonly BugStateMachine _stateMachine;
    private EventManager _eventManager;

    private bool _spawnFinished;
    
    public BugStateSpawn(BugStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _eventManager = EventManager.Instance;
        _eventManager.OnReset += OnReset;
    }
    
    private void OnReset()
    {
        if (IsActive)
        {
            _stateMachine.Visuals.Kill();
            GameObject.Destroy(_stateMachine.gameObject);    
        }
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.Visuals.Spawn(() => _spawnFinished = true);
    }

    public override void Tick()
    {
        base.Tick();
        if (_stateMachine.IsPaused)
        {
            return;
        }

        if (_spawnFinished)
        {
            StateTransition(BugStates.Move);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _stateMachine.Collider.enabled = true;
    }
}
