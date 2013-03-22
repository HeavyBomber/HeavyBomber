using Microsoft.Xna.Framework;

namespace PublicIterfaces.BasicGameObjects
{
    public class EmptyDrawable2DComposite : Drawable2DComposite
    {
        private Vector2 origin;

        public EmptyDrawable2DComposite()
        {
            origin = Vector2.Zero;
        }

        public EmptyDrawable2DComposite(Vector2 origin)
        {
            this.origin = origin;
        }

        public override Vector2 GetRelativePosition()
        {
            return Vector2.Zero;
        }

        public override Vector2 GetOrigin()
        {
            return origin;
        }

        public override void SetRootOrigin(Vector2 origin)
        {
            this.origin = origin;
        }

        protected override void createEmptyParent()
        {
        }

        public override Rectangle GetBounds()
        {
            var bounds = new Rectangle(0, 0, 0, 0);
            return bounds;
        }
    }
}
