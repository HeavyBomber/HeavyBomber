using System;
using Core.Game;
using HeavyBomber.GameScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StateManagement;

namespace HeavyBomber
{
    public class HeavyBomberGame : TiledGame
    {
        GraphicsDeviceManager graphics;

        public HeavyBomberGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        //protected override void LoadContent()
        //{
        //    // Create a new SpriteBatch, which can be used to draw textures.
        //    spriteBatch = new SpriteBatch(GraphicsDevice);

        //    // TODO: use this.Content to load your game content here
        //}

        //protected override void UnloadContent()
        //{
        //    // TODO: Unload any non ContentManager content here
        //}

       
        //protected override void Update(GameTime gameTime)
        //{
        //    // Allows the game to exit
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        //        this.Exit();

        //    // TODO: Add your update logic here

        //    base.Update(gameTime);
        //}

      
        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(Color.CornflowerBlue);

        //    // TODO: Add your drawing code here

        //    base.Draw(gameTime);
        //}

        protected override void initStateManager()
        {
            var factory = createGameObjectsFactory();
            factory.SetContentManager(Content);

            var userInterfaceFactory = createUserInterfaceFactory();
            
            var menuScreen = new MainMenuScreen(factory, userInterfaceFactory);
            StateManager.registerStateChange(States.MENU_STATE, menuScreen);
            menuScreen.Init();

            StateManager.registerStateChangeListener(this);
        }
    }
}
