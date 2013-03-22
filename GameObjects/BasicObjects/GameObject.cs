using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PublicIterfaces;

namespace GameObjects.BasicObjects
{
    public class GameObject : GameObjectBase
    {
        //private ISpritePresentation presentation;
        //public ISpritePresentation Presentation
        //{
        //    get { return presentation; }
        //    set { presentation = value; }
        //}

        //public ISpritePresentation GetPresentation()
        //{
        //    return presentation;
        //}

        public override void Update(GameTime gameTime)
        {
            
        }

        public List<IGameObject> GetChildren()
        {
            return children;
        }
    }
}
