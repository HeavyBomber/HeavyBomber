using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PublicIterfaces
{
    public interface ISpritesFactory
    {
        void SetContentManager(ContentManager content);
        ISprite CreateEmptySprite();
        ISprite CreateSpriteFromPath(string path);
        IFont CreateFontFromPath(string path);
    }
}
