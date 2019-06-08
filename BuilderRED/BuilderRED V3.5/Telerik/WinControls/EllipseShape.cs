// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.EllipseShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class EllipseShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddEllipse(bounds);
      return graphicsPath;
    }
  }
}
