using System.Collections.Generic;
using PublicIterfaces.GameObjects;

namespace GameObjects.ResourcePool
{
    public class ResourcePool<T> where T : IGameObject, new()
    {
        protected List<T> objects = new List<T>();

        public T UnusedObject
        {
            get
            {
                foreach (T baseObject in objects)
                {
                    if (!baseObject.IsInUse())
                        return baseObject;
                }

                T newObject = new T();
                objects.Add(newObject);
                return newObject;
            }
        }
    }
}
