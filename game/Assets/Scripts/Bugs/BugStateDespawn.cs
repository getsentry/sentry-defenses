using DG.Tweening;
using Manager;
using UnityEngine;

namespace Bugs
{
    public class BugStateDespawn : BugState
    {
        private readonly GameData _gameData;
        private readonly EventManager _eventManager;
        private readonly BugStateMachine _stateMachine;

        public BugStateDespawn(BugStateMachine stateMachine) : base(stateMachine)
        {
            _gameData = GameData.Instance;
            _eventManager = EventManager.Instance;
            
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _stateMachine.Visuals.Despawn();
            _gameData.bugs.Remove(_stateMachine.gameObject);
            
            GameObject.Destroy(_stateMachine.gameObject);
            
            _gameData.CurrentXp++;
        }
    }
}