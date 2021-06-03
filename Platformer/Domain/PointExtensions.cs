using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Platformer.Domain
{
    public static class PointExtensions
    {
        private static int speed = 1;

        private static readonly HashSet<SizeF> offsets = new HashSet<SizeF>
        {
            new SizeF(0, -speed),
            new SizeF(0, speed),
            new SizeF(speed, 0),
            new SizeF(-speed, 0),
            new SizeF(-speed, -speed),
            new SizeF(-speed, speed),
            new SizeF(speed, -speed),
            new SizeF(speed, speed),
        };

        public static IEnumerable<PointF> Neighbors(this PointF point)
        {
            return offsets
                .Select(offset => point + offset);
        }
    }
}