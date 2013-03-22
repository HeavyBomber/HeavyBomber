using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using Core.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.GameObjects;

namespace BomberPunk.GameObjects
{
    class DynamicText : GameObject
    {
        private const int letterSpacing = 10;
        private const int lineSpacing = 40;

 
        private int xOffset = 0;
        private int yOffset = 0;
        private int letterOffset;
        private float letterScale;
        private string infoText;
        private string comboText;
        private SpriteFont font;
        private int time = 0;
        private int beginTime = 0;
        private int colorPulse;
        private int colorMultiplier;
        private int colorIntensity = 1;
        private delegate Vector2 Transform(int overallTime, int enterTime, int xOffset, int yOffset);
        private Transform transform;
        private bool isEnabled = false;
        private int floatTime;

        public int XOffset
        {
            get { return xOffset; }
            set { xOffset = value; }
        }

        public int YOffset
        {
            get { return yOffset; }
            set { yOffset = value; }
        }

        public DynamicText()
        {
            this.XOffset = 90;
            this.YOffset = 0;
        }

        public void Load()
        {
            const string FONT_PATH = "Fonts/steampunk";
            font = GameResources.Content.Load<SpriteFont>(FONT_PATH);            
        }

        public void trigFloat(string infoText, string comboText)
        {
            transform = new Transform(functFloat);
            this.infoText = infoText;
            this.comboText = comboText;
            this.time = 0;
            beginTime = 0;
            this.letterScale = 1.5f;
            this.isEnabled = true;
            color = Color.FromNonPremultiplied(255, colorPulse, colorPulse, 255); ;
        }
        public void trigFloat(string infoText)
        {
            transform = new Transform(functFloat);
            this.infoText = infoText;
            this.comboText = String.Empty;
            this.time = 0;
            beginTime = 0;
            this.letterScale = 1.3f;
            this.isEnabled = true;
            color = Color.FromNonPremultiplied(255, colorPulse, colorPulse, 255); ;
        }
        private Vector2 functEnter(int time, int xOffset, int yOffset)
        {
            return new Vector2(xOffset, yOffset - 50 + time * 5);
        }
        private Vector2 functSin(int time, int xOffset, int yOffset)
        {
            return new Vector2(xOffset, (float)(Math.Sin(time + xOffset) * 2));
        }

        private Vector2 functFloat(int overallTime, int enterTime, int xOffset, int yOffset)
        {
            return functEnter(enterTime, xOffset, yOffset) + functSin(overallTime, xOffset, yOffset);
        }

        public void Update(GameTime gameTime)
        {
            if (this.isEnabled == true)
            {
                if (letterScale < 0)
                {
                    this.isEnabled = false;
                    return;
                }

                time += (int)(gameTime.ElapsedGameTime.TotalMilliseconds / 18);

                if(colorPulse > 255)
                {
                    colorMultiplier = -1;
                }
                if(colorPulse < colorIntensity)
                {
                    colorMultiplier = 1;
                }
                
                colorPulse += (int)(colorMultiplier * gameTime.ElapsedGameTime.TotalMilliseconds);


                color = Color.FromNonPremultiplied(255, colorPulse, colorPulse, 255);
                if (time < 13)
                {
                    floatTime = time;                    
                }
                if (time > 70)
                {
                    letterScale -= 0.05f;
                }
            }
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Vector2 shadowOfset = new Vector2(3,4);
            if (this.isEnabled == true)
            {
                letterOffset = XOffset;
                foreach (char c in infoText)
                {
                    spriteBatch.DrawString(font, c.ToString(), shadowOfset + transform(time, floatTime, letterOffset, YOffset), Color.Black, 0,
                          new Vector2(0, 0), letterScale, SpriteEffects.None, LayerIdentifiers.COMBO_TEXT);

                    spriteBatch.DrawString(font, c.ToString(), transform(time, floatTime, letterOffset, YOffset), Color.White, 0,
                           new Vector2(0, 0), letterScale, SpriteEffects.None, LayerIdentifiers.COMBO_TEXT);

                    letterOffset += letterSpacing;
                }

                const int COMBO_OFFSET = 60;
                letterOffset = XOffset + COMBO_OFFSET;
                foreach (char c in comboText)
                {
                    spriteBatch.DrawString(font, c.ToString(), shadowOfset + transform(time, floatTime, letterOffset, YOffset + lineSpacing), Color.Black, 0,
                           new Vector2(0, 0), letterScale, SpriteEffects.None, LayerIdentifiers.COMBO_TEXT);

                    spriteBatch.DrawString(font, c.ToString(), transform(time, floatTime, letterOffset, YOffset + lineSpacing), color, 0,
                           new Vector2(0, 0), letterScale, SpriteEffects.None, LayerIdentifiers.COMBO_TEXT);
                    letterOffset += letterSpacing;
                }
            }
            spriteBatch.End();
        }
    }
}
