using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;
namespace jani_a_varban
{
    internal class Animation : Game1
    {
        public static Texture2D playerSheet;
        public static Texture2D[] enemyTextures;
        public static int playerAnimationIndex = 0;
        public static int enemyAnimationIndex1 = 0;
        public static int enemyAnimationIndex2 = 0;
        public static int animationDelay = 5;
        public static void UpdateAnimations(SpriteBatch spriteBatch)
        {
            // dinamikus megoldás az animációkra //TODO: fix
            // entities/enemies/0/enemy0  69px wide 44px high
            // entities/enemies/1/idle    180 wide 180px high
            /*
            enemyList.ForEach(enemy =>
            {
                spriteBatch.Draw(enemySheet, new Rectangle((int)Math.Round(enemy.position.X - 36), (int)Math.Round(enemy.position.Y - 60), 160, 160), new Rectangle(80 * enemyAnimationIndex, 0, 80, 80), Color.White);
            });
            */

            animationDelay -= 1;
            foreach (var enemy in enemyList)
            {
                if (enemy.Texture.Name == "entities/enemies/0/enemy0")
                {
                    spriteBatch.Draw(enemy.Texture, new Rectangle((int)Math.Round(enemy.position.X), (int)Math.Round(enemy.position.Y - 5), 110, 70), new Rectangle(69 * enemyAnimationIndex1, 0, 69, 44), Color.White);
                }
                else if (enemy.Texture.Name == "entities/enemies/1/idle")
                {
                    spriteBatch.Draw(enemy.Texture, new Rectangle((int)Math.Round(enemy.position.X - 75), (int)Math.Round(enemy.position.Y - 65), 200, 200), new Rectangle(180 * enemyAnimationIndex2, 0, 180, 180), Color.White);
                }
            }
            spriteBatch.Draw(playerSheet, new Rectangle((int)Math.Round(Player.position.X - 28), (int)Math.Round(Player.position.Y - 50), 140, 140), new Rectangle(80 * playerAnimationIndex, 0, 80, 80), Color.White);
            if (animationDelay <= 0)
            {
                enemyAnimationIndex1 += 1;
                if (enemyAnimationIndex1 >= 6) enemyAnimationIndex1 = 0;
                enemyAnimationIndex2 += 1;
                if (enemyAnimationIndex2 >= 11) enemyAnimationIndex2 = 0;

                playerAnimationIndex += 1;
                if (playerAnimationIndex >= 9) playerAnimationIndex = 0;

                animationDelay = 5;
            };
        }
        public static void LoadTextures(ContentManager Content)
        {
            // spriteTextures:
            wallTexture = Content.Load<Texture2D>("tiles/wall");
            floorTexture = Content.Load<Texture2D>("tiles/ground");

            // spriteSheets:
            Game1.dimm = Content.Load<Texture2D>("dimm"); // nem kezdtem el shader-t írni, ez is megteszi

            playerSheet = Content.Load<Texture2D>("entities/player/NightBorne");

            enemyTextures = new Texture2D[]{ // nyílván szarul neveztem el őket
                Content.Load<Texture2D>("entities/enemies/0/enemy0"),
                Content.Load<Texture2D>("entities/enemies/1/idle")
            }; // utóirat: jó lesz ez így is
        }
    }
}
