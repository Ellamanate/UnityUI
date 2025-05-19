using UnityEngine;
using UnityEngine.UI;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Button _button;
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

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetIcon(Sprite icon, Color color)
        {
            _icon.sprite = icon;
            _icon.color = color;
        }

        public void Select()
        {
            _button.image.color = _selectedColor;
        }
        
        public void Deselect()
        {
            _button.image.color = _deselectedColor;
        }
        
        private void OnClick()
        {
            _controller.OnCharacterClicked(Id);
        }
    }
}