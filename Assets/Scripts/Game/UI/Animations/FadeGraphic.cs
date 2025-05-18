using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace UnityUI.Game
{
    public class FadeGraphic : MonoBehaviour
    {
        [SerializeField] private Graphic _graphic;

        private Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetFade(float targetAlpha)
        {
            _graphic.color = _graphic.color.ChangeAlpha(targetAlpha);
        }
        
        public UniTask Fade(
            float targetAlpha,
            float duration,
            Ease ease = Ease.Unset,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = _graphic
                .DOFade(targetAlpha, duration)
                .SetEase(ease);
            
            return _tween
                .AsyncWaitForKill(token: cancellationToken)
                .AsUniTask();
        }
    }
}