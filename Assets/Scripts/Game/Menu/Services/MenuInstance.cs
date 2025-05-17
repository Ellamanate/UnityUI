using UnityUI.Utils;
using Zenject;

namespace UnityUI.Game
{
    public class MenuInstance : IInitializable
    {
        private readonly MenuState _menuState;

        public MenuInstance(MenuState menuState)
        {
            _menuState = menuState;
        }

        public void Initialize()
        {
            _menuState.OnMenuInitialized(this);
        }

        public void StartMenu()
        {
            ConsoleLogger.Log("Starting menu");
        }
    }
}