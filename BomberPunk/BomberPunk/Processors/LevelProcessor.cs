using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameObjects;
using Core.Resources;
using GameEntities.Settings;
using Microsoft.Xna.Framework.Content;
using XMLContent;

namespace BomberPunk.Managers
{
    class LevelProcessor
    {
        private static LevelProcessor instance = new LevelProcessor();
        private string level;
        private ContentManager contentManager;
        public static ScenarioList ScenarioData;

        public static LevelProcessor Instance
        {
            get { return instance; }
        }

        private LevelProcessor()
        {
            
        }
        public void InitializeLevels()
        {
            ScenarioData = GameResources.Content.Load<ScenarioList>("Scenarios/ScenarioData");

            var scenarioCount = ScenarioData.Scenarios.Count();
            GameSettings.LastUnlockedLevels = new int[scenarioCount];

            for(int i = 0; i < scenarioCount; i++)
            {
                GameSettings.LastUnlockedLevels[i] = 1;
            }

        }

        public void LoadLevel(int scenario, int level)
        {
            var data = GameResources.Content.Load<LevelData>(ScenarioData.Scenarios[scenario].LevelPaths[level]);

            //PRZEKAZAC ZMIENNA DATA DO KOSTRUKTORA BOARDA!!!
            Board.Instance.Load(data.Rows, data.Cols);
            Board.Instance.SetTerrain(data.Terrain, data.PowerUps);
            Board.Instance.SetEnemies(data.Enemies);
            //Board.Instance.SetEnemies(data.Enemies);
        }
    }
}
