using System.Drawing;

namespace Platformer.Domain
{
    public class Block
    {
        public readonly TileType TileType;

        public Block(TileType tileType)
        {
            TileType = tileType;
        }

        public Point? Position { get; set; }
    }
}