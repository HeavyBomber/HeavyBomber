using Microsoft.Xna.Framework;

namespace Input
{
    public interface ISwipeListener
    {
        void SwipeExecuted(Vector2 delta);
    }
}
