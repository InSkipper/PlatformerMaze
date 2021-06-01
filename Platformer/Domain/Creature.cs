using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            PosX = map.Start.X;
            PosY = map.Start.Y;
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
            var shortestPath = FindShortestPath(Map, new PointF(PosX, PosY),
                    new PointF(TargetX, TargetY), deltaTime).FirstOrDefault();
            if (shortestPath != null)
            {
                var point = shortestPath.Reverse().Skip(1).FirstOrDefault();
                if (point.X == 0 && point.Y == 0)
                {
                    VelocityX = 0;
                    VelocityY = 0;
                }
                else
                {
                    VelocityX = point.X - PosX;
                    VelocityY = point.Y - PosY;
                }
            }

            MakeMove(VelocityX * deltaTime * 3, VelocityY * deltaTime * 3);
        }

        public IEnumerable<SinglyLinkedList<PointF>> FindShortestPath(Map map, PointF start, PointF end, float deltaTime)
        {
            var queue = new Queue<SinglyLinkedList<PointF>>();
            queue.Enqueue(new SinglyLinkedList<PointF>(start));
            var visited = new HashSet<PointF>();

            while (queue.Count != 0)
            {
                var list = queue.Dequeue();
                var point = list.Value;
                if (!map.InBounds(point.X, point.Y)
                || visited.Contains(new PointF(point.X, point.Y))
                ||
                /*map[point.X, point.Y] == TileType.Wall*/
                list.Previous != null &&
                    IsSolid((point.X - list.Previous.Value.X) * deltaTime, (point.Y - list.Previous.Value.Y) * deltaTime, list.Previous.Value)
                )
                    continue;

                if (Math.Abs(end.X - point.X) <= 0.5 && Math.Abs(end.Y - point.Y) <= 0.5)
                    yield return list;
                visited.Add(new PointF(point.X, point.Y));
                foreach (var nextPoint in point.Neighbors())
                    queue.Enqueue(new SinglyLinkedList<PointF>(nextPoint, list));
            }
        }

        private bool IsSolid(float velocityX, float velocityY, PointF pos)
        {
            var newPosX = pos.X + velocityX;
            var newPosY = pos.Y + velocityY;

            if (velocityX <= 0)
            {
                if (Map.IsSolid(newPosX, pos.Y)
                    || Map.IsSolid(newPosX, pos.Y + 0.9f))
                    return true;
            }
            else if (Map.IsSolid(newPosX + 1, pos.Y)
                     || Map.IsSolid(newPosX + 1, pos.Y + 0.9f))
                return true;

            if (velocityY <= 0)
            {
                if (Map.IsSolid(newPosX, newPosY)
                    || Map.IsSolid(newPosX + 0.9f, newPosY))
                    return true;
            }
            else if (Map.IsSolid(newPosX, newPosY + 1)
                     || Map.IsSolid(newPosX + 0.9f, newPosY + 1))
                return true;

            return false;
        }
    }
}