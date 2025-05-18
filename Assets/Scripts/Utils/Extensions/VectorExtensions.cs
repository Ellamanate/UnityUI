using UnityEngine;

namespace Utils.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ChangeX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }
        
        public static Vector2 ChangeY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, y);
        }
    }
}