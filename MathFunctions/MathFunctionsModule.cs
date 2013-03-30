using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathFunctions.Movement;
using Ninject.Modules;

namespace MathFunctions
{
    public class MathFunctionsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMathFunctionsFactory>().To(typeof(MathFunctionsFactory)).InSingletonScope();
        }
    }
}
