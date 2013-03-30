using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.Graphics2d;

namespace Graphics2d.Sprites
{
    public class Sprite : ISprite
    {
        private Texture2D texture;
        private Rectangle rectangle;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.rectangle = new Rectangle(0,0,
                texture.Width, texture.Height); 
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public Rectangle GetSourceRectangle()
        {
            return rectangle;
        }

        public Rectangle GetSourceRect()
        {
            return rectangle;
        }
    }
}
