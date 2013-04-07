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

        public void SwipeExecuted(Vector2 delta)
        {
            if(movementListener != null)
            {
                SelectProperDirection(delta);
            }
        }

        private void SelectProperDirection(Vector2 delta)
        {
            Direction currentDirection = Direction.None;
            if (delta.X != 0 || delta.Y != 0)
            {
                delta.Normalize();

                if (delta.X > 0)
                {
                    movementListener.MoveRight();
                }
                else if (delta.X < 0)
                {
                    movementListener.MoveLeft();
                }
                else if (delta.Y > 0)
                {
                    movementListener.MoveDown();

                }
                else if (delta.Y < 0)
                {
                    movementListener.MoveUp();
                }
            }
           
        }

        public bool ScreenClicked(Vector2 clickPoint)
        {
           if(clickPoint.X < 400)
           {
                movementListener.Stop();
           }
           else
           {
               movementListener.Action();
           }

            return true;
        }

        public void RegisterMovmentListener(IPlayerMovementReceiver movementListener)
        {
            this.movementListener = movementListener;
        }
    }
}
