// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ImageSegments
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  [Flags]
  public enum ImageSegments
  {
    Left = 1,
    TopLeft = 2,
    Top = 4,
    TopRight = 8,
    Right = 16, // 0x00000010
    BottomRight = 32, // 0x00000020
    Bottom = 64, // 0x00000040
    BottomLeft = 128, // 0x00000080
    Inner = 256, // 0x00000100
    All = Inner | BottomLeft | Bottom | BottomRight | Right | TopRight | Top | TopLeft | Left, // 0x000001FF
  }
}
