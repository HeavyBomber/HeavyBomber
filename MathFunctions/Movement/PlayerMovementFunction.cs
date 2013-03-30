using System;
using Input;
using Microsoft.Xna.Framework;
using PublicIterfaces;

namespace MathFunctions.Movement
{
    class PlayerMovementFunction : GameObjectBase, IMovementFunction
    {
        private IPlayerMovementReceiver movementListener;
        private Vector2 movementVector;

        public void SwipeExecuted(Direction direction)
        {
            if(movementListener != null)
            {
                movementListener.MoveRight();
            }
        }

        public bool ScreenClicked(Vector2 clickPoint)
        {
            throw new NotImplementedException();
        }

        public void RegisterMovmentListener(IPlayerMovementReceiver movementListener)
        {
            this.movementListener = movementListener;
        }
    }
}
