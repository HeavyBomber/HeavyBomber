using Microsoft.Xna.Framework;

namespace PublicIterfaces.BasicGameObjects.Presentation
{
    public interface ISpritePresentation : IPresentation
    {
        ISprite GetSprite();
        Rectangle SourceRect { get; }
    }
}
