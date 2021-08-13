using Manager;

namespace Game
{
    public class GameStateGameOver : GameState
    {
        private GameData _gameData;
        private readonly EventManager _eventManager;
        private GameOverMenu _gameOverMenu;
        
        public GameStateGameOver(GameStateMachine stateMachine) : base(stateMachine)
        {
            _gameData = GameData.Instance;
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

            _gameOverMenu.WaveText.text = $"Wave {_gameData.Level.ToString()}!";
            _gameOverMenu.Show(null);
            
            _eventManager.Reset();
        }

        public override void OnExit()
        {
            base.OnExit();
            _gameOverMenu.Hide(null);
        }
    }
}