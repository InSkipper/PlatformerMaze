using System;
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
            if (Math.Abs(TargetX - PosX) > 1e-2)
                VelocityX = (TargetX - PosX) /
                            (float)Math.Sqrt((TargetX - PosX) *
                                             (TargetX - PosX));
            else VelocityX = 0;
            if (Math.Abs(TargetY - PosY) > 1e-2)
                VelocityY = (TargetY - PosY) /
                            (float)Math.Sqrt((TargetY - PosY) *
                                             (TargetY - PosY));
            else VelocityY = 0;
            MakeMove(VelocityX * deltaTime * 2, VelocityY * deltaTime * 2);
        }
    }
}