using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BDC_V1.Utils
{
    public static class MakeBitmapTransparent
    {
        public static BitmapSource MakeTransparent(string resource, System.Drawing.Color? backColor = null)
        {
            var image  = new BitmapImage(new Uri(resource));
            var bitmap = image.ToBitmap();
            bitmap.MakeTransparent(backColor?? System.Drawing.Color.White);
            return bitmap.ToBitmapSource();
        }

        public static BitmapSource MakeTransparent(string resource, Size backgroundPixel)
        {
            var image  = new BitmapImage(new Uri(resource));
            var bitmap = image.ToBitmap();
            bitmap.MakeTransparent(bitmap.GetPixel((int)backgroundPixel.Width, (int)backgroundPixel.Height));
            return bitmap.ToBitmapSource();
        }
    }
}
