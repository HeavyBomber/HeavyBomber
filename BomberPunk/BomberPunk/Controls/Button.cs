using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameObjects;
using BomberPunk.GameStructs;
using GameEntities.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.Geometry;
using SpriteSheetRuntime;

namespace BomberPunk.Controls
{
    class Button : AnimatedObject
    {
        #region Constructors

        private bool isPressed;

        private bool wasPressed;
        private Circle buttonCircle;
        private int frameDelta;
        private new const float FRAME_TIME = 0.05f;
        #endregion

        #region Properties
        public Circle ButtonCircle
        {
            get { return buttonCircle; }
        }
        public bool WasPressed
        {
            get { return wasPressed; }
        }
        #endregion
        #region Methods

        public override void Update(GameTime gameTime)
        {
            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (accumulator > FRAME_TIME)
            {
                accumulator -= FRAME_TIME;
                {
                    currentFrame += frameDelta;
                    if (currentFrame >= frames - 1)
                    {
                        currentFrame = frames - 1;
                        frameDelta = 0;
                    }
                    else if (currentFrame <= 0)
                    {
                        currentFrame = 0;
                        frameDelta = 0;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
    
            int direciton = (int) currentDirection;
            spriteBatch.Draw(spriteSheet.Texture, Camera2D.ScreenCenter, spriteSheet.SourceRectangle((int)currentDirection * frames + currentFrame),
               color, Camera2D.Rotation, Position, 1,
               SpriteEffects.None, 1);
        
        }

        public override void Restore(Vector2 position, SpriteSheet spriteSheet, ObjectDataBase objectData)
        {
            base.Restore(position, spriteSheet, objectData);
            buttonCircle = new Circle(position
                + new Vector2(400 + spriteSheet.SourceRectangle(0).Width / 2, 280 + spriteSheet.SourceRectangle(0).Height / 2), 75);
            color = Color.Red;
        }

        public void Press()
        {
            frameDelta = 1;
            wasPressed = false;
            isPressed = true;
        }

        public void Release()
        {
            frameDelta = -1;
            wasPressed = false;
            if(isPressed == true)
            {
                wasPressed = true;
            }
            isPressed = false;
        }

        #endregion
    }

    class CopyOfButton : AnimatedObject
    {
        #region Constructors

        private bool isPressed;

        private bool wasPressed;
        private Circle buttonCircle;
        private int frameDelta;
        private new const float FRAME_TIME = 0.05f;
        #endregion

        #region Properties
        public Circle ButtonCircle
        {
            get { return buttonCircle; }
        }
        public bool WasPressed
        {
            get { return wasPressed; }
        }
        #endregion
        #region Methods

        public override void Update(GameTime gameTime)
        {
            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (accumulator > FRAME_TIME)
            {
                accumulator -= FRAME_TIME;
                {
                    currentFrame += frameDelta;
                    if (currentFrame >= frames - 1)
                    {
                        currentFrame = frames - 1;
                        frameDelta = 0;
                    }
                    else if (currentFrame <= 0)
                    {
                        currentFrame = 0;
                        frameDelta = 0;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            int direciton = (int)currentDirection;
            spriteBatch.Draw(spriteSheet.Texture, Camera2D.ScreenCenter, spriteSheet.SourceRectangle((int)currentDirection * frames + currentFrame),
               color, Camera2D.Rotation, Position, 1,
               SpriteEffects.None, 1);

        }

        public override void Restore(Vector2 position, SpriteSheet spriteSheet, ObjectDataBase objectData)
        {
            base.Restore(position, spriteSheet, objectData);
            buttonCircle = new Circle(position
                + new Vector2(400 + spriteSheet.SourceRectangle(0).Width / 2, 280 + spriteSheet.SourceRectangle(0).Height / 2), 75);
            color = Color.Red;
        }

        public void Press()
        {
            frameDelta = 1;
            wasPressed = false;
            isPressed = true;
        }

        public void Release()
        {
            frameDelta = -1;
            wasPressed = false;
            if (isPressed == true)
            {
                wasPressed = true;
            }
            isPressed = false;
        }

        #endregion
    }
}
