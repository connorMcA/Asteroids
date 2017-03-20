using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spaceships
{
    class ShapeDrawer
    {

        SpriteBatch spriteBatch;
        Texture2D pixel;

        public ShapeDrawer(SpriteBatch batch, GraphicsDevice graphics)
        {
            spriteBatch = batch;
            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
		/// Draws a line between the specified points.
		/// Assumes that spritebatch.Begin() has been called.
		/// </summary>
		/// <param name="x0">X coord of first point</param>
		/// <param name="y0">Y coord of first point</param>
		/// <param name="x1">X coord of second point</param>
		/// <param name="y1">Y coord of second point</param>
		/// <param name="thickness">"Width" of the line</param>
		/// <param name="color">Color of the line</param>
		public void DrawLine(int x0, int y0, int x1, int y1, int thickness, Color color)
        {
            // Length of the line we're drawing
            float length = Vector2.Distance(
                new Vector2(x0, y0),
                new Vector2(x1, y1));

            // Angle between the desired line and
            // the x axis
            float angle = (float)Math.Atan2(y1 - y0, x1 - x0);

            // Construct the rectangle to draw
            Rectangle rectToDraw = new Rectangle(
                x0,
                y0,
                (int)length,
                thickness);

            // Draw the rotated rectangle
            spriteBatch.Draw(
                pixel,
                rectToDraw,
                null,
                color,
                angle,
                new Vector2(0, 0.5f),
                SpriteEffects.None,
                0.0f);
        }

        /// <summary>
        /// draws a point at the given location with the given color
        /// </summary>
        /// <param name="x">the x location of the point</param>
        /// <param name="y">the y lcoation of the point</param>
        /// <param name="color">the color of the point</param>
        public void DrawPoint(int x, int y, Color color)
        {
            spriteBatch.Draw(
                pixel,
                new Vector2(x, y),
                color);
        }

        /// <summary>
        /// draws a filled rectangle with the top left corner at the point given. 
        /// </summary>
        /// <param name="x">the x location of the point</param>
        /// <param name="y">the y location of the point</param>
        /// <param name="width">width of the rectangle</param>
        /// <param name="height">height of the rectangle</param>
        /// <param name="color">color of the rectangle</param>
        public void DrawRectFilled(int x, int y, int width, int height, Color color)
        {
            spriteBatch.Draw(
                pixel,
                new Rectangle(
                    new Point(x, y), new Point(width, height)),
                color);
        }

        /// <summary>
        /// draws the outline of a rectangle with the top left corner at the given coordinates
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <param name="width">width of the rectangle</param>
        /// <param name="height">height of the rectangle</param>
        /// <param name="color">color of the rectangle</param>
        public void DrawRectOutline(int x, int y, int width, int height, Color color)
        {
            DrawLine(x, y, x + width, y, 1, color);
            DrawLine(x, y, x, y+height, 1, color);
            DrawLine(x, y+height, x + width, y+height, 1, color);
            DrawLine(x+width, y, x + width, y+height, 1, color);
        }
    }
}
