public class BugStateSpawn : BugState
{
    private readonly BugStateMachine _stateMachine;
    
    public BugStateSpawn(BugStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _stateMachine.Visuals.Spawn(() => StateTransition(BugStates.Move));
    }
}
