using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using PublicIterfaces.BasicGameObjects.Presentation;

namespace PublicIterfaces
{
    public interface ISpriteDrawer : IDrawer
    {
        void Accept(IList<ISpritePresentation> drawableObjects);
    }
}
