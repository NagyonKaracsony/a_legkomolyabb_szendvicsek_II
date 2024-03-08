using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;
using System.Threading;
namespace jani_a_varban
{
    public class Player : Game1
    {
        public int MovementLockDelay = 60; // ~ 1 sec movement delay at start
        public int hp = 1000;
        public int dmg = 350;
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
                if ((int) (position.Y / 72) != 0)
                {
                    if ((mapMatrix[(int)(position.Y / 72) - 1][(int)(position.X / 72)] == 0)) position.Y -= 72;
                }
            }
            else if (direction == "down")
            {
                if ((int)(position.Y / 72) != mapMatrix.Count-1)
                {
                    if ((mapMatrix[(int)(position.Y / 72) + 1][(int)(position.X / 72)] == 0)) position.Y += 72;
                }
            }
            else if (direction == "left")
            {
                if ((int)(position.X / 72) != 0)
                {
                    if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) - 1] == 0)) position.X -= 72;
                }
            }
            else if (direction == "right")
            {
                if ((int)(position.X / 72) != mapMatrix.Count-1)
                {
                    if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) + 1] == 0)) position.X += 72;
                }
            }
            this.MovementLockDelay = 5;

            CheckForEnemy(true);
            foreach (var enemy in enemyList) enemy.Move();
            CheckForEnemy(false);
        }
        public Player()
        {

        }
        public void HandleInput(bool isMovementLocked, KeyboardState keyboard, MouseState mouse)
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
            if (mouse.ScrollWheelValue > Camera.previousScrollValue)
            {
                Camera.Zoom = MathHelper.Clamp(Camera.Zoom += 0.075f, 0.75f, 1.25f);
            }
            else if (mouse.ScrollWheelValue < Camera.previousScrollValue)
            {
                Camera.Zoom = MathHelper.Clamp(Camera.Zoom -= 0.075f, 0.75f, 1.25f);
            }
            Camera.previousScrollValue = mouse.ScrollWheelValue;
        }
        public void CheckForEnemy(bool enemyIsTarget)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                var enemy = enemyList[i];
                if (enemy.position == Position) Battle(enemyIsTarget, enemy);
            }
        }
        public void Battle(bool enemyIsTarget, Enemy target)
        {
            if (enemyIsTarget)
            {
                target.hp -= dmg;
                hp -= target.dmg;
            }
            else
            {
                hp -= target.dmg;
                target.hp -= dmg;
            }
            if (target.hp <= 0) enemyList.Remove(target);
            if (hp <= 0) Debug.WriteLine("u died");
            Debug.WriteLine(hp);
            Debug.WriteLine(target.hp);
        }
    }
}