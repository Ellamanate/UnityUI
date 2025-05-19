using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUI.Game
{
    public class MainMenuView : MonoBehaviour, IMenuView
    {
        [SerializeField, FoldoutGroup("References")] private SlideButtonsAnimation _slideElements;
        [SerializeField, FoldoutGroup("References")] private FadeCanvasGroup _fadeBackground;
        [SerializeField, FoldoutGroup("References")] private BounceButton _selectCharactersButton;
        [SerializeField, FoldoutGroup("Duration")] private float _slideDuration;
        [SerializeField, FoldoutGroup("Duration")] private float _delayBetweenSlides;
        [SerializeField, FoldoutGroup("Duration")] private float _fadeDuration;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideIn;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideOut;
        [SerializeField, FoldoutGroup("Ease")] private Ease _slideEaseIn;
        [SerializeField, FoldoutGroup("Ease")] private Ease _slideEaseOut;
        [SerializeField, FoldoutGroup("Ease")] private Ease _fadeEaseIn;
        [SerializeField, FoldoutGroup("Ease")] private Ease _fadeEaseOut;
        
        private MainMenuState _controller;
        
        public void Initialize(MainMenuState controller)
        {
            _controller = controller;
        }

        private void OnEnable()
        {
            _selectCharactersButton.OnClick += _controller.OnCharacterButtonClicked;
        }

        private void OnDisable()
        {
            _selectCharactersButton.OnClick -= _controller.OnCharacterButtonClicked;
        }

        public void ToDefault()
        {
            _slideElements.SetState(_slideOut, 0);
            _fadeBackground.SetFade(0);
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public async UniTask Show(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(
                _slideElements.Run(
                    _slideIn,
                    1,
                    _slideDuration,
                    _delayBetweenSlides,
                    _fadeDuration,
                    _slideEaseIn,
                    _fadeEaseIn,
                    cancellationToken),
                _fadeBackground.Run(
                    1,
                    _slideDuration,
                    _fadeEaseIn, 
                    cancellationToken));
        }
        
        public async UniTask Hide(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(
                _slideElements.Run(
                    _slideOut,
                    0,
                    _slideDuration,
                    _delayBetweenSlides,
                    _fadeDuration,
                    _slideEaseOut,
                    _fadeEaseOut,
                    cancellationToken),
                _fadeBackground.Run(
                    0,
                    _slideDuration,
                    _fadeEaseOut,
                    cancellationToken));
        }
    }
}