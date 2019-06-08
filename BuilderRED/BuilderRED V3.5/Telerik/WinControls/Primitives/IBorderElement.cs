// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.IBorderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.Primitives
{
  public interface IBorderElement : IBoxStyle, IBoxElement
  {
    Color ForeColor { get; }

    Color ForeColor2 { get; }

    Color ForeColor3 { get; }

    Color ForeColor4 { get; }

    Color InnerColor { get; }

    Color InnerColor2 { get; }

    Color InnerColor3 { get; }

    Color InnerColor4 { get; }

    BorderBoxStyle BoxStyle { get; }

    GradientStyles GradientStyle { get; }

    float GradientAngle { get; }

    BorderDrawModes BorderDrawMode { get; }

    DashStyle BorderDashStyle { get; }

    float[] BorderDashPattern { get; }

    SmoothingMode SmoothingMode { get; set; }
  }
}
