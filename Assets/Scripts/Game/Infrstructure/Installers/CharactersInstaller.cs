using Zenject;

namespace UnityUI.Game
{
    public class CharactersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCharacters();
        }

        private void BindCharacters()
        {
            Container
                .Bind<CharactersService>()
                .AsSingle();
        }
    }
}