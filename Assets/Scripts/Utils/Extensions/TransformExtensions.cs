using DG.Tweening;
using UnityEngine;

namespace UnityUI.Utils
{
    public static class TransformExtensions
    {
        public static void SetSlide(
            this RectTransform rectTransform,
            Vector2 pivot,
            Vector2 anchorMin,
            Vector2 anchorMax)
        {
            rectTransform.pivot = pivot;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
        
        public static void SetHorizontalSlide(
            this RectTransform rectTransform,
            float pivotX,
            float anchorMinX,
            float anchorMaxX)
        {
            rectTransform.pivot = rectTransform.pivot.ChangeX(pivotX);
            rectTransform.anchorMin = rectTransform.anchorMin.ChangeX(anchorMinX);
            rectTransform.anchorMax = rectTransform.anchorMax.ChangeX(anchorMaxX);
        }
        
        public static void SetVerticalSlide(
            this RectTransform rectTransform,
            float pivotY,
            float anchorMinY,
            float anchorMaxY)
        {
            rectTransform.pivot = rectTransform.pivot.ChangeY(pivotY);
            rectTransform.anchorMin = rectTransform.anchorMin.ChangeY(anchorMinY);
            rectTransform.anchorMax = rectTransform.anchorMax.ChangeY(anchorMaxY);
        }

        public static Tween DoSlide(
            this RectTransform rectTransform,
            Vector2 pivot,
            Vector2 anchorMin,
            Vector2 anchorMax,
            float duration)
        {
            return DOTween
                .Sequence()
                .Join(rectTransform.DOPivot(pivot, duration))
                .Join(rectTransform.DOAnchorMin(anchorMin, duration))
                .Join(rectTransform.DOAnchorMax(anchorMax, duration))
                .OnUpdate(() =>
                {
                    rectTransform.anchoredPosition = Vector2.zero;
                });
        }
        
        public static Tween DoSlideX(
            this RectTransform rectTransform,
            float pivotX,
            float anchorMinX,
            float anchorMaxX,
            float duration)
        {
            return DOTween
                .Sequence()
                .Join(rectTransform.DOPivotX(pivotX, duration))
                .Join(rectTransform.DoAnchorMinX(anchorMinX, duration))
                .Join(rectTransform.DoAnchorMaxX(anchorMaxX, duration))
                .OnUpdate(() =>
                {
                    rectTransform.anchoredPosition = rectTransform.anchoredPosition.ChangeX(0);
                });
        }
        
        public static Tween DoSlideY(
            this RectTransform rectTransform,
            float pivotY,
            float anchorMinY,
            float anchorMaxY,
            float duration)
        {
            return DOTween
                .Sequence()
                .Join(rectTransform.DOPivotY(pivotY, duration))
                .Join(rectTransform.DoAnchorMinY(anchorMinY, duration))
                .Join(rectTransform.DoAnchorMaxY(anchorMaxY, duration))
                .OnUpdate(() =>
                {
                    rectTransform.anchoredPosition = rectTransform.anchoredPosition.ChangeY(0);
                });
        }
        
        public static Tween DoAnchorMinX(
            this RectTransform rectTransform,
            float targetValue,
            float duration)
        {
            return DOTween.To(
                () => rectTransform.anchorMin.x,
                x => rectTransform.anchorMin = rectTransform.anchorMin.ChangeX(x),
                targetValue,
                duration);
        }
        
        public static Tween DoAnchorMinY(
            this RectTransform rectTransform,
            float targetValue,
            float duration)
        {
            return DOTween.To(
                () => rectTransform.anchorMin.y,
                x => rectTransform.anchorMin = rectTransform.anchorMin.ChangeY(x),
                targetValue,
                duration);
        }
        
        public static Tween DoAnchorMaxX(
            this RectTransform rectTransform,
            float targetValue,
            float duration)
        {
            return DOTween.To(
                () => rectTransform.anchorMax.x,
                x => rectTransform.anchorMax = rectTransform.anchorMax.ChangeX(x),
                targetValue,
                duration);
        }
        
        public static Tween DoAnchorMaxY(
            this RectTransform rectTransform,
            float targetValue,
            float duration)
        {
            return DOTween.To(
                () => rectTransform.anchorMax.y,
                x => rectTransform.anchorMax = rectTransform.anchorMax.ChangeY(x),
                targetValue,
                duration);
        }
    }
}