using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Spaceships
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Random rng;
        private Spaceship ship;
        private Background background;
        private AsteroidManager asteroidManager;
        private CollisionManager collisionManager;
        private ParticleManager particleManager;
        private ShapeDrawer shapeDrawer;
        private static int score = 0;

        public static bool DEBUG =false;

        public static int WIDTH;
        public static int HEIGHT;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            WIDTH = GraphicsDevice.Viewport.Width;
            HEIGHT = GraphicsDevice.Viewport.Height;
            

            rng = new Random();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("gameText");

            shapeDrawer = new ShapeDrawer(spriteBatch, GraphicsDevice);

            Texture2D shipTexture = Content.Load<Texture2D>("ship");
            ship = new Spaceship(spriteBatch, shipTexture, shapeDrawer);

            Texture2D[] asteroidTextures = new Texture2D[4];
            asteroidTextures[0] = Content.Load<Texture2D>("asteroid1");
            asteroidTextures[1] = Content.Load<Texture2D>("asteroid2");
            asteroidTextures[2] = Content.Load<Texture2D>("asteroid3");
            asteroidTextures[3] = Content.Load<Texture2D>("asteroid4");

            asteroidManager = new AsteroidManager(rng, spriteBatch, asteroidTextures, shapeDrawer);

            particleManager = new ParticleManager(spriteBatch, shapeDrawer, ship);

            collisionManager = new CollisionManager(ship, asteroidManager, particleManager);
            
            background = new Background(rng, spriteBatch, GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() { 

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            asteroidManager.Update(gameTime);


            ship.Update();
            particleManager.Update();

            collisionManager.CheckCollisions();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();



            if (ship.LIVES > 0)
            {

                particleManager.Draw();
                asteroidManager.Draw();
               
                ship.Draw();
            }
            else
            {
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(WIDTH / 2 - 180, HEIGHT / 2 - 20), Color.White);
            }
            spriteBatch.DrawString(font, "Score" + score, new Vector2(10, 10), Color.White);
            for (int i = 0; i < ship.LIVES; i++)
            {
                spriteBatch.Draw(ship.ship, new Rectangle(10 + i * ship.ship.Width, 60, 64, 64), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void increaseScore()
        {
            score += 10;
        }
    }
}
