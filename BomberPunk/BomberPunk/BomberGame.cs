using System;
using System.Collections.Generic;
using System.Linq;
using BomberPunk.Effects;
using Core.Game;
using Core.StateManagement;
using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using BomberPunk;
using BomberPunk.GameScreens;
using BomberPunk.Managers;
using PhantomEngine.GameComponents;
using PhantomEngine.GameScreens;
using PhantomEngine.Input;
using PhantomEngine.Sound;
using Renderer;

namespace BomberPunk
{
    public class BomberGame : TiledGame
    {
        private GameScreenBase currentScreen;
        GraphicsDeviceManager graphics;
        public static bool ExitRequested;

        public static int ScreenWidth
        {
            get { return 800; }
        }
        public static int ScreenHeight
        {
            get { return 480; }
        }

        public BomberGame()
        {
           // AddComponent(new FrameRateCounter());
            AddComponent(ComboManager.Instance);
            AddComponent(StateManager.Instance);

            drawer = new Drawer();
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);


            StateManager.registerStateChange(
                States.MENU_STATE,
                new StateChangeInfo.StateFunction(startMainMenu),
                new StateChangeInfo.StateFunction(endMainMenu));
            StateManager.registerStateChange(
                States.GAMEPLAY_STATE,
                new StateChangeInfo.StateFunction(startGame),
                new StateChangeInfo.StateFunction(endGame));
            StateManager.registerStateChange(
                States.LEVEL_SELECT_STATE,
                new StateChangeInfo.StateFunction(startLevelSelect),
                new StateChangeInfo.StateFunction(endLevelSelect));
            
        }

        protected override void Initialize()
        {
            //Ta Metoda zostanie przenisiona do metody Load() ekranu wyboru levelu:
           // LevelProcessor.Instance.InitializeLevels();

            drawer.Init(new DrawableObjectsProvider(), graphics);
            base.Initialize();

            SoundManager.Initialize();
            StateManager.Instance.Initialize();
            BackgroundTransition.Instance.Init();
            Fog.Instance.Init();
        }


        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #region State Manager's state transtitions
        private void startMainMenu()
        {
            currentScreen = new MainMenuScreen(this.Services);
        }

        private void endMainMenu()
        {
            currentScreen.Dispose();
        }

        private void startGame()
        {
            currentScreen = new LevelScreen(this.Services);
        }

        private void endGame()
        {

        }


        private void startLevelSelect()
        {
            currentScreen = new LevelSelectionScreen(LevelProcessor.ScenarioData.Scenarios[0]);            
        }

        private void endLevelSelect()
        {
            
        }

        #endregion
   
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            if (ExitRequested)
            {
                this.Exit();
            }
            InputManager.Instance.Update();
            BackgroundTransition.Instance.Update(gameTime);
            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            drawer.Draw();
            base.Draw(gameTime);
        }

    }
}
