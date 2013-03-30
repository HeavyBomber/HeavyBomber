using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathFunctions.Movement
{
    public interface IPlayerMovementReceiver
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void Stop();
    }
}
