using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameObjects;
using BomberPunk.ObjectData;
using Microsoft.Xna.Framework.Content;
using XMLContent;

namespace BomberPunk.Managers
{
    class EnemyProcessor
    {
        private static EnemyProcessor instance = new EnemyProcessor();
        private string level;
        private ContentManager contentManager;
        private EnemyData enemyData;

        public static EnemyProcessor Instance
        {
         get { return instance; }
        }

        private EnemyProcessor()
        {
            enemyData = new EnemyData();
        }
        public EnemyData LoadEnemy(int enemyId, ContentManager content)
        {
            const string ENEMY_PATH = "Sprites/Enemies/Enemy";
            const string DATA_PATH = "/Data";
            var data = content.Load<EnemyDefinition>(ENEMY_PATH + enemyId + DATA_PATH);

            //EnemyData enemyData = new EnemyData();

            enemyData.SetBehaviorPatterns(
                (EnemyData.Behavior)Enum.Parse(typeof (EnemyData.Behavior), data.MovePattern.FindsPlayer, true),
                (EnemyData.Behavior)Enum.Parse(typeof(EnemyData.Behavior), data.MovePattern.NearBomb, true));


            enemyData.SetValues(data.Health, data.Sight, data.Speed, data.ShadowId);
            enemyData.SetSound(data.Sound);

            return enemyData;
        }
    }
}
