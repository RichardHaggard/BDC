// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HeartShape
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
  public class HeartShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddArc(new Rectangle(bounds.X, bounds.Y, bounds.Width / 2, bounds.Height / 2), 150f, 210f);
      graphicsPath.AddArc(new Rectangle(bounds.X + bounds.Width / 2, bounds.Y, bounds.Width / 2, bounds.Height / 2), 180f, 210f);
      graphicsPath.AddLine(graphicsPath.GetLastPoint(), (PointF) new Point(bounds.X + bounds.Width / 2, bounds.Bottom));
      graphicsPath.CloseFigure();
      return graphicsPath;
    }
  }
}
