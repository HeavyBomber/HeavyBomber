using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics2d;
using Ninject;
using PublicIterfaces;
using StateManagement;


namespace NinjectModules
{
    public class ModuleLoader
    {
        private static IKernel kernel;
        public IKernel Kernel
        {
            get { return kernel; }
        }

        public void Load()
        {
            kernel = new StandardKernel(new GraphicsModule(),
                                        new StateManagementModule());

        }

        public IStateManager getStateManager()
        {
            return new StateManager();
            //return kernel.Get<IStateManager>();
        }

        public ISpriteDrawer getDrawer()
        {
            return kernel.Get<ISpriteDrawer>();
        }

        public IObjectsPoolsFactory getObjectsPoolFactory()
        {
            throw new NotImplementedException();
        }

        public IGameObjectsFactory getGameObjectsFactory()
        {
            return kernel.Get<IGameObjectsFactory>();
        }

        public ISpriteFontDrawer GetFontDrawer()
        {
            return kernel.Get<ISpriteFontDrawer>();
        }
    }
}
