
namespace MathFunctions.Movement
{
    public interface IPlayerMovementReceiver
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void Stop();
        void Action();
    }
}
