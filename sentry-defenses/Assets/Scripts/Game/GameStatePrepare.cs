using Manager;

namespace Game
{
    public class GameStatePrepare : GameState
    {
        private EventManager _eventManager;
        private readonly StartMenu _startMenu;
        private readonly PickUpgradeMenu _pickUpgradeMenu;
        private readonly ApplyUpgradeMenu _applyUpgradeMenu;
        
        public GameStatePrepare(GameStateMachine stateMachine) : base(stateMachine)
        {
            _eventManager = EventManager.Instance;

            _pickUpgradeMenu = stateMachine.PickUpgradeMenu;
            _applyUpgradeMenu = stateMachine.ApplyUpgradeMenu;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _pickUpgradeMenu.Prepare();
            _applyUpgradeMenu.Prepare();
            
            StateTransition(GameStates.Fight);
        }
    }
}