using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.Graphics2d;

namespace GameObjects.BasicObjects
{
    class DrawableSprite : Drawable2DComposite, ISpritePresentation
    {
        private bool inUse;
        private ISprite sprite;

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public ISprite GetSprite()
        {
            return sprite;
        }

        public Color Color { get; set; }

        public override Rectangle GetBounds()
        {
            var texture = sprite.GetTexture();

            Rectangle bounds;
            float angle = MathHelper.WrapAngle(rotation);
            if (Math.Abs(angle) < 0.01)
            {
                bounds = new Rectangle((int) (GetAbsolutePosition().X + 0.5),
                                       (int) (GetAbsolutePosition().Y + 0.5),
                                       texture.Width,
                                       texture.Height);
            }
            else
            {
                bounds = new Rectangle();
            }

            return bounds;
        }
    }
}

