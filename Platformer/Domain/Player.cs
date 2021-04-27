namespace Platformer.Domain
{
    public class Player
    {
        public float PosX = 0;
        public float PosY = 0;
        public float VelocityX = 0;
        public float VelocityY = 0;
        private const float MaxVelocityX = 10f;
        private const float MaxVelocityY = 100f;
        private float deltaTime = 1 / 60f;

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
                if (Game.CurrentMap.GetTile(newPosX, PosY) == '#'
                    || Game.CurrentMap.GetTile(newPosX, PosY + 0.9f) == '#')
                {
                    PosX = (int)PosX + 1;
                    VelocityX = 0;
                }
            }
            else if (Game.CurrentMap.GetTile(newPosX + 1, PosY) == '#'
                     || Game.CurrentMap.GetTile(newPosX + 1, PosY + 0.9f) == '#')
            {
                newPosX = (int)PosX;
                VelocityX = 0;
            }

            PosX += newPosX;
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
                if (Game.CurrentMap.GetTile(PosX, newPosY) == '#'
                    || Game.CurrentMap.GetTile(VelocityX + 0.9f, newPosY) == '#')
                {
                    PosY = (int)PosY + 1;
                    VelocityY = 0;
                }
            }
            else if (Game.CurrentMap.GetTile(PosX, newPosY + 1) == '#'
                     || Game.CurrentMap.GetTile(PosX + 0.9f, newPosY + 1) == '#')
            {
                newPosY = (int)PosY;
                VelocityY = 0;
            }

            PosY += newPosY;
        }
    }
}