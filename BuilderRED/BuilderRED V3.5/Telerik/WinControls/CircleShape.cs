// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CircleShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class CircleShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      return this.CreatePath((RectangleF) bounds);
    }

    public override GraphicsPath CreatePath(RectangleF bounds)
    {
      float num = Math.Min(bounds.Width, bounds.Height);
      RectangleF rect = new RectangleF(bounds.X + (float) (((double) bounds.Width - (double) num) / 2.0), bounds.Y + (float) (((double) bounds.Height - (double) num) / 2.0), num, num);
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddEllipse(rect);
      return graphicsPath;
    }

    public override Region CreateRegion(Rectangle bounds)
    {
      return new Region(this.CreatePath(bounds));
    }
  }
}
