using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityUI.Utils
{
    public interface IAsyncEnteringState : IState
    {
        public UniTask Enter(CancellationToken cancellationToken);
    }
    
    public interface IAsyncEnteringState<T> : IState
    {
        public UniTask Enter(T param, CancellationToken cancellationToken);
    }
}