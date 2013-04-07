using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PublicIterfaces.BasicGameObjects
{
    public abstract class Drawable2DComposite : IGameObject
    {
        private const float DEFAULT_LAYER_DEPTH = 0.5f;

        private Drawable2DComposite parent;
        private bool isInUse;
        private bool isVisible;
        protected float rotation;
        protected Vector2 relativePosition;
        private Color color = Color.White;
        private float layerDepth;

        public Drawable2DComposite()
        {
            this.isVisible = true;
            createEmptyParent();
        }

        protected virtual void createEmptyParent()
        {
            parent = new EmptyDrawable2DComposite();
        }

        public virtual Vector2 GetRelativePosition()
        {
            return relativePosition;
        }

        public virtual Vector2 GetAbsolutePosition()
        {
            return relativePosition + parent.GetRelativePosition();
        }

        public void SetRelativePosition(Vector2 position)
        {
            this.relativePosition = position;
        }

        public virtual Vector2 GetOrigin()
        {
            return parent.GetOrigin();
        }

        public Color GetColor()
        {
            return color;
        }

        public virtual void SetRootOrigin(Vector2 origin)
        {
            parent.SetRootOrigin(origin);
        }

        public float GetRotation()
        {
            return rotation;
        }

        public void SetParent(Drawable2DComposite parent)
        {
            this.parent = parent;
        }

        public bool HasParent()
        {
            if(parent is EmptyDrawable2DComposite)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
  
        }

        public virtual bool IsInUse()
        {
            return isInUse;
        }

        public virtual void Init()
        {
            this.isInUse = true;
            setDefaultValues();
        }

        private void setDefaultValues()
        {
            createEmptyParent();
            this.SetRelativePosition(Vector2.Zero);
            this.isVisible = true;
            //this.SetRootOrigin(Vector2.Zero);
            this.SetRotation(0);
            this.layerDepth = DEFAULT_LAYER_DEPTH;
        }

        public virtual void SetLayerDepth(float depth)
        {
            this.layerDepth = depth;
        }

        public float GetLayerDepth()
        {
            return layerDepth;
        }

        public abstract Rectangle GetBounds();

        public virtual void Dispose()
        {
            this.isInUse = false;
        }

        public virtual void SetRotation(float newRotation)
        {
            //updateChildrenRotation(newRotation - rotation);
            this.rotation = newRotation;
        }

        public virtual void Rotate(float rotationDelta)
        {
            //updateChildrenRotation(rotationDelta);
            this.rotation += rotationDelta;
        }

        public bool IsVisible()
        {
            //return true;
            return isVisible && isInUse;
        }

        public virtual void Hide()
        {
            this.isVisible = false;
        }

        public virtual void Show()
        {
            this.isVisible = true;
        }
    }
}
