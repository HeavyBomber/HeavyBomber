using Microsoft.Xna.Framework;

namespace PublicIterfaces.GameObjects
{
    public interface IGameObject
    {
        void Update(GameTime gameTime);
        bool IsInUse();
        void Init();
        void Dispose();
    }
}
