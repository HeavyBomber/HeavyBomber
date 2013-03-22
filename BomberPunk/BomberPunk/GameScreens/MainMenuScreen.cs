using System;
using System.Collections.Generic;
using BomberPunk.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BomberPunk.GameForms;
using PhantomEngine.GameScreens;
using PhantomEngine.UI;

namespace BomberPunk.GameScreens
{

    internal class MainMenuScreen : GameScreenBase
    {
        private readonly List<MenuForm> menuForms;
        private bool duringInit;
        public MainMenuScreen(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
            duringInit = true;

            menuForms = new List<MenuForm>();
            menuForms.Add(new AnimatedCogsMenu());
        }

        public override void Update(GameTime gameTime)
        {
            if (duringInit)
            {
                //BackgroundTransition.Instance.Update(gameTime);
                if (!BackgroundTransition.Instance.DuringFade)
                {
                    duringInit = false;
                }
                return;
            }
            Fog.Instance.Update(gameTime);
           // BackgroundTransition.Instance.Update(gameTime);
            foreach (MenuForm menuForm in menuForms)
            {
                menuForm.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Fog.Instance.Draw(gameTime, spriteBatch);
            BackgroundTransition.Instance.Draw(gameTime, spriteBatch);
            foreach (MenuForm menuForm in menuForms)
            {
                menuForm.Draw(gameTime, spriteBatch);
            }
        }

        public override void Dispose()
        {
            content.Unload();
        }
    }
}