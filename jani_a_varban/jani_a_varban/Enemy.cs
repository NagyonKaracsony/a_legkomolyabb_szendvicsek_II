using Microsoft.Xna.Framework;
using System;
namespace jani_a_varban
{
    public class Enemy : Game1
    {
        Game1 game;
        public static Random rand = new();
        public int Hp = rand.Next(2) == 1 ? 200 : 300;
        public Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Enemy(Game1 game)
        {
            this.game = game;
            position.X = rand.Next(3, 13) * 72;
            position.Y = rand.Next(3, 13) * 72;
        }
        public void Move()
        {
            if (rand.Next(2) == 1) position.X = rand.Next(2) == 1 ? position.X + 72 : position.X - 72;
            else position.Y = rand.Next(2) == 1 ? position.Y + 72 : position.Y - 72;
        }
    }
}