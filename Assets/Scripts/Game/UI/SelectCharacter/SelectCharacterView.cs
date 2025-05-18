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
        [SerializeField] private Button _backButton;
        
        private SelectCharacterState _controller;
        private Tween _tween;
        
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