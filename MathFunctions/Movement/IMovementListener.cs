using Microsoft.Xna.Framework;

namespace MathFunctions.Movement
{
    public interface IMovementListener
    {
        void MoveBy(Vector2 delta);
    }
}
