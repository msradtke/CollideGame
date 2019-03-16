using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CollideGameTestClient.Entities
{
    public class Square : EntityBase
    {
        public Square(Point start, double radius, Vector vector, Path path,double width) : base(start, radius, vector, path)
        {
            Geometry = new RectangleGeometry(new Rect { Width = width, Height = width });
            path.Data = Geometry;
        }
    }
}
