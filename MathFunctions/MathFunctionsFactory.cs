﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects.Factories;
using Input;
using MathFunctions.Movement;

namespace MathFunctions
{
    class MathFunctionsFactory : GameObjectsFacoryBase, IMathFunctionsFactory
    {
        private IInputManager inputManager;
        public MathFunctionsFactory(IInputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public IMovementFunction CreatePlayerMovement(IPlayerMovementReceiver listener)
        {
            IMovementFunction function = new PlayerMovementFunction();
            inputManager.RegisterMovementListener(function);
            return function;
        }
    }
}
