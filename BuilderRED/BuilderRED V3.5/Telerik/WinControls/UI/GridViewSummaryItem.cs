// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSummaryItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSummaryItem : NotifyPropertyBase
  {
    private string formatString = string.Empty;
    private const string defaultFormatString = "{0}";
    private string name;
    private GridAggregateFunction aggregate;
    private string aggregateExpression;
    private GridViewTemplate template;

    public GridViewSummaryItem()
    {
      this.formatString = "{0}";
    }

    public GridViewSummaryItem(string name, string formatString, GridAggregateFunction aggregate)
    {
      this.name = name;
      this.Aggregate = aggregate;
      if (!string.IsNullOrEmpty(formatString))
        this.formatString = formatString;
      else
        this.formatString = "{0}";
    }

    public GridViewSummaryItem(string name, string formatString, string aggregateExpression)
    {
      this.name = name;
      this.formatString = string.IsNullOrEmpty(formatString) ? "{0}" : formatString;
      this.aggregateExpression = aggregateExpression;
    }

    [Browsable(false)]
    [DefaultValue("")]
    public string AggregateExpression
    {
      get
      {
        return this.aggregateExpression;
      }
      set
      {
        this.aggregateExpression = value;
      }
    }

    [DefaultValue(GridAggregateFunction.None)]
    public virtual GridAggregateFunction Aggregate
    {
      get
      {
        return this.aggregate;
      }
      set
      {
        if (this.aggregate == value)
          return;
        this.aggregate = value;
        this.OnPropertyChanged(nameof (Aggregate));
      }
    }

    [Description("Gets or sets the name of the column that will be used by the aggregate function.")]
    [DefaultValue("")]
    public virtual string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        if (!(this.name != value))
          return;
        this.name = value;
        this.OnPropertyChanged(nameof (Name));
      }
    }

    [Description("Gets or sets the formatting string that is applied to the value.")]
    public virtual string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        this.SetProperty<string>(nameof (FormatString), ref this.formatString, value);
      }
    }

    internal GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
      set
      {
        this.template = value;
      }
    }

    public string GetSummaryExpression()
    {
      if (!string.IsNullOrEmpty(this.AggregateExpression))
        return this.AggregateExpression;
      if (this.Aggregate == GridAggregateFunction.None || string.IsNullOrEmpty(this.Name))
        return string.Empty;
      string str = DataUtils.EscapeName(this.Name);
      switch (this.Aggregate)
      {
        case GridAggregateFunction.Sum:
          return "Sum(" + str + ")";
        case GridAggregateFunction.Min:
          return "Min(" + str + ")";
        case GridAggregateFunction.Max:
          return "Max(" + str + ")";
        case GridAggregateFunction.Last:
          return "Last(" + str + ")";
        case GridAggregateFunction.First:
          return "First(" + str + ")";
        case GridAggregateFunction.Count:
          return "Count(" + str + ")";
        case GridAggregateFunction.Avg:
          return "Avg(" + str + ")";
        case GridAggregateFunction.StDev:
          return "StDev(" + str + ")";
        case GridAggregateFunction.Var:
          return "Var(" + str + ")";
        default:
          return string.Empty;
      }
    }

    public virtual object Evaluate(IHierarchicalRow row)
    {
      string summaryExpression = this.GetSummaryExpression();
      if (string.IsNullOrEmpty(summaryExpression))
        return (object) null;
      object obj = (object) null;
      GridViewGroupRowInfo viewGroupRowInfo = row as GridViewGroupRowInfo;
      if (viewGroupRowInfo != null)
        return viewGroupRowInfo.Group.Evaluate(summaryExpression);
      GridViewHierarchyRowInfo hierarchyRowInfo = row as GridViewHierarchyRowInfo;
      if (hierarchyRowInfo != null)
        return hierarchyRowInfo.ActiveView.Evaluate(summaryExpression, (IEnumerable<GridViewRowInfo>) hierarchyRowInfo.ActiveView.ChildRows);
      GridViewTemplate gridViewTemplate = row as GridViewTemplate;
      if (gridViewTemplate != null)
        return gridViewTemplate.DataView.Evaluate(summaryExpression, 0, gridViewTemplate.DataView.Count);
      return obj;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.IsSuspended || this.Template == null)
        return;
      this.Template.MasterTemplate.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) this.Template, (object[]) null, new GridViewEventInfo(KnownEvents.SummaryItemChanged, GridEventType.Data, GridEventDispatchMode.Send)));
    }
  }
}
