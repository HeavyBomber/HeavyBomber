#region Using Statements
using System;
using System.Collections.Generic;
using BomberPunk.Effects;
using BomberPunk.GameStructs;
using BomberPunk.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using BomberPunk.GameObjects;
using BomberPunk.Managers;
using PhantomEngine.GameScreens;
using PhantomEngine.Managers;
using PhantomEngine.Sound;
using SpriteSheetRuntime;

#endregion

namespace BomberPunk.GameScreens
{
    /// <summary>
    /// Ekran levelu - ta klasa będzie nadzorować właściwy gameplay.
    /// W konstruktorze tej klasy będzie można przekazywać parametry,
    /// takie jak numer levelu itp, w zależności od tych parametrów wygenerowany zostanie konkrenty level.
    /// </summary>
    public class LevelScreen : GameScreenBase
    {
        #region Constants
      
        //public readonly Rectangle bounds = new Rectangle(0, 0, rows * bumperDiam, cols * bumperDiam);

        #endregion

        #region Fields
        private Board currentBoard;
        private int currentLevel = 1;
        private Hud hud;
        private Vector2 buttonPosition;
        private bool duringInit;

        private const string BUTTON_PATH = "Sprites/UI/Button";
        private const string BACKGROUND_PATH = "Sprites/UI/HUD/Background";

        private const string SPRITES_PATH = "/Sprites";

        //lista wszystkich obiektów, które będą wyświetlane na ekranie
        //private List<GameObject> gameObjects = new List<GameObject>();


        #endregion

        #region Constructors
        public LevelScreen(IServiceProvider serviceProvider)
        {
            // Create a new content manager to load content used just by this level.
            content = new ContentManager(serviceProvider, "Content");

            hud = new Hud();
            buttonPosition = new Vector2(240, 80);
            duringInit = true;
           
            CollisionManager.Instance.addCollisionMapping(CollisionIdentifiers.FIRE, CollisionIdentifiers.ENEMY);
            //CollisionManager.Instance.addCollisionMapping(CollisionIdentifiers.PLAYER, CollisionIdentifiers.ENEMY);

            Load();
        }
        #endregion

        #region Methods

        public void Load()
        {

            LevelProcessor.Instance.LoadLevel(0,0);
            //tutaj będą zawarte operacje wykonywane podczas wczytywania poziomu
            //ale najpierw trzeba jeszcze dodać ekran wczytywania
        }

        public override void Update(GameTime gameTime)
        {
            if(duringInit)
            {
                //BackgroundTransition.Instance.Update(gameTime);
                if(!BackgroundTransition.Instance.DuringFade)
                {
                    duringInit = false;
                }
                return;
            }
            HandleInput();
           // BackgroundTransition.Instance.Update(gameTime);
            Board.Instance.Update(gameTime);
            hud.Update(gameTime);
            //foreach (GameObject gameObject in gameObjects)
            //    gameObject.Update(gameTime);

        }

        private void HandleInput()
        {
            base.HandleInput();
        }

        protected override void OnBackButtonPressed()
        {
            SoundManager.PlaySound("menuclick");

            BackgroundTransition.Instance.GoToState("menu");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Board.Instance.Draw(gameTime, spriteBatch);
            BackgroundTransition.Instance.Draw(gameTime, spriteBatch);

            hud.Draw(gameTime, spriteBatch);

            if(duringInit)
            {
               // BackgroundTransition.Instance.Draw(gameTime, spriteBatch);
            }
            //foreach (GameObject gameObject in gameObjects)
            //    gameObject.Draw(gameTime, spriteBatch);
        }

        public override void Dispose()
        {
            Content.Unload();
        }
        #endregion
    }
}
