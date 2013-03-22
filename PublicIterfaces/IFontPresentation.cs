using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces.BasicGameObjects.Presentation;


namespace PublicIterfaces
{
    public interface IFontPresentation : IPresentation
    {
        IFont GetFont();
        string GetCaption();
    }
}
