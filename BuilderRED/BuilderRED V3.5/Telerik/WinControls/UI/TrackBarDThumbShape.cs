// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarDThumbShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class TrackBarDThumbShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddRectangle(new Rectangle(bounds.X, bounds.Y, bounds.Width, 3 * bounds.Height / 4));
      graphicsPath.CloseFigure();
      graphicsPath.AddPolygon(new Point[3]
      {
        new Point(0, 3 * bounds.Height / 4),
        new Point(bounds.Width / 2, bounds.Height),
        new Point(bounds.Width, 3 * bounds.Height / 4)
      });
      return graphicsPath;
    }
  }
}
