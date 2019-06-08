// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckedTreeNodeCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class CheckedTreeNodeCollection : IReadOnlyCollection<RadTreeNode>, IEnumerable<RadTreeNode>, IEnumerable
  {
    private CheckedTreeNodeEnumerator enumerator;

    public CheckedTreeNodeCollection(RadTreeNode rootNode)
    {
      this.enumerator = new CheckedTreeNodeEnumerator(rootNode);
    }

    public int Count
    {
      get
      {
        int num = 0;
        this.enumerator.Reset();
        while (this.enumerator.MoveNext())
          ++num;
        return num;
      }
    }

    public RadTreeNode this[int index]
    {
      get
      {
        return this.GetNodeByIndex(index);
      }
    }

    public bool Contains(RadTreeNode value)
    {
      this.enumerator.Reset();
      while (this.enumerator.MoveNext())
      {
        if (this.enumerator.Current == value)
          return true;
      }
      return false;
    }

    public void CopyTo(RadTreeNode[] array, int index)
    {
      this.enumerator.Reset();
      while (this.enumerator.MoveNext())
      {
        array[index] = this.enumerator.Current;
        ++index;
      }
    }

    public int IndexOf(RadTreeNode value)
    {
      int num = 0;
      this.enumerator.Reset();
      while (this.enumerator.MoveNext())
      {
        if (this.enumerator.Current == value)
          return num;
        ++num;
      }
      return -1;
    }

    public IEnumerator<RadTreeNode> GetEnumerator()
    {
      return this.enumerator.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.enumerator.GetEnumerator();
    }

    protected virtual RadTreeNode GetNodeByIndex(int index)
    {
      this.enumerator.Reset();
      while (this.enumerator.MoveNext())
      {
        if (index < 0)
          throw new IndexOutOfRangeException();
        if (index == 0)
          return this.enumerator.Current;
        --index;
      }
      throw new IndexOutOfRangeException();
    }
  }
}
