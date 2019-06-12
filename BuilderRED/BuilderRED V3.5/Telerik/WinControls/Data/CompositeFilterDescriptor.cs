// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.CompositeFilterDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Telerik.WinControls.Data
{
  public class CompositeFilterDescriptor : FilterDescriptor
  {
    private FilterDescriptorCollection filters;
    private bool notOperator;

    public static CompositeFilterDescriptor.DescriptorType GetDescriptorType(
      CompositeFilterDescriptor compositeDescriptor)
    {
      CompositeFilterDescriptor.DescriptorType descriptorType = CompositeFilterDescriptor.DescriptorType.Unknown;
      if (compositeDescriptor == null || compositeDescriptor.FilterDescriptors.Count != 2)
        return descriptorType;
      FilterDescriptor filterDescriptor1 = compositeDescriptor.FilterDescriptors[0];
      FilterDescriptor filterDescriptor2 = compositeDescriptor.FilterDescriptors[1];
      if (filterDescriptor1 is CompositeFilterDescriptor || filterDescriptor2 is CompositeFilterDescriptor)
        return descriptorType;
      if (filterDescriptor1.Operator == FilterOperator.IsGreaterThanOrEqualTo && filterDescriptor2.Operator == FilterOperator.IsLessThanOrEqualTo && compositeDescriptor.LogicalOperator == FilterLogicalOperator.And)
        descriptorType = CompositeFilterDescriptor.DescriptorType.Between;
      if (compositeDescriptor.NotOperator && descriptorType == CompositeFilterDescriptor.DescriptorType.Between || descriptorType == CompositeFilterDescriptor.DescriptorType.Unknown && filterDescriptor1.Operator == FilterOperator.IsLessThanOrEqualTo && (filterDescriptor2.Operator == FilterOperator.IsGreaterThanOrEqualTo && !compositeDescriptor.NotOperator) && compositeDescriptor.LogicalOperator == FilterLogicalOperator.Or)
        descriptorType = CompositeFilterDescriptor.DescriptorType.NotBetween;
      return descriptorType;
    }

    public static CompositeFilterDescriptor CreateDescriptor(
      CompositeFilterDescriptor.DescriptorType type,
      string propertyName,
      params object[] values)
    {
      if (type == CompositeFilterDescriptor.DescriptorType.Unknown)
        throw new InvalidOperationException("You cannot create undefined composite filter descriptor.");
      CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
      filterDescriptor.PropertyName = propertyName;
      switch (type - 1)
      {
        case CompositeFilterDescriptor.DescriptorType.Unknown:
        case CompositeFilterDescriptor.DescriptorType.Between:
          if (values != null && values.Length != 2)
            throw new ArgumentException(nameof (values));
          if (values == null)
            values = new object[2];
          filterDescriptor.LogicalOperator = FilterLogicalOperator.And;
          filterDescriptor.FilterDescriptors.Add(new FilterDescriptor(propertyName, FilterOperator.IsGreaterThanOrEqualTo, values[0]));
          filterDescriptor.FilterDescriptors.Add(new FilterDescriptor(propertyName, FilterOperator.IsLessThanOrEqualTo, values[1]));
          filterDescriptor.NotOperator = type == CompositeFilterDescriptor.DescriptorType.NotBetween;
          break;
      }
      return filterDescriptor;
    }

    public static CompositeFilterDescriptor CreateDescriptor(
      CompositeFilterDescriptor.DescriptorType type,
      string propertyName,
      Type dataType,
      params object[] values)
    {
      if (type == CompositeFilterDescriptor.DescriptorType.Unknown)
        throw new InvalidOperationException("You cannot create undefined composite filter descriptor.");
      CompositeFilterDescriptor filterDescriptor1 = new CompositeFilterDescriptor();
      filterDescriptor1.PropertyName = propertyName;
      switch (type - 1)
      {
        case CompositeFilterDescriptor.DescriptorType.Unknown:
        case CompositeFilterDescriptor.DescriptorType.Between:
          if (values != null && values.Length != 2)
            throw new ArgumentException(nameof (values));
          if (values == null)
            values = new object[2];
          FilterDescriptor filterDescriptor2;
          FilterDescriptor filterDescriptor3;
          if ((object) dataType == (object) typeof (DateTime))
          {
            DateTime? nullable1 = new DateTime?();
            DateTime? nullable2 = new DateTime?();
            if (values[0] != null)
              nullable1 = new DateTime?((DateTime) values[0]);
            if (values[1] != null)
              nullable2 = new DateTime?((DateTime) values[1]);
            filterDescriptor2 = (FilterDescriptor) new DateFilterDescriptor(propertyName, FilterOperator.IsGreaterThanOrEqualTo, nullable1);
            filterDescriptor3 = (FilterDescriptor) new DateFilterDescriptor(propertyName, FilterOperator.IsLessThanOrEqualTo, nullable2);
          }
          else
          {
            filterDescriptor2 = new FilterDescriptor(propertyName, FilterOperator.IsGreaterThanOrEqualTo, values[0]);
            filterDescriptor3 = new FilterDescriptor(propertyName, FilterOperator.IsLessThanOrEqualTo, values[1]);
          }
          filterDescriptor1.LogicalOperator = FilterLogicalOperator.And;
          filterDescriptor1.FilterDescriptors.AddRange(new FilterDescriptor[2]
          {
            filterDescriptor2,
            filterDescriptor3
          });
          filterDescriptor1.NotOperator = type == CompositeFilterDescriptor.DescriptorType.NotBetween;
          break;
      }
      return filterDescriptor1;
    }

    private static CompositeFilterDescriptor ConvertDescriptor(
      CompositeFilterDescriptor compositeFilter,
      CompositeFilterDescriptor.DescriptorType type,
      Type dataType)
    {
      if (compositeFilter == null)
        throw new ArgumentNullException(nameof (compositeFilter));
      if (type == CompositeFilterDescriptor.DescriptorType.Unknown)
        throw new InvalidOperationException("You cannot convert the filter descriptor to unknown type.");
      CompositeFilterDescriptor filterDescriptor1 = compositeFilter.Clone() as CompositeFilterDescriptor;
      while (filterDescriptor1.FilterDescriptors.Count > 2)
      {
        int index = filterDescriptor1.FilterDescriptors.Count - 1;
        filterDescriptor1.FilterDescriptors.RemoveAt(index);
      }
      while (filterDescriptor1.FilterDescriptors.Count < 2)
      {
        if ((object) dataType == (object) typeof (DateTime))
          filterDescriptor1.FilterDescriptors.Add((FilterDescriptor) new DateFilterDescriptor());
        else
          filterDescriptor1.FilterDescriptors.Add(new FilterDescriptor());
      }
      if (type == CompositeFilterDescriptor.DescriptorType.Between || type == CompositeFilterDescriptor.DescriptorType.NotBetween)
      {
        filterDescriptor1.LogicalOperator = FilterLogicalOperator.And;
        FilterDescriptor filterDescriptor2 = filterDescriptor1.FilterDescriptors[0];
        filterDescriptor2.PropertyName = filterDescriptor1.PropertyName;
        filterDescriptor2.Operator = FilterOperator.IsGreaterThanOrEqualTo;
        FilterDescriptor filterDescriptor3 = filterDescriptor1.FilterDescriptors[1];
        filterDescriptor3.PropertyName = filterDescriptor1.PropertyName;
        filterDescriptor3.Operator = FilterOperator.IsLessThanOrEqualTo;
        filterDescriptor1.NotOperator = type == CompositeFilterDescriptor.DescriptorType.NotBetween;
      }
      return filterDescriptor1;
    }

    public CompositeFilterDescriptor()
    {
      this.filters = new FilterDescriptorCollection();
      this.filters.CollectionChanged += new NotifyCollectionChangedEventHandler(this.filters_CollectionChanged);
    }

    [DefaultValue(FilterLogicalOperator.And)]
    [Browsable(true)]
    public virtual FilterLogicalOperator LogicalOperator
    {
      get
      {
        return this.FilterDescriptors.LogicalOperator;
      }
      set
      {
        if (this.FilterDescriptors.LogicalOperator == value)
          return;
        this.FilterDescriptors.LogicalOperator = value;
        this.OnPropertyChanged(nameof (LogicalOperator));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.filters;
      }
    }

    public bool NotOperator
    {
      get
      {
        return this.notOperator;
      }
      set
      {
        if (this.notOperator == value)
          return;
        this.notOperator = value;
        this.OnPropertyChanged(nameof (NotOperator));
      }
    }

    public override string PropertyName
    {
      get
      {
        if (this.IsSameName)
          return this.filters[0].PropertyName;
        return base.PropertyName;
      }
      set
      {
        if (this.IsSameName)
        {
          this.filters.BeginUpdate();
          for (int index = 0; index < this.filters.Count; ++index)
            this.filters[index].PropertyName = value;
          this.filters.EndUpdate();
        }
        base.PropertyName = value;
      }
    }

    public override FilterOperator Operator
    {
      get
      {
        return base.Operator;
      }
      set
      {
        base.Operator = value;
      }
    }

    public bool IsSameName
    {
      get
      {
        if (this.filters.Count == 0)
          return false;
        string propertyName = this.filters[0].PropertyName;
        for (int index = 1; index < this.filters.Count; ++index)
        {
          if (string.Compare(propertyName, this.filters[index].PropertyName, StringComparison.InvariantCulture) != 0)
            return false;
        }
        return true;
      }
    }

    public override string Expression
    {
      get
      {
        if (this.filters.Count == 0)
          return base.Expression;
        return CompositeFilterDescriptor.GetCompositeExpression(this);
      }
    }

    public static string GetCompositeExpression(CompositeFilterDescriptor filterDescriptor)
    {
      return CompositeFilterDescriptor.GetCompositeExpression(filterDescriptor, (Function<FilterDescriptor, object>) null);
    }

    public static string GetCompositeExpression(
      CompositeFilterDescriptor filterDescriptor,
      Function<FilterDescriptor, object> formatValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < filterDescriptor.filters.Count; ++index)
      {
        FilterDescriptor filter = filterDescriptor.filters[index];
        CompositeFilterDescriptor filterDescriptor1 = filter as CompositeFilterDescriptor;
        if (filterDescriptor1 != null || !string.IsNullOrEmpty(filter.PropertyName))
        {
          string str1 = filterDescriptor1 != null ? CompositeFilterDescriptor.GetCompositeExpression(filterDescriptor1, formatValue) : FilterDescriptor.GetExpression(filter, formatValue);
          stringBuilder.Append(string.Format("{0}", (object) str1));
          string str2 = filterDescriptor.filters.LogicalOperator == FilterLogicalOperator.And ? "AND" : "OR";
          if (index < filterDescriptor.filters.Count - 1)
            stringBuilder.Append(string.Format(" {0} ", (object) str2));
        }
      }
      if (stringBuilder.Length > 0)
      {
        stringBuilder.Insert(0, filterDescriptor.NotOperator ? "NOT (" : "(");
        stringBuilder.Append(")");
      }
      return stringBuilder.ToString();
    }

    public override string ToString()
    {
      return this.Expression;
    }

    public override object Clone()
    {
      CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
      filterDescriptor.PropertyName = this.PropertyName;
      filterDescriptor.Operator = this.Operator;
      filterDescriptor.Value = this.Value;
      filterDescriptor.IsFilterEditor = this.IsFilterEditor;
      filterDescriptor.notOperator = this.notOperator;
      filterDescriptor.LogicalOperator = this.LogicalOperator;
      foreach (FilterDescriptor filter in (Collection<FilterDescriptor>) this.filters)
        filterDescriptor.FilterDescriptors.Add(filter.Clone() as FilterDescriptor);
      return (object) filterDescriptor;
    }

    public CompositeFilterDescriptor ConvertTo(
      CompositeFilterDescriptor.DescriptorType type)
    {
      return CompositeFilterDescriptor.ConvertDescriptor(this, type, (Type) null);
    }

    public CompositeFilterDescriptor ConvertTo(
      CompositeFilterDescriptor.DescriptorType type,
      Type dataType)
    {
      return CompositeFilterDescriptor.ConvertDescriptor(this, type, dataType);
    }

    private void filters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnPropertyChanged("Filters[]");
    }

    public enum DescriptorType
    {
      Unknown,
      Between,
      NotBetween,
    }
  }
}
