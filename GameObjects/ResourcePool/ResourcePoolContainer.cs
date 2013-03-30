using PublicIterfaces.BasicGameObjects;

namespace GameObjects.ResourcePool
{
    public class ResourcePoolContainer
    {
        private object resourcePool;

        public ResourcePool<T> GetObjectPool<T>() where T : IGameObject, new()
        {
            if(resourcePool == null)
            {
                resourcePool = new ResourcePool<T>();
            }
            return resourcePool as ResourcePool<T>;
        }
    }
}
