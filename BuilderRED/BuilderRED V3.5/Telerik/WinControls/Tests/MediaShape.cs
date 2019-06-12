// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Tests.MediaShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.Tests
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class MediaShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      Size size = new Size(50, 50);
      if (bounds.Height < 50)
        size = new Size(bounds.Height / 2, bounds.Height / 2);
      Rectangle rectangle = bounds;
      GraphicsPath graphicsPath = new GraphicsPath();
      int height = rectangle.Height - 3 * size.Height / 4;
      if (height <= 0)
        height = 1;
      graphicsPath.AddArc(rectangle.Left, rectangle.Top + size.Height / 4, size.Width, height, 90f, 180f);
      graphicsPath.AddLine((float) (rectangle.Left + size.Width / 2), (float) (rectangle.Top + size.Height / 4), (float) rectangle.Left + (float) (2 * rectangle.Width) / 5f + (float) (rectangle.Width / 20), (float) (rectangle.Top + size.Height / 4));
      graphicsPath.AddArc((float) rectangle.Left + (float) (2 * rectangle.Width) / 5f + (float) (rectangle.Width / 20), (float) rectangle.Top, (float) rectangle.Width / 10f, (float) (size.Height / 2), 180f, 180f);
      graphicsPath.AddLine((float) rectangle.Left + (float) (3 * rectangle.Width) / 5f, (float) (rectangle.Top + size.Height / 4), (float) (rectangle.Right - size.Width / 2), (float) (rectangle.Top + size.Height / 4));
      graphicsPath.AddArc(rectangle.Right - size.Width, rectangle.Top + size.Height / 4, size.Width, height, 270f, 180f);
      graphicsPath.AddLine((float) (rectangle.Right - size.Width / 2), (float) (rectangle.Bottom - size.Height / 2), (float) rectangle.Left + (float) (3 * rectangle.Width) / 5f, (float) (rectangle.Bottom - size.Height / 2));
      graphicsPath.AddArc((float) rectangle.Left + (float) (2 * rectangle.Width) / 5f + (float) (rectangle.Width / 20), (float) (rectangle.Bottom - 3 * size.Height / 4), (float) rectangle.Width / 10f, (float) (size.Height / 2), 0.0f, 180f);
      graphicsPath.AddLine((float) (rectangle.Left + size.Width / 2), (float) (rectangle.Bottom - size.Height / 2), (float) rectangle.Left + (float) (2 * rectangle.Width) / 5f + (float) (rectangle.Width / 20), (float) (rectangle.Bottom - size.Height / 2));
      graphicsPath.CloseAllFigures();
      return graphicsPath;
    }
  }
}
