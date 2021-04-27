using System;
using System.Drawing;

namespace Platformer.Domain
{
    public class Map
    {
        public readonly TileType[,] Level;
        public readonly Point InitialPosition;


        public Map(TileType[,] level, Point initialPosition)
        {
            Level = level;
            InitialPosition = initialPosition;
        }

        public static Map FromText(string text)
        {
            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return FromLines(lines);
        }

        public static Map FromLines(string[] lines)
        {
            var level = new TileType[lines[0].Length, lines.Length];
            var initialPosition = Point.Empty;
            for (var y = 0; y < lines.Length; y++)
                for (var x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            level[x, y] = TileType.Wall;
                            break;
                        case 'P':
                            initialPosition = new Point(x, y);
                            break;
                        default:
                            level[x, y] = TileType.Ground;
                            break;
                    }
                }
            return new Map(level, initialPosition);
        }

        public bool InBounds(Point point)
        {
            var bound = new Rectangle(0, 0, Level.GetLength(0), Level.GetLength(1));
            return bound.Contains(point);
        }
    }
}