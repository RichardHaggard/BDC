// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TelerikHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls
{
  public class TelerikHelper
  {
    private const string VisualStudio2012DarkTheme = "VisualStudio2012Dark";
    private const string HighContrastBlackTheme = "HighContrastBlack";
    private const string FluentDarkTheme = "FluentDark";
    public static readonly ContentAlignment AnyBottomAlign;
    public static readonly ContentAlignment AnyCenterAlign;
    public static readonly ContentAlignment AnyLeftAlign;
    public static readonly ContentAlignment AnyMiddleAlign;
    public static readonly ContentAlignment AnyRightAlign;
    public static readonly ContentAlignment AnyTopAlign;

    public static bool IsWindows8OrHigher
    {
      get
      {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
          return false;
        if (Environment.OSVersion.Version.Major > 6)
          return true;
        if (Environment.OSVersion.Version.Major == 6)
          return Environment.OSVersion.Version.Minor >= 2;
        return false;
      }
    }

    public static bool IsWindows8OrLower
    {
      get
      {
        if (Environment.OSVersion.Version.Major < 6)
          return true;
        if (Environment.OSVersion.Version.Major == 6)
          return Environment.OSVersion.Version.Minor < 3;
        return false;
      }
    }

    public static bool IsWindows10CreatorsUpdateOrHigher
    {
      get
      {
        if (Environment.OSVersion.Version.Major >= 10)
          return Environment.OSVersion.Version.Build >= 15063;
        return false;
      }
    }

    public static bool IsWindows7
    {
      get
      {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 6)
          return Environment.OSVersion.Version.Minor == 1;
        return false;
      }
    }

    public static bool IsWindows7OrLower
    {
      get
      {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
          return false;
        if (Environment.OSVersion.Version.Major != 6 || Environment.OSVersion.Version.Minor != 1)
          return Environment.OSVersion.Version.Major < 6;
        return true;
      }
    }

    public static bool IsNet47OrHigher
    {
      get
      {
        System.Type type = System.Type.GetType("System.Runtime.Versioning.TargetFrameworkAttribute", false);
        if ((object) type == null)
          return false;
        object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(type, true);
        if (customAttributes == null || customAttributes.Length == 0)
          return false;
        PropertyInfo property = customAttributes[0].GetType().GetProperty("FrameworkName", BindingFlags.Instance | BindingFlags.Public);
        if ((object) property == null)
          return false;
        object obj = property.GetValue(customAttributes[0], (object[]) null);
        if (obj == null)
          return false;
        string[] strArray = obj.ToString().Replace(" ", string.Empty).Replace(".NETFramework,Version=v", string.Empty).Split(new char[1]
        {
          '.'
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 1)
        {
          int result = 0;
          if (int.TryParse(strArray[0], out result) && result > 4)
            return true;
        }
        else if (strArray.Length > 1)
        {
          int result1 = 0;
          int result2 = 0;
          if (int.TryParse(strArray[0], out result1) && int.TryParse(strArray[1], out result2) && (result1 > 4 || result1 == 4 && result2 >= 7))
            return true;
        }
        return false;
      }
    }

    public static bool IsMaterialTheme(string themeName)
    {
      if (!string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName))
        return ThemeResolutionService.ApplicationThemeName.StartsWith("Material");
      return !string.IsNullOrEmpty(themeName) && themeName.StartsWith("Material");
    }

    public static bool IsDarkTheme(string themeName)
    {
      if (!string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName))
        return ThemeResolutionService.ApplicationThemeName == "VisualStudio2012Dark" || ThemeResolutionService.ApplicationThemeName == "HighContrastBlack" || ThemeResolutionService.ApplicationThemeName == "FluentDark";
      return !string.IsNullOrEmpty(themeName) && (themeName == "VisualStudio2012Dark" || themeName == "HighContrastBlack" || themeName == "FluentDark");
    }

    public static bool IsCompatibleDataSource()
    {
      if (Environment.OSVersion.Version.Major <= 6)
        return Environment.OSVersion.Version.Minor < 2;
      return false;
    }

    public static bool ControlIsReallyVisible(Control control)
    {
      control.Invalidate();
      NativeMethods.RECT rect = new NativeMethods.RECT(control.Bounds);
      return NativeMethods.GetUpdateRect(control.Handle, ref rect, false);
    }

    public static string TextWithoutMnemonics(string text)
    {
      if (text == null)
        return (string) null;
      int length = text.IndexOf('&');
      if (length == -1)
        return text;
      StringBuilder stringBuilder = new StringBuilder(text.Substring(0, length));
      for (; length < text.Length; ++length)
      {
        if (text[length] == '&')
          ++length;
        if (length < text.Length)
          stringBuilder.Append(text[length]);
      }
      return stringBuilder.ToString();
    }

    public static bool ContainsMnemonic(string text)
    {
      if (text == null)
        return false;
      int length = text.Length;
      int num = text.IndexOf('&', 0);
      return num >= 0 && num <= length - 2 && text.IndexOf('&', num + 1) == -1;
    }

    public static StringFormat StringFormatForAlignment(ContentAlignment align)
    {
      return new StringFormat()
      {
        Alignment = TelerikAlignHelper.TranslateAlignment(align),
        LineAlignment = TelerikAlignHelper.TranslateLineAlignment(align)
      };
    }

    public static Image ImageFromString(string encodedImage)
    {
      if (string.IsNullOrEmpty(encodedImage))
        return (Image) null;
      MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encodedImage));
      Image original = Image.FromStream((Stream) memoryStream);
      Image image;
      if (original.PixelFormat == PixelFormat.Format24bppRgb)
      {
        image = (Image) new Bitmap(original);
        (image as Bitmap).MakeTransparent(Color.Magenta);
      }
      else
      {
        if (object.Equals((object) original.RawFormat, (object) ImageFormat.Gif))
          return original;
        BitmapData bitmapData = (original as Bitmap).LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, original.PixelFormat);
        image = (Image) new Bitmap((Image) new Bitmap(bitmapData.Width, bitmapData.Height, bitmapData.Stride, PixelFormat.Format32bppArgb, bitmapData.Scan0));
      }
      memoryStream.Dispose();
      original.Dispose();
      return image;
    }

    public static string ImageToString(Image image)
    {
      if (image == null)
        return string.Empty;
      using (MemoryStream memoryStream = new MemoryStream(1024))
      {
        if (object.Equals((object) image.RawFormat, (object) ImageFormat.Gif))
        {
          image.Save((Stream) memoryStream, TelerikHelper.GetImageFormat(image));
        }
        else
        {
          using (Bitmap bitmap = new Bitmap(image))
          {
            try
            {
              bitmap.Save((Stream) memoryStream, TelerikHelper.GetImageFormat(image));
            }
            catch
            {
              bitmap.Save((Stream) memoryStream, ImageFormat.Png);
            }
          }
        }
        return Convert.ToBase64String(memoryStream.ToArray(), 0, (int) memoryStream.Length);
      }
    }

    public static ImageFormat GetImageFormat(Image img)
    {
      ImageFormat rawFormat = img.RawFormat;
      if (TelerikHelper.CodecInfoExists(ImageCodecInfo.GetImageDecoders(), rawFormat) || TelerikHelper.CodecInfoExists(ImageCodecInfo.GetImageEncoders(), rawFormat))
        return rawFormat;
      ImageFormat imageFormat = rawFormat;
      if (rawFormat != ImageFormat.MemoryBmp && rawFormat != ImageFormat.Exif)
        imageFormat = ImageFormat.Bmp;
      return imageFormat;
    }

    private static bool CodecInfoExists(ImageCodecInfo[] codecs, ImageFormat rawFormat)
    {
      int codecInfoIndex = TelerikHelper.GetCodecInfoIndex(codecs, rawFormat);
      return codecInfoIndex >= 0 && codecInfoIndex < codecs.Length;
    }

    private static int GetCodecInfoIndex(ImageCodecInfo[] codecs, ImageFormat rawFormat)
    {
      for (int index = 0; index < codecs.Length; ++index)
      {
        ImageCodecInfo codec = codecs[index];
        string formatDescription = codec.FormatDescription;
        string codecName = codec.CodecName;
        if (rawFormat.Guid == codec.FormatID)
          return index;
      }
      return -1;
    }

    public static RectangleF PerformTopLeftRotation(
      ref RadMatrix matrix,
      RectangleF bounds,
      float angle)
    {
      RadMatrix identity = RadMatrix.Identity;
      RectangleF rectangleF = TelerikHelper.PerformCenteredRotation(ref identity, bounds, angle);
      identity.Translate(bounds.X - rectangleF.X, bounds.Y - rectangleF.Y, MatrixOrder.Append);
      matrix.Multiply(identity, MatrixOrder.Append);
      return new RectangleF(bounds.Location, rectangleF.Size);
    }

    public static RectangleF PerformCenteredRotation(
      ref RadMatrix matrix,
      RectangleF bounds,
      float angle)
    {
      SizeF sz = new SizeF(bounds.Width / 2f, bounds.Height / 2f);
      matrix.RotateAt(angle, PointF.Add(bounds.Location, sz), MatrixOrder.Append);
      return TelerikHelper.GetBoundingRect(bounds, matrix);
    }

    public static Rectangle GetBoundingRect(Rectangle rect, RadMatrix matrix)
    {
      if (matrix.IsIdentity)
        return rect;
      Point[] points = new Point[4]
      {
        new Point(rect.Left, rect.Top),
        new Point(rect.Right, rect.Top),
        new Point(rect.Right, rect.Bottom),
        new Point(rect.Left, rect.Bottom)
      };
      TelerikHelper.TransformPoints(points, matrix.Elements);
      int left = Math.Min(Math.Min(points[0].X, points[1].X), Math.Min(points[2].X, points[3].X));
      int right = Math.Max(Math.Max(points[0].X, points[1].X), Math.Max(points[2].X, points[3].X));
      int top = Math.Min(Math.Min(points[0].Y, points[1].Y), Math.Min(points[2].Y, points[3].Y));
      int bottom = Math.Max(Math.Max(points[0].Y, points[1].Y), Math.Max(points[2].Y, points[3].Y));
      return Rectangle.FromLTRB(left, top, right, bottom);
    }

    public static RectangleF GetBoundingRect(RectangleF rect, RadMatrix matrix)
    {
      PointF[] points = new PointF[4]
      {
        new PointF(rect.Left, rect.Top),
        new PointF(rect.Right, rect.Top),
        new PointF(rect.Right, rect.Bottom),
        new PointF(rect.Left, rect.Bottom)
      };
      TelerikHelper.TransformPoints(points, matrix.Elements);
      float left = Math.Min(Math.Min(points[0].X, points[1].X), Math.Min(points[2].X, points[3].X));
      float right = Math.Max(Math.Max(points[0].X, points[1].X), Math.Max(points[2].X, points[3].X));
      float top = Math.Min(Math.Min(points[0].Y, points[1].Y), Math.Min(points[2].Y, points[3].Y));
      float bottom = Math.Max(Math.Max(points[0].Y, points[1].Y), Math.Max(points[2].Y, points[3].Y));
      return RectangleF.FromLTRB(left, top, right, bottom);
    }

    public static void TransformPoints(Point[] points, float[] elements)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        int x = points[index].X;
        int y = points[index].Y;
        points[index].X = (int) Math.Round((double) x * (double) elements[0] + (double) y * (double) elements[2] + (double) elements[4]);
        points[index].Y = (int) Math.Round((double) x * (double) elements[1] + (double) y * (double) elements[3] + (double) elements[5]);
      }
    }

    public static void TransformPoints(PointF[] points, float[] elements)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        float x = points[index].X;
        float y = points[index].Y;
        points[index].X = (float) ((double) x * (double) elements[0] + (double) y * (double) elements[2]) + elements[4];
        points[index].Y = (float) ((double) x * (double) elements[1] + (double) y * (double) elements[3]) + elements[5];
      }
    }

    public static bool IsNumericType(System.Type type)
    {
      if ((object) type == null || type.IsArray)
        return false;
      switch (System.Type.GetTypeCode(type))
      {
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return true;
        default:
          return false;
      }
    }

    public static bool CanProcessMnemonic(Control control)
    {
      if (!control.Enabled || !control.Visible)
        return false;
      if (control.Parent != null)
        return TelerikHelper.CanProcessMnemonic(control.Parent);
      return true;
    }

    public static bool CanProcessMnemonicNoRecursive(Control control)
    {
      return control.Enabled && control.Visible;
    }

    protected internal static bool IsPseudoMnemonic(char charCode, string text)
    {
      if (!string.IsNullOrEmpty(text) && !WindowsFormsUtils.ContainsMnemonic(text))
      {
        char upper = char.ToUpper(charCode, CultureInfo.CurrentCulture);
        if ((int) char.ToUpper(text[0], CultureInfo.CurrentCulture) == (int) upper || (int) char.ToLower(charCode, CultureInfo.CurrentCulture) == (int) char.ToLower(text[0], CultureInfo.CurrentCulture))
          return true;
      }
      return false;
    }

    public static void SetDropShadow(IntPtr hWnd)
    {
      if (Environment.OSVersion.Version.Major < 5 || Environment.OSVersion.Version.Minor < 1 || (Environment.OSVersion.Platform != PlatformID.Win32NT || !NativeMethods.IsWindow(new HandleRef((object) null, hWnd))))
        return;
      int nIndex = -26;
      int int32 = NativeMethods.GetClassLongPtr(new HandleRef((object) null, hWnd), nIndex).ToInt32();
      if ((int32 & 131072) != 0)
        return;
      int num = int32 + 131072;
      NativeMethods.SetClassLong(new HandleRef((object) null, hWnd), nIndex, (IntPtr) num);
    }

    public static bool IsBottomAligned(ContentAlignment align)
    {
      return (align & TelerikHelper.AnyBottomAlign) != (ContentAlignment) 0;
    }

    public static bool IsRightAligned(ContentAlignment align)
    {
      return (align & TelerikHelper.AnyRightAlign) != (ContentAlignment) 0;
    }

    public static Form CreateOutlineForm()
    {
      return TelerikHelper.CreateOutlineForm((Bitmap) null, SystemColors.Highlight);
    }

    public static RadShimControl CreateOutlineForm1()
    {
      return TelerikHelper.CreateOutlineForm1((Bitmap) null, SystemColors.Highlight);
    }

    public static RadShimControl CreateOutlineForm1(Bitmap image, Color color)
    {
      RadShimControl radShimControl = new RadShimControl();
      try
      {
        radShimControl.Size = new Size(0, 0);
      }
      catch
      {
        radShimControl = new RadShimControl();
      }
      if (color != Color.Empty)
        radShimControl.BackColor = color;
      if (DWMAPI.IsAlphaBlendingSupported)
        radShimControl.Opacity = 0.5;
      else
        radShimControl.BackColor = ControlPaint.LightLight(SystemColors.Highlight);
      radShimControl.Text = "";
      radShimControl.Enabled = false;
      radShimControl.Visible = false;
      if (image != null)
        radShimControl.BackgroundImage = (Image) image;
      radShimControl.CreateControl();
      return radShimControl;
    }

    public static Form CreateOutlineForm(Bitmap image, Color color)
    {
      Form form = new Form();
      try
      {
        form.Size = new Size(0, 0);
      }
      catch
      {
        form = new Form();
      }
      if (color != Color.Empty)
        form.BackColor = color;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.FormBorderStyle = FormBorderStyle.None;
      if (DWMAPI.IsAlphaBlendingSupported)
        form.Opacity = 0.5;
      else
        form.BackColor = ControlPaint.LightLight(SystemColors.Highlight);
      form.ShowInTaskbar = false;
      form.Text = "";
      form.Enabled = false;
      form.Visible = false;
      if (image != null)
        form.BackgroundImage = (Image) image;
      form.CreateControl();
      return form;
    }

    public static void AnimateWindow(
      IntPtr animatedControlHandle,
      int animationDuration,
      int dwFlags)
    {
      NativeMethods.AnimateWindow(animatedControlHandle, animationDuration, (NativeMethods.AnimateWindowFlags) dwFlags);
    }

    public static Stream GetStreamFromResource(Assembly assembly, string resourceUri)
    {
      return assembly.GetManifestResourceStream(resourceUri) ?? (Stream) null;
    }

    public static Control ControlAtPoint(Point point)
    {
      return Control.FromHandle(NativeMethods.WindowFromPoint(point.X, point.Y));
    }

    public static RadControl FindRadControlParent(Control control)
    {
      RadControl radControl = (RadControl) null;
      for (; control.Parent != null; control = control.Parent)
      {
        radControl = control.Parent as RadControl;
        if (radControl != null)
          return radControl;
      }
      return radControl;
    }

    public static Color GetColorAtPoint(Point location)
    {
      Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
      using (Graphics graphics1 = Graphics.FromImage((Image) bitmap))
      {
        using (Graphics graphics2 = Graphics.FromHwnd(IntPtr.Zero))
        {
          IntPtr hdc = graphics2.GetHdc();
          NativeMethods.BitBlt(graphics1.GetHdc(), 0, 0, 1, 1, hdc, location.X, location.Y, 13369376);
          graphics1.ReleaseHdc();
          graphics2.ReleaseHdc();
        }
      }
      return bitmap.GetPixel(0, 0);
    }

    public static bool StringIsNullOrWhiteSpace(string str)
    {
      if (str == null)
        return true;
      for (int index = 0; index < str.Length; ++index)
      {
        if (!char.IsWhiteSpace(str[index]))
          return false;
      }
      return true;
    }

    public static string KeyCodeToUnicode(Keys key)
    {
      byte[] lpKeyState = new byte[(int) byte.MaxValue];
      if (!NativeMethods.GetKeyboardState(lpKeyState))
        return "";
      uint num = (uint) key;
      uint wScanCode = NativeMethods.MapVirtualKey(num, 0U);
      IntPtr keyboardLayout = NativeMethods.GetKeyboardLayout(0U);
      StringBuilder pwszBuff = new StringBuilder();
      NativeMethods.ToUnicodeEx(num, wScanCode, lpKeyState, pwszBuff, 5, 0U, keyboardLayout);
      return pwszBuff.ToString();
    }
  }
}
