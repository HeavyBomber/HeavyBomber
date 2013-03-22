using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Resources;
using PhantomEngine.GameScreens;
using Microsoft.Xna.Framework;
using PhantomEngine.UI;
using Microsoft.Xna.Framework.Graphics;
using XMLContent;

namespace BomberPunk.GameScreens
{
    public class LevelSelectionScreen : GameScreenBase
    {
        private WrapPanel wrapPanel;
        private Texture2D background;

        public LevelSelectionScreen(Scenario scenario)
        {
            background = GameResources.Content.Load<Texture2D>("Sprites/UI/background");

            Texture2D[] buttonTextures = {GameResources.Content.Load<Texture2D>("Sprites/UI/Menu/shortLocked"),
                                 GameResources.Content.Load<Texture2D>("Sprites/UI/Menu/shortUnlocked") };
            wrapPanel = new WrapPanel()
                            {
                                ButtonTexture =
                                    GameResources.Content.Load<Texture2D>("Sprites/UI/Menu/shortButtonBackground"),
                                Font = GameResources.Content.Load<SpriteFont>("Fonts/forque"),
                                Origin = Vector2.Zero,
                                Position = Vector2.Zero
                            };

            wrapPanel.Init(1, 10, buttonTextures, 100, 100);
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
        public override void Update(GameTime gameTime)
        {
            wrapPanel.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White );
            wrapPanel.Draw(gameTime, spriteBatch);
        }

    }
}
