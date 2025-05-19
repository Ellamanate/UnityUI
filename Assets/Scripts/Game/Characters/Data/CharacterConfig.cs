using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityUI.Game
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/Characters/CharacterConfig")]
    public class CharacterConfig : SerializedScriptableObject
    {
        [SerializeField] private Guid _guid;
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField, MinValue(0), MaxValue(1)] public float Experience { get; private set; }
        
        public string Id => _guid.ToString();
    }
}