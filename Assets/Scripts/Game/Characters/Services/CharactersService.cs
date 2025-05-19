using System.Collections.Generic;
using System.Linq;

namespace UnityUI.Game
{
    public class CharactersService
    {
        private readonly Dictionary<string, CharacterModel> _characters;
        
        public CharactersService(AssetsProvider assetsProvider)
        {
            _characters = assetsProvider.CharactersConfigs
                .ToDictionary(x => x.Id, x => new CharacterModel(x));
        }

        public CharacterModel GetCharacter(string id)
        {
            return _characters[id];
        }
        
        public CharacterModel[] GetAllCharacters()
        {
            return _characters.Values.ToArray();
        }
    }
}