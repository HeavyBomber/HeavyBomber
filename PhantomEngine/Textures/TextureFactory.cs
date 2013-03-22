using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PhantomEngine.Textures
{
    public class TextureFactory
    {
        protected Texture2D[] textures;
        protected int keyOffset;

        public TextureFactory(Texture2D[] textures)
        {
            this.textures = textures;
            keyOffset = 0;
        }

        public TextureFactory(Texture2D[] textures, int keyOffset)
        {
            this.textures = textures;
            this.keyOffset = keyOffset;
        }

        public Texture2D GetTexture(int key)
        {
            //if(key > textures.Count() - 1)
            //{
            //    return textures[textures.Count()-1];
            //}
            return textures[key - keyOffset];
        }
    }
}
