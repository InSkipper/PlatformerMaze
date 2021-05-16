namespace Platformer.Domain
{
    public class Camera
    {
        public float PositionX => Player.PosX;
        public float PositionY => Player.PosY;
        public Map Map;
        public Player Player;

        public Camera(Map map, Player player)
        {
            Map = map;
            Player = player;
        }

        public float OffsetX
        {
            get
            {
                var offset = PositionX - Map.VisibleTilesX / 2f;
                if (offset < 0)
                    return 0;
                if (offset > Map.Width - Map.VisibleTilesX)
                    return Map.Width - Map.VisibleTilesX;
                return offset;
            }
        }


        public float OffsetY
        {
            get
            {
                var offset = PositionY - Map.VisibleTilesY / 2f;
                if (offset < 0)
                    return 0;
                if (offset > Map.Height - Map.VisibleTilesY)
                    return Map.Height - Map.VisibleTilesY;
                return offset;
            }
        }

        public float TileOffsetX => (OffsetX - (int)OffsetX) * Map.TileSize;
        public float TileOffsetY => (OffsetY - (int)OffsetY) * Map.TileSize;
    }
}