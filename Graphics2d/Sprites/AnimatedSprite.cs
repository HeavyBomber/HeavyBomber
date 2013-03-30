using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces.Graphics2d;
using SpriteSheetRuntime;

namespace Graphics2d.Sprites
{
    class AnimatedSprite : IAnimatedSprite
    {
        private SpriteSheet spriteSheet;
        private int animationIndex;
        private int frameIndex;
        private float frameTime;

        public AnimatedSprite(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            this.frameTime = spriteSheet.FrameTime;
        }

        public Texture2D GetTexture()
        {
            return spriteSheet.Texture;
        }

        public void SetAnimationIndex(int index)
        {
            this.animationIndex = index;
        }

        public void IncreaseFrame()
        {
            this.frameIndex++;
            if(frameIndex >= spriteSheet.FramesPerDir)
            {
                frameIndex = 0;
            }
        }

        public float GetFrameTime()
        {
            return frameTime;
        }

        public Rectangle GetSourceRectangle()
        {
            return spriteSheet.SourceRectangle((frameIndex + 1) * (animationIndex + 1) - 1);
        }
    }
}
