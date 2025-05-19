using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class ProgressAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressTransform;
        
        private Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetProgress(float progress)
        {
            _progressTransform.anchorMax = _progressTransform.anchorMax.ChangeX(progress);
            _progressTransform.anchoredPosition = _progressTransform.anchoredPosition.ChangeX(0);
        }
        
        public UniTask Run(
            float progress,
            float duration,
            Ease ease = Ease.Unset,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = _progressTransform
                .DoAnchorMaxX(progress, duration)
                .SetEase(ease)
                .OnUpdate(() =>
                {
                    _progressTransform.anchoredPosition = _progressTransform.anchoredPosition.ChangeX(0);
                });
                
            return _tween
                .AsyncWaitForKill(cancellationToken)
                .AsUniTask();
        }
    }
}