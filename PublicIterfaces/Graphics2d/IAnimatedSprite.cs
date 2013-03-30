using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PublicIterfaces.Graphics2d
{
    public interface IAnimatedSprite : ISprite
    {
        void SetAnimationIndex(int index);
        void IncreaseFrame();
        float GetFrameTime();
    }
}
