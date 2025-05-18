using System;
using System.Threading;
using UnityUI.Utils;
using Zenject;

namespace UnityUI.Game
{
    public class MenuInstance : IInitializable, IDisposable
    {
        private readonly MenuState _menuState;
        private readonly MenuPageRouting _pageRouting;
        private readonly CancellationTokenSource _tokenSource;

        public MenuInstance(MenuState menuState, MenuPageRouting pageRouting)
        {
            _menuState = menuState;
            _pageRouting = pageRouting;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _menuState.SetMenuInstance(this);
        }
        
        public void Dispose()
        {
            _menuState.SetMenuInstance(null);
            _tokenSource.Dispose();
        }

        public void StartScene()
        {
            ConsoleLogger.Log("Starting menu");
            _ = _pageRouting.MoveToStateAsync<MainMenuState>(_tokenSource.Token);
        }
    }
}