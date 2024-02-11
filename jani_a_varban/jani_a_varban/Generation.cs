using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace jani_a_varban
{
    internal class Generation : Game1
    {
        public void GenerateRoom(int length, int height)
        {
            Texture2D floorTexture;
            floorTexture = Content.Load<Texture2D>("wall");
            int x = 0;
            int m = 0;
            for (int i = 0; i < length; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    _spriteBatch.Draw(floorTexture, new Vector2(x, m), Color.White);
                    x += 72;
                }
                x = 0;
                m += 72;
            }
        }
    }
}