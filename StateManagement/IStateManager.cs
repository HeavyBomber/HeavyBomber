namespace StateManagement
{
    public interface IStateManager
    {
        void RegisterStateChange(string state, GameScreenBase gameScreen);
        void RegisterStateChangeListener(IStateChangeListener listener);
        void SetState(string newState);
        //void RegisterState(string stateName);
    }
}
