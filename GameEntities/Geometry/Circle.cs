using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhantomEngine.Geometry
{
    public struct Circle
    {
        /// <summary>
        /// Center position of the circle.
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// Radius of the circle.
        /// </summary>
        public float Radius;

        /// <summary>
        /// Constructs a new circle.
        /// </summary>
        public Circle(Vector2 position, float radius)
        {
            Center = position;
            Radius = radius;
        }

        public bool PointIntersects(Vector2 point)
        {
            Vector2 direction = Center - point;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < Radius * Radius));
        }
    }
}
