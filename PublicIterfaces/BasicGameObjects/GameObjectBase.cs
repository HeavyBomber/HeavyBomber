using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PublicIterfaces.GameObjects;

namespace PublicIterfaces
{
    public abstract class GameObjectBase : IGameObject
    {
        private bool inUse;
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
            return inUse;
        }

        public virtual void Init()
        {
            this.inUse = true;
        }

        public void Dispose()
        {
            this.inUse = false;
        }
    }
}
