using System.Collections.Generic;

namespace HS.Location
{
    public interface ILocationGenerator
    {
        void Generate(out Point basePoint, out List<Point> points);
    }
}