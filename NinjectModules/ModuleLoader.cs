using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects;
using GameObjects.Factories;
using Graphics2d;
using Input;
using MathFunctions;
using Ninject;
using PublicIterfaces;
using PublicIterfaces.Content;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Graphics2d;
using StateManagement;
using UserInterface;


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
                                        new StateManagementModule(),
                                        new GameObjectsModule(),
                                        new UserInterfaceModule(),
                                        new InputModule(),
                                        new MathFunctionsModule());

        }

        public IStateManager GetStateManager()
        {
            return kernel.Get<IStateManager>();
        }

        public IGameObjectsFactory GetGameObjectsFactory()
        {
            return kernel.Get<IGameObjectsFactory>();
        }

        public IUserInterfaceFactory GetUserInterfaceFactory()
        {
            return kernel.Get<IUserInterfaceFactory>();
        }

        public ISpriteDrawer GetDrawer()
        {
            return kernel.Get<ISpriteDrawer>();
        }

        public IMathFunctionsFactory GetMathFunctionsFactory()
        {
            return kernel.Get<IMathFunctionsFactory>();
        }

        public ISpriteFontDrawer GetFontDrawer()
        {
            return kernel.Get<ISpriteFontDrawer>();
        }

        public IInputManager GetInputManager()
        {
            return kernel.Get<IInputManager>();
        }

        public IVirtualScreen GetVirtualScreen()
        {
            return kernel.Get<IVirtualScreen>();
        }

        public ICamera2D GetCamera()
        {
            return kernel.Get<ICamera2D>();
        }

        public IContentLoader GetContentLoader()
        {
            return kernel.Get<IContentLoader>();
        }
    }
}
