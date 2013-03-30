using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PublicIterfaces.Graphics2d
{
    public interface ISprite
    {
        Texture2D GetTexture();
        Rectangle GetSourceRectangle();
    }
}
