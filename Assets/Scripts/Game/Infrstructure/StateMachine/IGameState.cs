using UnityUI.Utils;

namespace UnityUI.Game
{
    public interface IGameState : IState
    {
        public void Initialize(GameStateMachine stateMachine);
    }
}