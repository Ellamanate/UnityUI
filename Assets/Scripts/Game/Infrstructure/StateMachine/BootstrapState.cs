using UnityUI.Utils;

namespace UnityUI.Game
{
    public class BootstrapState : IGameState, IEnteringState
    {
        private GameStateMachine _stateMachine;
        
        public void Initialize(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _ = _stateMachine.MoveToState<MenuState>();
        }
    }
}