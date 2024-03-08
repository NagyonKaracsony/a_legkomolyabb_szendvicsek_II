using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace jani_a_varban
{
    public class Enemy : Game1
    {
        public static Random rand = new();
        public int hp = rand.Next(2) == 1 ? 200 : 300;
        public int dmg = rand.Next(2) == 1 ? 200 : 300;
        public Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Enemy()
        {
            bool invalidSpawn = true;
            position.X = rand.Next(2, MapHeight - 2) * 72;
            position.Y = rand.Next(2, MapWidth - 2) * 72;
            while (invalidSpawn)
            {
                if (mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72)] == 1)
                {
                    position.X = rand.Next(2, MapHeight - 2) * 72;
                    position.Y = rand.Next(2, MapWidth - 2) * 72;
                }
                else invalidSpawn = false;
            }
        }
        public void Move()
        {
            bool intersects = false;
            if (player.Position.X == position.X)
            {
                if (player.Position.Y > position.Y)
                {
                    for (int i = ((int)(position.Y / 72)); i < ((int)(player.Position.Y / 72)); i++)
                    {
                        if (mapMatrix[((int)(position.X / 72))][i] == 1) intersects = true;
                    }
                    if (!intersects) position.Y += 72;
                }
                else if (player.Position.Y < position.Y)
                {
                    for (int i = ((int)(player.Position.Y / 72)); i < ((int)(position.Y / 72)); i++)
                    {
                        if (mapMatrix[((int)(position.X / 72))][i] == 1) intersects = true;
                    }
                    if (!intersects) position.Y -= 72;
                }
            }
            else if (player.Position.Y == position.Y)
            {
                if (player.Position.X > position.X)
                {
                    for (int i = ((int)(position.X / 72)); i < ((int)(player.Position.X / 72)); i++)
                    {
                        if (mapMatrix[1][((int)(position.Y / 72))] == 1) intersects = true;
                    }
                    if (!intersects) position.X += 72;
                }
                else if (player.Position.X < position.X)
                {
                    for (int i = ((int)(player.Position.X / 72)); i < ((int)(position.X / 72)); i++)
                    {
                        if (mapMatrix[1][(int)(position.Y / 72)] == 1) intersects = true;
                    }
                    if (!intersects) position.X -= 72;
                }
            }
            else
            {
                switch (rand.Next(5))
                {
                    case 0:
                        if ((int)(position.Y / 72) != 0)
                        {
                            if ((mapMatrix[(int)(position.Y / 72) - 1][(int)(position.X / 72)] == 0)) position.Y -= 72;
                        }
                        else Move();
                        break;
                    case 1:
                        if ((int)(position.Y / 72) != mapMatrix.Count - 1)
                        {
                            if ((mapMatrix[(int)(position.Y / 72) + 1][(int)(position.X / 72)] == 0)) position.Y += 72;
                            else Move();
                        }
                        break;
                    case 2:
                        if ((int)(position.X / 72) != 0)
                        {
                            if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) - 1] == 0)) position.X -= 72;
                        }
                        else Move();
                        break;
                    case 3:
                        if ((int)(position.X / 72) != mapMatrix.Count - 1)
                        {
                            if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) + 1] == 0)) position.X += 72;
                        }
                        else Move();
                        break;
                    default: break;
                }
            }
        }
    }
}