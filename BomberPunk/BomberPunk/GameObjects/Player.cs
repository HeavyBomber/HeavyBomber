using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using Core.Resources;
using GameEntities.Collisions;
using GameEntities.Enums;
using GameEntities.GameObjects;
using GameEntities.Input;
using GameEntities.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberPunk.GameObjects
{
    class Player : AnimatedObject
    {
        private const int SPEED = 5;
        private Vector2 movementVector = Vector2.Zero;
        private bool onTheBomb;
        private int permissionLevel = TerrainIdentifiers.Bomb;
        private int bombsAtOnce;
        private Texture2D effectTexture;

        private int powerUpDuration = 10;
        private int powerUpOfffsetDuration = 1;

        private TimeSpan powerUpTime;
        private byte alpha;
        private int delta = 0;

        private bool isInBerserk;
        private bool isImmune;
        private bool powerUpTriggered;
        private Color effectColor;
        private float effectAccumulator = 0;
       

        private delegate void PowerUpAction(GameTime gameTime);

        private PowerUpAction powerUpAction;

        protected float frameTimeStill = 0.2f;

        public Player()
        {
            effectColor = Color.FromNonPremultiplied(0, 0, 0, 0);
            collisionName = CollisionIdentifiers.PLAYER;
            effectTexture = GameResources.Content.Load<Texture2D>("Sprites/Effects/pulse");
            currentDirection = Direction.None;
        }

        private void berserk(GameTime gameTime)
        {
            if(powerUpTriggered)
            {
                Board.Instance.ReleasePowerUp(tileX, tileY);
                alpha = 255;
                powerUpTriggered = false;
                powerUpTime = gameTime.TotalGameTime;
            }

            const int ALPHA_DELTA = 20;

            alpha += (byte)delta;
            if (alpha > 255 - ALPHA_DELTA)
            {
                delta = -ALPHA_DELTA;
            }
            else if (alpha < ALPHA_DELTA)
            {
                delta = ALPHA_DELTA;
            }

            effectColor = Color.FromNonPremultiplied(255, 20, 20, alpha);

            effectAccumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (effectAccumulator > powerUpDuration)
            {
                effectColor = Color.FromNonPremultiplied(0, 0, 0, 0);
            }
            if (effectAccumulator > powerUpDuration + powerUpOfffsetDuration)
            {
                effectAccumulator = 0;
                powerUpAction = null;
                isInBerserk = false;
            }
        }

        private void setUpPowerUp(int key)
        {
            if(key == PowerUps.NONE)
            {
                return;
            }

            switch (key)
            {
                case PowerUps.NONE:
                    return;
                case PowerUps.BERSERKER:
                    isInBerserk = true;
                    powerUpAction = berserk;
                    powerUpTriggered = true;
                    break;
                default:
                    return;
            }
        }

        public override void Update(GameTime gameTime)
        {

            if(powerUpAction != null)
            {
                powerUpAction(gameTime);
            }
            bombsAtOnce = 1;

            layerDepth = (BasePosition.Y / Board.TILE_SIZE) * LayerIdentifiers.DELTA + LayerIdentifiers.ENV_BASE;
            tileX = (int)((basePosition.X + 0.5 * Board.TILE_SIZE) / Board.TILE_SIZE);
            tileY = (int) ((basePosition.Y + 0.5 * Board.TILE_SIZE)/Board.TILE_SIZE);

         
            setUpPowerUp(Board.Instance.PowerUpMap[TileX, TileY]);
            
            if(onTheBomb)
            {
                if (Board.Instance.TerrainMap[tileX, tileY] != TerrainIdentifiers.Bomb)
                {
                    onTheBomb = false;
                    permissionLevel = TerrainIdentifiers.Bomb;
                }
            }
            if (currentDirection == Direction.None)
            {
                accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (accumulator > frameTimeStill)
                {
                    accumulator -= frameTimeStill;
                    {
                        currentFrame++;
                        if (currentFrame >= frames)
                            currentFrame = 0;
                    }
                }
            }
            else
            {
                base.Update(gameTime);                
            }

            var tempPosition = basePosition + movementVector;
            float tileOffsetX = 0;
            float tileOffsetY = 0;

            if (currentDirection == Direction.Right)
            {
                tileOffsetX = 0.5f;
            }
            else if (currentDirection == Direction.Down)
            {
                tileOffsetY = 0.5f;
            }
            else if (currentDirection == Direction.Up)
            {
                tileOffsetY = -0.5f;
            }
            else if (currentDirection == Direction.Left)
            {
                tileOffsetX = -0.5f;
            }

            int tile = 0;

            if (currentDirection == Direction.Up || currentDirection == Direction.Down)
            {
                tile = (int)(tempPosition.X + Board.TILE_SIZE / 2) / Board.TILE_SIZE;

                if (tempPosition.X - SPEED > tile * Board.TILE_SIZE)
                {
                    tempPosition.X -= SPEED;
                }
                else if (tempPosition.X < tile * Board.TILE_SIZE)
                {
                    tempPosition.X += SPEED;
                }
            }
            else if (currentDirection == Direction.Left || currentDirection == Direction.Right)
            {
                tile = (int)(tempPosition.Y + Board.TILE_SIZE / 2) / Board.TILE_SIZE;

                if (tempPosition.Y - SPEED > tile * Board.TILE_SIZE)
                {
                    tempPosition.Y -= SPEED;
                }
                else if (tempPosition.Y < tile * Board.TILE_SIZE)
                {
                    tempPosition.Y += SPEED;
                }
            }

            if (currentDirection != Direction.None
                && Board.Instance.TerrainMap[(int)(tempPosition.X + Board.TILE_SIZE * (0.5 + tileOffsetX)) / Board.TILE_SIZE, (int)(tempPosition.Y + Board.TILE_SIZE * (0.5 + tileOffsetY)) / Board.TILE_SIZE] < permissionLevel)
                basePosition = tempPosition;
            else
                currentDirection = Direction.None;

            handleInput();
        }

        private void handleInput()
        {
            var delta = InputManager.Instance.GetGesture();
            if (delta != Vector2.Zero)
            {
                //czy jesli wektor bedzie mial wartosc ktorejs wspolrzednej rowna 0,
                //to wtedy co otrzymamy po znormalizowaniu tego wektora?
                delta.Normalize();
                movementVector = delta * SPEED;

                if (delta.X > 0)
                {
                    currentDirection = Direction.Right;
                } 
                else if (delta.X < 0)
                {
                    currentDirection = Direction.Left;
                }
                else if (delta.Y > 0)
                {
                    currentDirection = Direction.Down;

                }
                else if (delta.Y < 0)
                {
                    currentDirection = Direction.Up;
                }
            }
            else
            {
                Vector2 position = InputManager.Instance.TapExecuted();
                if (position != Vector2.Zero)
                {
                    if (position.X < 400)
                    {
                        currentDirection = Direction.None;
                    }
                    else
                    {
                        onTheBomb = true;
                        permissionLevel = TerrainIdentifiers.Barrel;
                        Board.Instance.PlantBomb();
                    }
                }

            }
        }

        private void headTo(Direction direction)
        {
            //switch (direction)
            //{
            //    case: Direction.Left

            //}
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Board.Instance.EffectsFactory.GetTexture(0), Camera2D.ScreenCenter, null,
            // color, Camera2D.Rotation, Position, Camera2D.Zoom,
            // SpriteEffects.None, layerDepth - 0.01f);

            //spriteBatch.Draw(effectTexture, new Vector2(Camera2D.BoundsRect.Left, Camera2D.BoundsRect.Top), null,
            //effectColor, Camera2D.Rotation, Vector2.Zero, Camera2D.Zoom,
            //SpriteEffects.None, LayerIdentifiers.PULSE);

            base.Draw(gameTime, spriteBatch);
        }
    }
}
