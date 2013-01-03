#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace HeroBash
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    public class BackgroundScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D texBG;
        Texture2D texLogo;
        Texture2D texHero;
        Texture2D texBash;

        Vector2 heroPos;
        Vector2 bashPos;

        bool logoBashed = false;
        float whiteFlashAlpha = 1f;

        ParallaxManager parallaxManager;

        Vector2 scrollPos;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "HeroBashContent");

            texBG = content.Load<Texture2D>("blank-white");
            texLogo = content.Load<Texture2D>("logo");
            texHero = content.Load<Texture2D>("hero-logo");
            texBash = content.Load<Texture2D>("bash-logo");

            heroPos = new Vector2(0, 0);
            bashPos = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width, 0);
            

            parallaxManager = new ParallaxManager(ScreenManager.GraphicsDevice.Viewport);
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/sky"), Vector2.Zero, 0f));
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/clouds1"), new Vector2(0, 50), -0.001f));
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/clouds2"), new Vector2(0, 0), -0.005f));
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/clouds3"), new Vector2(0, -50), -0.008f));
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/mountains3"), new Vector2(0, 300), -0.02f));
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/mountains2"), new Vector2(0, 100), -0.04f));
            parallaxManager.Layers.Add(new ParallaxLayer(content.Load<Texture2D>("background/mountains1"), new Vector2(0, 140), -0.07f));
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            scrollPos += new Vector2(5f, 0f);
            parallaxManager.Update(gameTime, scrollPos);

            if (!logoBashed)
            {
                heroPos += new Vector2(10, 0);
                bashPos += new Vector2(-10, 0);

                if (heroPos.X >= ScreenManager.GraphicsDevice.Viewport.Width / 2)
                {
                    logoBashed = true;
                }
            }

            if (logoBashed && whiteFlashAlpha > 0f)
                whiteFlashAlpha -= 0.05f;

            base.Update(gameTime, otherScreenHasFocus, false);
        }


        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            //spriteBatch.Draw(texBG, fullscreen,
              //               Color.White * TransitionAlpha * (0.5f + (0.5f * TransitionPosition)));
            parallaxManager.Draw(spriteBatch);

            if (!logoBashed)
            {
                spriteBatch.Draw(texHero, heroPos + new Vector2(0, viewport.Height / 3), null,
                             Color.White * TransitionAlpha, 0f, new Vector2(texHero.Width, texHero.Height / 2), 1f, SpriteEffects.None, 1);
                spriteBatch.Draw(texBash, bashPos + new Vector2(0, viewport.Height / 3), null,
                             Color.White * TransitionAlpha, 0f, new Vector2(0, texBash.Height / 2), 1f, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(texLogo, new Vector2(viewport.Width / 2, viewport.Height / 3), null,
                             Color.White * TransitionAlpha, 0f, new Vector2(texLogo.Width / 2, texLogo.Height / 2), 1f, SpriteEffects.None, 1);

                spriteBatch.Draw(texBG, fullscreen, null, Color.White * whiteFlashAlpha);

            }

            spriteBatch.End();

            ScreenManager.FadeBackBufferToBlack(1f-TransitionAlpha);
        }


        #endregion
    }
}
