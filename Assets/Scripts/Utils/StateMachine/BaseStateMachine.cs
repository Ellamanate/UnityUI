using System.Collections.Generic;

namespace UnityUI.Utils
{
    public class BaseStateMachine<TState> where TState : class, IState
    {
        protected Dictionary<string, TState> States;
        
        public TState CurrentState { get; private set; }
        
        protected T SetState<T>() where T : class, IState
        {
            string name = typeof(T).Name;
            var state = States[name] as T;
            CurrentState = state as TState;

            return state;
        }
    }
}