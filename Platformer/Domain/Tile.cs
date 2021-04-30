using System.Drawing;

namespace Platformer.Domain
{
    public class Tile
    {
        public readonly int Size;

        public Tile(TileType tileType, int size)
        {
            Size = size;
        }

        public Point? Position { get; set; }
    }
}