namespace PublicIterfaces.Graphics2d
{
    public interface IAnimatedSprite : ISprite
    {
        void SetAnimationIndex(int index);
        void IncreaseFrame();
        float GetFrameTime();
    }
}
