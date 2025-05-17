using UnityEngine;
using Zenject;

namespace UnityUI.Game
{
    public class StaticDataInstaller: MonoInstaller
    {
        [SerializeField] private BootstrapConfig _bootstrapConfig;
        
        public override void InstallBindings()
        {
            BindBootstrapConfig();
        }

        private void BindBootstrapConfig()
        {
            Container
                .Bind<BootstrapConfig>()
                .FromInstance(_bootstrapConfig)
                .AsSingle();
        }
    }
}