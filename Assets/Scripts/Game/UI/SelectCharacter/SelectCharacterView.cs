using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class SelectCharacterView : MonoBehaviour, IMenuView
    {
        [SerializeField, FoldoutGroup("References")] private ProgressAnimation _progressAnimation;
        [SerializeField, FoldoutGroup("References")] private ChangeIconAnimation _changeIconAnimation;
        [SerializeField, FoldoutGroup("References")] private SlideRectTransform _slideNavigationAnimation;
        [SerializeField, FoldoutGroup("References")] private SlideRectTransform _slideInfoAnimation;
        [SerializeField, FoldoutGroup("References")] private SlideRectTransform _slideSelectionAnimation;
        [SerializeField, FoldoutGroup("References")] private FadeCanvasGroup _fadeCharacterInfo;
        [SerializeField, FoldoutGroup("References")] private RectTransform _charactersParent;
        [SerializeField, FoldoutGroup("References")] private BounceButton _backButton;
        [SerializeField, FoldoutGroup("Duration")] private float _showDuration;
        [SerializeField, FoldoutGroup("Duration")] private float _fadeInfoDuration;
        [SerializeField, FoldoutGroup("Duration")] private float _fillProgressDuration;
        [SerializeField, FoldoutGroup("Duration")] private float _changeIconDuration;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideNavigationIn;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideNavigationOut;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideInfoIn;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideInfoOut;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideSelectionIn;
        [SerializeField, FoldoutGroup("SlideData")] private SlideData _slideSelectionOut;
        [SerializeField, FoldoutGroup("Ease")] private Ease _showEase;
        [SerializeField, FoldoutGroup("Ease")] private Ease _hideEase;
        [SerializeField, FoldoutGroup("Ease")] private Ease _progressEase;
        [SerializeField, FoldoutGroup("Ease")] private Ease _iconEase;
        
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
                _slideNavigationAnimation.Slide(_slideNavigationIn, _showDuration, _showEase, cancellationToken),
                _slideInfoAnimation.Slide(_slideInfoIn, _showDuration, _showEase, cancellationToken),
                _slideSelectionAnimation.Slide(_slideSelectionIn, _showDuration, _showEase, cancellationToken));

            await _fadeCharacterInfo.Run(1, _fadeInfoDuration, _iconEase, cancellationToken);
        }
        
        public async UniTask Hide(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll(
                _slideNavigationAnimation.Slide(_slideNavigationOut, _showDuration, _hideEase, cancellationToken),
                _slideInfoAnimation.Slide(_slideInfoOut, _showDuration, _hideEase, cancellationToken),
                _slideSelectionAnimation.Slide(_slideSelectionOut, _showDuration, _hideEase, cancellationToken));

            await _fadeCharacterInfo.Run(0, _fadeInfoDuration, _iconEase, cancellationToken);
        }
    }
}