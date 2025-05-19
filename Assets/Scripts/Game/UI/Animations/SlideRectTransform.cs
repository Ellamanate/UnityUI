using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class SlideRectTransform : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        
        private Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetSlide(SlideData slideData)
        {
            switch (slideData.Direction)
            {
                case SlideDirection.Horizontal:
                    SetHorizontalSlide(slideData.PivotX, slideData.AnchorMinX, slideData.AnchorMinX);
                    break;
                case SlideDirection.Vertical:
                    SetVerticalSlide(slideData.PivotY, slideData.AnchorMinY, slideData.AnchorMinY);
                    break;
                case SlideDirection.Both:
                    SetSlide(
                        new Vector2(slideData.PivotX, slideData.PivotY),
                        new Vector2(slideData.AnchorMinX, slideData.AnchorMinY),
                        new Vector2(slideData.AnchorMaxX, slideData.AnchorMaxY));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void SetHorizontalSlide(float pivotX, float anchorMinX, float anchorMaxX)
        {
            _transform.SetHorizontalSlide(pivotX, anchorMinX, anchorMaxX);
        }
        
        public void SetVerticalSlide(float pivotY, float anchorMinY, float anchorMaxY)
        {
            _transform.SetVerticalSlide(pivotY, anchorMinY, anchorMaxY);
        }
        
        public void SetSlide(Vector2 pivot, Vector2 anchorMin, Vector2 anchorMax)
        {
            _transform.SetSlide(pivot, anchorMin, anchorMax);
        }
        
        public UniTask Slide(
            SlideData slideData,
            float duration,
            Ease ease,
            CancellationToken cancellationToken = default)
        {
            return slideData.Direction switch
            {
                SlideDirection.Horizontal => SlideHorizontal(
                    slideData.PivotX,
                    slideData.AnchorMinX,
                    slideData.AnchorMaxX,
                    duration,
                    ease,
                    cancellationToken),
                SlideDirection.Vertical => SlideVertical(
                    slideData.PivotY,
                    slideData.AnchorMinY,
                    slideData.AnchorMaxY,
                    duration,
                    ease,
                    cancellationToken),
                SlideDirection.Both => Slide(
                    new Vector2(slideData.PivotX, slideData.PivotY),
                    new Vector2(slideData.AnchorMinX, slideData.AnchorMinY),
                    new Vector2(slideData.AnchorMaxX, slideData.AnchorMaxY),
                    duration,
                    ease,
                    cancellationToken),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public UniTask Slide(
            Vector2 pivot,
            Vector2 anchorMin,
            Vector2 anchorMax,
            float duration,
            Ease ease,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = _transform
                .DoSlide(pivot, anchorMin, anchorMax, duration)
                .SetEase(ease);
            
            return _tween
                .AsyncWaitForKill(cancellationToken)
                .AsUniTask();
        }
        
        public UniTask SlideVertical(
            float pivotY,
            float anchorMinY,
            float anchorMaxY,
            float duration,
            Ease ease,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = _transform
                .DoSlideY(pivotY, anchorMinY, anchorMaxY, duration)
                .SetEase(ease);
            
            return _tween
                .AsyncWaitForKill(cancellationToken)
                .AsUniTask();
        }
        
        public UniTask SlideHorizontal(
            float pivotX,
            float anchorMinX,
            float anchorMaxX,
            float duration,
            Ease ease,
            CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            _tween = _transform
                .DoSlideX(pivotX, anchorMinX, anchorMaxX, duration)
                .SetEase(ease);
            
            return _tween
                .AsyncWaitForKill(cancellationToken)
                .AsUniTask();
        }
    }
}