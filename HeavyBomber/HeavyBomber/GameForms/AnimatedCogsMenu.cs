using System;
using GameObjects.Factories;
using HeavyBomberPrefabricates.MainMenu;
using MathFunctions;
using Microsoft.Xna.Framework;
using Prefabricates.Particles;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.GameObjectsFactories;

namespace HeavyBomber.GameForms
{
    internal class AnimatedCogsMenu : GameObjectBase
    {
        public event EventHandler<EventArgs> GameStared;

        private const float EASING_FUNCTION_BOUND = 5.12f;
        private GaussianFunction easingFunction;
        private float easingFunctionArg = -EASING_FUNCTION_BOUND;
        private const float ALIGN_ROTATION = 0.050f;
        private IGameObjectsFactory gameObjectsFactory;
        private LargeCog largeCog;
        private Drawable2DComposite smallCog;
        private Drawable2DComposite backgroundCog;
        private float targetRotation;
        private float rotationMultiplier;
        private float halfRotation = MathHelper.ToRadians(180);
        private float fullRotation = MathHelper.ToRadians(360);

        public AnimatedCogsMenu(IGameObjectsFactory gameObjectsFactory, IUserInterfaceFactory interfaceFactory)
        {
            easingFunction = new GaussianFunction(0, 2f);

            this.gameObjectsFactory = gameObjectsFactory;
            largeCog = new LargeCog(gameObjectsFactory, interfaceFactory);
        }

        public override void Init()
        {
            base.Init();

            const string BACKGROUND_PATH = "Sprites/UI/background";
            var background = gameObjectsFactory.CreateSprite(BACKGROUND_PATH);
            this.children.Add(background);

            const string FOG_TEXTURE_PATH = "Sprites/UI/fog";
            var fogTexture = gameObjectsFactory.CreateSprite(FOG_TEXTURE_PATH);
            Fog2D fog = new Fog2D();
            fog.Init(800, 480, fogTexture);
            this.children.Add(fog);

            const string SMALL_COG_PATH = "Sprites/UI/Menu/smallCog";
            this.backgroundCog = gameObjectsFactory.CreateSprite(SMALL_COG_PATH);          
            backgroundCog.SetRelativePosition(new Vector2(104, -246));
            backgroundCog.SetRootOrigin(new Vector2(400, 50));
            this.children.Add(backgroundCog);

            this.smallCog = gameObjectsFactory.CreateSprite(SMALL_COG_PATH);
            smallCog.SetRelativePosition(new Vector2(415, -298));
            smallCog.SetRootOrigin(new Vector2(720, -2));
            smallCog.SetRotation(ALIGN_ROTATION);
            this.children.Add(smallCog);
            
            largeCog.MenuChanged += largeCog_MenuChanged;
            largeCog.StartClicked += largeCog_StartClicked;
            largeCog.Init();
            largeCog.SetRootOrigin(new Vector2(0, 480));
            this.children.Add(largeCog);
        }

        void largeCog_MenuChanged(object sender, EventArgs e)
        {
            startRotatingCogs();
        }

        void largeCog_StartClicked(object sender, EventArgs e)
        {
            startRotatingCogs();

            if (GameStared != null)
            {
               // GameStared(this, EventArgs.Empty);
            }
        }

        private void startRotatingCogs()
        {
            rotationMultiplier = 1;
            if(targetRotation == 0)
            {
                targetRotation = halfRotation;
            }
            else
            {
                targetRotation = fullRotation;
            }
        }

        public override void Update(GameTime gameTime)
        {
            float rotationIncrement = easingFunction.GetFunctionValue(easingFunctionArg) - 0.03f;
            easingFunctionArg += 0.2165f * rotationMultiplier;
            rotateCogsBy(rotationIncrement * rotationMultiplier);
            var currentRotation = largeCog.GetRotation();

            if (easingFunctionArg >= EASING_FUNCTION_BOUND)
            {
                //largeCog.SetRotation(targetRotation);
                rotationMultiplier = 0;
                easingFunctionArg = -EASING_FUNCTION_BOUND;
                if (targetRotation == MathHelper.ToRadians(360))
                {
                    targetRotation = 0;
                    resetRotations();
                }
            }
            
        }

        private void rotateCogsBy(float rotationIncrement)
        {
            largeCog.Rotate(rotationIncrement);
            smallCog.Rotate(-rotationIncrement * 2.000f);
            backgroundCog.Rotate(rotationIncrement * 2.000f);
        }

        private void resetRotations()
        {
            largeCog.SetRotation(0);
            smallCog.SetRotation(ALIGN_ROTATION);
            backgroundCog.SetRotation(0);
        }
    }
}
