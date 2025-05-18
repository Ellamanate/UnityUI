using UnityUI.Utils;

namespace UnityUI.Game
{
    public class InitializeMenuState : IMenuState, IEnteringState
    {
        private readonly MainMenuView _mainMenuView;
        private readonly SelectCharacterView _selectCharacterView;

        public InitializeMenuState(MainMenuView mainMenuView, SelectCharacterView selectCharacterView)
        {
            _mainMenuView = mainMenuView;
            _selectCharacterView = selectCharacterView;
        }

        public void Enter()
        {
            _mainMenuView.SetActive(false);
            _selectCharacterView.SetActive(false);
        }
    }
}