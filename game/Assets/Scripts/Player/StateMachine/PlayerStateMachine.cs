using UnityEngine;
using Utility.StateMachine;

public class PlayerStateMachine : StateMachine<PlayerStates>
{
    public PlayerData PlayerData;
    public Transform MouseTransform;

    protected override void Awake()
    {
        base.Awake();

        PlayerData = new PlayerData();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _states.Add(PlayerStates.Idle, new PlayerIdle(this));
        _states.Add(PlayerStates.Placing, new PlayerPlacing(this));

        _currentState = _states[PlayerStates.Idle];
    }
}
