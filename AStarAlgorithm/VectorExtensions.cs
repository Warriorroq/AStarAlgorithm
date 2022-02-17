using SFML.System;
namespace AStarAlgorithm
{
    public static class VectorExtensions
    {
        public static Vector2f Mul(this Vector2f vec, Vector2f vec2) 
            =>new Vector2f(vec.X * vec2.X, vec.Y * vec2.Y);
        public static Vector2f Div(this Vector2f vec, Vector2f vec2)
            => new Vector2f(vec.X / vec2.X, vec.Y / vec2.Y);
        public static Vector2i Abs(this Vector2i vec)
            => new Vector2i(vec.X < 0 ? -vec.X : vec.X, vec.Y < 0 ? -vec.Y : vec.Y);
    }
}
