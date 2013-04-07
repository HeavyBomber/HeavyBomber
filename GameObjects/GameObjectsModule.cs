using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects.Factories;
using Ninject.Modules;
using PublicIterfaces;
using PublicIterfaces.Content;
using PublicIterfaces.GameObjectsFactories;

namespace GameObjects
{
    public class GameObjectsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameObjectsFactory>().To(typeof(GameObjectsFactory)).InTransientScope();
            Bind<IContentLoader>().To(typeof(ContentLoader)).InTransientScope();
        }
    }
}
