// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.LinqIndex`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.Collections.Generic
{
  internal class LinqIndex<T> : Index<T> where T : IDataItem
  {
    public LinqIndex(RadCollectionView<T> collectionView)
      : base(collectionView)
    {
    }

    public override IList<T> Items
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    protected override void Perform()
    {
      throw new NotImplementedException();
    }
  }
}
