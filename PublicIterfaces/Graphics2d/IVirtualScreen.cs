using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicIterfaces.BasicGameObjects.Presentation;

namespace PublicIterfaces.Graphics2d
{
    public interface IVirtualScreen : ISpriteDrawingVisitor
    {
        void PutOnScreen(ISpritePresentation presentation);
        void PutOnScreen(IFontPresentation presentation);
    }
}
