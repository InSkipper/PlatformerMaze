namespace Platformer.Domain
{
    public class Player
    {
        public float PosX;
        public float PosY;
        public float VelocityX = 0;
        public float VelocityY = 0;
        private const float MaxVelocityX = 10f;
        private const float MaxVelocityY = 100f;
        private float deltaTime = 1 / 60f;

        public Player(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
        }

        public void MoveHorizontally(float velocity)
        {
            var newPosX = PosX + velocity * deltaTime;
            VelocityX += velocity * deltaTime;
            if (VelocityX > MaxVelocityX)
                VelocityX = MaxVelocityX;
            if (VelocityX < -MaxVelocityX)
                VelocityX = -MaxVelocityX;

            if (velocity <= 0)
            {
                if (Game.CurrentMap.IsSolid(newPosX, PosY)
                    || Game.CurrentMap.IsSolid(newPosX, PosY + 0.9f))
                {
                    newPosX = (int)newPosX + 1;
                    VelocityX = 0;
                }
            }
            else if (Game.CurrentMap.IsSolid(newPosX + 1, PosY)
                     || Game.CurrentMap.IsSolid(newPosX + 1, PosY + 0.9f))
            {
                newPosX = (int)newPosX;
                VelocityX = 0;
            }

            PosX = newPosX;
        }

        public void MoveVertically(float velocity)
        {
            var newPosY = PosY + velocity * deltaTime;
            VelocityY += velocity * deltaTime;
            if (VelocityY > MaxVelocityY)
                VelocityY = MaxVelocityY;
            if (VelocityY < -MaxVelocityY)
                VelocityY = -MaxVelocityY;

            if (velocity <= 0)
            {
                if (Game.CurrentMap.IsSolid(PosX, newPosY)
                    || Game.CurrentMap.IsSolid(PosX + 0.9f, newPosY))
                {
                    newPosY = (int)newPosY + 1;
                    VelocityY = 0;
                }
            }
            else if (Game.CurrentMap.IsSolid(PosX, newPosY + 1)
                     || Game.CurrentMap.IsSolid(PosX + 0.9f, newPosY + 1))
            {
                newPosY = (int)newPosY;
                VelocityY = 0;
            }

            PosY = newPosY;
        }
    }
}