using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollideGameTestClient.Entities
{
    public class GameArea
    {
        public GameArea(double width, double height)
        {
            Size = new Size(width, height);

            Width = width;
            Height = height;

            Sides = new Rect(Size);
        }

        public Rect Sides { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Size Size { get; set; }
    }
}
