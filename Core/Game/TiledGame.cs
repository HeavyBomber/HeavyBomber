using GameObjects.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjectModules;
using PublicIterfaces;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Input;
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
        private GameScreenBase currentScreen;
        protected GameScreenBase CurrentScreen
        {
            set { currentScreen = value; }
        }

        private IStateManager stateManager;
        protected IStateManager StateManager
        {
            get { return stateManager; }
            set { stateManager = value; }
        }

        private ISpriteDrawer spriteDrawer;
        private ISpriteFontDrawer spriteFontDawer;
        public static bool ExitRequested;

        protected TiledGame()
        {
            loader = new ModuleLoader();
            loader.Load();
            this.stateManager = loader.GetStateManager();
            this.spriteDrawer = loader.GetDrawer();
            this.spriteFontDawer = loader.GetFontDrawer();
            this.inputManager = loader.GetInputManager();
        }

        protected IGameObjectsFactory createGameObjectsFactory()
        {
            return loader.GetGameObjectsFactory();
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
            currentScreen.Visit(spriteDrawer);
            currentScreen.Visit(spriteFontDawer);
            //drawingVisitor.Visit(currentScreen as IS);
            //drawer.Draw(spriteBatch);
            //currentScreen.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected abstract void initStateManager();

        public void stateChanged(GameScreenBase gameScreen)
        {
            currentScreen = gameScreen;
            //drawer.SetDrawableObjectsProvider(currentScreen);
        }
     }
}
