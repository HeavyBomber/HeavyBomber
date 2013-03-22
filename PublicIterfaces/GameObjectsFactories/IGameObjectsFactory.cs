using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;

namespace PublicIterfaces.GameObjectsFactories
{
    public interface IGameObjectsFactory : IDisposable
    {
        void SetContentManager(ContentManager content);
        Drawable2DComposite CreateSpriteObject(string path);
        Drawable2DComposite CreateFont(string spriteFontPath, string caption);
        IList<ISpritePresentation> GetDrawableObjects();
        IList<IFontPresentation> GetDrawableFonts();
    }
}
