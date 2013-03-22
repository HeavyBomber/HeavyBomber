using System;
using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.UserInterface;

namespace GameObjects.Factories
{
    public interface IUserInterfaceFactory : IDisposable
    {
        Drawable2DComposite CreateButton(Drawable2DComposite background, Drawable2DComposite font, float letTextMargin,
                             EventHandler clickHandler);
    }
}
