using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUI.Game
{
    public class CharactersViewsFactory
    {
        private readonly AssetsProvider _assetsProvider;
        private readonly CharactersService _charactersService;

        public CharactersViewsFactory(
            AssetsProvider assetsProvider,
            CharactersService charactersService)
        {
            _assetsProvider = assetsProvider;
            _charactersService = charactersService;
        }

        public Dictionary<string, CharacterView> CreateCharacters(SelectCharacterState controller, Transform parent)
        {
            return _charactersService.GetAllCharacters()
                .ToDictionary(x => x.Id, x => Instantiate(x, controller, parent));
        }

        private CharacterView Instantiate(CharacterModel model, SelectCharacterState controller, Transform parent)
        {
            var view = Object.Instantiate(_assetsProvider.CharacterViewPrefab, parent);
            view.Initialize(controller, model.Id);
            view.SetIcon(model.Icon, model.Color);

            return view;
        }
    }
}