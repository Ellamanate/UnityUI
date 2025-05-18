namespace UnityUI.Game
{
    public abstract class BaseMenuState : IMenuState
    {
        protected MenuPageRouting PageRouting;
        
        public void Initialize(MenuPageRouting pageRouting)
        {
            PageRouting = pageRouting;
            OnInitialize();
        }

        protected virtual void OnInitialize() { }
    }
}