using SFML.System;
using SFML.Graphics;
namespace AStarAlgorithm.GLoop
{
    public class GameObject : IDisposable, ICloneable
    {
        public Vector2f position;
        public Shape shape;
        public GameObjectState state;
        public void Update()
        {
            if (shape is not null)
                shape.Position = position;
            if (state is GameObjectState.active)
                OnUpdate();
        }
        public virtual void Draw(RenderWindow render)
        {
            if (shape is not null)
                render.Draw(shape);
        }
        protected virtual void OnUpdate()
        {

        }

        public virtual void Dispose()
        {
            shape?.Dispose();
        }

        public virtual object Clone()
        {
            var clone = new GameObject();
            clone.position = position;
            clone.shape = CloneShape();
            return clone;
        }
        private Shape CloneShape() =>
            shape switch
            {
                CircleShape => new CircleShape(shape as CircleShape),
                ConvexShape => new ConvexShape(shape as ConvexShape),
                RectangleShape and _ => new RectangleShape(shape as RectangleShape),
            };
    }
    public enum GameObjectState
    {
        nonActive = 0,
        active = 1,
        destroyed = 2
    }
}
