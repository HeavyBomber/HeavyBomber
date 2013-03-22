using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;

namespace Prefabricates.Particles
{
    public class Fog2D : Drawable2DContainer
    {
        private Drawable2DComposite fogSprite;
        private int rows;
        private int cols;
        private Rectangle textureRectangle;

        public void Init(int desiredWidth, int desiredHeight, Drawable2DComposite fogSprite)
        {
            this.fogSprite = fogSprite;
            textureRectangle = fogSprite.GetBounds();
            AddChild(fogSprite);

            cols = (int)((float)desiredWidth/textureRectangle.Width + 0.5);
            rows = (int)((float)desiredHeight/textureRectangle.Height + 0.5);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle();
        }
    }
}
