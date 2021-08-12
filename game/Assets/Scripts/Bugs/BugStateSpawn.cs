using Manager;
using UnityEngine;

public class BugStateSpawn : BugState
{
    private readonly BugStateMachine _stateMachine;
    private EventManager _eventManager;
    
    public BugStateSpawn(BugStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _eventManager = EventManager.Instance;
        _eventManager.Resetting += OnReset;
    }
    
    private void OnReset()
    {
        if (IsActive)
        {
            GameObject.Destroy(_stateMachine.gameObject);    
        }
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.Visuals.Spawn(() => StateTransition(BugStates.Move));
    }

    public override void OnExit()
    {
        base.OnExit();
        _stateMachine.Collider.enabled = true;
    }
}
