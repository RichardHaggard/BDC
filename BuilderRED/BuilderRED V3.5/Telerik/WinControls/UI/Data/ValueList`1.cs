// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.ValueList`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Data
{
  public class ValueList<T> : List<ValueItem<T>>
  {
    public ValueList(params T[] items)
    {
      this.AddRange(items);
    }

    public void AddRange(params T[] items)
    {
      for (int index = 0; index < items.Length; ++index)
        this.Add(new ValueItem<T>(items[index]));
    }
  }
}
