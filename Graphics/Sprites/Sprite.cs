using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;

namespace Graphics.Sprites
{
    public class Sprite : ISprite
    {
        private Texture2D texture;
        private float layerDepth;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle SourceRect { get; private set; }
        public Vector2 Position { get; private set; }

        public float LayerDepth
        {
            get { return layerDepth; }
        }

        public Color Color { get; private set; }

        public Sprite()
        {
            
        }

        public Sprite(Texture2D texture, Rectangle sourceRect, Vector2 position, float layerDepth)
        {
            this.texture = texture;
            this.SourceRect = sourceRect;
            this.Position = position;
            this.layerDepth = layerDepth;
        }
    }
}
