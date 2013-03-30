using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;

namespace PublicIterfaces
{
    public abstract class GameObjectBase : IGameObject
    {
        protected bool isInUse;
        protected List<IGameObject> children = new List<IGameObject>();

        public List<IGameObject> GetChildren()
        {
            return children;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var gameObjectBase in children)
            {
                gameObjectBase.Update(gameTime);
            }
        }

        public virtual bool IsInUse()
        {
            return isInUse;
        }

        public virtual void Init()
        {
            this.isInUse = true;
        }

        public void Dispose()
        {
            this.isInUse = false;
            foreach (var gameObject in children)
            {
                gameObject.Dispose();
            }
        }
    }
}
