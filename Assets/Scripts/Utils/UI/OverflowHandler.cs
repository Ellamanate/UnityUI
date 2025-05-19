using UnityEngine;

namespace UnityUI.Utils
{
    [ExecuteInEditMode]
    public class OverflowHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _parentTransform;
        [SerializeField] private RectTransform _canvasTransform;
        [SerializeField] private float _ratio;

        private void Update()
        {
            _rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, 
                Mathf.Clamp(_rectTransform.rect.height * _ratio, 0, _canvasTransform.rect.width));
        }
    }
}