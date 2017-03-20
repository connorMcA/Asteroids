using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// the spaceship that the user controls
/// </summary>
namespace Spaceships
{
    class Spaceship
    {

        private Vector2 position;
        public Vector2 Position { get { return position; } }
        private Vector2 velocity;
        public Texture2D ship;
        private SpriteBatch spriteBatch;
        private Vector2 direction;
        public Vector2 Direction { get { return direction; } }
        private float degrees;
        private float acceleration;
        public float ACCELERATION { get { return acceleration; } }
        private float speed;
        private ShapeDrawer shapeDrawer;
        public bool isInvincible = false;

        public int MAXSPEED = 7;

        public int LIVES = 3;

        /// <summary>
        /// creates a new spaceship
        /// </summary>
        /// <param name="spriteBatch">lets this class draw the spaceship</param>
        /// <param name="ship">the ship texture</param>
        /// <param name="graphics">lets this class create a shapeDrawer for its particles</param>
        public Spaceship(SpriteBatch spriteBatch, Texture2D ship, ShapeDrawer shapeDrawer)
        {
            direction = new Vector2(1, 0);
            degrees = 0;
            acceleration = .1f;
            speed = 0.0f;
            position = new Vector2(Game1.WIDTH/2, Game1.HEIGHT/2);
            this.ship = ship;
            this.spriteBatch = spriteBatch;
            this.shapeDrawer = shapeDrawer;
        }

        /// <summary>
        /// update the position of the ship according to user input
        /// </summary>
        public void Update()
        {

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                degrees += .1f;

            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                degrees -= .1f;

            }

            direction = new Vector2((float)Math.Cos(degrees), (float)Math.Sin(degrees));


            if (keyboard.IsKeyDown(Keys.Up))
            {
                speed += acceleration;
                if (speed > MAXSPEED) speed = MAXSPEED;


            }
            else
            {
                float decceleration = 0.94f;
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    decceleration = 0.5f;
                }
                speed *= decceleration;
            }
            velocity = speed * direction;
            position += velocity;


            if (position.Y > Game1.HEIGHT) position.Y = 0;
            if (position.Y < 0) position.Y = Game1.HEIGHT;
            if (position.X > Game1.WIDTH) position.X = 0;
            if (position.X < 0) position.X = Game1.WIDTH;


        }

        /// <summary>
        /// draw the ship from the center of the texture
        /// </summary>
        public void Draw()
        {
            spriteBatch.Draw(ship, rotation: degrees+(float)Math.PI/2, origin: new Vector2(ship.Width/2, ship.Height/2), destinationRectangle: new Rectangle((int)position.X, (int)position.Y, 64, 64));

            if (Game1.DEBUG)
            {
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X + Radius(), (int)position.Y, 7, Color.Red);
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X - Radius(), (int)position.Y, 7, Color.Red);
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X, (int)position.Y + Radius(), 7, Color.Red);
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X, (int)position.Y - Radius(), 7, Color.Red);
            }
        }

        public int Radius()
        {
            return 32;
        }

        public void Destroy()
        {
            position = new Vector2(Game1.WIDTH / 2, Game1.HEIGHT / 2);
            speed = 0;
            direction = new Vector2(1, 0);
            degrees = 0;
            LIVES -=  1;
            isInvincible = true;
        }

    }
}
