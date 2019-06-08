// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementPropertyOptions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  [Flags]
  public enum ElementPropertyOptions
  {
    None = 0,
    CanInheritValue = 2,
    InvalidatesLayout = 4,
    AffectsLayout = 8,
    AffectsMeasure = 16, // 0x00000010
    AffectsArrange = 32, // 0x00000020
    AffectsParentMeasure = 64, // 0x00000040
    AffectsParentArrange = 128, // 0x00000080
    AffectsDisplay = 256, // 0x00000100
    AffectsTheme = 512, // 0x00000200
    Cancelable = 1024, // 0x00000400
  }
}
