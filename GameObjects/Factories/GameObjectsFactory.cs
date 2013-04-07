using System;
using System.Collections.Generic;
using GameObjects.BasicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.Content;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Graphics2d;

namespace GameObjects.Factories
{
    class GameObjectsFactory : GameObjectsFactoryBase, IGameObjectsFactory
    {
        private ISpritesFactory spritesFactory;
        private IVirtualScreen virtualScreen;
      
        public GameObjectsFactory(ISpritesFactory spritesFactory, IVirtualScreen virtualScreen)
        {
            this.spritesFactory = spritesFactory;
            this.virtualScreen = virtualScreen;
        }

        public Drawable2DComposite CreateSprite(string path)
        {
            ISprite texture = spritesFactory.CreateSpriteFromPath(path);
            DrawableSprite drawable = fetchObject<DrawableSprite>();
            drawable.Init();            
            drawable.Color = Color.White;
            drawable.SetSprite(texture);
            virtualScreen.PutOnScreen(drawable);
            return drawable;
        }

        public Drawable2DComposite CreateAnimatedSprite(string path)
        {
            IAnimatedSprite texture = spritesFactory.CreateAnimatedSpriteFromPath(path);
            DrawableAnimatedSprite drawable = fetchObject<DrawableAnimatedSprite>();
            drawable.Init();            
            drawable.Color = Color.White;
            drawable.SetSprite(texture);
            virtualScreen.PutOnScreen(drawable);
            return drawable;
        }

        public Drawable2DComposite CreateFont(string spriteFontPath, string caption)
        {
            IFont font = spritesFactory.CreateFontFromPath(spriteFontPath);
            DrawableFont drawableFont = fetchObject<DrawableFont>();
            drawableFont.Init();            
            drawableFont.SetFont(font);
            drawableFont.Color = Color.White;
            drawableFont.SetCaption(caption);
            virtualScreen.PutOnScreen(drawableFont);

            return drawableFont;
        }

        public void SetContentLoader(IContentLoader contentLoader)
        {
            spritesFactory.SetContentLoader(contentLoader);
        }
    }
}
