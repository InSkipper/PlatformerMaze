namespace Platformer.Domain
{
    public class Platform
    {
        public readonly PlatformType PlatformType;
        public readonly Point Size;

        public Platform(Point size, PlatformType platformType)
        {
            Size = size;
            PlatformType = platformType;
        }

        public Point? Position { get; set; }
        public Direction Direction { get; set; } = Direction.Horizontal;
    }
}