using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using BomberPunk.ObjectData;
using GameEntities.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.Camera;
using PhantomEngine.Enums;
using PhantomEngine.GameObjects;
using PhantomEngine.ObjectData;
using SpriteSheetRuntime;

namespace BomberPunk.GameObjects
{
    class Enemy : AnimatedObject
    {
        #region fields
        private EnemyData enemyData;
        private EnemyData.Behavior currentBehavior;
        private bool destinationReached;
        private Vector2 currentDestination;
        private Vector2 moveVector;
        private int currentStepLength;
        private byte alpha = 255;
        private bool isDead;
        private const double DYING_TIME = 1;
        private double deathTime;
        private TimeSpan creationTime;
        private bool lastFrameReached;
        private Texture2D shadowTexture;
        private bool isCreated;
        private bool isBlinking;

        private TimeSpan timeBlinking = TimeSpan.FromSeconds(2);
        private int permissionLevel = 0;
        private int delta;


        private Random random;

        private delegate void EnemyAction(GameTime gameTime);

        private EnemyAction currentAction;

        private event EnemyAction behaviorChanged;

        #endregion

        public Enemy()
        {
            this.collisionName = CollisionIdentifiers.ENEMY;
            collisionMargins = new Vector4(5, 10, 5, 10);
        }

        public override void Restore(Vector2 tilePosition, SpriteSheet spriteSheet, ObjectDataBase enemyData)
        {
            isCreated = true;
            isDead = false;
            lastFrameReached = false;
            destinationReached = true;
            this.tilePosition = tilePosition;
            currentDestination = tilePosition;
            this.enemyData = enemyData as EnemyData;
            shadowTexture = Board.Instance.EffectsFactory.GetTexture(this.enemyData.ShadowId);
            setAction(EnemyData.Behavior.DoNothing);
            base.Restore(tilePosition * Board.TILE_SIZE, spriteSheet, enemyData);
            random = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            if (isCreated)
            {
                delta = -1;
                creationTime = gameTime.TotalGameTime;
                isCreated = false;
                isBlinking = true;
            }

            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (accumulator > frameTime)
            {
                const int ALPHA_DELTA = 80;

                if (isBlinking)
                {
                    alpha += (byte)delta;
                    if (alpha > 255 - ALPHA_DELTA)
                    {
                        delta = -ALPHA_DELTA;
                    }
                    else if (alpha < ALPHA_DELTA)
                    {
                        delta = ALPHA_DELTA;
                    }

                    if (gameTime.TotalGameTime - creationTime > timeBlinking)
                    {
                        isBlinking = false;
                        alpha = (byte)255;
                    }

                    color = Color.FromNonPremultiplied(255, 255, 255, alpha);
                }
                accumulator -= frameTime;
                currentFrame++;
                if (currentFrame >= frames)
                    currentFrame = 0;
            }

            if (!lastFrameReached)
                layerDepth = (BasePosition.Y / Board.TILE_SIZE) * LayerIdentifiers.DELTA + LayerIdentifiers.ENV_BASE;

            if (currentAction != null)
                currentAction(gameTime);
        }

        //Wszystkie akcje mo¿na przenieœæ do Enemy Data??
        private void doNothing(GameTime gameTime)
        {
            if (destinationReached)
            {
                tilePosition = currentDestination;

                //Board.Instance.CollisionMap[(int)tilePosition.X, (int)tilePosition.Y] = 1;

                setRandomDestination();
                destinationReached = false;
                move(gameTime);
            }
            else
            {

                move(gameTime);
            }
        }

        private void die(GameTime gameTime)
        {
            if (!isDead)
            {
                ComboManager.Instance.EnemyKilled();
                GameSettings.SetGaugeValue((int)Gauges.Points, GameSettings.GaugeValues[(int)Gauges.Points] + 1);
                //SoundManager.PlaySound(enemyData.DeathSound);
                frameTime = 0.15f;
                isDead = true;
                deathTime = gameTime.TotalGameTime.TotalSeconds;
                currentDirection = Direction.None;
            }
            else if (currentDirection != Direction.Special &&
                gameTime.TotalGameTime.TotalSeconds - deathTime > DYING_TIME)
            {
                currentDirection = Direction.Special;
                currentFrame = 0;
            }
            else if (currentDirection == Direction.Special && currentFrame == frames - 1)
            {
                lastFrameReached = true;
                layerDepth = LayerIdentifiers.FLESH;
            }
            else if (lastFrameReached)
            {
                currentFrame = frames - 1;
                alpha--;
                color = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);
            }

            if (alpha == 0)
            {
                Shutdown();
            }

        }

