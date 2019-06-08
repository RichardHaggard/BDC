// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSortFieldEnumerator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridSortFieldEnumerator : IEnumerator<GridSortField>, IDisposable, IEnumerator
  {
    private int currentIndex = -1;
    private RadCollectionView<GridViewRowInfo> dataView;

    public GridSortFieldEnumerator(RadCollectionView<GridViewRowInfo> dataView)
    {
      this.dataView = dataView;
    }

    public GridSortField Current
    {
      get
      {
        return new GridSortField(this.dataView.SortDescriptors[this.currentIndex].PropertyName, this.dataView.SortDescriptors[this.currentIndex].Direction == ListSortDirection.Ascending ? RadSortOrder.Ascending : RadSortOrder.Descending);
      }
    }

    public void Dispose()
    {
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) new GridSortField(this.dataView.SortDescriptors[this.currentIndex].PropertyName, this.dataView.SortDescriptors[this.currentIndex].Direction == ListSortDirection.Ascending ? RadSortOrder.Ascending : RadSortOrder.Descending);
      }
    }

    public bool MoveNext()
    {
      ++this.currentIndex;
      return this.currentIndex < this.dataView.SortDescriptors.Count;
    }

    public void Reset()
    {
      this.currentIndex = -1;
    }
  }
}
