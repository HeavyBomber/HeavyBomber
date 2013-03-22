#region File Description
//-----------------------------------------------------------------------------
// FrameRateCounter.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using BomberPunk.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace BomberPunk
{
    /// <summary>
    /// General Timing and Frame Rate Display Component.
    /// Add this to the GameComponentCollection to display the frame rate
    /// </summary>
    public class FrameRateCounter : DrawableGameComponent
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Vector2 fpsScreenLocation;
        int frameRate = 0;
        int frameCounter = 0;
        long elapsedTime = 0;    // Elapsed time in ticks

        string fpsString;

        #region public FrameRateCounter(Game game)
        /// <summary>
        /// Constructor which initializes the Content Manager which is used later for loading the font for display.
        /// </summary>
        /// <param name="game"></param>
        public FrameRateCounter()
            : base(Resources.Game)
        {
        }
        #endregion

        #region protected override void LoadContent()
        /// <summary>
        /// Graphics device objects are created here including the font.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = Resources.SpriteBatch;
            content = Resources.Content;

            spriteFont = content.Load<SpriteFont>("Fonts/font");
            fpsString = string.Format("fps: {0}", frameRate);
            fpsScreenLocation = new Vector2(320, 32);
        }
        #endregion

        #region protected override void UnloadContent()
        /// <summary>
        /// Content Unloading
        /// </summary>
        protected override void UnloadContent()
        {
            content.Unload();
        }
        #endregion

        #region public override void Update(GameTime gameTime)
        public override void Update(GameTime gameTime)
        {
            // Add the elapsed time to the total
            elapsedTime += gameTime.ElapsedGameTime.Ticks;
            // Has a second gone by?
            if (elapsedTime > TimeSpan.TicksPerSecond)
            {
                // Remove the second
                elapsedTime -= TimeSpan.TicksPerSecond;
                // Update the frame rate counter
                frameRate = frameCounter;
                // Reset the counter (Updated in Draw())
                frameCounter = 0;
                fpsString = string.Format("fps: {0}", frameRate);
            }
        }
        #endregion

        #region public override void Draw(GameTime gameTime)
        /// <summary>
        /// Frame rate display occurs during the Draw method and uses the Font and Sprite batch to render text.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, fpsString, fpsScreenLocation, Color.White);
            spriteBatch.End();
        }
        #endregion
        #region ToString
        public override string ToString()
        {
            return fpsString;
        }
        #endregion
    }

}