using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Platformer.Domain
{
    public class Creature
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public Map Map { get; set; }
        public bool IsDead;

        public void MakeMove(float velocityX, float velocityY)
        {
            var newPosX = PosX + velocityX;
            var newPosY = PosY + velocityY;

            if (velocityX <= 0)
            {
                if (Map.IsSolid(newPosX, PosY)
                    || Map.IsSolid(newPosX, PosY + 0.9f))
                {
                    newPosX = (int)newPosX + 1;
                }
            }
            else if (Map.IsSolid(newPosX + 1, PosY)
                     || Map.IsSolid(newPosX + 1, PosY + 0.9f))
            {
                newPosX = (int)newPosX;
            }

            if (velocityY <= 0)
            {
                if (Map.IsSolid(newPosX, newPosY)
                    || Map.IsSolid(newPosX + 0.9f, newPosY))
                {
                    newPosY = (int)newPosY + 1;
                }
            }
            else if (Map.IsSolid(newPosX, newPosY + 1)
                     || Map.IsSolid(newPosX + 0.9f, newPosY + 1))
            {
                newPosY = (int)newPosY;
            }

            PosX = newPosX;
            PosY = newPosY;
        }

        public void MakeMove()
        {
            MakeMove(VelocityX, VelocityY);
        }

        public void MakeMove(float deltaTime)
        {
            MakeMove(VelocityX * deltaTime, VelocityY * deltaTime);
        }
    }

    public class Player : Creature
    {
        public Player(Map map)
        {
            PosX = map.InitialPosition.X;
            PosY = map.InitialPosition.Y;
            Map = map;
        }
    }

    public class Enemy : Creature
    {
        public float TargetX { get; set; }
        public float TargetY { get; set; }

        public Enemy(float posX, float posY)
        {
            PosX = posX;
            PosY = posY;
        }

        public void MoveToTarget(float deltaTime)
        {
            //if (Math.Abs(TargetX - PosX) > 1e-2)
            //    VelocityX = (TargetX - PosX) /
            //                (float)Math.Sqrt((TargetX - PosX) *
            //                                 (TargetX - PosX));
            //else VelocityX = 0;
            //if (Math.Abs(TargetY - PosY) > 1e-2)
            //    VelocityY = (TargetY - PosY) /
            //                (float)Math.Sqrt((TargetY - PosY) *
            //                                 (TargetY - PosY));
            //else VelocityY = 0;
            var shortestPath = FindShortestPath(Map, new PointF(PosX, PosY),
                new[] { new Point((int)TargetX, (int)TargetY) }).FirstOrDefault();
            if (shortestPath != null)
            {
                VelocityX = shortestPath.Reverse().Skip(1).FirstOrDefault().X - PosX;
                VelocityY = shortestPath.Reverse().Skip(1).FirstOrDefault().Y - PosY;
            }

            else
            {
                VelocityX = 0;
                VelocityY = 0;
            }
            MakeMove(VelocityX * deltaTime * 2, VelocityY * deltaTime * 2);
        }

        public IEnumerable<SinglyLinkedList<PointF>> FindShortestPath(Map map, PointF start, Point[] checkPoints)
        {
            var queue = new Queue<SinglyLinkedList<PointF>>();
            queue.Enqueue(new SinglyLinkedList<PointF>(start));
            var checkPointsSet = checkPoints.ToHashSet();
            var visited = new HashSet<Point>();

            while (queue.Count != 0)
            {
                var list = queue.Dequeue();
                var point = list.Value;
                if (!map.InBounds((int)point.X, (int)point.Y)
                || visited.Contains(new Point((int)point.X, (int)point.Y))
                || map[(int)point.X, (int)point.Y] == TileType.Wall)
                    continue;

                if (checkPointsSet.Contains(new Point((int)point.X, (int)point.Y)))
                    yield return list;
                visited.Add(new Point((int)point.X, (int)point.Y));
                foreach (var nextPoint in point.Neighbors())
                    queue.Enqueue(new SinglyLinkedList<PointF>(nextPoint, list));
            }
        }
    }



    public static class PointExtensions
    {
        public static IEnumerable<PointF> Neighbors(this PointF point)
        {
            return offsets
                .Select(offset => point + offset);
        }

        private static readonly HashSet<SizeF> offsets = new HashSet<SizeF>
        {
            new SizeF(0, -1),
            new SizeF(1, 0),
            new SizeF(-1, 0),
            new SizeF(1, 1),
            new SizeF(1, -1),
            new SizeF(-1, 1),
            new SizeF(-1, -1),
            new SizeF(0, 1),
        };
    }

    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public readonly T Value;
        public readonly SinglyLinkedList<T> Previous;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return Value;
            var current = Previous;
            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}