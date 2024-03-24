using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
namespace jani_a_varban
{
    public class Enemy
    {
        public static Random rand = new();
        public int hp = rand.Next(2) == 1 ? 200 : 300;
        public int dmg = rand.Next(2) == 1 ? 200 : 300;
        public int dp = 20;
        public Texture2D Texture;
        public bool HasKey = false;
        public Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Enemy()
        {
            Texture = Animation.enemyTextures[rand.Next(0, 2)];
            Debug.WriteLine(Texture.Name);
            bool invalidSpawn = true;
            position = new Vector2(rand.Next(2, Game1.MapHeight - 2) * 72, rand.Next(2, Game1.MapWidth - 2) * 72);
            while (invalidSpawn)
            {
                if (Game1.mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72)] == 1)
                {
                    position = new Vector2(rand.Next(2, Game1.MapHeight - 2) * 72, rand.Next(2, Game1.MapWidth - 2) * 72);
                }
                else invalidSpawn = false;
            }
        }
        public void ApproachPlayer()
        {
            bool intersects = false;
            if (Game1.player.Position.X == position.X)
            {
                if (Game1.player.Position.Y > position.Y)
                {
                    for (int i = ((int)(position.Y / 72)); i < ((int)(Game1.player.Position.Y / 72)) + 1; i++)
                    {
                        if (Game1.mapMatrix[i][((int)(position.X / 72))] == 1) intersects = true;
                    }
                    if (!intersects) position.Y += 72;
                    else RandomMove();
                }
                else if (Game1.player.Position.Y < position.Y)
                {
                    for (int i = ((int)(Game1.player.Position.Y / 72)); i < ((int)(position.Y / 72)) + 1; i++)
                    {
                        if (Game1.mapMatrix[i][((int)(position.X / 72))] == 1) intersects = true;
                    }
                    if (!intersects) position.Y -= 72;
                    else RandomMove();
                }
            }
            else if (Game1.player.Position.Y == position.Y)
            {
                if (Game1.player.Position.X > position.X)
                {
                    for (int i = ((int)(position.X / 72)); i < ((int)(Game1.player.Position.X / 72)) + 1; i++)
                    {
                        if (Game1.mapMatrix[((int)(position.Y / 72))][i] == 1) intersects = true;
                    }
                    if (!intersects) position.X += 72;
                    else RandomMove();

                }
                else if (Game1.player.Position.X < position.X)
                {
                    for (int i = ((int)(Game1.player.Position.X / 72)); i < ((int)(position.X / 72)) + 1; i++)
                    {
                        if (Game1.mapMatrix[((int)(position.Y / 72))][i] == 1) intersects = true;
                    }
                    if (!intersects) position.X -= 72;
                    else RandomMove();
                }
                Game1.player.CheckForEnemy(false); // scuffed, nem itt kéne ellenőrizni de működik
            }
            else RandomMove();
        }
        public void RandomMove()
        {
            switch (rand.Next(5))
            {
                case 0:
                    if ((int)(position.Y / 72) != 0)
                    {
                        if ((Game1.mapMatrix[(int)(position.Y / 72) - 1][(int)(position.X / 72)] == 0)) position.Y -= 72;
                    }
                    else RandomMove();
                    break;
                case 1:
                    if ((int)(position.Y / 72) != Game1.mapMatrix.Count - 1)
                    {
                        if ((Game1.mapMatrix[(int)(position.Y / 72) + 1][(int)(position.X / 72)] == 0)) position.Y += 72;
                    }
                    else RandomMove();
                    break;
                case 2:
                    if ((int)(position.X / 72) != 0)
                    {
                        if ((Game1.mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) - 1] == 0)) position.X -= 72;
                    }
                    else RandomMove();
                    break;
                case 3:
                    if ((int)(position.X / 72) != Game1.mapMatrix.Count - 1)
                    {
                        if ((Game1.mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) + 1] == 0)) position.X += 72;
                    }
                    else RandomMove();
                    break;
                default: break;
            }
        }
    }
}