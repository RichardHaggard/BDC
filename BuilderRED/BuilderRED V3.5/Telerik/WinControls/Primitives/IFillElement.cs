// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.IFillElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.Primitives
{
  public interface IFillElement
  {
    Color BackColor { get; set; }

    Color BackColor2 { get; set; }

    Color BackColor3 { get; set; }

    Color BackColor4 { get; set; }

    int NumberOfColors { get; set; }

    float GradientAngle { get; set; }

    float GradientPercentage { get; set; }

    float GradientPercentage2 { get; set; }

    GradientStyles GradientStyle { get; set; }

    Size Size { get; set; }

    SmoothingMode SmoothingMode { get; set; }
  }
}
