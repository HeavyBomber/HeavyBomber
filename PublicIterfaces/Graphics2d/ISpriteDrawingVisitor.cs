namespace PublicIterfaces.Graphics2d
{
    public interface ISpriteDrawingVisitor
    {
        void Visit(ISpriteDrawer drawer);
        void Visit(ISpriteFontDrawer drawer);
    }
}
