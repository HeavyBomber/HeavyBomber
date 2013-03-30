using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicIterfaces.BasicGameObjects.Presentation
{
    public interface IAnimatedSpritePresentation : ISpritePresentation
    {
        void SetCurrentSprite(int animationIndex, int frameIndex);
    }
}
