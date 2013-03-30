using Input;
using MathFunctions.Movement;
using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;

namespace Prefabricates.Gameplay
{
    public class Player : Drawable2DContainer, IPlayerMovementReceiver
    {
        private const float MOVEMENT_DELTA = 2;
        private Vector2 movementVector = Vector2.Zero;

        public void Init(Drawable2DComposite presentation)
        {
            this.AddChild(presentation);
        }

        public override void Update(GameTime gameTime)
        {
            this.relativePosition += movementVector;
            base.Update(gameTime);
        }

        public void SwipeExecuted(Direction direction)
        {
            this.movementVector = new Vector2(-2,0);
        }

        public void MoveBy(Vector2 delta)
        {
            this.relativePosition += delta;
        }

        public void MoveLeft()
        {
            throw new System.NotImplementedException();
        }

        public void MoveRight()
        {
            this.movementVector = new Vector2(-2, 0);
        }

        public void MoveUp()
        {
            throw new System.NotImplementedException();
        }

        public void MoveDown()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}
