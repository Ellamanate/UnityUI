using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityUI.Utils
{
    public class AsyncStateMachine<TState> : BaseStateMachine<TState> where TState : class, IState
    {
        public async UniTask<T> MoveToStateAsync<T>(CancellationToken cancellationToken) 
            where T : class, IAsyncEnteringState
        {
            var state = await ChangeState<T>(cancellationToken);
            await state.Enter(cancellationToken);
            
            return state;
        }

        public async UniTask<T> MoveToStateAsync<T, TParam>(TParam value, CancellationToken cancellationToken) 
            where T : class, IAsyncEnteringState<TParam>
        {
            var state = await ChangeState<T>(cancellationToken);
            await state.Enter(value, cancellationToken);
            
            return state;
        }
        
        private async UniTask<T> ChangeState<T>(CancellationToken cancellationToken) where T : class, IState
        {
            await ExitState(cancellationToken);
            return SetState<T>();
        }

        private async UniTask ExitState(CancellationToken cancellationToken)
        {
            if (CurrentState is IAsyncExitingState exitingState)
            {
                await exitingState.Exit(cancellationToken);
            }
        }
    }
}