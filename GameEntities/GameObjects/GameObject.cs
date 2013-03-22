using Graphics.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEntities.GameObjects
{
    /// <summary>
    /// Bazowa klasa dla wszystkich obiektów w grze
    /// Każdy element gry, którego pozycja będzie się zmieniała wraz z ruchem kamery
    /// powinien dziedziczyć po tej klasie.
    /// </summary>
    public abstract class GameObject
    {
        private const int DIRECTIONS = 4;

        protected IDrawableObject presentation;

        //drawing parameters
        protected float layerDepth;
        protected Vector2 scale;
        protected Vector2 basePosition;
        protected Vector2 tilePosition;
        //kolor obiektu
        protected Color color;



        #region Public properties

        protected GameObject()
        {
            color = Color.White;            
        }
        /// <summary>
        /// Gets the current position of object on the screen
        /// Zwraca pozycję relatywną do położenia kamery
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return Vector2.Zero;
                //return new Vector2(Camera2D.Position.X - basePosition.X, Camera2D.Position.Y - basePosition.Y);
            }
        }

        public Vector2 BasePosition
        {
            get { return basePosition; }
        }

        #endregion

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        #region Methods
        /// <summary>
        /// Creates startup game object
        /// </summary>
        /// <returns>returns itself</returns>
        protected GameObject startupGameObject()
        {
            scale = Vector2.One;
            color = Color.White;
            return this;
        }


        /// <summary>
        /// Creates startup game object
        /// </summary>
        /// <returns>returns itself</returns>
        protected GameObject startupGameObject(Vector2 s, Color c)
        {
            scale = s;
            color = c;
            return this;
        }
   
        #endregion
    }
}
