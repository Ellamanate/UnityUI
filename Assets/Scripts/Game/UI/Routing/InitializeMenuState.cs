using UnityUI.Utils;

namespace UnityUI.Game
{
    public class InitializeMenuState : IMenuState, IEnteringState
    {
        private readonly IMenuView[] _views;

        public InitializeMenuState(IMenuView[] views)
        {
            _views = views;
        }

        public void Enter()
        {
            foreach (var view in _views)
            {
                view.SetActive(false);
                view.ToDefault();
            }
        }
    }
}