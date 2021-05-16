namespace Platformer.Domain
{
    public class Player : ICreature
    {
        public float PosX;
        public float PosY;
        public float VelocityX = 0;
        public float VelocityY = 0;
        public Map Map { get; set; }
        public bool IsDead;

        public Player(Map map)
        {
            PosX = map.InitialPosition.X;
            PosY = map.InitialPosition.Y;
            Map = map;
        }

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

            //Если карта не закрыта, то выходит за пределы массива
            //if (Map.Level[(int)newPosX, (int)newPosY] == TileType.Spike
            //|| Map.Level[(int)newPosX + 1, (int)newPosY] == TileType.Spike
            //|| Map.Level[(int)newPosX, (int)newPosY + 1] == TileType.Spike
            //|| Map.Level[(int)newPosX + 1, (int)newPosY + 1] == TileType.Spike)
            //    IsDead = true;

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
}