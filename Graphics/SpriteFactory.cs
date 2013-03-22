using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;

namespace Graphics
{
    class SpriteFactory : ISpritesFactory
    {
        private ContentManager content;
        //public ISprite createSprite(string path, float layerDepth)
        //{
            
        //}
        public void setContentManager(ContentManager contentManager)
        {
            this.content = contentManager;
        }

        public ISprite createEmptySprite()
        {
            throw new NotImplementedException();
        }

        public ISprite createSpriteFromPath(string path)
        {
            Texture2D texture = content.Load<Texture2D>(path);

            ISprite sprite = new Sprite(texture,
                                        new Rectangle(0,0, texture.Width, texture.Height),
                                        Vector2.Zero,
                                        0);

            return sprite;
        }
    }
}
