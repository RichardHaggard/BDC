﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TypeFilter`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class TypeFilter<T> : Filter
  {
    public override bool Match(object obj)
    {
      return obj is T;
    }
  }
}
