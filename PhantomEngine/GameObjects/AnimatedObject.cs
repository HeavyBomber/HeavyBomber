#region Using Statements
using System;
using System.Collections.Generic;
using GameEntities.Enums;
using GameEntities.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomEngine.GameStructs;
using SpriteSheetRuntime;

#endregion

namespace PhantomEngine.GameObjects
{
    public class AnimatedObject : GameObject
    {
        #region Constants
        private const int SPEED = 8;
        protected float frameTime = 0.15f;
        private const int BLINK_FRAMES = 12;


        #endregion

        #region Fields

        protected string collisionName = CollisionIdentifiers.NONE;
        
        protected SpriteSheet spriteSheet;
        protected int frames;
        private bool inUse;
        protected int blinkFrames;
        protected float accumulator;
        protected int currentFrame;
        protected Direction currentDirection;
        protected Vector4 collisionMargins;
        protected Rectangle rect;

        private static List<AnimatedObject> animatedObjects = new List<AnimatedObject>();

        protected int tileX;
        protected int tileY;
        
        #endregion

        #region Properties

        public static List<AnimatedObject> AnimatedObjects
        {
            get
            {
                return animatedObjects;
            }
        }

        public bool InUse
        {
            get { return inUse; }
        }

        public int TileX
        {
            get { return tileX; }
        }

        public int TileY
        {
            get { return tileY; }
        }

        public string CollisionName
        {
            get { return collisionName; }
        }

        public virtual Rectangle GetRectangle()
        {
            rect = new Rectangle();
            rect.Offset((int)(basePosition.X + collisionMargins.X), (int)(basePosition.Y + collisionMargins.Y));
            rect.Width = spriteSheet.SourceRectangle(0).Width - (int)(collisionMargins.X + collisionMargins.Z);
            rect.Height = spriteSheet.SourceRectangle(0).Height - (int)(collisionMargins.Y + collisionMargins.W);
            return rect;
        }

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public virtual void Collision(AnimatedObject other)
        {

        }
        public virtual void Restore(Vector2 position, SpriteSheet spriteSheet, ObjectDataBase objectData)
        {
            this.basePosition = position;
            this.frameTime = spriteSheet.FrameTime;
            this.spriteSheet = spriteSheet;

            frames = spriteSheet.FramesPerDir;
            currentFrame = 0;

            this.inUse = true;
            this.startupGameObject();
            animatedObjects.Add(this);
        }

        public virtual void Shutdown()
        {
            this.inUse = false;
            animatedObjects.Remove(this);
        }

        public virtual void Update(GameTime gameTime)
        {
            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (accumulator > frameTime)
            {
                accumulator -= frameTime;
                {
                    currentFrame++;
                    if (currentFrame >= frames)
                        currentFrame = 0;
                }
            }
        }

        /// <summary>
        /// Funkcja odpowiadająca za rysowanie sprite'ów na ekranie.
        /// Nietypowe jest w niej to, że w funkcji Draw zamiast pozycji obiektu, podaje się pozycje środka kamery,
        /// a zamiast punktu odniesienia właściwą pozycję obiektu.
        /// Jest to wykonane w celu zapewnienia obracania oraz zoomowania kamery względem środka ekranu.
        /// </summary>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //int direciton = (int) currentDirection;
            //spriteBatch.Draw(spriteSheet.Texture, Camera2D.ScreenCenter, spriteSheet.SourceRectangle((int)currentDirection * frames + currentFrame),
            //   color, Camera2D.Rotation, Position, Camera2D.Zoom,
            //   SpriteEffects.None, layerDepth);
        }
        #endregion
    }
}
