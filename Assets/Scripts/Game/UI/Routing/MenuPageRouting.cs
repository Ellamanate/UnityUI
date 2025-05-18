using System.Collections.Generic;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class MenuPageRouting : AsyncStateMachine<IMenuState>
    {
        public MenuPageRouting(
            MainMenuState mainMenuState,
            SelectCharacterState selectCharacterState,
            InitializeMenuState initializeMenuState)
        {
            States = new Dictionary<string, IMenuState>
            {
                { nameof(MainMenuState), mainMenuState.InitializeState(this) },
                { nameof(SelectCharacterState), selectCharacterState.InitializeState(this) },
                { nameof(InitializeMenuState), initializeMenuState },
            };
            
            var state = SetState<InitializeMenuState>();
            state.Enter();
        }
    }
    
    public static partial class CleanCodeExtensions
    {
        public static IMenuState InitializeState(this BaseMenuState state, MenuPageRouting stateMachine)
        {
            state.Initialize(stateMachine);
            return state;
        }
    }
}