// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ImageHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  public static class ImageHelper
  {
    private const int OleHeaderSize = 78;

    public static unsafe void ApplyAlpha(Bitmap bitmap, float fAlpha)
    {
      int width = bitmap.Width;
      int height = bitmap.Height;
      BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
      byte num = (byte) ((double) fAlpha * (double) byte.MaxValue);
      byte* numPtr = (byte*) ((IntPtr) (void*) bitmapdata.Scan0 + new IntPtr(3));
      for (int index1 = 0; index1 < height; ++index1)
      {
        for (int index2 = 0; index2 < width; ++index2)
        {
          *numPtr = (byte) ((int) *numPtr * (int) num / (int) byte.MaxValue);
          numPtr += 4;
        }
      }
      bitmap.UnlockBits(bitmapdata);
    }

    public static void ApplyMask(Bitmap bitmap, Brush brush)
    {
      Rectangle rect = new Rectangle(Point.Empty, bitmap.Size);
      using (Bitmap bitmap1 = new Bitmap(bitmap.Size.Width, bitmap.Size.Height))
      {
        using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
          graphics.FillRectangle(brush, rect);
        BitmapData bitmapdata1 = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
        BitmapData bitmapdata2 = bitmap1.LockBits(rect, ImageLockMode.ReadOnly, bitmap1.PixelFormat);
        for (int index1 = 0; index1 < bitmap1.Height; ++index1)
        {
          for (int index2 = 0; index2 < bitmap1.Width; ++index2)
          {
            byte val = Marshal.ReadByte(bitmapdata2.Scan0, bitmapdata2.Stride * index1 + 4 * index2);
            Marshal.WriteByte(bitmapdata1.Scan0, bitmapdata1.Stride * index1 + 4 * index2 + 3, val);
          }
        }
        bitmap.UnlockBits(bitmapdata1);
        bitmap1.UnlockBits(bitmapdata2);
      }
    }

    public static Bitmap Crop(Bitmap image, Rectangle cropRectangle)
    {
      int width = cropRectangle.Width;
      int height = cropRectangle.Height;
      int x = cropRectangle.X;
      int y = cropRectangle.Y;
      Bitmap bitmap = new Bitmap(width, height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.DrawImage((Image) image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
      }
      return bitmap;
    }

    public static Region RegionFromBitmap(Bitmap bmp, Color transparent)
    {
      GraphicsPath bitmapRegionPath = ImageHelper.GetBitmapRegionPath(bmp, transparent);
      Region region = new Region(bitmapRegionPath);
      bitmapRegionPath.Dispose();
      return region;
    }

    public static GraphicsPath GetBitmapRegionPath(Bitmap bmp, Color transparent)
    {
      int width = bmp.Width;
      int height = bmp.Height;
      GraphicsPath graphicsPath = new GraphicsPath();
      int argb1 = transparent.ToArgb();
      for (int y = 0; y < height; ++y)
      {
        for (int x1 = 0; x1 < width; ++x1)
        {
          int argb2 = bmp.GetPixel(x1, y).ToArgb();
          if (argb2 != argb1)
          {
            int x2 = x1;
            while (x1 < width && argb2 != argb1)
              ++x1;
            graphicsPath.AddRectangle(new Rectangle(x2, y, x1 - x2, 1));
          }
        }
      }
      return graphicsPath;
    }

    public static bool PointInRegion(Region region, Point client)
    {
      Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
      Graphics g = Graphics.FromImage((Image) bitmap);
      IntPtr hrgn = region.GetHrgn(g);
      bool flag = NativeMethods.PtInRegion(hrgn, client.X, client.Y);
      region.ReleaseHrgn(hrgn);
      g.Dispose();
      bitmap.Dispose();
      return flag;
    }

    public static Image GetImageFromBytes(byte[] bytes)
    {
      if (bytes == null || bytes.Length == 0)
        return (Image) null;
      MemoryStream memoryStream = (MemoryStream) null;
      try
      {
        int count;
        int index;
        if (ImageHelper.IsOleContainer(bytes))
        {
          count = bytes.Length - 78;
          index = 78;
        }
        else
        {
          count = bytes.Length;
          index = 0;
        }
        memoryStream = new MemoryStream(bytes, index, count);
        return (Image) new Bitmap(Image.FromStream((Stream) memoryStream));
      }
      catch
      {
        return (Image) null;
      }
      finally
      {
        memoryStream?.Close();
      }
    }

    public static byte[] GetBytesFromImage(Image image)
    {
      if (image == null)
        return (byte[]) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        image.Save((Stream) memoryStream, image.RawFormat);
        return memoryStream.ToArray();
      }
    }

    private static bool IsOleContainer(byte[] imageData)
    {
      if (imageData != null && imageData[0] == (byte) 21)
        return imageData[1] == (byte) 28;
      return false;
    }

    public static Bitmap BitmapInvertColors(Bitmap original)
    {
      Bitmap bitmap = new Bitmap((Image) original);
      BitmapData bitmapdata1 = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
      int length = bitmapdata1.Stride * bitmapdata1.Height;
      byte[] numArray = new byte[length];
      Marshal.Copy(bitmapdata1.Scan0, numArray, 0, length);
      bitmap.UnlockBits(bitmapdata1);
      for (int index = 0; index < length; index += 4)
      {
        numArray[index] = (byte) ((uint) byte.MaxValue - (uint) numArray[index]);
        numArray[index + 1] = (byte) ((uint) byte.MaxValue - (uint) numArray[index + 1]);
        numArray[index + 2] = (byte) ((uint) byte.MaxValue - (uint) numArray[index + 2]);
      }
      BitmapData bitmapdata2 = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
      Marshal.Copy(numArray, 0, bitmapdata2.Scan0, length);
      bitmap.UnlockBits(bitmapdata2);
      return bitmap;
    }

    public static Color HslToRgb(double h, double s, double l, double a)
    {
      if (a > 1.0)
        a = 1.0;
      double num1;
      double num2 = num1 = l;
      double num3 = num1;
      double num4 = num1;
      double num5 = l <= 0.5 ? l * (1.0 + s) : l + s - l * s;
      if (num5 > 0.0)
      {
        h *= 6.0;
        double num6 = l + l - num5;
        double num7 = (num5 - num6) / num5;
        int num8 = (int) h;
        double num9 = h - (double) num8;
        double num10 = num5 * num7 * num9;
        double num11 = num6 + num10;
        double num12 = num5 - num10;
        switch (num8)
        {
          case 0:
            num4 = num5;
            num3 = num11;
            num2 = num6;
            break;
          case 1:
            num4 = num12;
            num3 = num5;
            num2 = num6;
            break;
          case 2:
            num4 = num6;
            num3 = num5;
            num2 = num11;
            break;
          case 3:
            num4 = num6;
            num3 = num12;
            num2 = num5;
            break;
          case 4:
            num4 = num11;
            num3 = num6;
            num2 = num5;
            break;
          case 5:
            num4 = num5;
            num3 = num6;
            num2 = num12;
            break;
        }
      }
      return Color.FromArgb((int) Convert.ToByte(a * (double) byte.MaxValue), (int) Convert.ToByte(num4 * (double) byte.MaxValue), (int) Convert.ToByte(num3 * (double) byte.MaxValue), (int) Convert.ToByte(num2 * (double) byte.MaxValue));
    }

    public static Image ChangeImagePixels(Image image, Color color)
    {
      Bitmap bitmap1 = new Bitmap(image);
      Bitmap bitmap2 = new Bitmap(bitmap1.Width, bitmap1.Height);
      for (int x = 0; x < bitmap1.Width; ++x)
      {
        for (int y = 0; y < bitmap1.Height; ++y)
        {
          Color pixel = bitmap1.GetPixel(x, y);
          Color color1 = color.A != (byte) 0 ? Color.FromArgb((int) pixel.A, color) : Color.Transparent;
          bitmap2.SetPixel(x, y, color1);
        }
      }
      return (Image) bitmap2;
    }

    public static bool AreColorsSame(Color color1, Color color2)
    {
      return (int) color1.A == (int) color2.A && (int) color1.R == (int) color2.R && ((int) color1.G == (int) color2.G && (int) color1.B == (int) color2.B);
    }

    public static bool IsDarkColor(Color color)
    {
      return Math.Sqrt((double) ((int) color.R * (int) color.R) * 0.241 + (double) ((int) color.G * (int) color.G) * 0.691 + (double) ((int) color.B * (int) color.B) * 0.068) < 140.0;
    }
  }
}
