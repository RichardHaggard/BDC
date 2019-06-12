// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LineInfoCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class LineInfoCollection : ReadOnlyCollection<LineInfo>
  {
    public LineInfoCollection()
      : base((IList<LineInfo>) new List<LineInfo>())
    {
    }

    public LineInfo FirstLine
    {
      get
      {
        if (this.Count > 0)
          return this[0];
        return (LineInfo) null;
      }
    }

    public LineInfo LastLine
    {
      get
      {
        int count = this.Count;
        if (count > 0)
          return this[count - 1];
        return (LineInfo) null;
      }
    }

    protected internal void Add(LineInfo line)
    {
      this.Items.Add(line);
    }

    protected internal void RemoveRange(int index, int count)
    {
      (this.Items as List<LineInfo>).RemoveRange(index, count);
    }

    protected internal void Clear()
    {
      this.Items.Clear();
    }

    public LineInfo BinarySearchByYCoordinate(float y)
    {
      return this.BinarySearch((IComparer<LineInfo>) new LineInfoYComparer(y));
    }

    public LineInfo BinarySearchByOffset(int offset)
    {
      return this.BinarySearch((IComparer<LineInfo>) new LineInfoOffsetComparer(offset));
    }

    public LineInfo BinarySearchByBlockIndex(int index)
    {
      return this.BinarySearch((IComparer<LineInfo>) new LineInfoBlockIndexComparer(index));
    }

    protected virtual LineInfo BinarySearch(IComparer<LineInfo> comparer)
    {
      int index = (this.Items as List<LineInfo>).BinarySearch((LineInfo) null, comparer);
      if (index >= 0)
        return this[index];
      return (LineInfo) null;
    }
  }
}
