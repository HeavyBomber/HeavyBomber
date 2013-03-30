using Input;

namespace MathFunctions.Movement
{
    public interface IMovementFunction : ISwipeListener, ITapListener
    {
        void RegisterMovmentListener(IPlayerMovementReceiver movementListener);
    }
}
