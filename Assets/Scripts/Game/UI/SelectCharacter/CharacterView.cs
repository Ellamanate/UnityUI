using UnityEngine;
using UnityEngine.UI;

namespace UnityUI.Game
{
    public class CharacterView : BounceButton
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _deselectedColor;
        
        private SelectCharacterState _controller;
        
        public string Id { get; private set; }
        
        public void Initialize(SelectCharacterState controller, string id)
        {
            _controller = controller;
            Id = id;
        }

        protected override void OnClicked()
        {
            _controller.OnCharacterClicked(Id);
        }

        public void SetIcon(Sprite icon, Color color)
        {
            _icon.sprite = icon;
            _icon.color = color;
        }

        public void Select()
        {
            _background.color = _selectedColor;
        }
        
        public void Deselect()
        {
            _background.color = _deselectedColor;
        }
    }
}