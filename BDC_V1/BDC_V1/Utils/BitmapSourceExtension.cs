using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BDC_V1.Utils
{
    public static class BitmapSourceExtension
    {
        public static Bitmap ToBitmap(this BitmapSource bitmapSource)
        {
            // BitmapImage bitmapSource = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using(var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapSource));
                enc.Save(outStream);

                var bitmap = new Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }
    }
}
