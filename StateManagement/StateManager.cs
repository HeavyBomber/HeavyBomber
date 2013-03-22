using System.Collections.Generic;

namespace StateManagement
{
    public class StateChangeInfo
    {
        public delegate void StateFunction();
        public StateFunction enterState = null;
        public StateFunction exitState = null;

        public StateChangeInfo(StateFunction enterState, StateFunction exitState)
        {
            this.enterState = enterState;
            this.exitState = exitState;
        }
    }

    public static class States
    {
        public static string MENU_STATE = "menu";
        public static string LEVEL_SELECT_STATE = "levelselection";
        public static string GAMEPLAY_STATE = "gameplay";
        public static string END_STATE = "end";
    }

    class StateManager : IStateManager
    {
        private IStateChangeListener screenChangeListener;
        // the name of the current state
        protected string currentState = string.Empty;
        private GameScreenBase currentScreen;
        // the name of the new state
        protected string newState = string.Empty;
        // a list of functions to be called when a state is changed
        Dictionary<string, List<GameScreenBase>> stateChangeMap = new Dictionary<string, List<GameScreenBase>>();
        // true if subsequent calles to SetState are queued (to allow state change function to be called in the correct order)
        bool stateChangeLocked;
        // a list of pending state changed
        List<string> pendingStateChangeRequests = new List<string>();
        // a list of new state changes
        List<string> newStateChangeRequests = new List<string>();

        public void shutdown()
        {
            endCurrentState();
        }

        public void endCurrentState()
        {
            setState(States.END_STATE);
        }

        protected void updateStates()
        {
            // we can only change the states if stateChangeLocked is false (i.e. the state change
            // isn't called from one of the enter or exit state functions)
            if (!stateChangeLocked)
            {
                // set the lock
                stateChangeLocked = true;

                // push all our new state changes onto the pending state change list
                foreach (string info in newStateChangeRequests)
                    pendingStateChangeRequests.Add(info);

                newStateChangeRequests.Clear();

                // now process the pending state changes
                while (pendingStateChangeRequests.Count != 0)
                {
                    foreach (string stateChangeRequest in pendingStateChangeRequests)
                    {
                        if (!stateChangeRequest.Equals(currentState))
                        {
                            // call the exit function for all state listeners that are in the current state
                            if (stateChangeMap.ContainsKey(currentState))
                            {
                                foreach (GameScreenBase info in stateChangeMap[currentState])
                                {
                                    info.Dispose();
                                }
                            }

                            if (!stateChangeRequest.Equals(States.END_STATE))
                            {
                                if (stateChangeMap.ContainsKey(stateChangeRequest))
                                {
                                    foreach (GameScreenBase info in stateChangeMap[stateChangeRequest])
                                    {
                                        info.Init();
                                    }
                                }
                            }
                        }

                        currentState = stateChangeRequest;
                        currentScreen = stateChangeMap[currentState][0];
                    }

                    // all current pending state changes have been processed, so clear the list
                    pendingStateChangeRequests.Clear();

                    // push all our new state changes onto the pending state change list
                    foreach (string info in newStateChangeRequests)
                        pendingStateChangeRequests.Add(info);

                    newStateChangeRequests.Clear();
                }

                stateChangeLocked = false;
            }
        }

        public void setState(string newState)
        {
            // we have to jump through some hoops to ensure that all state change requests are
            // processed in order because it is possible to change the state from within
            // an enter or exit state function. To do this we add all state requests to 
            // newStateChangeRequests, lock the state change loop, copy all requests
            // from newStateChangeRequests to pendingStateChangeRequests and then
            // process the state changes in pendingStateChangeRequests.

            // add our requested state change to the newStateChangeRequests vector
            newStateChangeRequests.Add(newState);

            updateStates();
        }

        public void registerStateChange(string state, GameScreenBase gameScreen)
        {
            if (!state.Equals(States.END_STATE))
            {
                if (!isRegisteredState(state))
                    stateChangeMap.Add(state, new List<GameScreenBase>());
                stateChangeMap[state].Add(gameScreen);
            }
            if (currentScreen == null)
            {
                currentScreen = gameScreen;
            }
        }

        public bool isRegisteredState(string state)
        {
            return stateChangeMap.ContainsKey(state);
        }

        public void registerStateChangeListener(IStateChangeListener listener)
        {
            this.screenChangeListener = listener;
            listener.stateChanged(currentScreen);
        }

        public void registerState(string stateName, GameScreenBase gameState)
        {
            this.registerStateChange(stateName, gameState);
        }
    }
}
