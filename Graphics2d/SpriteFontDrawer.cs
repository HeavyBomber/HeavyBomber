using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicIterfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics2d
{
    class SpriteFontDrawer : ISpriteFontDrawer
    {
        private SpriteBatch spriteBatch;
         private ICamera2D camera2d;

        public SpriteFontDrawer(ICamera2D camera2d)
        {
            this.camera2d = camera2d;
        }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Accept(IList<IFontPresentation> drawableObjects)
        {
            Draw(drawableObjects);
        }

        public void Draw(IList<IFontPresentation> drawableFonts)
        {
            foreach (IFontPresentation presentation in drawableFonts)
            {
                if (presentation.IsVisible())
                {
                    //zamiast origin w gameplayu bedzie camera2d.ScreenCenter
                    IFont font = presentation.GetFont();
                    var absolutePosition = presentation.GetAbsolutePosition();
                    var origin = presentation.GetOrigin();
                    spriteBatch.DrawString(font.GetFont(), presentation.GetCaption(), origin,
                                     presentation.GetColor(), presentation.GetRotation(), origin - absolutePosition, camera2d.Zoom,
                                     SpriteEffects.None, presentation.LayerDepth);
                }
            }
        }
    }
}
