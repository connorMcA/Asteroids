using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceships
{
    class AsteroidManager
    {

        public List<Asteroid> asteroids { get; }
        private Texture2D[] asteroidTextures;
        private SpriteBatch spriteBatch;
        private Random rng;
        private ShapeDrawer shapeDrawer;
        

        private int MAX_ASTEROIDS = 10;

        public AsteroidManager(Random rng, SpriteBatch spriteBatch, Texture2D[] asteroidTextures, ShapeDrawer shapeDrawer)
        {
            this.rng = rng;
            this.spriteBatch = spriteBatch;
            this.shapeDrawer = shapeDrawer;
            this.asteroidTextures = asteroidTextures;
            asteroids = new List<Asteroid>();

            for (int i = 0; i < MAX_ASTEROIDS; i++)
            {
                asteroids.Add(createAsteroid(64));
            }
        }

        public void Update(GameTime gameTime)
        {

            foreach (Asteroid asteroid in asteroids.ToArray())
            {

                asteroid.Update();
                if (asteroid.isOutOfBounds())
                {
                    asteroids.Remove(asteroid);

                }
            }

            while (asteroids.Count < MAX_ASTEROIDS)
            {
                asteroids.Add(createAsteroid(64));

            }


        }

        public void Draw()
        {
            foreach (Asteroid asteroid in asteroids.ToArray())
            {
                asteroid.Draw();
            }
        }

        private Asteroid createAsteroid(int MAX_SIZE)
        {
            int x = 0;
            int y = 0;
            int radius = rng.Next(MAX_SIZE/2, MAX_SIZE);
            int caseNum = 0;
            Vector2 direction = new Vector2((float)rng.NextDouble(), (float)(rng.NextDouble()));
            switch (rng.Next(0, 4))
            {
                case 0:
                    {
                        x = rng.Next(-radius, Game1.WIDTH + radius);
                        y = -radius;
                        direction.X = direction.X * 2 - 1;
                        caseNum = 0;
                        break;
                    }
                case 1:
                    {
                        x = Game1.WIDTH + radius;
                        y = rng.Next(-radius, Game1.HEIGHT + radius);
                        caseNum = 1;
                        direction.X = -direction.X;
                        direction.Y = direction.Y * 2 - 1;
                        break;
                    }
                case 2:
                    {
                        x = rng.Next(-radius, Game1.WIDTH + radius);
                        y = Game1.HEIGHT + radius;
                        caseNum = 2;
                        direction.Y = -direction.Y;
                        direction.X = direction.X * 2 - 1;
                        break;
                    }
                case 3:
                    {
                        x = -radius;
                        y = rng.Next(-radius, Game1.HEIGHT + radius);
                        caseNum = 3;
                        direction.Y = direction.Y * 2 - 1;
                        break;
                    }
            }

            direction = Vector2.Normalize(direction);
            return new Asteroid(spriteBatch, asteroidTextures[rng.Next(0, 4)], radius, x, y, direction, rng.Next(1, 3), caseNum, false, shapeDrawer);
        }

        public Asteroid childAsteroid(Asteroid asteroid)
        {

            Vector2 direction = Vector2.Normalize(new Vector2((float)(Math.PI * (rng.NextDouble() * 2 - 1)), (float)(Math.PI * (rng.NextDouble() * 2 - 1))));

            return new Asteroid(spriteBatch, asteroidTextures[rng.Next(0, 4)], asteroid.radius/2, 
                (int)asteroid.Position.X, (int)asteroid.Position.Y, direction, asteroid.speed/1.5f, 4, true, shapeDrawer);
        }

        public void Destroy(Asteroid asteroid)
        {
            if (asteroid.isChild)
            {
                asteroids.Remove(asteroid);
            }
            else
            {
                asteroids.Add(childAsteroid(asteroid));
                asteroids.Add(childAsteroid(asteroid));
                asteroids.Remove(asteroid);
                
            }
        }
    }
}
