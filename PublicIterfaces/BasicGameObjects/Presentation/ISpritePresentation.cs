using PublicIterfaces.Graphics2d;

namespace PublicIterfaces.BasicGameObjects.Presentation
{
    public interface ISpritePresentation : IPresentation
    {
        ISprite GetSprite();
    }
}
