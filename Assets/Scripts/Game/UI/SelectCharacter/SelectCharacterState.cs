using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class SelectCharacterState : BaseMenuState, IAsyncEnteringState, IAsyncExitingState, IDisposable
    {
        private readonly SelectCharacterView _view;
        private readonly CancellationTokenSource _tokenSource;

        public SelectCharacterState(SelectCharacterView view)
        {
            _view = view;
            _tokenSource = new CancellationTokenSource();
        }
        
        protected override void OnInitialize()
        {
            _view.Initialize(this);
        }

        public void Dispose()
        {
            _tokenSource.Dispose();
        }
        
        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _view.SetActive(true);
            
            await _view.Fade(1, 1, cancellationToken);
        }
        
        public async UniTask Exit(CancellationToken cancellationToken)
        {
            await _view.Fade(0, 1, cancellationToken);
            
            _view.SetActive(false);
        }

        public void OnBackButtonClicked()
        {
            _ = PageRouting.MoveToStateAsync<MainMenuState>(_tokenSource.Token);
        }
    }
}