using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// sets up the background
/// draws two different shapes with two different colors
/// uses Gaussian distributions to create variance in the shapes
/// </summary>
namespace Spaceships
{

    class Background
    {

        private float R, G, B;
        private Random rng;
        private int midRad;
        private int minRad;
        private int maxRad;
        private float stdDev;
        private ShapeDrawer shapeDrawer;
        private static int MAXSHAPES = 2000;
        private Color[] colors = new Color[MAXSHAPES * 2];
        private Vector4[] points = new Vector4[MAXSHAPES * 2];
        private static int MIDWIDTH;
        private static int MIDHEIGHT;

        /// <summary>
        /// creates the background image
        /// uses Gaussian Distributions to create variance in the radius from the center
        /// The first shape it creates is a diamond in the middle of the screen
        /// the second shape is a hollow circle around the diamond
        /// </summary>
        /// <param name="rand"> random number generator</param>
        /// <param name="spriteBatch">draws the lines</param>
        /// <param name="graphics">needed to create a ShapeDrawer</param>
        public Background(Random rand, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            rng = rand;

            minRad = rng.Next(0, 200);
            maxRad = rng.Next(minRad+100, 600);

            shapeDrawer = new ShapeDrawer(spriteBatch, graphics);
            MIDHEIGHT = graphics.Viewport.Height / 2;
            MIDWIDTH = graphics.Viewport.Width / 2;

            R = (float)rng.NextDouble();
            G = (float)rng.NextDouble();
            B = (float)rng.NextDouble();
            midRad = 0;
            stdDev = (maxRad - minRad) / 4;
            for (int i = 0; i < MAXSHAPES; i++)
            {
                colors[i] = new Color((float)rng.Gaussian(R, .1), (float)rng.Gaussian(G, .1), (float)rng.Gaussian(B, .1));
                float x = (float)rng.Gaussian(midRad, stdDev);
                float y = (float)rng.Gaussian(midRad, stdDev);

                Vector2 unitCircle1 = OnUnitCircle(2, 0, 0);
                Vector2 unitCircle2 = OnUnitCircle(1f, unitCircle1.X, unitCircle1.Y);
                points[i] = new Vector4((x * unitCircle1.X + MIDWIDTH), y * unitCircle1.Y + MIDHEIGHT, x * unitCircle2.X + MIDWIDTH, y * unitCircle2.Y + MIDHEIGHT);
            }


            R = (float)rng.NextDouble();
            G = (float)rng.NextDouble();
            B = (float)rng.NextDouble();
            midRad = (minRad + maxRad) / 2;
            stdDev = (midRad - minRad) / 4;
            for (int i = 0; i < MAXSHAPES; i++)
            {
                colors[i+MAXSHAPES] = new Color((float)rng.Gaussian(R, .1), (float)rng.Gaussian(G, .1), (float)rng.Gaussian(B, .1));
                float x = (float)rng.Gaussian(midRad, stdDev);
                float y = (float)rng.Gaussian(midRad, stdDev);

                Vector2 unitCircle1 = OnUnitCircle(2, 0, 0);
                Vector2 unitCircle2 = OnUnitCircle(1f, unitCircle1.X, unitCircle1.Y);
                points[i +MAXSHAPES] = new Vector4((x * unitCircle1.X + MIDWIDTH), y*unitCircle1.Y + MIDHEIGHT, x*unitCircle2.X + MIDWIDTH, y*unitCircle2.Y + MIDHEIGHT);
            }
        }

        public void Update()
        {
                Vector4 point = points[MAXSHAPES];
                Vector2 tempPoint = new Vector2(point.X -MIDWIDTH, point.Y - MIDHEIGHT);
                float Length = tempPoint.Length();


            for (int i = 0; i < MAXSHAPES; i++)
            {

                midRad = (minRad + maxRad) / 2;
                stdDev = (midRad - minRad) / 4;
                point = points[i + MAXSHAPES];
                Vector2 firstPoint = Vector2.Normalize(new Vector2(point.Z -MIDWIDTH, point.W - MIDHEIGHT));
                Vector2 secondPoint = Vector2.Normalize(new Vector2(point.Z - MIDWIDTH, point.W - MIDHEIGHT));
                float angle = (float)Math.Atan2(secondPoint.Y, secondPoint.X);
                angle += .2f;
                secondPoint = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Length = (float)rng.Gaussian(midRad, stdDev);

                points[i+MAXSHAPES] = new Vector4(firstPoint.X * Length + MIDWIDTH, firstPoint.Y * Length + MIDHEIGHT, secondPoint.X * Length + MIDWIDTH, secondPoint.Y * Length + MIDHEIGHT);
            }

            for (int i = 0; i < MAXSHAPES; i++)
            {

                midRad = 0;
                stdDev = (maxRad - minRad) / 4;
                point = points[i];
                Vector2 firstPoint = Vector2.Normalize(new Vector2(point.Z - MIDWIDTH, point.W - MIDHEIGHT));
                Vector2 secondPoint = Vector2.Normalize(new Vector2(point.Z - MIDWIDTH, point.W - MIDHEIGHT));
                float angle = (float)Math.Atan2(secondPoint.Y, secondPoint.X);
                angle += .2f;
                secondPoint = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Length = (float)rng.Gaussian(midRad, stdDev);

                points[i] = new Vector4(firstPoint.X * Length + MIDWIDTH, firstPoint.Y * Length + MIDHEIGHT, secondPoint.X * Length + MIDWIDTH, secondPoint.Y * Length + MIDHEIGHT);
            }

        }

        public void Draw()
        {
            for (int i = 0; i < MAXSHAPES * 2; i++)
            {
                Vector4 point = points[i];
                shapeDrawer.DrawLine((int)point.X, (int)point.Y, (int)point.Z, (int)point.W, 
                    2,colors[i]);   
            }
        }

        private Vector2 OnUnitCircle(float range, float preX, float preY)
        {
            float x = preX + (float)(rng.NextDouble() * range - range/2);
            float y = preY + (float)(rng.NextDouble() * range - range/2);
            return Vector2.Normalize(new Vector2(x , y));
        }

    }
}
