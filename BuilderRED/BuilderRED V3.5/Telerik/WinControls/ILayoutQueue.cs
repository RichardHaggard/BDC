﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ILayoutQueue
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public interface ILayoutQueue
  {
    int Count { get; }

    void Add(RadElement e);

    void Remove(RadElement e);
  }
}
