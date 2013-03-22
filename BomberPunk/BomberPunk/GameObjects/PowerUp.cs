using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.Camera;
using PhantomEngine.GameObjects;

namespace BomberPunk.GameObjects
{
    class PowerUp : TileObject
    {
        private Texture2D texture;
        private TimeSpan creationTime;
        private int timeAvaiable = 5;
        private int timeBlinking = 2;
        private bool isBlinking;
        private byte alpha = 255;
        private int delta;
        private bool powerUpCreated;


        public PowerUp()
        {
            layerDepth = LayerIdentifiers.POWER_UP;
            delta = -1;
        }
        public override void Initialize(int key, int x, int y)
        {
            this.key = key;
            switch (key)
            {
                case PowerUps.BERSERKER:
                    texture = Board.Instance.PowerUpsFactory.GetTexture(key);
                    break;
                default:
                    break;
            }

            xTile = x;
            yTile = y;

            this.basePosition = new Vector2(xTile * Board.TILE_SIZE, (yTile) * Board.TILE_SIZE);
            powerUpCreated = true;
            this.isBlinking = false;
        }

        public void Update(GameTime gameTime)
        {
            const int ALPHA_DELTA = 40;
            if (powerUpCreated)
            {
                creationTime = gameTime.TotalGameTime;
                powerUpCreated = false;
            }
            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (accumulator > frameTime)
            {
                if (gameTime.TotalGameTime.TotalSeconds - creationTime.TotalSeconds > timeAvaiable)
                {
                    isBlinking = true;
                }
                if (gameTime.TotalGameTime.TotalSeconds - creationTime.TotalSeconds > timeAvaiable + timeBlinking)
                {
                    Board.Instance.ReleasePowerUp(xTile, yTile);
                }
                if (isBlinking)
                {
                    alpha += (byte)delta;
                    if (alpha > 255 - ALPHA_DELTA)
                    {
                        delta = -ALPHA_DELTA;
                    }
                    else if (alpha < ALPHA_DELTA)
                    {
                        delta = ALPHA_DELTA;
                    }

                    color = Color.FromNonPremultiplied(255, 255, 255, alpha);
                }
                accumulator -= frameTime;

            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Camera2D.ScreenCenter, null,
                                 color, Camera2D.Rotation,
                                 Position,
                                 Camera2D.Zoom,
                                 SpriteEffects.None, LayerIdentifiers.POWER_UP);
        }
    }
}
