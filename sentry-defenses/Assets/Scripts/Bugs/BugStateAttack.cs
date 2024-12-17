using Manager;
using UnityEngine;

namespace Bugs
{
    public class BugStateAttack : BugState
    {
        private readonly GameData _gameData;
        private EventManager _eventManager;
        private readonly BugStateMachine _stateMachine;
        
        public BugStateAttack(BugStateMachine stateMachine) : base(stateMachine)
        {
            _gameData = GameData.Instance;
            _eventManager = EventManager.Instance;
            
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _gameData.HitPoints--;
            _eventManager.UpdateHitPoints();
            
            _gameData.bugs.Remove(_stateMachine.gameObject);
            GameObject.Destroy(_stateMachine.gameObject);    
        }
    }
}