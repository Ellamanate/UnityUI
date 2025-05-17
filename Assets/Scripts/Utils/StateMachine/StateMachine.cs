namespace UnityUI.Utils
{
    public class StateMachine<TState> : BaseStateMachine<TState> where TState : class, IState
    {
        public T MoveToState<T>() where T : class, IEnteringState
        {
            var state = ChangeState<T>();
            state.Enter();
            
            return state;
        }

        public T MoveToState<T, TParam>(TParam value) where T : class, IEnteringState<TParam>
        {
            var state = ChangeState<T>();
            state.Enter(value);
            
            return state;
        }
        
        private T ChangeState<T>() where T : class, IState
        {
            ExitState();
            return SetState<T>();
        }

        private void ExitState()
        {
            if (CurrentState is IExitingState exitingState)
            {
                exitingState.Exit();
            }
        }
    }
}