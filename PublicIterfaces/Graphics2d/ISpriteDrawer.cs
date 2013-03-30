using System.Collections.Generic;
using PublicIterfaces.BasicGameObjects.Presentation;

namespace PublicIterfaces.Graphics2d
{
    public interface ISpriteDrawer : IDrawer
    {
        void Accept(IList<ISpritePresentation> drawableObjects);
    }
}
