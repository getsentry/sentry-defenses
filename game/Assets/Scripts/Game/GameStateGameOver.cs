using System.Collections.Generic;
using Manager;
using Sentry;

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
            SentrySdk.AddBreadcrumb("Game Over", "app.lifecycle", "session", new Dictionary<string, string>
            {
                {"bugs", _gameData.BugCount.ToString()},
            });
            base.OnEnter();

            _gameOverMenu.WaveText.text = $"{_gameData.BugCount.ToString()} bugs!";
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