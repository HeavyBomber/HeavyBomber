using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects.Factories;
using Input;
using MathFunctions;
using MathFunctions.Movement;
using Microsoft.Xna.Framework;
using Prefabricates.Gameplay;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Graphics2d;
using StateManagement;

namespace HeavyBomber.GameScreens
{
    class LevelScreen : GameScreenBase
    {
        private Scene2D scene;
        private IGameObjectsFactory objectsFactory;
        private ICamera2D camera;
        private IMathFunctionsFactory functionsFactory;
        private Player player;

        public LevelScreen(IGameObjectsFactory gameObjectsFactory, IUserInterfaceFactory interfaceFactory,
            IMathFunctionsFactory functionsFactory, ICamera2D camera2D)
        {
            this.camera = camera2D;
            this.objectsFactory = gameObjectsFactory;
            scene = new Scene2D(gameObjectsFactory, interfaceFactory);
            this.functionsFactory = functionsFactory;
        }

        public override void Init()
        {
           // IPlayerMovementFunction movementFunction;

            player = new Player();
            var playerPresentation = objectsFactory.CreateAnimatedSprite("Sprites/Hero/Sprites");
            player.AddChild(playerPresentation);

            IMovementFunction movementFunction = functionsFactory.CreatePlayerMovement(player);
            movementFunction.RegisterMovmentListener(player);

            player.Init();

            scene.Init(new Rectangle(0, 0, 900, 500), camera, player);
        }

        //private Board currentBoard;
        //private int currentLevel = 1;
        //private Hud hud;
        //private Vector2 buttonPosition;
        //private bool duringInit;
        //private const string BUTTON_PATH = "Sprites/UI/Button";
        //private const string BACKGROUND_PATH = "Sprites/UI/HUD/Background";
        //private const string SPRITES_PATH = "/Sprites";

        //public LevelScreen(IServiceProvider serviceProvider)
        //{
        //    // Create a new content manager to load content used just by this level.
        //    content = new ContentManager(serviceProvider, "Content");

        //    hud = new Hud();
        //    buttonPosition = new Vector2(240, 80);
        //    duringInit = true;

        //    CollisionManager.Instance.addCollisionMapping(CollisionIdentifiers.FIRE, CollisionIdentifiers.ENEMY);
        //    //CollisionManager.Instance.addCollisionMapping(CollisionIdentifiers.PLAYER, CollisionIdentifiers.ENEMY);

        //    Load();
        //}

        //public void Load()
        //{

        //    LevelProcessor.Instance.LoadLevel(0, 0);
        //    //tutaj będą zawarte operacje wykonywane podczas wczytywania poziomu
        //    //ale najpierw trzeba jeszcze dodać ekran wczytywania
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    if (duringInit)
        //    {
        //        //BackgroundTransition.Instance.Update(gameTime);
        //        if (!BackgroundTransition.Instance.DuringFade)
        //        {
        //            duringInit = false;
        //        }
        //        return;
        //    }
        //    HandleInput();
        //    // BackgroundTransition.Instance.Update(gameTime);
        //    Board.Instance.Update(gameTime);
        //    hud.Update(gameTime);
        //    //foreach (GameObject gameObject in gameObjects)
        //    //    gameObject.Update(gameTime);

        //}

        //private void HandleInput()
        //{
        //    base.HandleInput();
        //}

        //protected override void OnBackButtonPressed()
        //{
        //    SoundManager.PlaySound("menuclick");

        //    BackgroundTransition.Instance.GoToState("menu");
        //}

        //public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    Board.Instance.Draw(gameTime, spriteBatch);
        //    BackgroundTransition.Instance.Draw(gameTime, spriteBatch);

        //    hud.Draw(gameTime, spriteBatch);

        //    if (duringInit)
        //    {
        //        // BackgroundTransition.Instance.Draw(gameTime, spriteBatch);
        //    }
        //    //foreach (GameObject gameObject in gameObjects)
        //    //    gameObject.Draw(gameTime, spriteBatch);
        //}

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }
    }
}
