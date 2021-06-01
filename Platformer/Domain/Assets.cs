using System.Collections.Generic;
using System.Drawing;

namespace Platformer.Domain
{
    public class Assets
    {
        private static readonly Dictionary<string, Bitmap> assetNameToBitmap = new Dictionary<string, Bitmap>();

        public static Dictionary<string, Bitmap> LoadAssets()
        {
            assetNameToBitmap["wall"]  = new Bitmap(@"Sprites\Walls.png");
            assetNameToBitmap["grass"] = new Bitmap(@"Sprites\Grass.png");
            assetNameToBitmap["creature"] = new Bitmap(@"C:\Users\User\Desktop\TestCarecter-export.png");

            return assetNameToBitmap;
        }
    }
}