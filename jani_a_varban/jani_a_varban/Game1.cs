using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace jani_a_varban
{
    public class Game1 : Game
    {
        Player player;
        public static Texture2D playerTexture;
        public static Texture2D enemyTexture;
        public static Texture2D floorTexture;
        public static Texture2D wallTexture;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public const int MapWidth = 14;
        public const int MapHeight = 14;
        public Vector2 EnteranceCoordinates = new Vector2();
        public Vector2 ExitCoordinates = new Vector2();
        public static List<Enemy> enemyList = new List<Enemy>();
        public static List<List<int>> mapMatrix = Map.Generate(MapWidth, MapHeight);
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Main _spriteBatch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Entities
            player = new Player(this);
            for (int i = 0; i < 3; i++)
            {
                enemyList.Add(new Enemy(this));
            }

            // Textures ¯\_(ツ)_/¯
            playerTexture = Content.Load<Texture2D>("hero-down");
            enemyTexture = Content.Load<Texture2D>("skeleton");
            wallTexture = Content.Load<Texture2D>("wall");
            floorTexture = Content.Load<Texture2D>("floor");
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            player.HandleInput(!(player.MovementLockDelay == 0), Keyboard.GetState());

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Map.DrawMap(_spriteBatch, mapMatrix);
            _spriteBatch.Draw(playerTexture, player.Position, Color.White);

            foreach (var enemy in enemyList)
            {
                _spriteBatch.Draw(enemyTexture, enemy.Position, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}