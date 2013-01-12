using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace HeroBash
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public static class BackgroundBox
    {
        public static void Draw(SpriteBatch sb, Texture2D bg, Vector2 pos, Vector2 dimensions, Color color)
        {
            Draw(sb, bg, new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y), color);
        }

        public static void Draw(SpriteBatch sb, Texture2D bg, Rectangle rect, Color color)
        {
            sb.Draw(bg, rect, null, color);
        }
    }
}
