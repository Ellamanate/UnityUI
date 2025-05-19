using UnityEngine;

namespace UnityUI.Game
{
    public class AssetsProvider
    {
        public CharacterView CharacterViewPrefab;
        public CharacterConfig[] CharactersConfigs;
        
        public void LoadAssets()
        {
            LoadCharacters();
        }

        private void LoadCharacters()
        {
            CharacterViewPrefab = Resources.Load<CharacterView>("Characters/Prefabs/CharacterView");
            CharactersConfigs = Resources.LoadAll<CharacterConfig>("Characters/Configs");
        }
    }
}