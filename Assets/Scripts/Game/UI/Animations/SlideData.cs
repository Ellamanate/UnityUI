using System;

namespace UnityUI.Game
{
    [Serializable]
    public struct SlideData
    {
        public SlideDirection Direction;
        public float PivotX;
        public float PivotY;
        public float AnchorMinX;
        public float AnchorMinY;
        public float AnchorMaxX;
        public float AnchorMaxY;
    }
}