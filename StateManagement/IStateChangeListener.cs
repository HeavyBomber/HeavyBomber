namespace StateManagement
{
    public interface IStateChangeListener
    {
        void StateChanged(GameScreenBase gameScreen);
    }
}
