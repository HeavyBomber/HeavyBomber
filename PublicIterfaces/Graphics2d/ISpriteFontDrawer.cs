using System.Collections.Generic;

namespace PublicIterfaces.Graphics2d
{
    public interface ISpriteFontDrawer : IDrawer
    {
        void Accept(IList<IFontPresentation> drawableObjects);
    }
}
