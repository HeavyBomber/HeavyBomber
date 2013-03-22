using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects.Factories;
using PublicIterfaces.GameObjectsFactories;
using Ninject.Modules;

namespace UserInterface
{
    public class UserInterfaceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserInterfaceFactory>().To(typeof(UserInterfaceFactory)).InTransientScope();
        }
    }
}
