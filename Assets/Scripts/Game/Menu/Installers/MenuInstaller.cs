using Zenject;

namespace UnityUI.Game
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMenuInstance();
        }

        private void BindMenuInstance()
        {
            Container
                .BindInterfacesAndSelfTo<MenuInstance>()
                .AsSingle()
                .NonLazy();
        }
    }
}