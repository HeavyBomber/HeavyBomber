using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PublicIterfaces;
using PublicIterfaces.Graphics2d;
using PublicIterfaces.ObjectPool;

namespace StateManagement
{
    public abstract class GameScreenBase
    {
        public event EventHandler<ScreenChangeEventArgs> ScreenChangeRequested;

        private IObjectsPool objectsPool;
        protected IObjectsPool ObjectsPool
        {
            get { return objectsPool; }
            set { objectsPool = value; }
        }

        public abstract void Init();
        public abstract void Dispose();
        public abstract void Update(GameTime gameTime);
    
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

        protected void requestStateChange(string stateName)
        {
            if(ScreenChangeRequested != null)
            {
                var args = new ScreenChangeEventArgs();
                args.StateName = stateName;
                ScreenChangeRequested(this, args);
            }
        }

    }
}
