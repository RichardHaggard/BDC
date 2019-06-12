// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ValueResetFlags
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  [Flags]
  public enum ValueResetFlags
  {
    None = 0,
    Inherited = 1,
    Binding = 2,
    TwoWayBindingLocal = 4,
    Style = 8,
    Animation = 16, // 0x00000010
    Local = 32, // 0x00000020
    DefaultValueOverride = 64, // 0x00000040
    All = DefaultValueOverride | Local | Animation | Style | TwoWayBindingLocal | Binding | Inherited, // 0x0000007F
  }
}
