using UnityUI.Utils;

namespace UnityUI.Game
{
    public class BootstrapState : IGameState, IEnteringState
    {
        private readonly AssetsProvider _assetsProvider;
        
        private GameStateMachine _stateMachine;

        public BootstrapState(AssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }
        
        public void Initialize(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _assetsProvider.LoadAssets();
            _ = _stateMachine.MoveToState<MenuState>();
        }
    }
}