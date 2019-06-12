// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.AggregateItems`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  internal class AggregateItems<T> : IDataAggregate where T : IDataItem
  {
    private IEnumerable<T> items;

    public AggregateItems(IEnumerable<T> items)
    {
      this.items = items;
    }

    public IEnumerable GetData()
    {
      return (IEnumerable) this.items;
    }
  }
}
