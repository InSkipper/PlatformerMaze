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
            assetNameToBitmap["enemy"] = new Bitmap(@"Sprites\Skeleton.png");
            assetNameToBitmap["player"] = new Bitmap(@"Sprites\Player2.png");
            assetNameToBitmap["exit"] = new Bitmap(@"Sprites\Exit.png");

            return assetNameToBitmap;
        }
    }
}