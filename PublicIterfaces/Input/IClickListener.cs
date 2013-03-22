using Microsoft.Xna.Framework;

namespace PublicIterfaces.Input
{
    public interface IClickListener
    {
        bool ScreenClicked(Vector2 clickPoint);
    }
}
