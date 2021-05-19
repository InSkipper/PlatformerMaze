﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Platformer.Domain
{
    public class Map
    {
        public static int TileSize = 60;
        public readonly Point InitialPosition;
        private readonly TileType[,] level;
        private readonly bool[,] isSolid;

        public Map(TileType[,] level, bool[,] isSolid, Point initialPosition)
        {
            this.level = level;
            this.isSolid = isSolid;
            InitialPosition = initialPosition;
        }

        public TileType this[float x, float y]
        {
            get
            {
                if (InBounds(x, y))
                    return level[(int)x, (int)y];
                return TileType.Wall;
            }
        }

        public int Width => level.GetLength(0);
        public int Height => level.GetLength(1);

        public static Map FromText(string text)
        {
            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return FromLines(lines);
        }

        public static Map FromLines(string[] lines)
        {
            var level = new TileType[lines[0].Length, lines.Length];
            var isSolid = new bool[lines[0].Length, lines.Length];
            var initialPosition = Point.Empty;
            for (var y = 0; y < lines.Length; y++)
                for (var x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            level[x, y] = TileType.Wall;
                            isSolid[x, y] = true;
                            break;
                        case 'P':
                            initialPosition = new Point(x, y);
                            break;
                        case 'A':
                            level[x, y] = TileType.Spike;
                            break;
                        default:
                            level[x, y] = TileType.Ground;
                            break;
                    }
                }
            return new Map(level, isSolid, initialPosition);
        }

        public bool InBounds(float x, float y)
        {
            return x >= 0 && x < level.GetLength(0) && y >= 0 && y < level.GetLength(1);
        }

        public bool IsSolid(float x, float y)
        {
            if (InBounds(x, y))
                return isSolid[(int)x, (int)y];
            return true;
        }
    }
}