using Microsoft.Xna.Framework.Content;
using PublicIterfaces.BasicGameObjects;

namespace PublicIterfaces.Graphics2d
{
    public interface ISpritesFactory
    {
        void SetContentManager(ContentManager content);
        ISprite CreateEmptySprite();
        ISprite CreateSpriteFromPath(string path);
        IAnimatedSprite CreateAnimatedSpriteFromPath(string path);
        IFont CreateFontFromPath(string path);
    }
}
