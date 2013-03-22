using Microsoft.Xna.Framework;
using PublicIterfaces.GameObjects;

namespace PublicIterfaces.BasicGameObjects.Presentation
{
    public interface IPresentation : IGameObject
    {
        bool IsVisible();
        float LayerDepth { get; set; }
        Color GetColor();
        Vector2 GetAbsolutePosition();
        Vector2 GetOrigin();
        float GetRotation();
    }
}
