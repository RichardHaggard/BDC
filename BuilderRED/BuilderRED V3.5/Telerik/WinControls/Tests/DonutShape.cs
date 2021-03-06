﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Tests.DonutShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.Tests
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class DonutShape : ElementShape
  {
    private int thickness = 10;

    [DefaultValue(10)]
    public int Thickness
    {
      get
      {
        return this.thickness;
      }
      set
      {
        this.thickness = value;
      }
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddEllipse(bounds);
      bounds.Inflate(-this.thickness, -this.thickness);
      graphicsPath.AddEllipse(bounds);
      return graphicsPath;
    }
  }
}
