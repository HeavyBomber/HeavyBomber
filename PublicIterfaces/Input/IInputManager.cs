using PublicIterfaces.Sound;

namespace PublicIterfaces.Input
{
    public interface IInputManager
    {
        void RegisterClickListener(IClickListener clickListener);
        void Update();
    }
}
