using Manager;

namespace Game
{
    public class GameStateStart : GameState
    {
        private EventManager _eventManager;
        private readonly StartMenu _startMenu;
        
        public GameStateStart(GameStateMachine stateMachine) : base(stateMachine)
        {
            _eventManager = EventManager.Instance;
            _eventManager.Upgrading += OnUpgrade;

            _startMenu = stateMachine.StartMenu;
        }

        private void OnUpgrade()
        {
            if (!IsActive)
            {
                return;
            }
            
            _startMenu.Hide(() => StateTransition(GameStates.Upgrading));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _startMenu.Show();
        }
    }
}