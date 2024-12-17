using Manager;

namespace Game
{
    public class GameStateStart : GameState
    {
        private EventManager _eventManager;
        private readonly StartMenu _startMenu;
        private readonly PickUpgradeMenu _pickUpgradeMenu;
        private readonly ApplyUpgradeMenu _applyUpgradeMenu;
        
        public GameStateStart(GameStateMachine stateMachine) : base(stateMachine)
        {
            _eventManager = EventManager.Instance;
            _eventManager.OnFight += OnStart;

            _startMenu = stateMachine.StartMenu;
            _pickUpgradeMenu = stateMachine.PickUpgradeMenu;
            _applyUpgradeMenu = stateMachine.ApplyUpgradeMenu;
        }

        private void OnStart()
        {
            if (!IsActive)
            {
                return;
            }
            
            _startMenu.Hide(() => StateTransition(GameStates.Fight));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _startMenu.Show();
            _pickUpgradeMenu.Hide(null);
            _applyUpgradeMenu.Hide(null);
        }
    }
}