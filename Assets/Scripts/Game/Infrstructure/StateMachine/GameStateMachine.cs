using System.Collections.Generic;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class GameStateMachine : StateMachine<IGameState>
    {
        public GameStateMachine(BootstrapState bootstrapState, MenuState menuState)
        {
            States = new Dictionary<string, IGameState>
            {
                { nameof(BootstrapState), bootstrapState.InitializeState(this) },
                { nameof(MenuState), menuState.InitializeState(this) },
            };
            
            var state = SetState<BootstrapState>();
            state.Enter();
        }
    }
    
    public static partial class CleanCodeExtensions
    {
        public static IGameState InitializeState(this IGameState state, GameStateMachine stateMachine)
        {
            state.Initialize(stateMachine);
            return state;
        }
    }
}