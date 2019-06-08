// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class SplitterCollection : ICollection, IEnumerable<SplitterElement>, IEnumerable
  {
    private RadSplitContainer owner;

    internal SplitterCollection(RadSplitContainer owner)
    {
      this.owner = owner;
    }

    public SplitterElement this[int index]
    {
      get
      {
        return this.owner.SplitContainerElement.Children[index] as SplitterElement;
      }
    }

    public void CopyTo(Array array, int index)
    {
      ArrayList arrayList = new ArrayList(this.Count);
      foreach (SplitterElement splitterElement in this)
        arrayList.Add((object) splitterElement);
      arrayList.CopyTo(array, index);
    }

    public int Count
    {
      get
      {
        return this.owner.SplitContainerElement.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    public object SyncRoot
    {
      get
      {
        return (object) this.owner;
      }
    }

    public IEnumerator GetEnumerator()
    {
      for (int i = 0; i < this.Count; ++i)
        yield return (object) this[i];
    }

    IEnumerator<SplitterElement> IEnumerable<SplitterElement>.GetEnumerator()
    {
      for (int i = 0; i < this.Count; ++i)
        yield return this[i];
    }
  }
}
