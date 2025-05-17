using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityUI.Utils
{
    public interface IAsyncExitingState
    {
        public UniTask Exit(CancellationToken cancellationToken);
    }
}