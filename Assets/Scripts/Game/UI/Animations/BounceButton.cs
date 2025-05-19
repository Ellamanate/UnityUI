using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class BounceButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClick;
        
        [SerializeField] private ScaleRectTransform _scaleAnimation;
        [SerializeField] private float _hoverSize;
        [SerializeField] private float _clickSize;
        [SerializeField] private float _hoverDuration;
        [SerializeField] private float _clickDuration;

        private CancellationTokenSource _tokenSource;

        private void OnDisable()
        {
            _tokenSource.CancelAndDispose();
        }
        
        protected virtual void OnClicked() { }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _tokenSource = _tokenSource.Refresh();
            _ = Click(_tokenSource.Token);
            
            OnClicked();
            OnClick?.Invoke();
        }

        private async UniTask Click(CancellationToken token)
        {
            await _scaleAnimation.Run(_clickSize, _clickDuration, cancellationToken: token);
            await _scaleAnimation.Run(1, _clickDuration, cancellationToken: token);
        }
    }
}