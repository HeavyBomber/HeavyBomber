using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;

namespace Graphics2d.Sprites
{
    public class Sprite : ISprite
    {
        private Texture2D texture;
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }
    }
}
