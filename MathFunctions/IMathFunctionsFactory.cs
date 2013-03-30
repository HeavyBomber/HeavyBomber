using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathFunctions.Movement;

namespace MathFunctions
{
    public interface IMathFunctionsFactory
    {
        IMovementFunction CreatePlayerMovement(IPlayerMovementReceiver listener);
    }
}
