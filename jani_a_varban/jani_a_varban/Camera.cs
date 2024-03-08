using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jani_a_varban
{
    public static class Camera
    {
        public static Vector2 Position;
        public static float Zoom = 0.8f;
        public static int previousScrollValue;
        public static Matrix GetViewMatrix(GraphicsDevice graphicsDevice)
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                   Matrix.CreateScale(Zoom, Zoom, 1) *
                   Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
        }
    }
}