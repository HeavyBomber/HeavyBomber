using System;
using System.Collections.Generic;
using GameObjects.Factories;
using HeavyBomber.GameStructs;
using HeavyBomberPrefabricates.MainMenu;
using MathFunctions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Prefabricates.Particles;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.GameObjects;
using PublicIterfaces.GameObjectsFactories;
using UserInterface.UI;

namespace HeavyBomber.GameForms
{
    internal class AnimatedCogsMenu : GameObjectBase
    {
        private const float EASING_FUNCTION_BOUND = 5.12f;

        private readonly Vector2 largeCogPosition = new Vector2(602, 602);
        private readonly Vector2 largeCogOrigin = new Vector2(0, 480);

        private readonly Vector2 smallCogPosition = new Vector2(296, 296);
        private readonly Vector2 smallCogOrigin = new Vector2(720, -2);

        private readonly Vector2 backgroundCogPosition = new Vector2(296, 296);
        private readonly Vector2 backgroundCogOrigin = new Vector2(400, 50);

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
            const string FOG_TEXTURE_PATH = "Sprites/UI/fog";
            var fogTexture = gameObjectsFactory.CreateSpriteObject(FOG_TEXTURE_PATH);
            fogTexture.Init();
            Fog2D fog = new Fog2D();
            fog.Init(800, 480, fogTexture);


            const string SMALL_COG_PATH = "Sprites/UI/Menu/smallCog";

            this.backgroundCog = gameObjectsFactory.CreateSpriteObject(SMALL_COG_PATH);
            backgroundCog.SetRelativePosition(new Vector2(104, -246));
            backgroundCog.SetRootOrigin(new Vector2(400, 50));
            backgroundCog.Init();

            this.smallCog = gameObjectsFactory.CreateSpriteObject(SMALL_COG_PATH);
            smallCog.SetRelativePosition(new Vector2(420, -298));
            smallCog.SetRootOrigin(new Vector2(720, -2));
            smallCog.SetRotation(ALIGN_ROTATION);
            smallCog.Init();

            largeCog.MenuChanged += new EventHandler(largeCog_MenuChanged);

            largeCog.Init();
            base.Init();
        }

        void largeCog_MenuChanged(object sender, EventArgs e)
        {
            startRotatingCogs();
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

        private void selectNewEntries()
        {
            rotationMultiplier = 1;
        }
    }
}
