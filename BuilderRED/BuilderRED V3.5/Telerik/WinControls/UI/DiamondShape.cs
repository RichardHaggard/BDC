﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DiamondShape
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
  public class DiamondShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddPolygon(new PointF[4]
      {
        new PointF((float) bounds.X + 0.5f * (float) bounds.Width, (float) bounds.Top),
        new PointF((float) bounds.Right, (float) bounds.Y + 0.5f * (float) bounds.Height),
        new PointF((float) bounds.X + 0.5f * (float) bounds.Width, (float) bounds.Bottom),
        new PointF((float) bounds.Left, (float) bounds.Y + 0.5f * (float) bounds.Height)
      });
      return graphicsPath;
    }
  }
}