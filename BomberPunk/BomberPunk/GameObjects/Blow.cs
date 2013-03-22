using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using GameEntities.Collisions;
using GameEntities.GameObjects;
using Microsoft.Xna.Framework;
using PhantomEngine.GameObjects;

namespace BomberPunk.GameObjects
{
    class Blow : AnimatedObject
    {
        public Blow()
        {
            this.collisionName = CollisionIdentifiers.FIRE;
            layerDepth = LayerIdentifiers.BLOW;

        }
        public override void Restore(Vector2 position, SpriteSheetRuntime.SpriteSheet spriteSheet, ObjectDataBase objectData)
        {
            base.Restore(position, spriteSheet, objectData);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentFrame == frames - 1)
            {
                this.Shutdown();
            }
        }
    }
}
