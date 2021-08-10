using UnityEngine;
using Utility.StateMachine;

public class GameStateMachine : StateMachine<GameStates>
{
    public Transform MouseTransform;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        _states.Add(GameStates.Idle, new GameStateIdle(this));
        _states.Add(GameStates.Placing, new GameStatePlacing(this));
        _states.Add(GameStates.Fighting, new GameStateFighting(this));
        _states.Add(GameStates.Upgrading, new GameStateUpgrading(this));

        _currentState = _states[GameStates.Upgrading];
    }
}
