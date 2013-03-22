using Microsoft.Xna.Framework;

namespace Graphics
{
    public interface ICamera2D
    {
        Vector2 ScreenCenter { get; }
        Vector2 Position { get; }
        float Rotation { set; get; }
        float Zoom { set; get; }
        bool IsChanged { get; }
    }
}