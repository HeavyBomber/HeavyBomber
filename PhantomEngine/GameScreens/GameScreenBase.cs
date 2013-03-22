using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomEngine.GameScreens
{
    /// <summary>
    /// Klasa bazowa ekranu gry
    /// </summary>
    public abstract class GameScreenBase
    {
        #region Properties
        public ContentManager Content
        {
            get { return content; }
        }
        protected ContentManager content;
        #endregion

        #region Methods
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Dispose();

        public void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                OnBackButtonPressed();
            }
        }
        protected virtual void OnBackButtonPressed()
        {
            //if (BackButtonReleased != null)
            //    BackButtonReleased(this, null);
        }

        #endregion
    }
}
