using System;
using Zenject;

namespace UnityUI.Game
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetsProvider();
            BindStateMachine();
            BindLoadingService();
        }

        private void BindAssetsProvider()
        {
            Container
                .Bind<AssetsProvider>()
                .AsSingle();
        }
        
        private void BindStateMachine()
        {
            Container
                .Bind<GameStateMachine>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<BootstrapState>()
                .AsSingle();

            Container
                .Bind(typeof(MenuState), typeof(IDisposable))
                .To<MenuState>()
                .AsSingle();
        }

        private void BindLoadingService()
        {
            Container
                .Bind<ScenesService>()
                .AsSingle();
        }
    }
}