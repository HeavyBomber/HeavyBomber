using System;
using System.Collections.Generic;
using System.Linq;
using BomberPunk.GameObjects;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using BomberPunk.ObjectData;
using Core.Resources;
using GameEntities.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhantomEngine.Controls;
using PhantomEngine.GameObjects;
using SpriteSheetRuntime;
using XMLContent.Controls;


namespace BomberPunk.HUD
{
    public class Hud : GameObject
    {
        private Texture2D backgroundTexture;
        private IndicatorBase[] gauges;
        private int[] values;
        private SpriteFont gaugeFont;
        Vector2 fontOffset = new Vector2( -15, 10);


        public Hud()
        {
            const string TEXTURE_PATH = "Sprites/UI/HUD/Background/texture";
            const string FONT_PATH = "Fonts/wendy";

            const int GAUGE_INTERVAL = 59;
       
            backgroundTexture = GameResources.Content.Load<Texture2D>(TEXTURE_PATH);
            gaugeFont = GameResources.Content.Load<SpriteFont>(FONT_PATH);

            HudData hudData = GameResources.Content.Load<HudData>("UI/hudData");

            gauges = new IndicatorBase[hudData.Indicators.Length];

           
            Vector2 gaugePosition = new Vector2(1, 0);
            for (int i = 0; i < hudData.Indicators.Length - 1; i++)
            {
                gauges[i] = new Gauge(gaugePosition, hudData.Indicators[i]);

                gaugePosition.Y += GAUGE_INTERVAL;
            }

            gauges[hudData.Indicators.Length - 1] = new SimpleIndicator(gaugePosition,
                                                                        hudData.Indicators[hudData.Indicators.Length - 1
                                                                            ]);
        }

        public void Update(GameTime gameTime)
        {
            if(GameSettings.GaugeValueChanged)
            {
                GameSettings.GaugeValueChanged = false;

                for (int i = 0; i < GameSettings.GAUGE_COUNT; i++)
                {
                    gauges[i].SetValue(GameSettings.GaugeValues[i]);
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, null,
               color, 0, basePosition, 1,
               SpriteEffects.None, LayerIdentifiers.HUD);

            foreach (var gauge in gauges)
            {
                string s = Convert.ToString(gauge.Name);
                gauge.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(gaugeFont, s, gauge.BasePosition, Color.Goldenrod, 0, fontOffset, 0.8f, SpriteEffects.None, LayerIdentifiers.GAUGE_TEXT);
            }

            //foreach (var indicator in indicators)
            //{
            //    string s = Convert.ToString(indicator.GaugeType);

            // indicator.Draw(gameTime, spriteBatch);
            // //spriteBatch.DrawString(gaugeFont, s, gauge.BasePosition, Color.Goldenrod, 0, fontOffset, 0.8f, SpriteEffects.None, LayerIdentifiers.GAUGE_TEXT);

            //}
        }
    }
}
