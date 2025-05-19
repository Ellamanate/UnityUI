using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class SelectCharacterState : BaseMenuState, IAsyncEnteringState, IAsyncExitingState, IDisposable
    {
        private readonly SelectCharacterView _view;
        private readonly CharactersViewsFactory _charactersViewsFactory;
        private readonly CharactersService _charactersService;
        private readonly CancellationTokenSource _tokenSource;

        private bool _isActive;
        
        public SelectCharacterState(
            SelectCharacterView view,
            CharactersViewsFactory charactersViewsFactory,
            CharactersService charactersService)
        {
            _view = view;
            _charactersViewsFactory = charactersViewsFactory;
            _charactersService = charactersService;
            _tokenSource = new CancellationTokenSource();
        }
        
        protected override void OnInitialize()
        {
            _view.Initialize(this);
            _view.InstantiateCharacters(_charactersViewsFactory);
            SelectCharacter(_view.GetDefaultViewId());
        }

        public void Dispose()
        {
            _tokenSource.CancelAndDispose();
        }
        
        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _isActive = true;
            _view.SetActive(true);
            _view.ToDefault();
            
            await _view.Show(cancellationToken);
            
            SelectCharacter(_view.GetDefaultViewId());
        }
        
        public async UniTask Exit(CancellationToken cancellationToken)
        {
            _isActive = false;
            
            await _view.Hide(cancellationToken);
            
            _view.SetActive(false);
        }

        public void OnBackButtonClicked()
        {
            if (_isActive)
            {
                _ = PageRouting.MoveToStateAsync<MainMenuState>(_tokenSource.Token);
            }
        }
        
        public void OnCharacterClicked(string id)
        {
            if (_isActive)
            {
                SelectCharacter(id);
            }
        }

        private void SelectCharacter(string id)
        {
            var characterModel = _charactersService.GetCharacter(id);
            _view.SelectCharacter(id);
            _view.SetCharacterInfo(characterModel.Icon, characterModel.Color, characterModel.Experience);
        }
    }
}