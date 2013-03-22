using System;
using GameEntities.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UserInterface.UI;

namespace UserInterface.Controls
{
    class Digit : GameObject, DigitBase
    {
        private bool isRunning;
        protected bool isBinary;
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

        public Digit(Vector2 position, Texture2D alphabet)
        {
            cypherWidth = 124;
            cypherHeight = 124;

            basePosition = position;
            cyphersTexture = alphabet;
            cypherRect = new Rectangle(0, 0, cypherWidth, cypherHeight);
        }

        public void SetValue(int value)
        {
            if (value < 0 || value > 10)
                throw new Exception("Given number exceeded the range.");

            this.isRunning = true;
            desiredDigit = value;

            currentOffset = value * cypherHeight;

            cypherRect.Y = currentOffset;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CyphersTexture, basePosition, cypherRect,
              color, 0, Vector2.Zero, 1,
              SpriteEffects.None, LayerDepths.DIGIT_TEXT);
        }
    }
}
