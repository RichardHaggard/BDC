﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PageViewItemSizeMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  [Flags]
  public enum PageViewItemSizeMode
  {
    Individual = 0,
    EqualWidth = 1,
    EqualHeight = 2,
    EqualSize = EqualHeight | EqualWidth, // 0x00000003
  }
}
