using Manager;
using Utility.StateMachine;

namespace Game
{
    public class GameStateGameOver : GameState
    {
        private readonly EventManager _eventManager;
        private GameOverMenu _gameOverMenu;
        
        public GameStateGameOver(GameStateMachine stateMachine) : base(stateMachine)
        {
            _eventManager = EventManager.Instance;
            _eventManager.Upgrading += OnUpgrading;

            _gameOverMenu = stateMachine.GameOverMenu;
        }

        private void OnUpgrading()
        {
            if (!IsActive)
            {
                return;
            }
            
            StateTransition(GameStates.Upgrading);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _gameOverMenu.Show();
            _eventManager.Reset();
        }

        public override void OnExit()
        {
            base.OnExit();
            _gameOverMenu.Hide();
        }
    }
}