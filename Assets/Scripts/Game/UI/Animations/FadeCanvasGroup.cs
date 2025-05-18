using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UnityUI.Game
{
    public class FadeCanvasGroup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetFade(float targetAlpha)
        {
            _canvasGroup.alpha = targetAlpha;
        }
        
        public UniTask Fade(
            float targetAlpha,
            float duration,
            Ease ease = Ease.Unset,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = DOTween.To(
                    () => _canvasGroup.alpha,
                    x => _canvasGroup.alpha = x,
                    targetAlpha,
                    duration)
                .SetEase(ease);
            
            return _tween
                .AsyncWaitForKill(token: cancellationToken)
                .AsUniTask();
        }
    }
}