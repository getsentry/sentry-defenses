using System.Collections.Generic;
using Sentry;
using UnityEngine;

public class GameStatePlaceSentry : GameState
{
    private readonly PlayerInput _input;
    private readonly GameData _data;
    private readonly Transform _mouseTransform;

    private GameObject _sentryGameObject;
    
    public GameStatePlaceSentry(GameStateMachine stateMachine) : base(stateMachine)
    {
        _input = PlayerInput.Instance;
        _data = GameData.Instance;
        _mouseTransform = stateMachine.MouseTransform;
    }

    public override void Tick()
    {
        base.Tick();

        if (_input.GetMouseDown() && !Helpers.IsMouseOverUI())
        {
            SentrySdk.AddBreadcrumb("Mouse down", "click", "user", new Dictionary<string, string>
            {
                {"position", _mouseTransform.position.ToString()}
            });

            _sentryGameObject = GameObject.Instantiate(_data.SentryPrefab, _mouseTransform.position, Quaternion.identity, _mouseTransform);
            var sentry = _sentryGameObject.GetComponent<SentryTower>();
            sentry.Wiggle();
        }
        
        // Checking for tower because the up from the button click gets read here too
        if (_sentryGameObject != null && _input.GetMouseUp())
        {
            SentrySdk.AddBreadcrumb("Mouse up", "click", "user", new Dictionary<string, string>
            {
                {"position", _mouseTransform.position.ToString()}
            });

            var sentry = _sentryGameObject.GetComponent<SentryTower>();
            sentry.Drop();
            
            _sentryGameObject.transform.parent = null;
            _sentryGameObject = null;
            
            StateTransition(GameStates.Fight);
        }
    }
}
