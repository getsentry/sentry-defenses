using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using Utility.StateMachine;

public class GameStateIdle : GameState
{
    private readonly PlayerInput _input;
    private GameData _data;

    private EventManager _eventManager;
    
    public GameStateIdle(GameStateMachine stateMachine) : base(stateMachine)
    {
        _input = PlayerInput.Instance;
        _data = GameData.Instance;

        _eventManager = EventManager.Instance;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _eventManager.Reset();
    }

    public override void Tick()
    {
        base.Tick();

        if (_input.GetMouseDown())
        {
            StateTransition(GameStates.Placing);
        }
    }
}