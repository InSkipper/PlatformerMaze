using System.Windows.Forms;

namespace Platformer.Domain
{
    public class Camera
    {
        public float PositionX => Player.PosX;
        public float PositionY => Player.PosY;
        private int width, height;
        public Map Map;
        public Player Player;

        public Camera(Map map, Player player, int width, int height)
        {
            Map = map;
            Player = player;
            this.width = width;
            this.height = height;
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
        public int VisibleTilesX
        {
            get
            {
                if (Form.ActiveForm != null)
                    return width/ Map.TileSize + 1;
                return 0;
            }
        }

        public int VisibleTilesY
        {
            get
            {
                if (Form.ActiveForm != null)
                    return height / Map.TileSize + 1;
                return 0;
            }
        }

        public float TileOffsetX => (OffsetX - (int)OffsetX) * Map.TileSize;
        public float TileOffsetY => (OffsetY - (int)OffsetY) * Map.TileSize;
    }
}