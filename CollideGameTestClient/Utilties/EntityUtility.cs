using CollideGameTestClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CollideGameTestClient.Utilties
{
    public static class EntityUtility
    {
        public static GameArea GameArea { get; set; }

        static EntityUtility()
        {
        }

        public static void CheckForCollision(List<EntityBase> e)
        {
            var entities = new List<EntityBase>(e);
            foreach (var entity in entities)
            {
                foreach (var entity2 in entities)
                {
                    if (entity == entity2)
                        continue;

                    var distance = Math.Abs(Point.Subtract(entity.Point, entity2.Point).Length);
                    if (distance > entity.HitboxRadius + entity2.HitboxRadius)
                    {
                        RemoveCollision(entity, entity2);
                        continue;
                    }
                    if (AreColliding(entity, entity2))
                        continue;
                    if (distance <= entity.HitboxRadius + entity2.HitboxRadius) //collision
                        HandleCollision(entity, entity2);

                }
                HandleWallCollision(entity);
            }
        }
        public static void RemoveCollision(EntityBase a, EntityBase b)
        {
            if (a.Collisions.Contains(b))
                a.Collisions.Remove(b);

            if (b.Collisions.Contains(a))
                b.Collisions.Remove(a);

        }
        public static bool AreColliding(EntityBase a, EntityBase b)
        {
            if (a.Collisions.Contains(b))
                return true;

            if (b.Collisions.Contains(a))
                return true;

            return false;
        }
        public static void AddCollision(EntityBase a, EntityBase b)
        {
            a.Collisions.Add(b);
            b.Collisions.Add(a);
        }
        public static void HandleWallCollision(EntityBase entity)
        {

            if (entity.Point.X >= GameArea.Width - entity.HitboxRadius)
            {
                entity.Vector = new Vector(entity.Vector.X * -1, entity.Vector.Y);
            }
            if (entity.Point.X <= 0 + entity.HitboxRadius)
            {
                entity.Vector = new Vector(entity.Vector.X * -1, entity.Vector.Y);
            }
            if (entity.Point.Y >= GameArea.Height - entity.HitboxRadius)
            {
                entity.Vector = new Vector(entity.Vector.X, entity.Vector.Y * -1);
            }
            if (entity.Point.Y <= 0 + entity.HitboxRadius)
            {
                entity.Vector = new Vector(entity.Vector.X, entity.Vector.Y * -1);
            }
        }

        public static void HandleCollision(EntityBase one, EntityBase two)
        {
            AddCollision(one, two);
            var d = one.Point - two.Point;
            var distance = d.Length;
            d.Normalize();
            var dotprod = Vector.Multiply(d, one.Vector - two.Vector);
            var impulse = d * dotprod;

            one.Vector -= impulse;
            two.Vector += impulse;

        }

        public static void MoveEntity(EntityBase entity, Vector vector)
        {
            entity.Vector = vector;
        }

        public static void MoveEntities(List<EntityBase> e)
        {
            var entities = new List<EntityBase>(e);
            foreach (var entity in entities)
            {

                entity.Point = new Point(entity.X + entity.Vector.X, entity.Y + entity.Vector.Y);
            }
        }
        public static EntityBase GetRandomEntity()
        {
            var path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            path.Fill = Brushes.Blue;

            var minVel = .001;
            Random r = new Random();
            

            double width = r.NextDouble() * (MaxWidth - 1) + 1;
            double height = r.NextDouble() * (MaxHeight - 1) + 1;
            double radius = r.NextDouble() * (MaxRadius - 1) + 1;
            double xVelocity = r.NextDouble() * (MaxSpeed - minVel) + minVel;
            double yVelocity = r.NextDouble() * (MaxSpeed - minVel) + minVel;

            double xStart = r.NextDouble() * (GameArea.Width - 2 * radius) + radius;
            double yStart = r.NextDouble() * (GameArea.Height - 2 * radius) + radius;
            var start = new Point(xStart, yStart);

            EntityBase entityBase;
            var t = r.Next(0,2);
            if (t == 0)
                entityBase = new Circle(start, radius, new Vector(xVelocity, yVelocity), path);
            else
                entityBase = new Square(start, width/2, new Vector(xVelocity, yVelocity), path, width);

            return entityBase;
        }

        public static bool TryAddEntity(EntityBase entity, List<EntityBase> entities)
        {
            var entityBases = new List<EntityBase>(entities);
            bool noCollision = true;
            foreach (var e in entityBases)
            {
                var distance = Math.Abs(Point.Subtract(entity.Point, e.Point).Length);
                if (distance > entity.HitboxRadius + e.HitboxRadius)
                {
                    continue;
                }
                if (distance <= entity.HitboxRadius + e.HitboxRadius) //collision
                {
                    noCollision = false;
                    break;
                }
            }

            return noCollision;
        }


        public static double MaxWidth { get; set; }
        public static double MaxHeight { get; set; }
        public static double MaxRadius { get; set; }
        public static double MaxSpeed { get; set; }

    }
}
