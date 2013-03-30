using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;

namespace PublicIterfaces.Graphics2d
{
    public interface ICamera2D : IGameObject
    {
        Vector2 ScreenCenter { get; set; }
        Vector2 Position { get; }
        float Rotation { set; get; }
        float Zoom { set; get; }
        bool IsChanged { get; }
    }
}