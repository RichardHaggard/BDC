// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupByExpression
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridGroupByExpression : GroupDescriptor
  {
    private static Regex selectFieldParser = new Regex("^\\s*(\\[(?<fieldName>[^\\[\\]\\!]+)\\]|(?<fieldName>[^\\[\\]\\! (),]+)|((?<aggregate>\\w+)\\s*\\(\\s*(\\[(?<fieldName>[^\\[\\]\\!]+)\\]|(?<fieldName>[^\\[\\]\\! (),]+))\\s*\\)))(\\s*(as)?\\s*(\\[(?<fieldAlias>[^\\[\\]\\!]+)\\]|(?<fieldAlias>[^\\[\\]\\! (),]+)))?\\s*(format\\s*((\\\"(?<formatString>.*?)\\\")|(\\'(?<formatString>.*?)\\')))?\\s*(?<delimiter>([,])|(group\\s*by))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
    private static Regex groupByFieldParser = new Regex("^\\s*(\\[(?<fieldName>[^\\[\\]\\!]+)\\]|(?<fieldName>[^\\[\\]\\! (),]+)|((?<aggregate>\\w+)\\s*\\(\\s*(\\[(?<fieldName>[^\\[\\]\\!]+)\\]|(?<fieldName>[^\\[\\]\\! (),]+))\\s*\\)))\\s*(?<sortDirection>(ASC|DESC))?\\s*((?<delimiter>[,])|$)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
    private GridGroupByFieldCollection selectFields = new GridGroupByFieldCollection();
    private GridGroupByFieldCollection groupByFields = new GridGroupByFieldCollection();
    private List<NotifyCollectionChangedEventArgs> duplicateEventsList = new List<NotifyCollectionChangedEventArgs>();
    public const string GroupByClause = "Group By";
    private string defaultFormatString;
    private string expressionAsString;
    private int index;
    private bool clearSelectedFieldsSilently;

    public GridGroupByExpression()
    {
      this.selectFields.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SelectFields_CollectionChanged);
      this.groupByFields.CollectionChanged += new NotifyCollectionChangedEventHandler(this.GroupByFields_CollectionChanged);
    }

    public GridGroupByExpression(string expression)
      : this()
    {
      this.SetExpression(expression);
    }

    public GridGroupByExpression(string expression, string defaultFormatString)
      : this()
    {
      this.defaultFormatString = defaultFormatString;
      this.Format = this.defaultFormatString;
      this.SetExpression(expression);
    }

    public GridGroupByExpression(GridViewDataColumn column)
      : this()
    {
      if (!column.AllowGroup || !column.IsDataBound)
        throw new FormatException("Cannot group by this column.");
      GridGroupByField field = new GridGroupByField();
      GridViewDataColumn column1 = column;
      if (column1 == null)
        throw new FormatException("Invalid column type. Should be GridViewDataColumn or inheritor.");
      field.FieldName = column1.Name;
      field.FieldAlias = column1.HeaderText;
      this.UpdateGroupByFieldFromColumn(column1, field);
      this.SelectFields.Add(field);
      this.GroupByFields.Add(field);
    }

    [DefaultValue(null)]
    public string DefaultFormatString
    {
      get
      {
        return this.defaultFormatString;
      }
      set
      {
        if (!(this.defaultFormatString != value))
          return;
        this.defaultFormatString = value;
        this.Format = this.defaultFormatString;
        this.OnPropertyChanged(nameof (DefaultFormatString));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public GridGroupByFieldCollection SelectFields
    {
      get
      {
        return this.selectFields;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public GridGroupByFieldCollection GroupByFields
    {
      get
      {
        return this.groupByFields;
      }
    }

    internal static GridGroupByExpression CreateFromDescriptor(
      GroupDescriptor descriptor)
    {
      GridGroupByExpression groupByExpression = new GridGroupByExpression();
      foreach (SortDescriptor groupName in (Collection<SortDescriptor>) descriptor.GroupNames)
        groupByExpression.GroupByFields.Add(new GridGroupByField(groupName.PropertyName));
      return groupByExpression;
    }

    private void UpdateGroupByFieldFromColumn(GridViewDataColumn column, GridGroupByField field)
    {
      string defaultFormatString = this.defaultFormatString;
      if (string.IsNullOrEmpty(defaultFormatString) || defaultFormatString.IndexOf("{1") < 0)
        defaultFormatString = this.defaultFormatString;
      if (!field.IsFieldAliasSet && string.IsNullOrEmpty(column.HeaderText))
        field.HeaderText = column.HeaderText;
      field.FormatString = defaultFormatString;
    }

    private void SetExpression(string expression)
    {
      this.expressionAsString = expression;
      this.ParseFieldLists();
      this.OnPropertyChanged(new PropertyChangedEventArgs("Expression"));
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Expression
    {
      get
      {
        return this.BuildStringExpression();
      }
      set
      {
        this.SetExpression(value);
      }
    }

    public static GridGroupByExpression Parse(string expression)
    {
      return new GridGroupByExpression(expression);
    }

    private string BuildStringExpression()
    {
      StringBuilder stringBuilder = new StringBuilder();
      string defaultFormatString = string.IsNullOrEmpty(this.defaultFormatString) ? (string) null : (string.Compare(this.defaultFormatString, GridGroupByField.DefaultFormatString) == 0 ? (string) null : this.defaultFormatString);
      if (this.GroupByFields.Count < 1)
        return string.Empty;
      if (this.SelectFields.Count > 0)
      {
        stringBuilder.AppendFormat("{0}", (object) this.SelectFields[0].ToSelectString(defaultFormatString));
        for (int index = 1; index < this.SelectFields.Count; ++index)
          stringBuilder.AppendFormat(", {0}", (object) this.SelectFields[index].ToSelectString(defaultFormatString));
      }
      stringBuilder.AppendFormat(" {0} {1}", (object) "Group By", (object) this.GroupByFields[0].ToGroupByString());
      for (int index = 1; index < this.GroupByFields.Count; ++index)
        stringBuilder.AppendFormat(", {0}", (object) this.GroupByFields[index].ToGroupByString());
      return stringBuilder.ToString();
    }

    public override string ToString()
    {
      return this.BuildStringExpression();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int Index
    {
      get
      {
        return this.index;
      }
    }

    internal void SetIndex(int value)
    {
      this.index = value;
    }

    private void ParseFieldLists()
    {
      this.ParseGroupByFieldList(this.expressionAsString, this.ParseSelectFieldList(this.expressionAsString, 0), false);
    }

    private static void UpdateReference(GridGroupByFieldCollection list, GridGroupByField ggbf)
    {
      GridGroupByField byAlias = list.FindByAlias(ggbf.FieldName);
      if (byAlias == null)
        return;
      if (byAlias.IsAggregate && ggbf.IsAggregate)
        throw new FormatException(string.Format("Cannot create aggregate of aggregate: {0}({1}({2}))", (object) byAlias.Aggregate, (object) ggbf.Aggregate, (object) byAlias.FieldName));
      ggbf.FieldName = byAlias.FieldName;
      if (!byAlias.IsAggregate)
        return;
      ggbf.Aggregate = byAlias.Aggregate;
    }

    private static void UpdateFieldNameAndAliasReferences(
      GridGroupByFieldCollection list,
      GridGroupByField groupByField)
    {
      GridGroupByExpression.UpdateReference(list, groupByField);
      GridGroupByField byName = list.FindByName(groupByField.FieldName);
      if (byName != null && (byName.IsFieldAliasSet && groupByField.IsFieldAliasSet && byName.Aggregate == groupByField.Aggregate ^ string.Compare(byName.FieldAlias, groupByField.FieldAlias) == 0))
        throw new FormatException("Same aggregates on same field should have same alias.");
    }

    private static void UpdateGroupByReferences(
      GridGroupByFieldCollection list,
      GridGroupByField groupByField)
    {
      GridGroupByField gridGroupByField = list.Find(groupByField.FieldName);
      if (gridGroupByField == null)
        return;
      groupByField.FieldName = gridGroupByField.FieldName;
      if (!gridGroupByField.IsFieldAliasSet)
        return;
      groupByField.FieldAlias = gridGroupByField.FieldAlias;
    }

    private int ParseSelectFieldList(string fieldList, int pos)
    {
      this.clearSelectedFieldsSilently = true;
      this.SelectFields.Clear();
      this.clearSelectedFieldsSilently = false;
      if (string.IsNullOrEmpty(fieldList))
        return -1;
      Match match;
      do
      {
        match = GridGroupByExpression.selectFieldParser.Match(fieldList.Substring(pos));
        if (!match.Success)
          throw new FormatException(string.Format("Cannot parse \"{0}\". Verify the correctness of the string.", (object) fieldList));
        GridGroupByField groupByField = new GridGroupByField();
        if (match.Groups["fieldAlias"].Success)
          groupByField.FieldAlias = match.Groups["fieldAlias"].ToString();
        if (match.Groups["aggregate"].Success)
          groupByField.SetAggregate(match.Groups["aggregate"].ToString());
        groupByField.FormatString = match.Groups["formatString"].Success ? match.Groups["formatString"].ToString() : (string) null;
        if (!match.Groups["fieldName"].Success)
          throw new FormatException("Could not parse field name. Please contact Telerik support");
        groupByField.FieldName = match.Groups["fieldName"].ToString();
        GridGroupByExpression.UpdateFieldNameAndAliasReferences(this.selectFields, groupByField);
        this.selectFields.Add(groupByField);
        pos += match.Length;
        if (!match.Groups["delimiter"].Success)
          throw new Exception("Delimiter (,) or Group By clause expected");
      }
      while (match.Groups["delimiter"].Length == 1);
      return pos;
    }

    private void ParseGroupByFieldList(string fieldList, int pos, bool allowRelation)
    {
      this.GroupByFields.Clear();
      if (string.IsNullOrEmpty(fieldList))
        return;
      Match match;
      do
      {
        match = GridGroupByExpression.groupByFieldParser.Match(fieldList.Substring(pos));
        if (!match.Success)
          throw new FormatException(string.Format("Cannot parse \"{0}\". Verify the correctness of the string.", (object) fieldList));
        GridGroupByField groupByField = new GridGroupByField();
        if (match.Groups["aggregate"].Success)
          groupByField.SetAggregate(match.Groups["aggregate"].ToString());
        groupByField.SortOrder = !match.Groups["sortDirection"].Success || string.Compare("DESC", match.Groups["sortDirection"].Value) != 0 ? RadSortOrder.Ascending : RadSortOrder.Descending;
        if (!match.Groups["fieldName"].Success)
          throw new FormatException("Could not parse field name. Please contact Telerik support");
        groupByField.FieldName = match.Groups["fieldName"].ToString();
        GridGroupByExpression.UpdateGroupByReferences(this.selectFields, groupByField);
        this.groupByFields.Add(groupByField);
        pos += match.Length;
      }
      while (match.Groups["delimiter"].Success);
    }

    private void GroupByFields_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.GroupNames.BeginUpdate();
      this.GroupNames.Clear();
      for (int index = 0; index < this.groupByFields.Count; ++index)
        this.GroupNames.Add(this.groupByFields[index].FieldName, GridViewHelper.GetSortDirection(this.groupByFields[index].SortOrder));
      this.GroupNames.EndUpdate();
      if (this.IsDuplicateEvent(e, this.selectFields))
        return;
      this.OnPropertyChanged("GroupByFields");
    }

    private void SelectFields_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.Aggregates.Clear();
      for (int index = 0; index < this.selectFields.Count; ++index)
      {
        string str = string.Empty;
        switch (this.selectFields[index].Aggregate)
        {
          case GridAggregateFunction.None:
            str = string.Empty;
            break;
          case GridAggregateFunction.Sum:
            str = "Sum";
            break;
          case GridAggregateFunction.Min:
            str = "Min";
            break;
          case GridAggregateFunction.Max:
            str = "Max";
            break;
          case GridAggregateFunction.Last:
            str = "Last";
            break;
          case GridAggregateFunction.First:
            str = "First";
            break;
          case GridAggregateFunction.Count:
            str = "Count";
            break;
          case GridAggregateFunction.Avg:
            str = "Avg";
            break;
          case GridAggregateFunction.StDev:
            str = "StDev";
            break;
          case GridAggregateFunction.Var:
            str = "Var";
            break;
        }
        if (!string.IsNullOrEmpty(str))
          this.Aggregates.Add(string.Format("{0}({1})", (object) str, (object) this.selectFields[index].FieldName));
      }
      if (this.IsDuplicateEvent(e, this.groupByFields) || this.clearSelectedFieldsSilently)
        return;
      this.OnPropertyChanged("SelectFields");
    }

    private bool IsDuplicateEvent(
      NotifyCollectionChangedEventArgs e,
      GridGroupByFieldCollection otherCollection)
    {
      if (e.Action == NotifyCollectionChangedAction.ItemChanged)
      {
        GridGroupByField newItem = e.NewItems[0] as GridGroupByField;
        if (otherCollection.Contains(newItem))
        {
          NotifyCollectionChangedEventArgs changedEventArgs = this.duplicateEventsList.Find((Predicate<NotifyCollectionChangedEventArgs>) (args =>
          {
            if (args.NewItems[0] == e.NewItems[0])
              return args.PropertyName == e.PropertyName;
            return false;
          }));
          if (changedEventArgs == null)
          {
            this.duplicateEventsList.Add(e);
            return true;
          }
          this.duplicateEventsList.Remove(changedEventArgs);
        }
      }
      return false;
    }

    private static void ParseGroupByFieldListOld(
      string fieldList,
      bool allowRelation,
      string formatString)
    {
    }

    public bool IsSame(GridGroupByExpression expression)
    {
      foreach (GridGroupByField selectField1 in (Collection<GridGroupByField>) expression.SelectFields)
      {
        bool flag = false;
        foreach (GridGroupByField selectField2 in (Collection<GridGroupByField>) this.SelectFields)
        {
          if (selectField2.FieldName.ToLower() == selectField1.FieldName.ToLower())
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return false;
      }
      foreach (GridGroupByField groupByField1 in (Collection<GridGroupByField>) expression.GroupByFields)
      {
        bool flag = false;
        foreach (GridGroupByField groupByField2 in (Collection<GridGroupByField>) this.GroupByFields)
        {
          if (groupByField2.FieldName.ToLower() == groupByField1.FieldName.ToLower())
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return false;
      }
      return true;
    }

    public bool ContainsSameGroupByField(GridGroupByExpression expression)
    {
      bool flag = false;
      foreach (GridGroupByField groupByField1 in (Collection<GridGroupByField>) expression.GroupByFields)
      {
        foreach (GridGroupByField groupByField2 in (Collection<GridGroupByField>) this.GroupByFields)
        {
          if (groupByField2.FieldName.ToUpper() == groupByField1.FieldName.ToUpper())
          {
            flag = true;
            break;
          }
        }
        if (flag)
          break;
      }
      return flag;
    }

    public void CopyFrom(GridGroupByExpression expression)
    {
      this.SelectFields.Clear();
      this.GroupByFields.Clear();
      foreach (GridGroupByField selectField in (Collection<GridGroupByField>) expression.SelectFields)
      {
        GridGroupByField gridGroupByField = new GridGroupByField();
        gridGroupByField.CopyFrom(selectField);
        this.SelectFields.Add(gridGroupByField);
      }
      foreach (GridGroupByField groupByField in (Collection<GridGroupByField>) expression.GroupByFields)
      {
        GridGroupByField gridGroupByField = new GridGroupByField();
        gridGroupByField.CopyFrom(groupByField);
        this.GroupByFields.Add(gridGroupByField);
      }
    }

    internal GridGroupByExpression Clone()
    {
      return GridGroupByExpression.Parse(this.ToString());
    }

    internal void Update()
    {
      this.SetExpression(this.Expression);
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj is GridGroupByExpression)
      {
        GridGroupByExpression groupByExpression = (GridGroupByExpression) obj;
        if (groupByExpression.GroupByFields.Count == this.GroupByFields.Count)
        {
          for (int index = 0; index < this.GroupByFields.Count; ++index)
          {
            if (this.GroupByFields[index].FieldName == groupByExpression.GroupByFields[index].FieldName)
              flag = true;
          }
          if (this.GroupByFields.Count == 0)
            flag = true;
        }
      }
      return flag;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
