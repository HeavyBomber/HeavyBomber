using Microsoft.Xna.Framework.Content;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.Content;

namespace PublicIterfaces.Graphics2d
{
    public interface ISpritesFactory
    {
        void SetContentLoader(IContentLoader content);
        ISprite CreateSpriteFromPath(string path);
        IAnimatedSprite CreateAnimatedSpriteFromPath(string path);
        IFont CreateFontFromPath(string path);
    }
}
