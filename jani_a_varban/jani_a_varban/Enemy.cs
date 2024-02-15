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
            bool invalidSpawn = true;
            position.X = rand.Next(3, 13) * 72;
            position.Y = rand.Next(3, 13) * 72;
            while (invalidSpawn)
            {
                if (mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72)] == 1)
                {
                position.X = rand.Next(3, 13) * 72;
                position.Y = rand.Next(3, 13) * 72;
                } else invalidSpawn = false;
            }
        }
        public void Move()
        {
            switch (rand.Next(5))
            {
                case 0:
                    if ((mapMatrix[(int)(position.Y / 72) - 1][(int)(position.X / 72)] == 0)) position.Y -= 72;
                    else Move();
                    break;
                case 1:
                    if ((mapMatrix[(int)(position.Y / 72) + 1][(int)(position.X / 72)] == 0)) position.Y += 72;
                    else Move();
                    break;
                case 2:
                    if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) - 1] == 0)) position.X -= 72;
                    else Move();
                    break;
                case 3:
                    if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) + 1] == 0)) position.X += 72;
                    else Move();
                    break;
                default: break;
            }
        }
    }
}