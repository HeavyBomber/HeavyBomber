using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;

namespace PublicIterfaces.GameObjectsFactories
{
    public interface IGameObjectsFactory
    {
        void SetContentManager(ContentManager content);
        Drawable2DComposite CreateSprite(string path);
        Drawable2DComposite CreateAnimatedSprite(string path);
        Drawable2DComposite CreateFont(string spriteFontPath, string caption);
    }
}
