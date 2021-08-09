using Utility.StateMachine;

public class GameStateMachine : StateMachine<GameStates>
{
    protected override void Initialize()
    {
        base.Initialize();
        
        _states.Add(GameStates.Placing, new GamePlacing(this));
        _states.Add(GameStates.Fighting, new GameFighting(this));

        _currentState = _states[GameStates.Placing];
    }
}
