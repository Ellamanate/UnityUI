using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUI.Game
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private SlideButtonsAnimation _slideElements;
        [SerializeField] private FadeCanvasGroup _fadeBackground;
        [SerializeField] private BounceButton _selectCharactersButton;
        [SerializeField] private float _slideDuration;
        [SerializeField] private float _delayBetweenSlides;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private SlideData _slideIn;
        [SerializeField] private SlideData _slideOut;
        [SerializeField] private Ease _slideEaseIn;
        [SerializeField] private Ease _slideEaseOut;
        [SerializeField] private Ease _fadeEaseIn;
        [SerializeField] private Ease _fadeEaseOut;
        
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