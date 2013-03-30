using System.Collections.Generic;

namespace PublicIterfaces.Graphics2d
{
    public interface ISpriteFontsProvider
    {
        IList<IFontPresentation> GetFontPresentations();
    }
}
