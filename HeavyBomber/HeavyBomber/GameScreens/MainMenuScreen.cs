using System;
using System.Collections.Generic;
using BomberPunk.GameStructs;
using GameObjects.Factories;
using HeavyBomber.GameForms;
using HeavyBomber.GameStructs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.GameObjectsFactories;
using StateManagement;
using UserInterface.UI;

namespace HeavyBomber.GameScreens
{
    internal class MainMenuScreen : GameScreenBase
    {
        //private List<MenuForm> menuForms;
        private bool duringInit;
        private Texture2D backgroundTexture;
        private IGameObjectsFactory objectsFactory;

        private AnimatedCogsMenu cogsMenu;

        public MainMenuScreen(IGameObjectsFactory gameObjectsFactory, IUserInterfaceFactory interfaceFactory)
        {
            this.objectsFactory = gameObjectsFactory;

            cogsMenu = new AnimatedCogsMenu(objectsFactory, interfaceFactory);
            //this.ObjectsPool = objectsFactory.getObjectsPool();
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

        public override void Visit(ISpriteDrawer drawer)
        {
            drawer.Accept(objectsFactory.GetDrawableObjects());
        }

        public override void Visit(ISpriteFontDrawer drawer)
        {
            drawer.Accept(objectsFactory.GetDrawableFonts());
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
            duringInit = true;
            const string BACKGROUND_PATH = "Sprites/UI/background";
            
            var background = objectsFactory.CreateSpriteObject(BACKGROUND_PATH);
            //background.SetRootOrigin(new Vector2(400,240));
            //background.setRotation(0.1f);
            //background.SetRelativePosition(new Vector2(400,240));
            cogsMenu.Init();
            //menuForms = new List<MenuForm>();
            //menuForms.Add(new AnimatedCogsMenu(content));
        }

        public override void Dispose()
        {
            objectsFactory.Dispose();
        }
    }
}