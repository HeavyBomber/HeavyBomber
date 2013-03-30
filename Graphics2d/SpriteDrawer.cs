using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using System.Collections.Generic;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.Graphics2d;

namespace Graphics2d
{
    class SpriteDrawer : ISpriteDrawer
    {
        private ICamera2D camera2d;
        private SpriteBatch spriteBatch;

        public SpriteDrawer(ICamera2D camera2d)
        {
            this.camera2d = camera2d;
        }

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Accept(IList<ISpritePresentation> drawableObjects)
        {
            foreach (ISpritePresentation presentation in drawableObjects)
            {
                if (presentation.IsVisible())
                {
                    //zamiast origin w gameplayu bedzie camera2d.ScreenCenter
                    ISprite sprite = presentation.GetSprite();
                    spriteBatch.Draw(sprite.GetTexture(), presentation.GetOrigin(), sprite.GetSourceRectangle(),
                                    presentation.GetColor(), presentation.GetRotation(), presentation.GetOrigin() - presentation.GetAbsolutePosition(),
                                    camera2d.Zoom,SpriteEffects.None, presentation.LayerDepth);
                }
            }
        }
    }
}
