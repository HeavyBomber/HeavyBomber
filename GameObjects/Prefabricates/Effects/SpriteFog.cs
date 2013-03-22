using System.Collections.Generic;
using Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;

namespace Graphics.Effects
{
    class SpriteFog : IDrawableObject
    {
        private Texture2D fogTexture;
        private Vector2[] fogPositions;
        private int rows = 3;
        private int cols = 5;
        private Color color = Color.FromNonPremultiplied(255, 255, 255, 100);

        public void Init()
        {
            const string FOG_TEXTURE_PATH = "Sprites/UI/fog";
            fogTexture = GameResources.Content.Load<Texture2D>(FOG_TEXTURE_PATH);
            fogPositions = new Vector2[rows * cols];

            int i = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    fogPositions[i] = new Vector2(x * fogTexture.Width, y * fogTexture.Height);
                    i++;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < fogPositions.Length; i++)
            {
                fogPositions[i].Y -= 1;

                if (fogPositions[i].X < -fogTexture.Width)
                {
                    fogPositions[i].X = BomberGame.ScreenWidth - 1;
                }

                if (fogPositions[i].Y < -fogTexture.Height)
                {
                    fogPositions[i].Y = BomberGame.ScreenHeight - 1;
                }

                if (fogPositions[i].X > BomberGame.ScreenWidth)
                {
                    fogPositions[i].X = 0;
                }

                if (fogPositions[i].Y > BomberGame.ScreenHeight)
                {
                    fogPositions[i].Y = 0;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < fogPositions.Length; i++)
            {
                spriteBatch.Draw(fogTexture, fogPositions[i], null, color, 0, Vector2.Zero, 1, SpriteEffects.None, LayerIdentifiers.FOG);
            }
        }

        public IEnumerable<ISprite> getRepresentation()
        {
            throw new System.NotImplementedException();
        }
    }
}
