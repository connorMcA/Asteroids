using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// a particle that the spaceship can shoot
/// doesn't actually handle collisions or anything
/// simply moves in the direction the ship was facing when fired
/// Left Click to fire
/// </summary>
namespace Spaceships
{
    class Particle
    {

        public Vector2 currPosition;
        private int radius = 1;
        public int Radius { get { return radius; } }
        private Vector2 previousPosition;
        private int speed = 15;
        private Vector2 velocity;
        private ShapeDrawer shapeDrawer;
        public Vector2 startPosition;

        /// <summary>
        /// create the particle
        /// </summary>
        /// <param name="position">the current position of the ship (and thus the particle)</param>
        /// <param name="direction">the direction the ship is facing</param>
        /// <param name="shapeDrawer">used to draw the line of the particle</param>
        public Particle(Vector2 position, Vector2 direction , ShapeDrawer shapeDrawer)
        {
            startPosition = position;
            currPosition = position;
            previousPosition = currPosition;
            velocity = speed * direction;
            this.shapeDrawer = shapeDrawer;
        }

        /// <summary>
        /// update the position of the particle
        /// </summary>
        public void Update()
        {
            previousPosition = currPosition;
            currPosition += velocity;


        }

        /// <summary>
        /// draw the particle
        /// </summary>
        public void Draw()
        {
            shapeDrawer.DrawLine((int)previousPosition.X, (int)previousPosition.Y,
                (int)currPosition.X, (int)currPosition.Y, 7, Color.Red);
        }
    }
}
