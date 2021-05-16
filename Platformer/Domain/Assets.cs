using System.Collections.Generic;
using System.Drawing;

namespace Platformer.Domain
{
    public class Assets
    {
        private static Dictionary<string, Bitmap> assetNameToBitmap = new Dictionary<string, Bitmap>();

        public static Dictionary<string, Bitmap> LoadAssets()
        {
            assetNameToBitmap["wall"] = new Bitmap(@"C:\Users\User\Desktop\Walls.png");
            assetNameToBitmap["grass"] = new Bitmap(@"C:\Users\User\Desktop\Grass.png");

            return assetNameToBitmap;
        }
    }
}