using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
namespace jani_a_varban
{
    public class Game1 : Game
    {
        public static int difficulty = 4;
        public static Random random = new Random();
        public static Player player;
        public static Vector2 StartPosition = new Vector2();
        public static GameWindow GameWindow { get; private set; }
        public static Texture2D enemyTexture;
        public static Texture2D floorTexture;
        public static Texture2D dimm;
        public static Texture2D wallTexture;
        public GraphicsDeviceManager _graphics;
        public GraphicsDevice _device;
        public SpriteBatch spriteBatch;
        public const int MapWidth = 15;
        public const int MapHeight = 15;
        public static Vector2 EnteranceCoordinates = new Vector2();
        public static Vector2 ExitCoordinates = new Vector2();
        public static List<Enemy> enemyList = new List<Enemy>();
        public static List<List<List<int>>> Chunks = new List<List<List<int>>>();
        public static List<List<int>> mapMatrix = Map.Generate(MapWidth, MapHeight, new Vector2(random.Next(2, MapHeight - 2), 0), true);
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
            _graphics.IsFullScreen = false;
            Camera.Position = new Vector2(MapHeight * 36, MapWidth * 36); // center fix camera on map center
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Main spriteBatch
            // eszetekbe se jusson mégegyet csinálni! nem kell több spriteBatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Textures ¯\_(ツ)_/¯
            Animation.LoadTextures(Content);

            // Load Textures before entity Initialization, why? cuz fuck you exception. that's why.

            // Entities
            Game1.player = new Player();
            for (int i = 0; i < difficulty; i++) enemyList.Add(new Enemy());
            enemyList[random.Next(1, enemyList.Count)].HasKey = true;
            player.Position = new Vector2(StartPosition.Y * 72, StartPosition.X * 72);
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            player.HandleInput(player.MovementLockDelay != 0, Keyboard.GetState(), Mouse.GetState(Window));
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            // Rectangle sourceRectangle = new Rectangle(72 * i - 8, 0, 48, 64);
            GraphicsDevice.Clear(Color.DimGray);
            spriteBatch.Begin(transformMatrix: Camera.GetViewMatrix(GraphicsDevice));

            // Multithread? - chunks are demanding af
            Map.DrawMap(spriteBatch);

            // Multithread? - nah
            Animation.UpdateAnimations(spriteBatch);

            // Draw the image at the calculated position with scaled size
            spriteBatch.Draw(dimm, new Vector2(-MapWidth * 28, 0), null, Color.White, 0f, Vector2.Zero, Camera.Zoom, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}