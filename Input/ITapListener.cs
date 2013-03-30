using Microsoft.Xna.Framework;

namespace Input
{
    public interface ITapListener
    {
        bool ScreenClicked(Vector2 clickPoint);
    }
}
