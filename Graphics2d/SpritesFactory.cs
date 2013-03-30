using System;
using Graphics2d.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.Graphics2d;
using SpriteSheetRuntime;

namespace Graphics2d
{
    public class SpritesFactory : ISpritesFactory
    {

        private ContentManager content;
        //public ISprite createSprite(string path, float layerDepth)
        //{
            
        //}
        public void SetContentManager(ContentManager contentManager)
        {
            this.content = contentManager;
        }

        public ISprite CreateEmptySprite()
        {
            throw new NotImplementedException();
        }

        public ISprite CreateSpriteFromPath(string path)
        {
            Texture2D texture = content.Load<Texture2D>(path);
            ISprite sprite = new Sprite(texture);
            return sprite;
        }

        public IAnimatedSprite CreateAnimatedSpriteFromPath(string path)
        {
            SpriteSheet spriteSheet = content.Load<SpriteSheet>(path);
            IAnimatedSprite animated = new AnimatedSprite(spriteSheet);
            return animated;
        }

        public IFont CreateFontFromPath(string path)
        {
            SpriteFont spriteFont = content.Load<SpriteFont>(path);
            IFont font = new Font(spriteFont);
            return font;
        }
    }
}
