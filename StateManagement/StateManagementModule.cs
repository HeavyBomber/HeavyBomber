using PublicIterfaces;
using Ninject.Modules;

namespace StateManagement
{
    public class StateManagementModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStateManager>().To(typeof(StateManager)).InSingletonScope();
        }
    }
}
