using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class SelectCharacterView : MonoBehaviour
    {
        [SerializeField] private ProgressAnimation _progressAnimation;
        [SerializeField] private ChangeIconAnimation _changeIconAnimation;
        [SerializeField] private SlideRectTransform _slideNavigationAnimation;
        [SerializeField] private SlideRectTransform _slideInfoAnimation;
        [SerializeField] private SlideRectTransform _slideSelectionAnimation;
        [SerializeField] private FadeCanvasGroup _fadeCharacterInfo;
        [SerializeField] private RectTransform _charactersParent;
        [SerializeField] private BounceButton _backButton;
        [SerializeField] private float _showDuration;
        [SerializeField] private float _fadeInfoDuration;
        [SerializeField] private float _fillProgressDuration;
        [SerializeField] private float _changeIconDuration;
        [SerializeField] private SlideData _slideNavigationIn;
        [SerializeField] private SlideData _slideNavigationOut;
        [SerializeField] private SlideData _slideInfoIn;
        [SerializeField] private SlideData _slideInfoOut;
        [SerializeField] private SlideData _slideSelectionIn;
        [SerializeField] private SlideData _slideSelectionOut;
        [SerializeField] private Ease _easeShow;
        [SerializeField] private Ease _easeHide;
        [SerializeField] private Ease _progressEase;
        [SerializeField] private Ease _iconEase;
        
        private SelectCharacterState _controller;
        private CharacterView _selectedCharacter;
        private CancellationTokenSource _tokenSource;
        private Dictionary<string, CharacterView> _characterViews;
        
        public void Initialize(SelectCharacterState controller)
        {
            _controller = controller;
        }
        
        private void OnEnable()
        {
            _backButton.OnClick += _controller.OnBackButtonClicked;
        }

        private void OnDisable()
        {
            _tokenSource.TryCancel();
            _backButton.OnClick -= _controller.OnBackButtonClicked;
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void ToDefault()
        {
            _fadeCharacterInfo.SetFade(0);
            _progressAnimation.SetProgress(0);
            _changeIconAnimation.SetScale(0);
            _slideNavigationAnimation.SetSlide(_slideNavigationOut);
            _slideInfoAnimation.SetSlide(_slideInfoOut);
            _slideSelectionAnimation.SetSlide(_slideSelectionOut);
            _selectedCharacter?.Deselect();
            _selectedCharacter = null;
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

        public void SetCharacterInfo(Sprite sprite, Color color, float experience)
        {
            _tokenSource = _tokenSource.Refresh();
            _progressAnimation.SetProgress(0);
            _progressAnimation.Run(experience, _fillProgressDuration, _progressEase, _tokenSource.Token);
            _changeIconAnimation.Run(sprite, color, _changeIconDuration, _iconEase, _tokenSource.Token);
        }
        
        public async UniTask Show(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(
                _slideNavigationAnimation.Slide(_slideNavigationIn, _showDuration, _easeShow, cancellationToken),
                _slideInfoAnimation.Slide(_slideInfoIn, _showDuration, _easeShow, cancellationToken),
                _slideSelectionAnimation.Slide(_slideSelectionIn, _showDuration, _easeShow, cancellationToken));

            await _fadeCharacterInfo.Run(1, _fadeInfoDuration, _easeShow, cancellationToken);
        }
        
        public async UniTask Hide(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(
                _slideNavigationAnimation.Slide(_slideNavigationOut, _showDuration, _easeShow, cancellationToken),
                _slideInfoAnimation.Slide(_slideInfoOut, _showDuration, _easeShow, cancellationToken),
                _slideSelectionAnimation.Slide(_slideSelectionOut, _showDuration, _easeShow, cancellationToken));

            await _fadeCharacterInfo.Run(0, _fadeInfoDuration, _easeHide, cancellationToken);
        }
    }
}