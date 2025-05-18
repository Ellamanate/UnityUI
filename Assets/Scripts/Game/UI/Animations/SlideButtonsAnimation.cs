using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UnityUI.Game
{
    public class SlideButtonsAnimation : MonoBehaviour
    {
        [SerializeField] private ButtonData[] _elements;

        public void SetState(SlideData data, float alpha)
        {
            foreach (var element in _elements)
            {
                element.Slide.SetSlide(data);
                element.Fade.SetFade(alpha);
            }
        }
        
        public async UniTask Run(
            SlideData data,
            float targetAlpha,
            float slideDuration,
            float delayBetweenSlides,
            float fadeDuration,
            Ease slideEase = Ease.Unset,
            Ease fadeEase = Ease.Unset,
            CancellationToken cancellationToken = default)
        {
            if (_elements.Length == 1)
            {
                await _elements[0].Run(
                    data,
                    targetAlpha,
                    slideDuration,
                    fadeDuration,
                    slideEase,
                    fadeEase,
                    cancellationToken);
            }
            else
            {
                for (int i = 0; i < _elements.Length - 1; i++)
                {
                    var buttonData = _elements[i];
                    
                    _ = buttonData.Slide.Slide(
                        data,
                        slideDuration,
                        ease: slideEase,
                        cancellationToken: cancellationToken);
                    
                    _ = buttonData.Fade.Fade(
                        targetAlpha,
                        fadeDuration,
                        ease: fadeEase,
                        cancellationToken: cancellationToken);
                    
                    await UniTask.Delay(TimeSpan.FromSeconds(delayBetweenSlides), cancellationToken: cancellationToken);
                }

                await _elements[^1].Run(
                    data,
                    targetAlpha,
                    slideDuration,
                    fadeDuration,
                    slideEase,
                    fadeEase,
                    cancellationToken);
            }
        }

        [Serializable]
        public struct ButtonData
        {
            public SlideRectTransform Slide;
            public FadeCanvasGroup Fade;
        }
    }
    
    public static partial class CleanCodeExtensions
    {
        public static UniTask Run(this SlideButtonsAnimation.ButtonData buttonData,
            SlideData data,
            float targetAlpha,
            float slideDuration,
            float fadeDuration,
            Ease slideEase,
            Ease fadeEase,
            CancellationToken cancellationToken)
        {
            return UniTask.WhenAll(
                buttonData.Slide.Slide(
                    data,
                    slideDuration,
                    ease: slideEase,
                    cancellationToken: cancellationToken),
                buttonData.Fade.Fade(
                    targetAlpha,
                    fadeDuration,
                    ease: fadeEase,
                    cancellationToken: cancellationToken));
        }
    }
}