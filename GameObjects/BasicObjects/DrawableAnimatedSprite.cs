using PublicIterfaces.Graphics2d;

namespace GameObjects.BasicObjects
{
    class DrawableAnimatedSprite : DrawableSprite
    {
        private IAnimatedSprite sprite;
        private double accumulator;
        private float frameTime;

        public void SetSprite(IAnimatedSprite sprite)
        {
            this.sprite = sprite;
            this.frameTime = sprite.GetFrameTime();
            base.SetSprite(sprite);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            accumulator += gameTime.ElapsedGameTime.TotalSeconds;
            if(accumulator > frameTime)
            {
                sprite.IncreaseFrame();
                accumulator -= frameTime;
            }
            base.Update(gameTime);
        }
    }
}
