﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace CollideGameTestClient.Entities
{
    public class Circle : EntityBase
    {


        public Circle(Point start,double radius, Vector vector, Path path):base(start,radius,vector,path)
        {
            Radius = radius;
            Geometry = new EllipseGeometry(new Point(0,0), Radius, Radius);
            path.Data = Geometry;
        }
        public double Radius { get; set; }

    }
}
