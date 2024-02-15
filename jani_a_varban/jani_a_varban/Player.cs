using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace jani_a_varban
{
    public class Player : Game1
    {
        Game1 game;
        public int MovementLockDelay = 60; // ~ 1 sec movement delay at start
        public int Hp = 1000;
        public Vector2 position = Game1.EnteranceCoordinates;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public void Move(string direction)
        {
            if (direction == "up")
            {
                if ((mapMatrix[(int)(position.Y / 72) - 1][(int)(position.X / 72)] == 0)) position.Y -= 72;
            }
            else if (direction == "down")
            {
                if ((mapMatrix[(int)(position.Y / 72) + 1][(int)(position.X / 72)] == 0)) position.Y += 72;
            }
            else if (direction == "left")
            {
                if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) - 1] == 0)) position.X -= 72;
            }
            else if (direction == "right")
            {
                if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) + 1] == 0)) position.X += 72;
            }
            this.MovementLockDelay = 35;
            foreach (var enemy in enemyList)
            {
                enemy.Move();
            }
        }
        public Player(Game1 game)
        {
            this.game = game;
        }
        public void HandleInput(bool isMovementLocked, KeyboardState keyboard)
        {
            // upon movement set delay
            if (isMovementLocked) this.MovementLockDelay -= 1;
            else
            {
                if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up)) Move("up");
                else if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down)) Move("down");
                else if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left)) Move("left");
                else if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right)) Move("right");
            }
        }
    }
}