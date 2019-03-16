using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using PropertyChanged;
using System.ComponentModel;

namespace CollideGameTestClient.Entities
{
    public class EntityBase : INotifyPropertyChanged
    {
        public EntityBase(Point start, double radius, Vector vector, Path path)
        {
            Point = start;
            HitboxRadius = radius;
            Vector = vector;
            Path = path;
            Collisions = new List<EntityBase>();
        }
        public double X { get { return Point.X; } }
        public double Y { get { return Point.Y; } }
        public Point Point { get; set; }        
        public Vector Vector { get; set; }
        public Geometry Geometry { get; set; }
        public Path Path { get; set; }
        public double HitboxRadius { get; set; }
        public List<EntityBase> Collisions { get; set; }
        public EntityBase Collision { get; set; }
        public delegate void ChangeViewEventHandler(object sender, object FactoryParameter = null);

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
