using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;

namespace Graphics.Effects
{
    public class BackgroundTransition : IDrawableObject
    {
        private static BackgroundTransition instance = new BackgroundTransition();
        private Texture2D backgroundTexture;
        private bool duringTransition = false;
        private string pendingState;
        private readonly Vector2 startPosition = new Vector2(0, -480);
        private Vector2 transitionPosition;
        private float accumulator;
        private readonly float frameTime = 0.03f;
        private byte alpha = 255;
        private Color transitionTextureColor;
        private bool duringFade = false;
        private bool readyToLoad;
        private float functionArg;

        public static BackgroundTransition Instance
        {
            get { return instance; }
        }

        public bool DuringFade
        {
            get { return duringFade; }
        }

        private float TransitionFunction(float x)
        {
           return (float)(Math.Exp(-x) * Math.Abs(Math.Cos(2 * x)));
        }

        public void Init()
        {
            const string BACKGROUND_PATH = "Sprites/UI/background";
            backgroundTexture = GameResources.Content.Load<Texture2D>(BACKGROUND_PATH);
            basePosition = new Vector2(0,0);
            transitionTextureColor = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);
        }

        public void GoToState(string state)
        {
            pendingState = state;
            transitionPosition = startPosition;
            duringTransition = true;
            functionArg = 0;

        }

        private void SetState()
        {
            StateManager.Instance.setState(pendingState);
            functionArg = 0;
        }

        public void Update(GameTime gameTime)
        {
             accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
             if (accumulator > frameTime)
             {
                 if(readyToLoad)
                 {
                     SetState();
                     readyToLoad = false;
                 }
                 const float TRANSITION_INCREMENT = 1f;
                 accumulator -= frameTime;

                 if (duringTransition)
                 {
                     //transitionPosition.Y += TRANSITION_INCREMENT;

                     transitionPosition.Y = -480 * TransitionFunction(functionArg);
                     functionArg += 0.2f;

                     //if (transitionPosition.Y > -TRANSITION_INCREMENT)
                     if(functionArg > 2f * Math.PI)
                     {
                         transitionPosition = Vector2.Zero;
                         duringTransition = false;
                         duringFade = true;
                         readyToLoad = true;
                     }
                 }

                 if (duringFade)
                 {
                     const byte ALPHA_DECREMENT = 5;
                     alpha -= ALPHA_DECREMENT;
                     transitionTextureColor = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);

                     if (alpha < ALPHA_DECREMENT)
                     {
                         duringFade = false;
                         alpha = 255;
                         transitionTextureColor = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);
                     }
                 }  
             }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if(duringTransition || duringFade)
            {
                spriteBatch.Draw(backgroundTexture, transitionPosition, null, transitionTextureColor, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerIdentifiers.TRANSITION_IMAGE);                
            }
            spriteBatch.Draw(backgroundTexture, basePosition, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerIdentifiers.BACKGROUND_IMAGE);
        }

        public IEnumerable<ISprite> getRepresentation()
        {
            throw new NotImplementedException();
        }
    }
}
