// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataItemComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  internal class GanttViewDataItemComparer : IComparer<GanttViewDataItem>
  {
    private List<GanttViewDataItemComparer.GanttViewDataItemDescriptor> context = new List<GanttViewDataItemComparer.GanttViewDataItemDescriptor>();
    private RadGanttViewElement ganttView;

    public GanttViewDataItemComparer(RadGanttViewElement ganttView)
    {
      this.ganttView = ganttView;
      this.Update();
    }

    public void Update()
    {
      this.context.Clear();
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttView;
      }
    }

    public virtual int Compare(GanttViewDataItem x, GanttViewDataItem y)
    {
      object start1 = (object) x.Start;
      object start2 = (object) y.Start;
      if (start1 == start2)
        return 0;
      if (start1 == null || start1 == DBNull.Value)
        return -1;
      if (start2 == null || start2 == DBNull.Value)
        return 1;
      IComparable comparable = start1 as IComparable;
      if (comparable == null)
        throw new ArgumentException("Argument_ImplementIComparable");
      return comparable.CompareTo(start2);
    }

    private class GanttViewDataItemDescriptor
    {
      public int Index = -1;
      public PropertyDescriptor Descriptor;
      public bool Descending;
    }
  }
}
