namespace Settings
{
    public static class GameSettings
    {
        private const int SETTINGS_COUNT = 2;
        private static bool[] systemSettings;

        public static bool[] SystemSettings
        {
            get { return systemSettings; }
        }

        static GameSettings()
        {
            systemSettings = new bool[SETTINGS_COUNT];
        }
    }
}
