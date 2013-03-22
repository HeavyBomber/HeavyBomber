using Graphics.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PublicIterfaces;

namespace Graphics
{
    public class Drawer : IDrawer
    {
        private IDrawableObjectsProvider objectsProvider;
        private ICamera2D camera2d;

        public Drawer(ICamera2D camera2d)
        {
            this.camera2d = camera2d;
        }

        public void SetDrawableObjectsProvider(IDrawableObjectsProvider objectsProvider)
        {
            this.objectsProvider = objectsProvider;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
              foreach(ISprite sprite in objectsProvider.GetSprites())
              {
                  spriteBatch.Draw(sprite.Texture, camera2d.ScreenCenter, sprite.SourceRect,
                                   sprite.Color, camera2d.Rotation, sprite.Position, camera2d.Zoom,
                                   SpriteEffects.None, sprite.LayerDepth);
              }
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    foreach(IDrawableObject dObject in objectsProvider.GetObjects())
        //    {
        //        foreach(ISprite sprite in dObject.getRepresentation())
        //        {
        //            spriteBatch.Draw(sprite.Texture, camera2d.ScreenCenter, sprite.SourceRect,
        //                             sprite.Color, camera2d.Rotation, sprite.Position, camera2d.Zoom,
        //                             SpriteEffects.None, sprite.LayerDepth);
        //        }
        //    }
        //}
    }
}
