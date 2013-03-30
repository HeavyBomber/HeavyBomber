using System;
using GameObjects.Factories;
using HeavyBomber.GameForms;
using Microsoft.Xna.Framework;
using PublicIterfaces.GameObjectsFactories;
using StateManagement;

namespace HeavyBomber.GameScreens
{
    internal class MainMenuScreen : GameScreenBase
    {
        private AnimatedCogsMenu cogsMenu;

        public MainMenuScreen(IGameObjectsFactory gameObjectsFactory, IUserInterfaceFactory interfaceFactory)
        {
            cogsMenu = new AnimatedCogsMenu(gameObjectsFactory, interfaceFactory);
        }

        public override void Dispose()
        {
            cogsMenu.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            cogsMenu.Update(gameTime);
            //if (duringInit)
            //{
            //    //BackgroundTransition.Instance.Update(gameTime);
            //    if (!BackgroundTransition.Instance.DuringFade)
            //    {
            //        duringInit = false;
            //    }
            //    return;
            //}
            //Fog.Instance.Update(gameTime);
            //// BackgroundTransition.Instance.Update(gameTime);
            //foreach (MenuForm menuForm in menuForms)
            //{
            //    menuForm.Update(gameTime);
            //}
        }


        //public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
            //Fog.Instance.Draw(gameTime, spriteBatch);
            //BackgroundTransition.Instance.Draw(gameTime, spriteBatch);
            //spriteBatch.Draw(backgroundTexture, new Vector2(0,0), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerIdentifiers.BACKGROUND_IMAGE);

            //foreach (MenuForm menuForm in menuForms)
            //{
            //    menuForm.Draw(gameTime, spriteBatch);
            //}
        //}

        public override void Init()
        {
            cogsMenu.GameStared += onGameStarted;
            cogsMenu.Init();
        }

        private void onGameStarted(object sender, EventArgs e)
        {
            requestStateChange("gameplay");
        }
    }
}