using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicIterfaces
{
    public interface ISpriteDrawingVisitor
    {
        void Visit(ISpriteDrawer drawer);
        void Visit(ISpriteFontDrawer drawer);
    }
}
