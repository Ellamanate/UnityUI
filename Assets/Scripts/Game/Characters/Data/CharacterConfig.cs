using UnityEngine;

namespace UnityUI.Game
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/Characters/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField, Range(0, 1)] public float Experience { get; private set; }
        
        public string Id => _id;
    }
}