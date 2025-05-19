using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class MainMenuState : BaseMenuState, IAsyncEnteringState, IAsyncExitingState, IDisposable
    {
        private readonly MainMenuView _view;
        private readonly CancellationTokenSource _tokenSource;

        private bool _isActive;
        
        public MainMenuState(MainMenuView view)
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
            _tokenSource.CancelAndDispose();
        }
        
        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _isActive = true;
            _view.SetActive(true);
            
            await _view.Show(cancellationToken);
        }
        
        public async UniTask Exit(CancellationToken cancellationToken)
        {
            _isActive = false;
            
            await _view.Hide(cancellationToken);
            
            _view.SetActive(false);
        }

        public void OnCharacterButtonClicked()
        {
            if (_isActive)
            {
                _ = PageRouting.MoveToStateAsync<SelectCharacterState>(_tokenSource.Token);
            }
        }
    }
}