using GameObjects.Factories;
using Input;
using MathFunctions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjectModules;
using PublicIterfaces;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Graphics2d;
using PublicIterfaces.ObjectPool;
using StateManagement;

namespace Core.Game
{
    public abstract class TiledGame : Microsoft.Xna.Framework.Game, IStateChangeListener
    {
        private ModuleLoader loader;
        protected IObjectsPoolsFactory objectsPoolsFactory;
        protected IInputManager inputManager;
        private SpriteBatch spriteBatch;
        protected GameScreenBase currentScreen;
        protected IStateManager stateManager;
        private ISpriteDrawer spriteDrawer;
        private ISpriteFontDrawer spriteFontDawer;
        private IVirtualScreen virtualScreen;
        public static bool ExitRequested;
        protected ICamera2D camera;

        protected TiledGame()
        {
            loader = new ModuleLoader();
            loader.Load();
            this.stateManager = loader.GetStateManager();
            this.spriteDrawer = loader.GetDrawer();
            this.spriteFontDawer = loader.GetFontDrawer();
            this.inputManager = loader.GetInputManager();
            this.virtualScreen = loader.GetVirtualScreen();
            this.camera = loader.GetCamera();
        }

        protected IGameObjectsFactory createGameObjectsFactory()
        {
            return loader.GetGameObjectsFactory();
        }

        protected IMathFunctionsFactory createMathFunctionsFactory()
        {
            return loader.GetMathFunctionsFactory();
        }

        protected IUserInterfaceFactory createUserInterfaceFactory()
        {
            return loader.GetUserInterfaceFactory();
        }
        
        public void AddComponent(IGameComponent component)
        {
            Components.Add(component);
        }

        protected override void Initialize()
        {
            //drawer.Init(GraphicsDevice);
            base.Initialize();
            spriteDrawer.SetSpriteBatch(spriteBatch);
            spriteFontDawer.SetSpriteBatch(spriteBatch);
        }

        protected override void LoadContent()
        {
            initStateManager();

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
 
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            if (ExitRequested)
            {
                this.Exit();
            }
            inputManager.Update();
            //BackgroundTransition.Instance.Update(gameTime);
            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            virtualScreen.Visit(spriteDrawer);
            virtualScreen.Visit(spriteFontDawer);
            //drawingVisitor.Visit(currentScreen as IS);
            //drawer.Draw(spriteBatch);
            //currentScreen.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected abstract void initStateManager();

        public void StateChanged(GameScreenBase gameScreen)
        {
            currentScreen = gameScreen;
            //drawer.SetDrawableObjectsProvider(currentScreen);
        }
     }
}
