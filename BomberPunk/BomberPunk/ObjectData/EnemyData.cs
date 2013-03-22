using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhantomEngine.ObjectData;

namespace BomberPunk.ObjectData
{
    class EnemyData : ObjectDataBase
    {
        public enum Behavior
        {
            DoNothing = 0,
            RunAway = 1,
            Follow = 2,
            Die = 3
        }

        private int shadowId;
        private string deathSound;

        private Behavior playerDetected;
        private Behavior bombDetected;
        private Behavior nothingHappens;

        public string DeathSound
        {
            get { return deathSound; }
        }
        public Behavior PlayerDetected
        {
            get { return playerDetected; }
        }

        public Behavior BombDetected
        {
            get { return bombDetected; }
        }

        public Behavior NothingHappens
        {
            get { return nothingHappens; }
        }

        public int ShadowId
        {
            get { return shadowId; }
        }

        public void PlayerFound()
        {

        }

        public void BombFound()
        {


        }

        public void SetBehaviorPatterns(Behavior findsPlayer, Behavior nearBomb)
        {
            playerDetected = findsPlayer;
            bombDetected = nearBomb;
            nothingHappens = Behavior.DoNothing;
        }

        public void SetValues(int health, int sight, int speed, int shadowId)
        {
            this.shadowId = shadowId;
        }

        internal void SetSound(string sound)
        {
            deathSound = sound;
        }
    }
}
