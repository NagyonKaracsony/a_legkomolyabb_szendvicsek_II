using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Threading.Tasks;
namespace jani_a_varban
{
    public class Player : Game1
    {
        public int MovementLockDelay = 60; // ~ 1 sec movement delay at start
        public int hp = 2000;
        public int dmg = 500;
        public int dp = 50;
        public static bool HasKey = false;
        public static Vector2 position = EnteranceCoordinates;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public void Move(string direction)
        {
            if (direction == "up")
            {
                if ((int)(position.Y / 72) != 0)
                {
                    if ((mapMatrix[(int)(position.Y / 72) - 1][(int)(position.X / 72)] == 0)) position.Y -= 72;
                }
            }
            else if (direction == "down")
            {
                if ((int)(position.Y / 72) != mapMatrix.Count - 1)
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
                if ((int)(position.X / 72) != mapMatrix.Count - 1)
                {
                    if ((mapMatrix[(int)(position.Y / 72)][(int)(position.X / 72) + 1] == 0)) position.X += 72;
                    if (position.X == ((MapWidth -1) * 72) && Player.HasKey)
                    {
                        Map.LoadNextLevel(difficulty += 1);
                        HasKey = false;
                        player.hp = 2000;
                    }
                }
            }
            this.MovementLockDelay = 15;

            CheckForEnemy(true);
            ProcessEnemies();
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
                else if (keyboard.IsKeyDown(Keys.E))
                {
                    // limit test frequency shit
                    this.MovementLockDelay = 50;
                    Map.LoadNextLevel(difficulty);
                    difficulty += 1;
                }
            }
            // egyszerűen nem lehet megoldani a dimm effect arányát //TODO: fix
            /*
            if (mouse.ScrollWheelValue > Camera.previousScrollValue) Camera.Zoom = MathHelper.Clamp(Camera.Zoom += 0.075f, 0.5f, 1.0f);
            else if (mouse.ScrollWheelValue < Camera.previousScrollValue) Camera.Zoom = MathHelper.Clamp(Camera.Zoom -= 0.075f, 0.5f, 1.0f);
            Camera.previousScrollValue = mouse.ScrollWheelValue;
            */
        }
        public void CheckForEnemy(bool enemyIsTarget)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                var enemy = enemyList[i];
                if (enemy.position == player.Position) Battle(enemyIsTarget, enemy);
            }
        }
        // kétszer fut le, 2x damage a playerre, talán az enemyre is? //TODO: fix
        public void Battle(bool enemyIsTarget, Enemy target)
        {
            if (enemyIsTarget)
            {
                target.hp -= dmg;
                if (target.hp <= 0)
                {
                    if (target.HasKey) Player.HasKey = true;
                    enemyList.Remove(target);
                }
                else
                {
                    hp -= target.dmg;
                    if (hp <= 0)
                    {
                        Debug.WriteLine("u died");
                        difficulty = 3;
                        Map.LoadNextLevel(difficulty);
                    }
                }
            }
            else
            {
                hp -= target.dmg;
                if (hp <= 0)
                {
                    Debug.WriteLine("u died");
                    difficulty = 3;
                    Map.LoadNextLevel(difficulty);
                }
                else if (target.hp <= 0)
                {
                    if (target.HasKey) Player.HasKey = true;
                    enemyList.Remove(target);
                }
                target.hp -= dmg;
            }
        }
        public static void ProcessEnemies()
        {
            for (int i = 0; i < enemyList.Count; i++) enemyList[i].ApproachPlayer();
        }
    }
}