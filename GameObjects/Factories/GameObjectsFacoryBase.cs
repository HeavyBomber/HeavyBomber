using System;
using System.Collections.Generic;
using GameObjects.ResourcePool;
using PublicIterfaces.BasicGameObjects;

namespace GameObjects.Factories
{
    public abstract class GameObjectsFacoryBase
    {
        protected Dictionary<Type, ResourcePoolContainer> resources = new Dictionary<Type, ResourcePoolContainer>();         

        protected T fetchObject<T>() where T : IGameObject, new()
        {
            T result;
            Type typeToFetch = typeof(T);
            if (resources.ContainsKey(typeToFetch))
            {
                result = resources[typeToFetch].GetObjectPool<T>().UnusedObject;
            }
            else
            {
                var container = new ResourcePoolContainer();
                result = container.GetObjectPool<T>().UnusedObject;
                resources.Add(typeToFetch, container);
            }
            return result;
        }
    }
}
