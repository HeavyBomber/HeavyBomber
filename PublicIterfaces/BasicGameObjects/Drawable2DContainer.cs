using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PublicIterfaces.BasicGameObjects
{
    public class Drawable2DContainer : Drawable2DComposite
    {
        private List<Drawable2DComposite> children = new List<Drawable2DComposite>();

        public void AddChild(Drawable2DComposite child)
        {
            child.SetParent(this);
            children.Add(child);
        }

        public List<Drawable2DComposite> GetChildren()
        {
            return children;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var gameObjectBase in children)
            {
                gameObjectBase.Update(gameTime);
            }
        }

        public override Rectangle GetBounds()
        {
            Rectangle bounds = new Rectangle((int)(this.GetAbsolutePosition().X + 0.5),
                                             (int)(this.GetAbsolutePosition().Y + 0.5),
                                             0, 0);

            foreach (var drawable2DComposite in children)
            {
                var boundsToCompare = drawable2DComposite.GetBounds();

                if (boundsToCompare.Width > 0 && boundsToCompare.Height > 0)
                {
                    if (boundsToCompare.X < bounds.X)
                    {
                        bounds.X = boundsToCompare.X;
                    }
                    if (boundsToCompare.Right > bounds.Right)
                    {
                        bounds.Width += boundsToCompare.Right - bounds.Right;
                    }
                    if (boundsToCompare.Y < bounds.Y)
                    {
                        bounds.Y = boundsToCompare.Y;
                    }
                    if (boundsToCompare.Bottom > bounds.Bottom)
                    {
                        bounds.Height += boundsToCompare.Bottom - bounds.Bottom;
                    }
                }
            }

            return bounds;
        }

        public override void SetRotation(float newRotation)
        {
            updateChildrenRotation(newRotation - rotation);
            this.rotation = newRotation;
        }

        public override void Rotate(float rotationDelta)
        {
            updateChildrenRotation(rotationDelta);
            this.rotation += rotationDelta;
        }

        public override void Dispose()
        {
            foreach (var drawable2DComposite in children)
            {
                drawable2DComposite.Dispose();
            }
            base.Dispose();
        }

        private void updateChildrenRotation(float rotationDelta)
        {
            foreach (var drawable2DComposite in children)
            {
                drawable2DComposite.Rotate(rotationDelta);
            }
        }

        public override void Show()
        {
            foreach (var drawable2DComposite in children)
            {
                drawable2DComposite.Show();
            }
            base.Show();
        }

        public override void Hide()
        {
            foreach (var drawable2DComposite in children)
            {
                drawable2DComposite.Hide();
            }
            base.Hide();
        }
    }
}
