using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUI.Game
{
    public class ChangeIconAnimation : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetIcon(Sprite sprite, Color color)
        {
            _icon.sprite = sprite;
            _icon.color = color;
        }
        
        public void SetScale(float scale)
        {
            _icon.rectTransform.localScale = Vector3.one * scale;
        }
        
        public UniTask Run(
            Sprite sprite,
            Color color,
            float duration,
            Ease ease = Ease.Unset,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            
            var sequence = DOTween.Sequence();

            if (_icon.rectTransform.localScale != Vector3.zero)
            {
                sequence
                    .Append(_icon.rectTransform.DOScale(0, duration / 2f))
                    .AppendCallback(() => SetIcon(sprite, color));
            }
            else
            {
                SetIcon(sprite, color);
            }
            
            sequence
                .Append(_icon.rectTransform.DOScale(1, duration / 2f))
                .SetEase(ease);

            _tween = sequence;
            
            return _tween
                .AsyncWaitForKill(token: cancellationToken)
                .AsUniTask();
        }
    }
}