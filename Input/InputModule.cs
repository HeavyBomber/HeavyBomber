using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicIterfaces;
using Ninject.Modules;

namespace Input
{
    public class InputModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInputManager>().To(typeof(InputManager)).InSingletonScope();
        }
    }
}
