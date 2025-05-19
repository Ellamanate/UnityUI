using UnityEngine;

namespace UnityUI.Game
{
    public class CharacterModel
    {
        private readonly CharacterConfig _config;

        public CharacterModel(CharacterConfig config)
        {
            _config = config;
        }
        
        public Sprite Icon => _config.Icon;
        public Color Color => _config.Color;
        public string Id => _config.Id;
        public float Experience => _config.Experience;
    }
}