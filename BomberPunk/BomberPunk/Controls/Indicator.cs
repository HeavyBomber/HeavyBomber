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
using PhantomEngine.Resources;
using XMLContent.Controls;


namespace BomberPunk.HUD
{
    class Indicator : IndicatorBase
    {

        public Indicator (Vector2 position, IndicatorData data)
        {
            backgroundTexture = Resources.Content.Load<Texture2D>("Sprites/UI/HUD/Gauge/gauge2");
            this.basePosition = position;
            isBinary = data.IsBinary;
        }

       

        public override void SetValue(int value)
        {
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, basePosition, null,
             color, 0, Vector2.Zero, 1,
             SpriteEffects.None, LayerIdentifiers.GAUGE_TEXTURE);

            //spriteBatch.Draw(backgroundTexture, basePosition, null,
            // color, 0, Vector2.Zero, 1,
            // SpriteEffects.None, LayerIdentifiers.GAUGE_TEXTURE);
        }
    }
}
