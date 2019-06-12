// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.IPrimitiveElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Primitives
{
  public interface IPrimitiveElement : IShapedElement
  {
    Size Size { get; }

    bool IsDesignMode { get; }

    RadElement Parent { get; }

    ComponentThemableElementTree ElementTree { get; }

    float BorderThickness { get; }

    RectangleF GetPaintRectangle(float left, float angle, SizeF scale);

    bool ShouldUsePaintBuffer();

    RectangleF GetExactPaintingRectangle(float angle, SizeF scale);

    Font Font { get; set; }
  }
}
