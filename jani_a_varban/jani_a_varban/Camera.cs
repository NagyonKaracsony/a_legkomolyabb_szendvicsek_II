using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jani_a_varban
{
    public class Camera
    {
        public Vector2 Position;
        public float Zoom;

        public Camera(Vector2 position, float zoom = 1.0f)
        {
            Position = position;
            Zoom = zoom;
        }

        public Matrix GetViewMatrix(GraphicsDevice graphicsDevice)
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                   Matrix.CreateScale(Zoom, Zoom, 1) *
                   Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void ZoomIn(float amount)
        {
            Zoom += amount;
        }

        public void ZoomOut(float amount)
        {
            Zoom -= amount;
        }
    }
}