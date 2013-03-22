using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicIterfaces
{
    public interface ISpriteFontsProvider
    {
        IList<IFontPresentation> GetFontPresentations();
    }
}
