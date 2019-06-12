// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabOffice12Shape
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
  public class TabOffice12Shape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLines(new Point[14]
      {
        new Point(bounds.X + 1, bounds.Bottom),
        new Point(bounds.X, bounds.Bottom),
        new Point(bounds.X + 2, bounds.Bottom - 1),
        new Point(bounds.X + 3, bounds.Bottom - 2),
        new Point(bounds.X + 3, bounds.Y + 3),
        new Point(bounds.X + 4, bounds.Y + 2),
        new Point(bounds.X + 5, bounds.Y + 1),
        new Point(bounds.Right - 5, bounds.Y + 1),
        new Point(bounds.Right - 4, bounds.Y + 2),
        new Point(bounds.Right - 3, bounds.Y + 3),
        new Point(bounds.Right - 3, bounds.Bottom - 2),
        new Point(bounds.Right - 2, bounds.Bottom - 1),
        new Point(bounds.Right, bounds.Bottom),
        new Point(bounds.Right - 1, bounds.Bottom)
      });
      return graphicsPath;
    }
  }
}
