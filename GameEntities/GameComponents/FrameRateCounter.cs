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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace GameEntities.GameComponents
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

        public FrameRateCounter(Game game)
            : base(game)
        {
        }

        public void Initialize()
        {
            
        }

        protected override void LoadContent()
        {
            spriteBatch = Resources.GameResources.SpriteBatch;
            content = Resources.GameResources.Content;

            spriteFont = content.Load<SpriteFont>("Fonts/font");
            fpsString = string.Format("fps: {0}", frameRate);
            fpsScreenLocation = new Vector2(320, 32);
        }


        protected override void UnloadContent()
        {
            content.Unload();
        }

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

        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, fpsString, fpsScreenLocation, Color.White);
            spriteBatch.End();
        }

        public override string ToString()
        {
            return fpsString;
        }
    }

}