using CollideGameTestClient.Entities;
using CollideGameTestClient.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace CollideGameTestClient.Model
{
    public class Game
    {
        public GameArea GameArea { get; set; }
        public List<EntityBase> Entities { get; set; }
        public string StatusMessage { get; set; }
        public Game()
        {
            Initialize();

        }
        void Initialize()
        {
            StatusMessage = "Game Started";
            GameArea = new GameArea(500, 500);
            EntityUtility.GameArea = GameArea;
            Entities = new List<EntityBase>();
            var path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            path.Fill = Brushes.Blue;
            var path2 = new Path();
            path2.Stroke = Brushes.Black;
            path2.StrokeThickness = 1;
            path2.Fill = Brushes.Blue;
            var path3 = new Path();
            path3.Stroke = Brushes.Black;
            path3.StrokeThickness = 1;
            path3.Fill = Brushes.Blue;


            var startPoint = new Point(50, 50);

            testCircle = new Circle(startPoint, 10, new System.Windows.Vector(1,0), path);
            leftTopCircle = new Circle(new Point(15,15), 10, new System.Windows.Vector(0,1), path2);
            bottomRightCircle = new Circle(new Point(250, 250), 10, new System.Windows.Vector(1.5,1), path3);

            Entities.Add(testCircle);
            Entities.Add(leftTopCircle);
            Entities.Add(bottomRightCircle);

            



        }
        Circle testCircle;
        Circle leftTopCircle;
        Circle bottomRightCircle;
        public void Start()
        {
            IsRunning = true;
            int count = 0;

            int frameCount = 0;
            testCircle.Point = new Point(50 + count, 50);
            ++count;
            var startTime = DateTime.UtcNow;
            var prevTime = DateTime.UtcNow;
            int xSwitch = 1;
            int ySwitch = 1;

            while (true)
            {
                var span = DateTime.UtcNow - prevTime;
                var persec = TimeSpan.FromMilliseconds(1000/120);

                if (span >= persec)
                {
                    EntityUtility.MoveEntities(Entities);
                    EntityUtility.CheckForCollision(Entities);
                    //var currentX = testCircle.Vector.X;
                    //testCircle.Vector = Vector.Add(testCircle.Vector, new Vector(1.5*xSwitch,1*ySwitch));
                    prevTime = DateTime.UtcNow;

                }
                
            }
        }
        public bool IsRunning { get; set; }
    }
}
