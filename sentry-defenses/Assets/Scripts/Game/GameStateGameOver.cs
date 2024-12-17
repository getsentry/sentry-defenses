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
            _eventManager.OnFight += OnFight;

            _gameOverMenu = stateMachine.GameOverMenu;
        }

        private void OnFight()
        {
            if (!IsActive)
            {
                return;
            }
            
            StateTransition(GameStates.Fight);
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _gameOverMenu.SetBugCount(_gameData.BugCount);
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