namespace Input
{
    public interface IInputManager
    {
        void RegisterClickListener(ITapListener clickListener);
        void RegisterMovementListener(ISwipeListener movementListener);
        void Update();
    }
}
