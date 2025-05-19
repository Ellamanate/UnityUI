using System;
using System.Threading;

namespace UnityUI.Utils
{
    public static class AsyncExtensions
    {
        public static void CheckCanceled(this CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }
        }

        public static CancellationTokenSource Refresh(this CancellationTokenSource token)
        {
            token?.CancelAndDispose();

            return new CancellationTokenSource();
        }

        public static void CancelAndDispose(this CancellationTokenSource token)
        {
            if (token != null)
            {
                token.TryCancel();
                token.Dispose();
            }
        }

        public static void TryCancel(this CancellationTokenSource token)
        {
            if (token != null && !token.IsCancellationRequested)
            {
                token.Cancel(true);
            }
        }
    }
}