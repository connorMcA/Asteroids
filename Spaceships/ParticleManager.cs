using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceships
{
    class ParticleManager
    {
        private List<Particle> bullets;
        public List<Particle> Bullets { get { return bullets; } }
        private SpriteBatch spriteBatch;
        private ShapeDrawer shapeDrawer;
        private KeyboardState prevKeyboard;
        private Spaceship ship;

        private int MAX_PARTICLES = 5;


        public ParticleManager(SpriteBatch spriteBatch, ShapeDrawer shapeDrawer, Spaceship ship)
        {
            this.spriteBatch = spriteBatch;
            this.shapeDrawer = shapeDrawer;
            bullets = new List<Particle>(capacity: MAX_PARTICLES);
            prevKeyboard = Keyboard.GetState();
            this.ship = ship;
        }

        public void Update()
        {
            KeyboardState currKeyboard = Keyboard.GetState();

            if (bullets.Count < MAX_PARTICLES)
            {
                if (currKeyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space))
                {
                    bullets.Add(new Particle(ship.Position, ship.Direction, shapeDrawer));
                }
            }

            foreach (Particle bullet in bullets.ToArray())
            {
                bullet.Update();
                if (Vector2.DistanceSquared(bullet.startPosition, bullet.currPosition) > 400 * 400)
                {
                    DestroyBullet(bullet);
                }
            }

            prevKeyboard = currKeyboard;
        }

        public void Draw()
        {
            foreach (Particle bullet in bullets.ToArray())
            {
                bullet.Draw();
            }
        }

        public void DestroyBullet(Particle bullet)
        {
            bullets.Remove(bullet);
        }
        
    }
}
