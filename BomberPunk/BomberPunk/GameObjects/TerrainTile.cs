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
    class TerrainTile : TileObject
    {
        private const int ALPHA_DECREMENT = 15;
        private const int SMOKE_ALPHA_DECREMENT = 5;

        private byte alpha = 255;
        private byte smokeAlpha = 255;
        private Texture2D texture;
        private Texture2D blendedTexture;
        private float baseLayerDepth;
        private float positionLayerDepth;
       
        private bool drawBarrel;
        private bool isDestroying = false;
        private Color blendedColor;



        public override void Initialize(int key, int x, int y)
        {
            switch (key)
            {
                case TerrainIdentifiers.WeakWall:
                    baseLayerDepth = LayerIdentifiers.ENV_BASE;
                    positionLayerDepth = LayerIdentifiers.DELTA;
                    texture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Destroyable);
                    break;
                case TerrainIdentifiers.StrongWall:
                    baseLayerDepth = LayerIdentifiers.ENV_BASE;
                    positionLayerDepth = LayerIdentifiers.DELTA;
                    texture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Undestroyable);
                    break;
                case TerrainIdentifiers.Barrel:
                    baseLayerDepth = LayerIdentifiers.FLOOR;
                    positionLayerDepth = 0;
                    drawBarrel = true;
                    blendedColor = Color.White;
                    texture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Ground);
                    blendedTexture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Blowable);
                    break;
                default:
                    baseLayerDepth = LayerIdentifiers.FLOOR;
                    positionLayerDepth = 0;
                    texture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Ground);
                    break;
            }
            xTile = x;
            yTile = y;

            this.basePosition = new Vector2(xTile * Board.TILE_SIZE, (yTile) * Board.TILE_SIZE);
        }

        public void Destroy()
        {
            if(drawBarrel)
            {
                Board.Instance.Blow(this.basePosition, false);
                drawBarrel = false;
                return;
            }

            baseLayerDepth = LayerIdentifiers.FLOOR;
            positionLayerDepth = 0;

            isDestroying = true;
            blendedTexture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Destroyable);
            texture = Board.Instance.TerrainFactory.GetTexture((int)TerrainBehaviors.Ground);
        }

        public void Update(GameTime gameTime)
        {
            if (isDestroying)
            {
                accumulator += (float) gameTime.ElapsedGameTime.TotalSeconds;
                if (accumulator > frameTime)
                {
                    accumulator -= frameTime;

                    if (smokeAlpha < SMOKE_ALPHA_DECREMENT)
                    {
                        //alpha = 255;
                        isDestroying = false;
                        return;
                    }
                    if (alpha > 0)
                    {
                        alpha -= ALPHA_DECREMENT;
                    }
                    //smokeAlpha -= SMOKE_ALPHA_DECREMENT;
                    //smokeColor = Color.FromNonPremultiplied(smokeAlpha, smokeAlpha, smokeAlpha, smokeAlpha);
                     
                    blendedColor = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);


                }
            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Camera2D.ScreenCenter, null,
                                color, Camera2D.Rotation, Board.Instance.Position - new Vector2(xTile * Board.TILE_SIZE, (yTile) * Board.TILE_SIZE), Camera2D.Zoom,
                                SpriteEffects.None, baseLayerDepth + yTile * positionLayerDepth);


            if (isDestroying || drawBarrel)
            {
                spriteBatch.Draw(blendedTexture, Camera2D.ScreenCenter, null,
                                 blendedColor, Camera2D.Rotation,
                                 Position,
                                 Camera2D.Zoom,
                                 SpriteEffects.None, LayerIdentifiers.ENV_BASE + yTile * LayerIdentifiers.DELTA);

                //spriteBatch.Draw(Board.Instance.EffectsFactory.GetTexture(3), Camera2D.ScreenCenter, null,
                //                 smokeColor, Camera2D.Rotation,
                //                 Board.Instance.Position -
                //                 new Vector2(xTile * Board.TILE_SIZE, (yTile) * Board.TILE_SIZE),
                //                 Camera2D.Zoom,
                //                 SpriteEffects.None, 0.1f + yTile * 0.01f);
            }
        }
        
    }
}
