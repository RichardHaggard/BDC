// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualRowEnumerator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  internal class VisualRowEnumerator : IEnumerator<GridViewRowInfo>, IDisposable, IEnumerator
  {
    private IEnumerator<GridRowElement> traverser;

    public VisualRowEnumerator(IList<GridRowElement> list)
    {
      this.traverser = list.GetEnumerator();
    }

    ~VisualRowEnumerator()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.traverser == null)
        return;
      this.traverser.Dispose();
      this.traverser = (IEnumerator<GridRowElement>) null;
    }

    public GridViewRowInfo Current
    {
      get
      {
        return this.traverser.Current?.RowInfo;
      }
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
      return this.traverser.MoveNext();
    }

    public void Reset()
    {
      this.traverser.Reset();
    }
  }
}
