using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// follows the spaceship around
/// with a random speed and acceleration and texture 
/// </summary>
namespace Spaceships
{
    class Asteroid
    {

        private Vector2 position;
        public Vector2 Position { get { return position; } }
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        public float speed;
        public int radius { get; }
        private Vector2 direction;
        private int caseNum;
        public bool isChild;
        private float angle;
        private ShapeDrawer shapeDrawer;

        /// <summary>
        /// creates the asteroid
        /// </summary>
        /// <param name="spriteBatch">allows this class to draw the asteroid</param>
        /// <param name="texture"> the texture for this follower</param>
        /// <param name="radius">the size of this asteroid</param>
        /// <param name="X">random x value for it to start at</param>
        /// <param name="Y">random y value for it to start at</param>
        /// <param name="angle">the angle at which the asteroid moves</param>
        /// <param name="speed">this instances specific max speed</param>
        public Asteroid(SpriteBatch spriteBatch, Texture2D texture, int radius, int X, int Y, Vector2 direction, float speed, int caseNum, bool isChild, ShapeDrawer shapeDrawer)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            position = new Vector2(X, Y);
            this.radius = radius;
            this.direction = direction;
            angle = (float)Math.Atan2(direction.X, -direction.Y);
            this.speed = speed;
            this.caseNum = caseNum;
            this.isChild = isChild;
            this.shapeDrawer = shapeDrawer;
            
        }

        /// <summary>
        /// update the location of the asteroid
        /// </summary>
        public void Update()
        {

            position += speed * direction;


        }

        /// <summary>
        /// draw the follower
        /// </summary>
        public void Draw()
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y,  2 * radius, 2 * radius), 
                null, Color.White, 0, new Vector2(radius, radius), 0, 0);

            if (Game1.DEBUG)
            {
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X + radius, (int)position.Y, 7, Color.Red);
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X - radius, (int)position.Y, 7, Color.Red);
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X, (int)position.Y + radius, 7, Color.Red);
                shapeDrawer.DrawLine((int)position.X, (int)position.Y,
                    (int)position.X, (int)position.Y - radius, 7, Color.Red);
            }


        }

        public bool isOutOfBounds()
        {
            if (position.Y < 0 && caseNum != 0) return true;
            if (position.Y > Game1.HEIGHT && caseNum != 2) return true;
            if (position.X < 0 && caseNum != 3) return true;
            if (position.X > Game1.WIDTH && caseNum != 1) return true;
           
            return false;
        }


    }
}
