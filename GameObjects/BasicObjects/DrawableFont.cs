using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;

namespace GameObjects.BasicObjects
{
    internal class DrawableFont : Drawable2DComposite, IFontPresentation
    {
        private bool inUse;
        private IFont font;
        private string caption;

        public void SetFont(IFont font)
        {
            this.font = font;
        }

        public void SetCaption(string caption)
        {
            this.caption = caption;
        }

        public string GetCaption()
        {
            return caption;
        }

        public IFont GetFont()
        {
            return font;
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float LayerDepth { get; set; }
        public Rectangle SourceRect { get; set; }
        public Color Color { get; set; }

        public override Rectangle GetBounds()
        {
            return new Rectangle(0,0,0,0);
        }
    }
}
