// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabIEShape
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
  public class TabIEShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(new Point(bounds.X, bounds.Y + bounds.Height + 1), new Point(bounds.X, bounds.Y + 2));
      graphicsPath.AddLine(new Point(bounds.X, bounds.Y + 2), new Point(bounds.X + 2, bounds.Y));
      graphicsPath.AddLine(new Point(bounds.X + 2, bounds.Y), new Point(bounds.X + bounds.Width - 2, bounds.Y));
      graphicsPath.AddLine(new Point(bounds.X + bounds.Width - 2, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + 2));
      graphicsPath.AddLine(new Point(bounds.X + bounds.Width, bounds.Y + 2), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height + 1));
      return graphicsPath;
    }
  }
}
