using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.BasicGameObjects.Presentation;
using PublicIterfaces.GameObjects;

namespace GameObjects.BasicObjects
{
    class DrawableSpriteObject : Drawable2DComposite, ISpritePresentation
    {
        private bool inUse;
        private ISprite sprite;

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
            SourceRect = new Rectangle(0, 0, 
                                    sprite.GetTexture().Width,
                                    sprite.GetTexture().Height);

        }

        public ISprite GetSprite()
        {
            return sprite;
        }

        public float LayerDepth { get; set; }

        //public override Vector2 GetAbsolutePosition()
        //{
        //    throw new NotImplementedException();
        //}

        public Rectangle SourceRect { get; set; }
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

