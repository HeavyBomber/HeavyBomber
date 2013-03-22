
namespace PhantomEngine.GameObjects
{
    public abstract class TileObject : GameObject
    {
        protected int xTile;
        protected int yTile;
        protected float frameTime = 0.01f;
        protected float accumulator;
        protected int key;

        public abstract void Initialize(int key, int xTile, int yTile);

    }
}
