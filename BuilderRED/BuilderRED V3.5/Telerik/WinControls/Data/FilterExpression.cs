// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterExpression
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using Telerik.Data.Expressions;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Data
{
  [Editor("Telerik.WinControls.UI.Design.FilterDescriptionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  public class FilterExpression : CompositeFilterDescriptor
  {
    internal static readonly string ParameterName = "@FilterEditor1";
    internal static readonly string ParameterName2 = "@FilterEditor2";
    private FilterPredicateCollection predicates = new FilterPredicateCollection();
    private FilterParameterDictionary parameters = new FilterParameterDictionary();
    private FilterExpressionCollection owner;

    public FilterExpression()
    {
      this.predicates.CollectionChanging += new NotifyCollectionChangingEventHandler(this.predicates_CollectionChanging);
      this.predicates.CollectionChanged += new NotifyCollectionChangedEventHandler(this.predicates_CollectionChanged);
      this.parameters.CollectionChanging += new NotifyCollectionChangingEventHandler(this.parameters_CollectionChanging);
      this.parameters.CollectionChanged += new NotifyCollectionChangedEventHandler(this.parameters_CollectionChanged);
    }

    public FilterExpression(string fieldName)
      : this()
    {
      this.PropertyName = fieldName;
    }

    public FilterExpression(
      FilterExpression.BinaryOperation binaryOperation,
      GridKnownFunction function,
      params object[] values)
      : this()
    {
      this.LogicalOperator = GridViewHelper.GetLogicalOperator(binaryOperation);
      this.predicates.Add(new FilterPredicate(FilterExpression.BinaryOperation.AND, function, values));
    }

    public FilterExpression(
      string fieldName,
      FilterExpression.BinaryOperation binaryOperation,
      GridKnownFunction function,
      params object[] values)
      : this(binaryOperation, function, values)
    {
      this.PropertyName = fieldName;
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FilterParameterDictionary Parameters
    {
      get
      {
        return this.parameters;
      }
    }

    [DefaultValue("")]
    [Browsable(true)]
    [Description("Gets or sets a value that indicates the name of the associated field.")]
    public string FieldName
    {
      get
      {
        if (string.IsNullOrEmpty(this.PropertyName))
          return string.Empty;
        return this.PropertyName;
      }
      set
      {
        this.PropertyName = value;
        this.OnPropertyChanged(nameof (FieldName));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FilterPredicateCollection Predicates
    {
      get
      {
        return this.predicates;
      }
    }

    [DefaultValue(FilterExpression.BinaryOperation.AND)]
    [Browsable(true)]
    [Description("Gets or sets a value that indicates which binary operator will be used when concatenating multiple filter expressions.")]
    public FilterExpression.BinaryOperation BinaryOperator
    {
      get
      {
        return GridViewHelper.GetLogicalOperator(this.LogicalOperator);
      }
      set
      {
        this.LogicalOperator = GridViewHelper.GetLogicalOperator(value);
        this.OnPropertyChanged(nameof (BinaryOperator));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsValid
    {
      get
      {
        for (int index = 0; index < this.predicates.Count; ++index)
        {
          if (this.IsPredicateValid(this.predicates[index]))
            return true;
        }
        return false;
      }
    }

    public override bool IsFilterEditor
    {
      get
      {
        if (this.Parameters.Contains(FilterExpression.ParameterName) || this.Parameters.Contains(FilterExpression.ParameterName2))
          return true;
        foreach (FilterPredicate predicate in (Collection<FilterPredicate>) this.Predicates)
        {
          if (predicate.Values.Contains((object) FilterExpression.ParameterName) || predicate.Values.Contains((object) FilterExpression.ParameterName2))
            return true;
        }
        return base.IsFilterEditor;
      }
    }

    public int GetValidPredicateCount()
    {
      int num = 0;
      for (int index = 0; index < this.predicates.Count; ++index)
      {
        FilterPredicate predicate = this.predicates[index];
        if (predicate != null && predicate.Function != GridKnownFunction.NoFilter && this.IsPredicateValid(predicate))
          ++num;
      }
      return num;
    }

    public FilterExpression Combine(FilterExpression e2)
    {
      FilterExpression filterExpression = new FilterExpression();
      filterExpression.Predicates.AddRange((IEnumerable<FilterPredicate>) this.Predicates);
      IEnumerator<ParameterValuePair> enumerator1 = this.Parameters.GetEnumerator();
      while (enumerator1.MoveNext())
        filterExpression.Parameters.Add(enumerator1.Current.Name, enumerator1.Current.Value);
      filterExpression.Predicates.AddRange((IEnumerable<FilterPredicate>) e2.Predicates);
      IEnumerator<ParameterValuePair> enumerator2 = e2.Parameters.GetEnumerator();
      while (enumerator2.MoveNext())
        filterExpression.Parameters.Add(enumerator2.Current.Name, enumerator2.Current.Value);
      filterExpression.BinaryOperator = this.BinaryOperator;
      filterExpression.Owner = this.Owner;
      return filterExpression;
    }

    public override string ToString()
    {
      return this.Expression;
    }

    private void RebuildFilter()
    {
      this.FilterDescriptors.BeginUpdate();
      for (int index = this.FilterDescriptors.Count - 1; index >= 0; --index)
      {
        FilterDescriptor filterDescriptor = this.FilterDescriptors[index];
        filterDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.FilterExpression_PropertyChanged);
        this.FilterDescriptors.Remove(filterDescriptor);
      }
      this.FilterDescriptors.Clear();
      FilterDescriptorCollection filterDescriptors = this.FilterDescriptors;
      if (this.predicates.Count > 0)
        filterDescriptors.LogicalOperator = GridViewHelper.GetLogicalOperator(this.predicates[0].BinaryOperator);
      List<FilterPredicate> validPredicates = this.GetValidPredicates();
      for (int index = 0; index < validPredicates.Count; ++index)
      {
        object[] predicateValues = this.GetPredicateValues(validPredicates[index]);
        FilterOperator filterOperator = GridViewHelper.GetFilterOperator(validPredicates[index].Function);
        if (filterOperator != FilterOperator.None)
        {
          FilterDescriptor filterDescriptor = new FilterDescriptor(this.PropertyName, filterOperator, predicateValues[0]);
          filterDescriptor.PropertyChanged += new PropertyChangedEventHandler(this.FilterExpression_PropertyChanged);
          filterDescriptors.Add(filterDescriptor);
        }
        else
        {
          CompositeFilterDescriptor.DescriptorType type = CompositeFilterDescriptor.DescriptorType.Between;
          if (this.predicates[index].Function == GridKnownFunction.NotBetween)
            type = CompositeFilterDescriptor.DescriptorType.NotBetween;
          CompositeFilterDescriptor descriptor = CompositeFilterDescriptor.CreateDescriptor(type, this.PropertyName, predicateValues[0], predicateValues[1]);
          descriptor.PropertyChanged += new PropertyChangedEventHandler(this.FilterExpression_PropertyChanged);
          filterDescriptors.Add((FilterDescriptor) descriptor);
        }
        if (this.IsComposite(validPredicates.Count - index, validPredicates))
        {
          CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
          filterDescriptor.PropertyChanged += new PropertyChangedEventHandler(this.FilterExpression_PropertyChanged);
          filterDescriptor.LogicalOperator = GridViewHelper.GetLogicalOperator(validPredicates[index + 2].BinaryOperator);
          filterDescriptors.Add((FilterDescriptor) filterDescriptor);
          filterDescriptors = filterDescriptor.FilterDescriptors;
        }
      }
      this.FilterDescriptors.EndUpdate();
    }

    private void FilterExpression_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged(e);
    }

    private bool IsComposite(int range, List<FilterPredicate> predicates)
    {
      if (range <= 2 || range > predicates.Count)
        return false;
      FilterExpression.BinaryOperation binaryOperator = predicates[0].BinaryOperator;
      for (int index = 1; index < range; ++index)
      {
        if (predicates[index].BinaryOperator != binaryOperator)
          return true;
        binaryOperator = predicates[index].BinaryOperator;
      }
      return false;
    }

    private List<FilterPredicate> GetValidPredicates()
    {
      List<FilterPredicate> filterPredicateList = new List<FilterPredicate>();
      for (int index = 0; index < this.predicates.Count; ++index)
      {
        if (this.IsPredicateValid(this.predicates[index]))
          filterPredicateList.Add(this.predicates[index]);
      }
      return filterPredicateList;
    }

    internal static string GetExpression(
      FilterExpression filterExpression,
      FilterPredicate predicate)
    {
      string name = DataUtils.EscapeName(filterExpression.FieldName);
      string str = FilterExpression.GetCompositeExpression(name, predicate.Function, (object) predicate.Values);
      if (string.IsNullOrEmpty(str))
        str = FilterExpression.GetSingleExpression(name, predicate.Function, (object) predicate.Values);
      return str;
    }

    internal FilterExpressionCollection Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        if (this.owner == value)
          return;
        this.owner = value;
        this.OnPropertyChanged(nameof (Owner));
      }
    }

    internal object[] GetPredicateValues(FilterPredicate predicate)
    {
      int length = Math.Min(predicate.Values.Count, predicate.FilterFunction.ParameterCount);
      object[] objArray = new object[length];
      for (int index = 0; index < length; ++index)
      {
        object key = predicate.Values[index];
        objArray[index] = this.IsParameter(key) ? this.parameters[(string) key] : key;
      }
      return objArray;
    }

    private bool IsPredicateValid(FilterPredicate filterPredicate)
    {
      object[] predicateValues = this.GetPredicateValues(filterPredicate);
      if (predicateValues.Length == 0)
        return false;
      for (int index = 0; index < predicateValues.Length; ++index)
      {
        if (predicateValues[index] == null)
          return false;
      }
      return true;
    }

    private static string GetSingleExpression(
      string name,
      GridKnownFunction gridKnownFunction,
      params object[] values)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      if (values.Length > 0)
      {
        str1 = values[0] is string ? "'" + values[0] + "'" : values[0].ToString();
        str2 = DataUtils.EscapeLikeValue(values[0].ToString());
      }
      switch (gridKnownFunction)
      {
        case GridKnownFunction.Contains:
          return string.Format("{0} LIKE '%{1}%'", (object) name, (object) str2);
        case GridKnownFunction.DoesNotContain:
          return string.Format("{0} NOT LIKE '%{1}%'", (object) name, (object) str2);
        case GridKnownFunction.StartsWith:
          return string.Format("{0} LIKE '{1}%'", (object) name, (object) str2);
        case GridKnownFunction.EndsWith:
          return string.Format("{0} LIKE '%{1}'", (object) name, (object) str2);
        case GridKnownFunction.EqualTo:
          return string.Format("{0} = {1}", (object) name, (object) str1);
        case GridKnownFunction.NotEqualTo:
          return string.Format("{0} <> {1}", (object) name, (object) str1);
        case GridKnownFunction.GreaterThan:
          return string.Format("{0} > {1}", (object) name, (object) str1);
        case GridKnownFunction.LessThan:
          return string.Format("{0} < {1}", (object) name, (object) str1);
        case GridKnownFunction.GreaterThanOrEqualTo:
          return string.Format("{0} >= {1}", (object) name, (object) str1);
        case GridKnownFunction.LessThanOrEqualTo:
          return string.Format("{0} <= {1}", (object) name, (object) str1);
        case GridKnownFunction.IsEmpty:
          return string.Format("{0} LIKE ''", (object) name);
        case GridKnownFunction.NotIsEmpty:
          return string.Format("{0} NOT LIKE ''", (object) name);
        case GridKnownFunction.IsNull:
          return string.Format("{0} IS NULL", (object) name);
        case GridKnownFunction.NotIsNull:
          return string.Format("NOT ({0} IS NULL)", (object) name);
        default:
          return (string) null;
      }
    }

    private static string GetCompositeExpression(
      string name,
      GridKnownFunction function,
      params object[] values)
    {
      if (function != GridKnownFunction.Between && function != GridKnownFunction.NotBetween)
        return (string) null;
      string str1 = values[0] is string ? "'" + values[0] + "'" : values[0].ToString();
      string str2 = values[1] is string ? "'" + values[1] + "'" : values[1].ToString();
      if (function == GridKnownFunction.Between)
        return string.Format("{0} >= {1} AND {0} <= {2}", (object) name, (object) str1, (object) str2);
      return string.Format("NOT ({0} >= {1} AND {0} <= {2})", (object) name, (object) str1, (object) str2);
    }

    private bool IsParameter(object key)
    {
      string str = key as string;
      if (str != null)
        return str.Trim()[0] == '@';
      return false;
    }

    private void parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.RebuildFilter();
    }

    private void predicates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.RebuildFilter();
    }

    private void parameters_CollectionChanging(object sender, NotifyCollectionChangingEventArgs e)
    {
      this.RebuildFilter();
    }

    private void predicates_CollectionChanging(object sender, NotifyCollectionChangingEventArgs e)
    {
      this.RebuildFilter();
    }

    public enum BinaryOperation
    {
      AND,
      OR,
    }
  }
}
