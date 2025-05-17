using Tymski;
using UnityEngine;

namespace UnityUI.Game
{
    [CreateAssetMenu(fileName = "BootstrapConfig", menuName = "Configs/BootstrapConfig")]
    public class BootstrapConfig : ScriptableObject
    {
        [field: SerializeField] public SceneReference MenuScene { get; private set; }
    }
}