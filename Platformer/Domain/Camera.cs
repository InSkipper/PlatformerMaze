using System.Windows.Forms;

namespace Platformer.Domain
{
    public class Camera
    {
        public float PositionX => Creature.PosX;
        public float PositionY => Creature.PosY;
        public Map Map;
        public Creature Creature;

        public Camera(Map map, Creature creature)
        {
            Map = map;
            Creature = creature;
        }

        public float OffsetX
        {
            get
            {
                var offset = PositionX - VisibleTilesX / 2f;
                if (offset < 0)
                    return 0;
                if (offset > Map.Width - VisibleTilesX)
                    return Map.Width - VisibleTilesX;
                return offset;
            }
        }


        public float OffsetY
        {
            get
            {
                var offset = PositionY - VisibleTilesY / 2f;
                if (offset < 0)
                    return 0;
                if (offset > Map.Height - VisibleTilesY)
                    return Map.Height - VisibleTilesY;
                return offset;
            }
        }
        public int VisibleTilesX => 600 / Map.TileSize;
        public int VisibleTilesY => 400 / Map.TileSize;

        public float TileOffsetX => (OffsetX - (int)OffsetX) * Map.TileSize;
        public float TileOffsetY => (OffsetY - (int)OffsetY) * Map.TileSize;
    }
}