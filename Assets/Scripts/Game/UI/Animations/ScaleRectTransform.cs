using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace UnityUI.Game
{
    public class ScaleRectTransform : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        private Tween _tween;
        
        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetScale(float scale)
        {
            _rectTransform.localScale = Vector3.one * scale;
        }

        public UniTask Run(
            float scale,
            float duration,
            Ease ease = Ease.Unset,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = DOTween.To(
                    () => _rectTransform.localScale,
                    x => _rectTransform.localScale = x,
                    Vector3.one * scale,
                    duration)
                .SetEase(ease);
            
            return _tween
                .AsyncWaitForKill(cancellationToken)
                .AsUniTask();
        }
    }
}