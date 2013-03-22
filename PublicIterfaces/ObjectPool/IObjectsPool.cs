using System.Collections.Generic;

namespace PublicIterfaces.ObjectPool
{
    public interface IObjectsPool// : ISpritesProvider
    {
        List<GameObjectBase> getActiveObjects();
    }
}
