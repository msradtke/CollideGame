using CollideGameTestClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollideGameTestClient.Utilties
{
    public static class EntityUtility
    {
        public static GameArea GameArea { get; set; }

        static EntityUtility()
        {
        }

        public static void CheckForCollision(List<EntityBase> entities)
        {
            foreach (var entity in entities)
            {
                HandleWallCollision(entity);
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

            //var angle = Vector.AngleBetween(one.Vector, two.Vector);

            //var vector1 = new Vector(Math.Sin(one.Vector.X), Math.Cos(one.Vector.Y));
            //one.Vector = vector1;
            //one.Vector.Normalize();

            //var vector2 = new Vector(Math.Sin(two.Vector.X), Math.Cos(two.Vector.Y));
            //two.Vector = vector2;
            //two.Vector.Normalize();
            var vector1 = Vector.CrossProduct(one.Vector, two.Vector);
            one.Vector = vector1;
        }

        public static void MoveEntity(EntityBase entity, Vector vector)
        {
            entity.Vector = vector;
        }

        public static void MoveEntities(List<EntityBase> entities)
        {
            foreach (var entity in entities)
            {

                entity.Point = new Point(entity.X + entity.Vector.X, entity.Y + entity.Vector.Y);
            }
        }
    }
}
