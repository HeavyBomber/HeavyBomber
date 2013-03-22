using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects.Factories;
using Ninject.Modules;
using PublicIterfaces;
using StateManagement;

namespace NinjectModules
{
    class StateManagementModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStateManager>().To(typeof(StateManager)).InSingletonScope();
            //Bind<IDrawableObjectsProvider>().To(typeof (GameScreenBase)).InTransientScope();
            Bind<IGameObjectsFactory>().To(typeof(GameObjectsFactory)).InTransientScope();
        }
    }
}
