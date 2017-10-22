using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class CollisionDetector
    {
        public static bool Overlaps(IPhysicalObject2D a, IPhysicalObject2D b)
        {
            return (a.X <= b.X + b.Width && a.Y + a.Height >= b.Y && a.Y <= b.Y + b.Height && a.X + a.Width >= b.X) ||
                   (a.Y + a.Height >= b.Y && a.X + a.Width >= b.X && a.X <= b.X + b.Width && a.Y <= b.Y + b.Height) ||
                   (a.X + a.Width >= b.X && a.Y + a.Height >= b.Y && a.Y <= b.Y + b.Height && a.X <= b.X + b.Width) ||
                   (a.Y <= b.Y + b.Height && a.X + a.Width >= b.X && a.X <= b.X + b.Width && a.Y + a.Height >= b.Y);
        }
    }
}
