using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhantomEngine.Helpers
{
    static class MathUtil
    {
        public static bool RectIntersect(Rectangle rectangle1, Rectangle rectangle2)
        {
            const int BUFFER = 0;
            return ((rectangle1.Left < rectangle2.Right - BUFFER)
              && (rectangle1.Right - BUFFER > rectangle2.Left)
              && (rectangle1.Top < rectangle2.Bottom - BUFFER)
              && (rectangle1.Top + rectangle1.Height - BUFFER > rectangle2.Top));
        }

        public static bool PointIntersect(Rectangle rectangle1, Vector2 point)
        {
            return ((rectangle1.Left < point.X)
              && (rectangle1.Left + rectangle1.Width > point.X)
              && (rectangle1.Top < point.Y)
              && (rectangle1.Top + rectangle1.Height > point.Y));
        }
    }
}
