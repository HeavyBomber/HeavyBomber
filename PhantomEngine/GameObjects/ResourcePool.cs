using System.Collections.Generic;

namespace PhantomEngine.GameObjects
{
    public class ResourcePool<T> where T : AnimatedObject, new()
    {
        protected List<T> objects = new List<T>();

        public T UnusedObject
        {
            get
            {
                foreach (T gameObject in objects)
                {
                    if (!gameObject.InUse)
                        return gameObject;
                }

                T newObject = new T();
                objects.Add(newObject);
                return newObject;
            }
        }
    }
}
