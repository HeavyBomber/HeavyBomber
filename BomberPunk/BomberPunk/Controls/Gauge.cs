using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.Controls;
using BomberPunk.GameObjects;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using BomberPunk.ObjectData;
using Core.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.Controls;
using PhantomEngine.Enums;
using SpriteSheetRuntime;
using XMLContent.Controls;

namespace BomberPunk.HUD
{
    class Gauge : IndicatorBase
    {

        private const int DIGITS_COUNT = 6;
        private Vector2 initialPosition = new Vector2(10, 17);
        private AnimatedDigit[] digits;
        // private bool isBinary;
        //private GaugeData.Gauges gaugeType;


        //private Texture2D backgroundTexture;

        public Gauge(Vector2 position, IndicatorData data)
        {

            name = data.Name;
            key = data.Key;
            digits = new AnimatedDigit[data.DigitCount];

            this.isBinary = data.IsBinary;
            backgroundTexture = GameResources.Content.Load<Texture2D>(data.BackgroundTexturePath);
            var digitSprites = GameResources.Content.Load<SpriteSheet>("Sprites/UI/HUD/Digit/Sprites");
            this.basePosition = position;
            for (int i = 0; i < digits.Length; i++)
            {

                digits[i] = new AnimatedDigit();
                digits[i].Restore(initialPosition + basePosition, digitSprites, null);
                digits[i].CyphersTexture = GameResources.Content.Load<Texture2D>(data.AlphabetTexturePath);
                initialPosition.X += digitSprites.SourceRectangle(0).Width;
            }
        }

        public override void SetValue(float value)
        {
            var stringValue = Convert.ToString(value);

            if (isBinary)
            {
                if (value > DIGITS_COUNT)
                {
                    return;
                }
                for (int i = 0; i < DIGITS_COUNT; i++)
                {
                    if (i < value)
                    {
                        digits[i].SetValue(1);
                    }
                    else
                    {
                        digits[i].SetValue(0);
                    }
                }
            }
            else
            {

                if (stringValue.Length > DIGITS_COUNT)
                {
                    return;
                }


                for (int i = 0; i < stringValue.Length; i++)
                {
                    digits[DIGITS_COUNT - stringValue.Length + i].SetValue((int)Char.GetNumericValue(stringValue[i]));
                }
            }
        }

        public override void SetValue(string value)
        {

            if (value.Length > DIGITS_COUNT)
            {
                return;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '-')
                {
                    digits[DIGITS_COUNT - value.Length + i].SetValue(10);
                }
                digits[DIGITS_COUNT - value.Length + i].SetValue((int)Char.GetNumericValue(value[i]));
            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, basePosition, null,
              color, 0, Vector2.Zero, 1,
              SpriteEffects.None, LayerIdentifiers.GAUGE_TEXTURE);
        }
    }
}
