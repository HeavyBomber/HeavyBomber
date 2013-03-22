using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEntities.Resources
{
    public static class GameResources
    {
        public static SpriteBatch SpriteBatch;
        public static ContentManager Content;
        public static GameTime GameTime;

        public static void Cleanup()
        {
            if (SpriteBatch != null)
                SpriteBatch.Dispose();
            SpriteBatch = null;

            if (Content != null)
                Content.Dispose();
            Content = null;

            GameTime = null;
        }
    }
}
