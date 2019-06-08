// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarRThumbShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class TrackBarRThumbShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddPolygon(new Point[3]
      {
        new Point(3 * bounds.Width / 4, 0),
        new Point(bounds.Width, bounds.Height / 2),
        new Point(3 * bounds.Width / 4, bounds.Height)
      });
      graphicsPath.AddRectangle(new Rectangle(0, 0, 3 * bounds.Width / 4, bounds.Height));
      this.MirrorPath(graphicsPath, (RectangleF) bounds);
      return graphicsPath;
    }
  }
}
