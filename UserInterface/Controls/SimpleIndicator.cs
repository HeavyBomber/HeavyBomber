using GameEntities.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UserInterface.UI;
using XMLContent.Controls;

namespace UserInterface.Controls
{
    public class SimpleIndicator : IndicatorBase
    {
        private Vector2 initialPosition = new Vector2(0, 0);

        public SimpleIndicator(Vector2 position, IndicatorData data)
        {
            name = data.Name;
            key = data.Key;
            digits = new Digit[data.DigitCount];

            backgroundTexture = GameResources.Content.Load<Texture2D>(data.BackgroundTexturePath);
            var alphabet = GameResources.Content.Load<Texture2D>(data.AlphabetTexturePath);
            this.basePosition = position;
            for (int i = 0; i < digits.Length; i++)
            {
                digits[i] = new Digit(initialPosition + basePosition, alphabet);

                initialPosition.X += alphabet.Width;
            }
        }


        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, basePosition, null,
             color, 0, Vector2.Zero, 1,
             SpriteEffects.None, LayerDepths.GAUGE_TEXTURE);

            foreach (Digit digit in digits)
            {
                digit.Draw(gameTime, spriteBatch);
            }
        }
    }
}
