namespace Platformer.Domain
{
    public readonly struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format($"{X} {Y}");
        }
    }
}