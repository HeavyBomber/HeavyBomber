using System;
using Input;
using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.UserInterface;

namespace UserInterface.Buttons
{
    class Button : Drawable2DContainer, IButton, ITapListener
    {
        public event EventHandler Click;

        public void Init(Drawable2DComposite backgroundTexture, Drawable2DComposite font, float leftTextMargin)
        {
            font.SetRelativePosition(new Vector2(leftTextMargin, 0));
            this.AddChild(backgroundTexture);
            this.AddChild(font);

            base.Init();
        }

        private void OnClick()
        {
            if(Click != null)
            {
                Click(this, EventArgs.Empty);
            }
        }

        public bool ScreenClicked(Vector2 clickPoint)
        {
            var bounds = this.GetBounds();
            if( this.IsVisible() &&
                clickPoint.X > bounds.Left &&
                clickPoint.X < bounds.Right &&
                clickPoint.Y > bounds.Top &&
                clickPoint.Y < bounds.Bottom)
            {
                OnClick();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
