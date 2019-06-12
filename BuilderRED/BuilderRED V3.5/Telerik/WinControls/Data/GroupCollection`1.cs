// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.GroupCollection`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  public class GroupCollection<T> : ReadOnlyCollection<Group<T>>, IDisposable
    where T : IDataItem
  {
    public static GroupCollection<T> Empty = new GroupCollection<T>((IList<Group<T>>) new List<Group<T>>());

    public GroupCollection(IList<Group<T>> list)
      : base(list)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public IList<Group<T>> GroupList
    {
      get
      {
        return this.Items;
      }
    }

    public void Dispose()
    {
      this.Items.Clear();
    }
  }
}
