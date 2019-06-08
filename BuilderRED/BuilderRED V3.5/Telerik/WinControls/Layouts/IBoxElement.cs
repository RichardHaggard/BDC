// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.IBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Layouts
{
  public interface IBoxElement
  {
    float Width { get; set; }

    float LeftWidth { get; set; }

    float TopWidth { get; set; }

    float RightWidth { get; set; }

    float BottomWidth { get; set; }

    SizeF Offset { get; }

    SizeF BorderSize { get; }

    float HorizontalWidth { get; }

    float VerticalWidth { get; }
  }
}