        private void setRandomDestination()
        {
            int integerDirection = random.Next(3);
            int i;

            for (i = 0; i < 4; i++)
            {
                currentDirection = (Direction)Enum.Parse(typeof(Direction), Convert.ToString(integerDirection), true);

                if (canGo(currentDirection))
                    break;

                integerDirection = (integerDirection + 1) % 4;
            }

            if (i == 4)
            {
                moveVector = Vector2.Zero;
                this.currentDirection = Direction.None;

                return;
            }

            switch (currentDirection)
            {
                case Direction.Down:
                    moveVector = new Vector2(0, 1);
                    break;
                case Direction.Up:
                    moveVector = new Vector2(0, -1);
                    break;
                case Direction.Left:
                    moveVector = new Vector2(-1, 0);
                    break;
                case Direction.Right:
                    moveVector = new Vector2(1, 0);
                    break;
            }

            const int STOP_PROBABILITY = 30;
            do
            {
                currentDestination += moveVector;

                if (random.Next(100) < STOP_PROBABILITY)
                    break;

            } while (canGo(currentDirection));


            Board.Instance.CollisionMap[(int)currentDestination.X, (int)currentDestination.Y] = 1;
            Board.Instance.CollisionMap[(int)tilePosition.X, (int)tilePosition.Y] = 0;
        }


        private void move(GameTime gameTime)
        {
            if (moveVector == Vector2.Zero)
                destinationReached = true;

            basePosition = BasePosition + moveVector;


            switch (currentDirection)
            {
                case Direction.Down:
                    if (BasePosition.Y >= currentDestination.Y * Board.TILE_SIZE)
                    {
                        destinationReached = true;
                    }
                    return;
                case Direction.Up:
                    if (BasePosition.Y < currentDestination.Y * Board.TILE_SIZE)
                    {
                        destinationReached = true;
                    }
                    return;
                case Direction.Left:
                    if (BasePosition.X <= currentDestination.X * Board.TILE_SIZE)
                    {
                        destinationReached = true;
                    }
                    return;
                case Direction.Right:
                    if (BasePosition.X > currentDestination.X * Board.TILE_SIZE)
                    {
                        destinationReached = true;
                    }
                    return;
            }

        }

        public override void Collision(AnimatedObject other)
        {
            if ((other.CollisionName == CollisionIdentifiers.FIRE
                || other.CollisionName == CollisionIdentifiers.PLAYER)
                && isBlinking == false)
            {
                setAction(EnemyData.Behavior.Die);
            }
        }

        private bool canGo(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    if (Board.Instance.TerrainMap[(int)currentDestination.X, (int)currentDestination.Y + 1] == 0
                        && Board.Instance.CollisionMap[(int)currentDestination.X, (int)currentDestination.Y + 1] == 0)
                        return true;
                    break;
                case Direction.Up:
                    if (Board.Instance.TerrainMap[(int)currentDestination.X, (int)currentDestination.Y - 1] <= permissionLevel
                        && Board.Instance.CollisionMap[(int)currentDestination.X, (int)currentDestination.Y - 1] <= permissionLevel)
                        return true;
                    break;
                case Direction.Left:
                    if (Board.Instance.TerrainMap[(int)currentDestination.X - 1, (int)currentDestination.Y] <= permissionLevel
                        && Board.Instance.CollisionMap[(int)currentDestination.X - 1, (int)currentDestination.Y] <= permissionLevel)
                        return true;
                    break;
                case Direction.Right:
                    if (Board.Instance.TerrainMap[(int)currentDestination.X + 1, (int)currentDestination.Y] <= permissionLevel
                        && Board.Instance.CollisionMap[(int)currentDestination.X + 1, (int)currentDestination.Y] <= permissionLevel)
                        return true;
                    break;
            }
            return false;
        }

        private void follow(GameTime gameTime)
        {

        }

        private void runAway(GameTime gameTime)
        {


        }
        private void setAction(EnemyData.Behavior behavior)
        {
            if (behavior == EnemyData.Behavior.DoNothing)
                currentAction = doNothing;
            else if (behavior == EnemyData.Behavior.Follow)
                currentAction = follow;
            else if (behavior == EnemyData.Behavior.RunAway)
                currentAction = runAway;
            else if (behavior == EnemyData.Behavior.Die)
                currentAction = die;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);


            spriteBatch.Draw(shadowTexture, Camera2D.ScreenCenter, null,
             color, Camera2D.Rotation, Position, Camera2D.Zoom,
             SpriteEffects.None, layerDepth - 0.01f);
        }

    }
}
