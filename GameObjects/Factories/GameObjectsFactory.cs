using System;
using System.Collections.Generic;
using GameObjects.BasicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.GameObjects;
using PublicIterfaces.GameObjectsFactories;

namespace GameObjects.Factories
{
    class GameObjectsFactory : GameObjectsFacoryBase, IGameObjectsFactory
    {
        protected ISpritesFactory spritesFactory;
        private IList<ISpritePresentation> drawableObjects = new List<ISpritePresentation>();
        private IList<IFontPresentation> drawableFonts = new List<IFontPresentation>(); 

        public GameObjectsFactory(ISpritesFactory spritesFactory)
        {
            this.spritesFactory = spritesFactory;
        }

        public void SetContentManager(ContentManager content)
        {
            spritesFactory.SetContentManager(content);
        }

        public Drawable2DComposite CreateSpriteObject(string path)
        {
            ISprite texture = spritesFactory.CreateSpriteFromPath(path);
            DrawableSpriteObject drawable = fetchObject<DrawableSpriteObject>();
            drawable.Color = Color.White;
            drawable.SetSprite(texture);
            if(!drawableObjects.Contains(drawable))
            {
                drawableObjects.Add(drawable);
            }
            drawable.Init();
            return drawable;
        }

        public Drawable2DComposite CreateFont(string spriteFontPath, string caption)
        {
            IFont font = spritesFactory.CreateFontFromPath(spriteFontPath);
            DrawableFontObject drawableFont = fetchObject<DrawableFontObject>();
            drawableFont.SetFont(font);
            drawableFont.Color = Color.White;
            drawableFont.SetCaption(caption);
            if (!drawableFonts.Contains(drawableFont))
            {
                drawableFonts.Add(drawableFont);
            }
            drawableFont.Init();

            return drawableFont;
        }

        public IList<ISpritePresentation> GetDrawableObjects()
        {
            return drawableObjects;
        }

        public IList<IFontPresentation> GetDrawableFonts()
        {
            return drawableFonts;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
