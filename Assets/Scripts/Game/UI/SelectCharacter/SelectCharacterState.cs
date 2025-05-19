using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class SelectCharacterState : BaseMenuState, IAsyncEnteringState, IAsyncExitingState, IDisposable
    {
        private readonly SelectCharacterView _view;
        private readonly CharactersViewsFactory _charactersViewsFactory;
        private readonly CharactersService _charactersService;
        private readonly CancellationTokenSource _tokenSource;

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
            _tokenSource.Dispose();
        }
        
        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _view.SetActive(true);
            
            await _view.Show(cancellationToken);
        }
        
        public async UniTask Exit(CancellationToken cancellationToken)
        {
            await _view.Hide(cancellationToken);
            
            _view.SetActive(false);
        }

        public void OnBackButtonClicked()
        {
            _ = PageRouting.MoveToStateAsync<MainMenuState>(_tokenSource.Token);
        }
        
        public void OnCharacterClicked(string id)
        {
            SelectCharacter(id);
        }

        private void SelectCharacter(string id)
        {
            var characterModel = _charactersService.GetCharacter(id);
            _view.SelectCharacter(id);
            _view.SetIcon(characterModel.Icon, characterModel.Color);
            _view.SetExperience(characterModel.Experience);
        }
    }
}