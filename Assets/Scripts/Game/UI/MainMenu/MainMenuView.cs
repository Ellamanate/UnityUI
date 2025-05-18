using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUI.Game
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _selectCharactersButton;
        
        private MainMenuState _controller;
        private Tween _tween;
        
        public void Initialize(MainMenuState controller)
        {
            _controller = controller;
        }

        private void OnEnable()
        {
            _selectCharactersButton.onClick.AddListener(_controller.OnCharacterButtonClicked);
        }

        private void OnDisable()
        {
            _selectCharactersButton.onClick.RemoveListener(_controller.OnCharacterButtonClicked);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public UniTask Fade(float targetAlpha, float duration, CancellationToken cancellationToken)
        {
            _tween?.Kill();
            _tween = DOTween.To(
                () => _canvasGroup.alpha,
                x => _canvasGroup.alpha = x,
                targetAlpha,
                duration);
            
            return _tween
                .AsyncWaitForKill(token: cancellationToken)
                .AsUniTask();
        }
    }
}