using System;
using Sirenix.OdinInspector;

namespace UnityUI.Game
{
    [Serializable]
    public struct SlideData
    {
        public SlideDirection Direction;
        [ShowIf(nameof(DrawHorizontal))] public float PivotX;
        [ShowIf(nameof(DrawVertical))] public float PivotY;
        [ShowIf(nameof(DrawHorizontal))]public float AnchorMinX;
        [ShowIf(nameof(DrawVertical))] public float AnchorMinY;
        [ShowIf(nameof(DrawHorizontal))]public float AnchorMaxX;
        [ShowIf(nameof(DrawVertical))] public float AnchorMaxY;
        
        public bool DrawVertical => Direction == SlideDirection.Vertical || Direction == SlideDirection.Both;
        public bool DrawHorizontal => Direction == SlideDirection.Horizontal || Direction == SlideDirection.Both;
    }
}