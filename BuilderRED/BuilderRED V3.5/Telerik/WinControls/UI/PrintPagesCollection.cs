// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PrintPagesCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class PrintPagesCollection : IEnumerable
  {
    private List<PrintPageColumnsCollection> columnRanges;

    public PrintPagesCollection()
    {
      this.columnRanges = new List<PrintPageColumnsCollection>();
    }

    public int Add(params GridViewColumn[] columns)
    {
      this.columnRanges.Add(new PrintPageColumnsCollection((IEnumerable<GridViewColumn>) columns));
      return this.columnRanges.Count - 1;
    }

    public void Clear()
    {
      this.columnRanges.Clear();
    }

    public void Insert(int index, params GridViewColumn[] columns)
    {
      this.columnRanges.Insert(index, new PrintPageColumnsCollection((IEnumerable<GridViewColumn>) columns));
    }

    public void RemoveAt(int index)
    {
      this.columnRanges.RemoveAt(index);
    }

    public PrintPageColumnsCollection this[int index]
    {
      get
      {
        return this.columnRanges[index];
      }
      set
      {
        this.columnRanges[index] = value;
      }
    }

    public int Count
    {
      get
      {
        return this.columnRanges.Count;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this.columnRanges.GetEnumerator();
    }
  }
}
