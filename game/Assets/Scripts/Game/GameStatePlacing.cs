using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using Utility.StateMachine;

public class GameStatePlacing : GameState
{
    private readonly PlayerInput _input;
    private readonly GameData _data;
    private EventManager _eventManager;

    private readonly Transform _mouseTransform;

    private GameObject _tower;
    
    public GameStatePlacing(GameStateMachine stateMachine) : base(stateMachine)
    {
        _input = PlayerInput.Instance;
        _data = GameData.Instance;
        _eventManager = EventManager.Instance;

        _mouseTransform = stateMachine.MouseTransform;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        _tower = GameObject.Instantiate(_data.SentryPrefab, _mouseTransform);
    }

    public override void Tick()
    {
        base.Tick();

        if (_input.GetMouseUp())
        {
            _tower.GetComponent<Sentry>().Activate();
            _tower.transform.parent = null;
            _tower = null;

            _data.PlacedSentryCount++;
            _eventManager.SentryPlaced();
            
            if (_data.PlacedSentryCount == _data.MaxSentryCount)
            {
                StateTransition(GameStates.Fighting);
                return;
            }
            
            StateTransition(GameStates.Idle);            
        }
    }
}
