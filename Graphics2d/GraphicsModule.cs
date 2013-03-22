using Ninject.Modules;
using PublicIterfaces;

namespace Graphics2d
{
    public class GraphicsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISpriteDrawer>().To(typeof(SpriteDrawer)).InSingletonScope();
            Bind<ISpriteFontDrawer>().To(typeof(SpriteFontDrawer)).InSingletonScope();
            Bind<ICamera2D>().To(typeof(Camera2D)).InSingletonScope();
            Bind<ISpritesFactory>().To(typeof(SpritesFactory)).InSingletonScope();
        }
    }
}
