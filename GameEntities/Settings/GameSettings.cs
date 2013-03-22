using System;

namespace GameEntities.Settings
{
    public static class GameSettings
    {
        public const int GAUGE_COUNT = 7;
        public const int INDICATOR_COUNT = 3;
        public const int SYSTEM_SETTINGS_COUNT = 2;

        private static bool gaugeValueChanged;
        private static float[] gaugeValues;
        private static int currentScenario;
        private static int currentLevel;


        static GameSettings()
        {
            gaugeValues = new float[GAUGE_COUNT]; 
        }


        private static int[] lastUnlockedLevels;

        public static int[] LastUnlockedLevels
        {
            get { return lastUnlockedLevels; }
            set { lastUnlockedLevels = value; }
        }

        public static void SetGaugeValue(int gauge, float value)
        {
            GaugeValues[gauge] = value;
            gaugeValueChanged = true;
        }

        public static void SetGaugeValue(int gauge, string value)
        {
            GaugeValues[gauge] = (float)Convert.ToDouble(value);
            gaugeValueChanged = true;
        }

        public static bool GaugeValueChanged
        {
            get { return gaugeValueChanged; }
            set { gaugeValueChanged = value; }
        }

        public static float[] GaugeValues
        {
            get { return gaugeValues; }
        }

        public static int CurrentScenario
        {
            get { return currentScenario; }
        }

        public static int CurrentLevel
        {
            get { return currentLevel; }
        }

    }
}
