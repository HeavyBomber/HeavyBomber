using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using GameEntities.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.GameObjects;
using Sound;

namespace BomberPunk.GameObjects
{
    class Bomb : AnimatedObject
    {
        private double creationTime;
        public const float BOMB_TIMEOUT = 2;

        public Bomb()
        {
            layerDepth = LayerIdentifiers.BOMB;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if(creationTime == 0)
            {
                creationTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else if(gameTime.TotalGameTime.TotalSeconds - creationTime > BOMB_TIMEOUT)
            {
                Board.Instance.Blow(this.BasePosition, true);
                SoundManager.PlaySound("blow");
                this.Shutdown();
            }
        }

        public override void Shutdown()
        {
            this.creationTime = 0;
            base.Shutdown();
        }
    }
}
