using Avalonia.Media.Imaging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosinka.Helpers
{
    internal static class ImageHelper
    {
        public static Bitmap BackOfCard;
        static ImageHelper()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string generalImagePath = Path.Combine("..", "..", "..", "..", "Kosinka", "Assets");
            BackOfCard = new Bitmap(Path.Combine(generalImagePath, "card.png"));
        }
    }
}
