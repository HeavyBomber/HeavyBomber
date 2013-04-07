using Microsoft.Xna.Framework;

namespace PublicIterfaces.BasicGameObjects.Presentation
{
    public interface IPresentation : IGameObject
    {
        bool IsVisible();
        float GetLayerDepth();
        Color GetColor();
        Vector2 GetAbsolutePosition();
        Vector2 GetOrigin();
        float GetRotation();
        bool HasParent();
    }
}
