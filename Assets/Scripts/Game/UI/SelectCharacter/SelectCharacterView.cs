using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUI.Game
{
    public class SelectCharacterView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private FadeCanvasGroup _fadeBackground;
        [SerializeField] private RectTransform _charactersParent;
        [SerializeField] private Image _characterIcon;
        [SerializeField] private Button _backButton;
        [SerializeField] private float _showDuration;
        [SerializeField] private Ease _easeShow;
        [SerializeField] private Ease _easeHide;
        
        private SelectCharacterState _controller;
        private CharacterView _selectedCharacter;
        private Dictionary<string, CharacterView> _characterViews;
        
        public void Initialize(SelectCharacterState controller)
        {
            _controller = controller;
        }
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(_controller.OnBackButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(_controller.OnBackButtonClicked);
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void ToDefault()
        {
            _fadeBackground.SetFade(0);
        }

        public string GetDefaultViewId()
        {
            return _characterViews.Keys.First();
        }
        
        public void InstantiateCharacters(CharactersViewsFactory factory)
        {
            _characterViews = factory.CreateCharacters(_controller, _charactersParent);
        }
        
        public void SelectCharacter(string id)
        {
            _selectedCharacter?.Deselect();
            _selectedCharacter = _characterViews[id];
            _selectedCharacter.Select();
        }

        public void SetIcon(Sprite sprite, Color color)
        {
            _characterIcon.sprite = sprite;
            _characterIcon.color = color;
        }

        public void SetExperience(float experience)
        {
            
        }
        
        public UniTask Show(CancellationToken cancellationToken)
        {
            return _fadeBackground.Fade(1, _showDuration, _easeShow, cancellationToken);
        }
        
        public UniTask Hide(CancellationToken cancellationToken)
        {
            return _fadeBackground.Fade(0, _showDuration, _easeHide, cancellationToken);
        }
    }
}