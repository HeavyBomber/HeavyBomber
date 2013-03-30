using System;
using System.Collections.Generic;
using GameObjects.BasicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Graphics2d;

namespace GameObjects.Factories
{
    class GameObjectsFactory : GameObjectsFacoryBase, IGameObjectsFactory
    {
        private ISpritesFactory spritesFactory;
        private IVirtualScreen virtualScreen;
      
        public GameObjectsFactory(ISpritesFactory spritesFactory, IVirtualScreen virtualScreen)
        {
            this.spritesFactory = spritesFactory;
            this.virtualScreen = virtualScreen;
        }

        public void SetContentManager(ContentManager content)
        {
            spritesFactory.SetContentManager(content);
        }

        public Drawable2DComposite CreateSprite(string path)
        {
            ISprite texture = spritesFactory.CreateSpriteFromPath(path);
            DrawableSprite drawable = fetchObject<DrawableSprite>();
            drawable.Color = Color.White;
            drawable.SetSprite(texture);
            virtualScreen.PutOnScreen(drawable);
            drawable.Init();
            return drawable;
        }

        public Drawable2DComposite CreateAnimatedSprite(string path)
        {
            IAnimatedSprite texture = spritesFactory.CreateAnimatedSpriteFromPath(path);
            DrawableAnimatedSprite drawable = fetchObject<DrawableAnimatedSprite>();
            drawable.Color = Color.White;
            drawable.SetSprite(texture);
            virtualScreen.PutOnScreen(drawable);
            drawable.Init();
            return drawable;
        }

        public Drawable2DComposite CreateFont(string spriteFontPath, string caption)
        {
            IFont font = spritesFactory.CreateFontFromPath(spriteFontPath);
            DrawableFont drawableFont = fetchObject<DrawableFont>();
            drawableFont.SetFont(font);
            drawableFont.Color = Color.White;
            drawableFont.SetCaption(caption);
            virtualScreen.PutOnScreen(drawableFont);
            drawableFont.Init();

            return drawableFont;
        }
    }
}
