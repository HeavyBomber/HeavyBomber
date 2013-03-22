using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameObjects;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using BomberPunk.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.Controls;
using PhantomEngine.GameObjects;
using PhantomEngine.ObjectData;
using SpriteSheetRuntime;
using XMLContent.Controls;

namespace BomberPunk.Controls
{
    class AnimatedDigit : AnimatedObject
    {
        private bool isRunning;
        private string upperChar;
        private Texture2D cyphersTexture;
        private int positionOffset = 20;
        private float cypherSpeed = 3f;
        private float currentDigit = 0;
        private int desiredDigit;
        private Rectangle cypherRect;
        private int cypherWidth;
        private int cypherHeight;
        private int currentOffset;

        public Texture2D CyphersTexture
        {
            get { return cyphersTexture; }
            set { cyphersTexture = value; }
        }

        public override void Restore(Vector2 position, SpriteSheet spriteSheet, ObjectDataBase objectData)
        {
            const string CYPHERS_PATH = "Sprites/UI/HUD/Digit/cyphers";
            
            //this.cyphersTexture = Resources.Content.Load<Texture2D>(CYPHERS_PATH + id);
            cypherWidth = spriteSheet.SourceRectangle(0).Width;
            cypherHeight = spriteSheet.SourceRectangle(0).Height;
            cypherRect = new Rectangle(0, 0, cypherWidth, cypherHeight);

            base.Restore(position, spriteSheet, objectData);
        }

        public void SetValue(int value)
        {
            this.isRunning = true;
            desiredDigit = value;

            currentOffset = value*cypherHeight;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isRunning)
                return;

            //base.Update(gameTime);

            var inc = 0;
            if (cypherRect.Y < currentOffset)
                inc = 1;
            else if (cypherRect.Y > currentOffset)
                inc = -1;
            else
            {
                isRunning = false;
                return;
            }


            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (accumulator > spriteSheet.FrameTime)
            {
                accumulator -= spriteSheet.FrameTime;
                {
                    cypherRect.Y += (int)(inc * cypherSpeed);

                    currentFrame -= inc;
                    if (currentFrame >= frames)
                        currentFrame = 0;
                    else if (currentFrame < 0)
                        currentFrame = frames - 1;
                }
            }
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            int direciton = (int)currentDirection;
            spriteBatch.Draw(spriteSheet.Texture, basePosition, spriteSheet.SourceRectangle((int)currentDirection * frames + currentFrame),
               color, 0, Vector2.Zero, 1,
               SpriteEffects.None, LayerIdentifiers.DIGIT_BACKGROUND);

            spriteBatch.Draw(CyphersTexture, basePosition, cypherRect,
              color, 0, Vector2.Zero, 1,
              SpriteEffects.None, LayerIdentifiers.DIGIT_TEXT);
        }
    }
}
