using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;
using PublicIterfaces.BasicGameObjects.Presentation;

namespace UserInterface.UI
{
    public abstract class UIForm : GameObjectBase
    {
        protected float rotation;
        protected Vector2 position;
        protected Vector2 origin;
        protected SpriteFont font;

        public float Rotation
        {
            get { return rotation; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public abstract ISpritePresentation GetPresentation();
        //public abstract bool IsInUse();
        //public abstract void Init();
        //public abstract void Update(GameTime gameTime);
        //public abstract List<GameObjectBase> GetChildren();
    }
}
