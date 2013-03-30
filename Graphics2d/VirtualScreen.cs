using System;
using System.Collections.Generic;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.Graphics2d;

namespace Graphics2d
{
    class VirtualScreen : IVirtualScreen
    {
        private IList<ISpritePresentation> drawableSprites = new List<ISpritePresentation>();
        private IList<IFontPresentation> drawableFonts = new List<IFontPresentation>();

        public void PutOnScreen(ISpritePresentation presentation)
        {
            if (!drawableSprites.Contains(presentation))
            {
                drawableSprites.Add(presentation);
            }
        }

        public void PutOnScreen(IFontPresentation presentation)
        {
            if (!drawableFonts.Contains(presentation))
            {
                drawableFonts.Add(presentation);
            }
        }

        public IList<ISpritePresentation> GetDrawableObjects()
        {
            return drawableSprites;
        }

        public IList<IFontPresentation> GetDrawableFonts()
        {
            return drawableFonts;
        }

        public void Visit(ISpriteDrawer drawer)
        {
            drawer.Accept(drawableSprites);
        }

        public void Visit(ISpriteFontDrawer drawer)
        {
            drawer.Accept(drawableFonts);
        }
    }
}
