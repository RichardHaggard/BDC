// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupSummaryEvaluationEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GroupSummaryEvaluationEventArgs : EventArgs
  {
    private string formatString;
    private DataGroup group;
    private object value;
    private GridViewSummaryItem summaryItem;
    private IHierarchicalRow parent;
    private object context;

    public GroupSummaryEvaluationEventArgs(
      object value,
      DataGroup group,
      string formatString,
      GridViewSummaryItem summaryItem,
      IHierarchicalRow parent,
      object context)
    {
      this.formatString = formatString;
      this.group = group;
      this.value = value;
      this.summaryItem = summaryItem;
      this.parent = parent;
      this.context = context;
    }

    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        this.formatString = value;
      }
    }

    public DataGroup Group
    {
      get
      {
        return this.group;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public GridViewSummaryItem SummaryItem
    {
      get
      {
        return this.summaryItem;
      }
    }

    public IHierarchicalRow Parent
    {
      get
      {
        return this.parent;
      }
    }

    public object Context
    {
      get
      {
        return this.context;
      }
    }
  }
}
