using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Spaceships
{
    class CollisionManager
    {

        private Spaceship ship;
        private AsteroidManager asteroidManager;
        private ParticleManager particleManager;


        public CollisionManager(Spaceship ship, AsteroidManager asteroidManager, ParticleManager particleManager )
        {
            this.ship = ship;
            this.asteroidManager = asteroidManager;
            this.particleManager = particleManager;
        }

        public void CheckCollisions()
        {
            foreach(Asteroid asteroid in asteroidManager.asteroids.ToArray())
            {   
                if (!ship.isInvincible && Vector2.DistanceSquared(asteroid.Position, ship.Position) < (asteroid.radius + ship.Radius())* (asteroid.radius + ship.Radius())){
                    ship.Destroy();
                }
                foreach(Particle bullet in particleManager.Bullets.ToArray())
                {
                    if (Vector2.DistanceSquared(asteroid.Position, bullet.currPosition) < (asteroid.radius + bullet.Radius) * (asteroid.radius + bullet.Radius))
                    {
                        asteroidManager.Destroy(asteroid);
                        particleManager.DestroyBullet(bullet);
                        Game1.increaseScore();
                    }
                }
            }
        }
    }
}
