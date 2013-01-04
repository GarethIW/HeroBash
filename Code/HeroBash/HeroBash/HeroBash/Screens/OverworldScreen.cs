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
using TiledLib;
using System.Collections.Generic;
#endregion

namespace HeroBash
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    /// 

    class OverworldLocation
    {
        public Vector2 Position;
        public List<OverworldLocation> LinkedLocations = new List<OverworldLocation>();
    }

    public class OverworldScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D texBG;
        Texture2D texDistance;

        Vector2 heroPos;
        Vector2 princessPos;

        Camera overworldCamera;
        Map overworldMap;

        Vector2 locationSize = new Vector2(96, 96);
        Vector2 tileSize = new Vector2(64, 64);
        int locationSpacing = 3;

        List<OverworldLocation> Locations = new List<OverworldLocation>();

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OverworldScreen()
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

            overworldMap = content.Load<Map>("maps/overworld");
            overworldCamera = new Camera(ScreenManager.GraphicsDevice.Viewport, overworldMap);
            overworldCamera.Target = new Vector2(0, (((overworldMap.Height / 2) * overworldMap.TileHeight)+(overworldMap.TileHeight/2)) - (overworldCamera.Height / 2));

            texBG = content.Load<Texture2D>("blank-white");
            texDistance = content.Load<Texture2D>("distance");

            heroPos = new Vector2(0, 0);
            princessPos = new Vector2(0, 0);

            Vector2 locPos = new Vector2((2 * overworldMap.TileWidth) + (overworldMap.TileWidth / 2), (overworldMap.Height * overworldMap.TileHeight) / 2);
            int numLocs = 0;
            for (int stage = 0; stage <= 8; stage++)
            {
                if (stage <= 4)
                    numLocs++;
                else
                    numLocs--;

                locPos.Y = ((overworldMap.Height * overworldMap.TileHeight) / 2) - (((numLocs - 1) * (overworldMap.TileHeight * locationSpacing)) / 2);

                for (int i = 1; i <= numLocs; i++)
                {
                    OverworldLocation loc = new OverworldLocation();
                    loc.Position = locPos;
                    Locations.Add(loc);

                    locPos.Y += overworldMap.TileHeight * locationSpacing;
                }

                locPos.X += overworldMap.TileWidth * locationSpacing;
                
            }
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
            overworldCamera.Update(ScreenManager.GraphicsDevice.Viewport);

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

            overworldMap.DrawLayer(spriteBatch, "FG", overworldCamera);

            foreach (OverworldLocation loc in Locations)
            {
                spriteBatch.Draw(texBG, new Rectangle((int)((loc.Position.X - (locationSize.X / 2)) - overworldCamera.Position.X), (int)((loc.Position.Y - (locationSize.Y / 2)) - overworldCamera.Position.Y), (int)locationSize.X, (int)locationSize.Y), null, Color.White * 0.5f);
            }

            spriteBatch.End();

            ScreenManager.FadeBackBufferToBlack(1f-TransitionAlpha);
        }


        #endregion
    }
}
