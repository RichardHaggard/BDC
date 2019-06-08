// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IBoxStyle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public interface IBoxStyle
  {
    Color LeftColor { get; set; }

    Color LeftShadowColor { get; set; }

    Color TopColor { get; set; }

    Color TopShadowColor { get; set; }

    Color RightColor { get; set; }

    Color RightShadowColor { get; set; }

    Color BottomColor { get; set; }

    Color BottomShadowColor { get; set; }
  }
}
