// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowViewCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class RowViewCollection : IEnumerator<IRowView>, IDisposable, IEnumerator, IEnumerable<IRowView>, IEnumerable
  {
    private GridTableElement tableElement;
    private VisualRowsCollection.VisualRowsCollectionEnumerator enumerator;

    public RowViewCollection(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
      this.Reset();
    }

    public IRowView Current
    {
      get
      {
        GridDetailViewRowElement current = this.enumerator.Current as GridDetailViewRowElement;
        if (current != null)
          return (IRowView) current.ContentCell.ChildTableElement;
        return (IRowView) null;
      }
    }

    public void Dispose()
    {
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public bool MoveNext()
    {
      while (this.enumerator.MoveNext())
      {
        if (this.enumerator.Current is GridDetailViewRowElement)
          return true;
      }
      return false;
    }

    public void Reset()
    {
      this.enumerator = new VisualRowsCollection.VisualRowsCollectionEnumerator((VisualRowsCollection) this.tableElement.VisualRows);
      this.enumerator.Reset();
    }

    public IEnumerator<IRowView> GetEnumerator()
    {
      this.Reset();
      return (IEnumerator<IRowView>) this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
