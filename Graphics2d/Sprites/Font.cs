using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicIterfaces;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces.BasicGameObjects;

namespace Graphics2d.Sprites
{
    class Font : IFont
    {
        private SpriteFont font;

        public Font(SpriteFont fontType)
        {
            this.font = fontType;
        }

        public SpriteFont GetFont()
        {
            return font;
        }
    }
}
