using Manager;
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
            _sentryGameObject = GameObject.Instantiate(_data.SentryPrefab, _mouseTransform.position, Quaternion.identity, _mouseTransform);
            var sentry = _sentryGameObject.GetComponent<SentryTower>();
            sentry.Wiggle();
            return;
        }
        
        // Checking for tower because the up from the button click gets read here too
        if (_sentryGameObject != null && _input.GetMouseUp())
        {
            var sentry = _sentryGameObject.GetComponent<SentryTower>();
            sentry.Drop();
            
            _sentryGameObject.transform.parent = null;
            _sentryGameObject = null;
            
            StateTransition(GameStates.Fight);
        }
    }
}
