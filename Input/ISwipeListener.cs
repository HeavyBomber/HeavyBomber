using Microsoft.Xna.Framework;

namespace Input
{
    public interface ISwipeListener
    {
        void SwipeExecuted(Direction direction);
    }
}
