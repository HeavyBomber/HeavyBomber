using GameObjects.Factories;
using Input;
using MathFunctions;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.GameObjectsFactories;
using PublicIterfaces.Graphics2d;
using Microsoft.Xna.Framework;

namespace Prefabricates.Gameplay
{
    public class Scene2D : GameObjectBase
    {
        private Rectangle sceneBounds;
        private ICamera2D camera;
        private Drawable2DContainer player;
        private IGameObjectsFactory gameObjectsFactory;
        private IUserInterfaceFactory interfaceFactory;
        private IInputManager inputManager;

        public Scene2D(IGameObjectsFactory gameObjectsFactory, IUserInterfaceFactory interfaceFactory)
        {
            this.gameObjectsFactory = gameObjectsFactory;
            this.interfaceFactory = interfaceFactory;
        }

        public void Init(Rectangle sceneBounds, ICamera2D camera, Drawable2DContainer player)
        {
            this.sceneBounds = sceneBounds;
            this.camera = camera;
            this.children.Add(camera);
            this.player = player;
           
            this.children.Add(player);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            centerCameraOnPlayer();
            checkIfCameraIsNotOutOfBounds();
        }

        private void checkIfCameraIsNotOutOfBounds()
        {
            //throw new NotImplementedException();
        }

        private void centerCameraOnPlayer()
        {
            var playerPosition = player.GetAbsolutePosition();
            if (camera.ScreenCenter.X < playerPosition.X)
            {
                camera.ScreenCenter += new Vector2(1,0);
            }
            else if (camera.ScreenCenter.X > playerPosition.X)
            {
                camera.ScreenCenter -= new Vector2(1, 0);
            }

            if(camera.ScreenCenter.Y < playerPosition.Y)
            {
                camera.ScreenCenter += new Vector2(0,1);
            }
            else if(camera.ScreenCenter.Y > playerPosition.Y)
            {
                camera.ScreenCenter -= new Vector2(0,1);
            }
        }
    }
}
