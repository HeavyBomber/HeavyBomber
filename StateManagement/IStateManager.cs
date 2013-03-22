namespace StateManagement
{
    public interface IStateManager
    {
        void registerStateChange(string state, GameScreenBase gameScreen);
        void registerStateChangeListener(IStateChangeListener listener);
        //void registerState(string stateName);
    }
}
