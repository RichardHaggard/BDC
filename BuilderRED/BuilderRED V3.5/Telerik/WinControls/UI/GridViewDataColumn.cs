// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDataColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  public abstract class GridViewDataColumn : GridViewColumn, IDataConversionInfoProvider, IGridViewEventListener, ITypeDescriptorContext, IServiceProvider
  {
    public static RadProperty FormatStringProperty = RadProperty.Register(nameof (FormatString), typeof (string), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty));
    public static RadProperty DataEditFormatStringProperty = RadProperty.Register("DataEditFormatString", typeof (string), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty));
    public static RadProperty FormatInfoProperty = RadProperty.Register(nameof (FormatInfo), typeof (CultureInfo), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty NullValueProperty = RadProperty.Register(nameof (NullValue), typeof (object), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty DataSourceNullValueProperty = RadProperty.Register(nameof (DataSourceNullValue), typeof (object), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty UseDataTypeConverterWhenSortingProperty = RadProperty.Register(nameof (UseDataTypeConverterWhenSorting), typeof (bool), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty DataTypeConverterProperty = RadProperty.Register(nameof (DataTypeConverter), typeof (TypeConverter), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty DataTypeProperty = RadProperty.Register(nameof (DataType), typeof (Type), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) typeof (string)));
    public static RadProperty AllowFilteringProperty = RadProperty.Register(nameof (AllowFiltering), typeof (bool), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static RadProperty AllowSearchingProperty = RadProperty.Register(nameof (AllowSearching), typeof (bool), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static RadProperty AllowNaturalSortProperty = RadProperty.Register(nameof (AllowNaturalSort), typeof (bool), typeof (GridViewDataColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    private string excelExportFormatString = string.Empty;
    protected DisplayFormatType? excelFormat = new DisplayFormatType?();
    private FilterDescriptor filterDescriptor;
    private GridViewColumnValuesCollection distinctValues;
    private GridViewColumnValuesCollection distinctValuesWithFilter;
    private GridViewColumnValuesCollection distinctValuesWithFilterSnapshot;
    private bool containsBlanks;

    public GridViewDataColumn()
    {
      int num = (int) this.SetDefaultValueOverride(GridViewColumn.AllowResizeProperty, (object) true);
    }

    public GridViewDataColumn(string fieldName)
      : base(fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewColumn.AllowResizeProperty, (object) true);
    }

    public GridViewDataColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewColumn.AllowResizeProperty, (object) true);
    }

    internal override bool CanBeCurrent
    {
      get
      {
        return true;
      }
    }

    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the user can filter by this column.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    public virtual bool AllowFiltering
    {
      get
      {
        return (bool) this.GetValue(GridViewDataColumn.AllowFilteringProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.AllowFilteringProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the user can search by this column.")]
    [DefaultValue(true)]
    [Browsable(true)]
    public virtual bool AllowSearching
    {
      get
      {
        return (bool) this.GetValue(GridViewDataColumn.AllowSearchingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.AllowSearchingProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether clicking on the header cell of this column would allow the user to set natural (no) sort.")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Behavior")]
    public virtual bool AllowNaturalSort
    {
      get
      {
        return (bool) this.GetValue(GridViewDataColumn.AllowNaturalSortProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.AllowNaturalSortProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [DefaultValue(null)]
    [Description("Gets or sets the data type converter used to convert data between the data source and grid editors.")]
    public TypeConverter DataTypeConverter
    {
      get
      {
        TypeConverter dataTypeConverter = this.GetValue(GridViewDataColumn.DataTypeConverterProperty) as TypeConverter;
        if (dataTypeConverter == null && (object) this.DataType != null)
        {
          dataTypeConverter = this.GetDefaultDataTypeConverter(this.DataType);
          this.SuspendPropertyNotifications();
          int num1 = (int) this.ResetValue(GridViewDataColumn.DataTypeConverterProperty, ValueResetFlags.Local);
          int num2 = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeConverterProperty, (object) dataTypeConverter);
          this.ResumePropertyNotifications();
        }
        return dataTypeConverter;
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.DataTypeConverterProperty, (object) value);
      }
    }

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the data type converter of this column should be used when sorting.")]
    public bool UseDataTypeConverterWhenSorting
    {
      get
      {
        return (bool) this.GetValue(GridViewDataColumn.UseDataTypeConverterWhenSortingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.UseDataTypeConverterWhenSortingProperty, (object) value);
      }
    }

    protected virtual TypeConverter GetDefaultDataTypeConverter(Type type)
    {
      return TypeDescriptor.GetConverter(type);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Data")]
    [DefaultValue(null)]
    [Editor("Telerik.WinControls.UI.Design.FilterDescriptionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual FilterDescriptor FilterDescriptor
    {
      get
      {
        if (this.OwnerTemplate == null)
          return this.filterDescriptor;
        return this.GetFilterDescriptor();
      }
      set
      {
        if (!this.SetFilterDescriptor(value))
          return;
        this.OnNotifyPropertyChanged(nameof (FilterDescriptor));
      }
    }

    internal bool SetFilterDescriptor(FilterDescriptor value)
    {
      GridViewTemplate ownerTemplate = this.OwnerTemplate;
      if (ownerTemplate == null || ownerTemplate.MasterTemplate != null && ownerTemplate.MasterTemplate.IsLoading || !this.AllowFiltering)
      {
        if (this.filterDescriptor == value)
          return false;
        this.filterDescriptor = value;
        if (this.filterDescriptor != null)
          this.filterDescriptor.IsFilterEditor = true;
        return true;
      }
      FilterDescriptor filterDescriptor = this.FilterDescriptor;
      if (filterDescriptor == value)
        return false;
      int index = ownerTemplate.FilterDescriptors.IndexOf(filterDescriptor);
      bool flag1 = filterDescriptor != null;
      bool flag2 = value != null;
      if (index >= 0)
      {
        if (flag2)
        {
          this.OwnerTemplate.EventDispatcher.SuspendEvent(EventDispatcher.FilterChangingEvent);
          this.OwnerTemplate.EventDispatcher.SuspendEvent(EventDispatcher.FilterChangedEvent);
        }
        ownerTemplate.FilterDescriptors.RemoveAt(index);
        if (flag2)
        {
          this.OwnerTemplate.EventDispatcher.ResumeEvent(EventDispatcher.FilterChangingEvent);
          this.OwnerTemplate.EventDispatcher.ResumeEvent(EventDispatcher.FilterChangedEvent);
        }
        flag1 = ownerTemplate.FilterDescriptors.IndexOf(filterDescriptor) < 0;
      }
      if (value != null)
      {
        value.PropertyName = this.Name;
        value.IsFilterEditor = true;
        ownerTemplate.FilterDescriptors.Add(value);
        flag1 = ownerTemplate.FilterDescriptors.IndexOf(value) >= 0;
      }
      return flag1;
    }

    private FilterDescriptor GetFilterDescriptor()
    {
      foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.OwnerTemplate.FilterDescriptors)
      {
        if (filterDescriptor.PropertyName == this.Name && filterDescriptor.IsFilterEditor)
          return filterDescriptor;
      }
      return (FilterDescriptor) null;
    }

    [DefaultValue(typeof (string))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Data")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Gets or sets the data type of the column.")]
    [TypeConverter(typeof (Telerik.WinControls.UI.DataTypeConverter))]
    public virtual Type DataType
    {
      get
      {
        return (Type) this.GetValue(GridViewDataColumn.DataTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.DataTypeProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [DefaultValue(typeof (DisplayFormatType), "None")]
    [Description("Gets or sets the type of the excel export.")]
    public virtual DisplayFormatType ExcelExportType
    {
      get
      {
        if (!this.excelFormat.HasValue)
          return DisplayFormatType.None;
        return this.excelFormat.Value;
      }
      set
      {
        this.excelFormat = new DisplayFormatType?(value);
      }
    }

    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    [Browsable(true)]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the excel export format string. Note that this format is considered only if ExcelExportType property is set to DisplayFormatType.Custom")]
    [Category("Behavior")]
    public string ExcelExportFormatString
    {
      get
      {
        return this.excelExportFormatString;
      }
      set
      {
        this.excelExportFormatString = value;
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [SerializationConverter(typeof (FormatInfoSerializationCoverter))]
    [DefaultValue(typeof (CultureInfo), "")]
    [Description("Gets or sets the culture info used when formatting cell values.")]
    [Browsable(true)]
    public CultureInfo FormatInfo
    {
      get
      {
        return (CultureInfo) this.GetValue(GridViewDataColumn.FormatInfoProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.FormatInfoProperty, (object) value);
      }
    }

    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Description("Gets or sets the format string applied to the textual content of each cell in the column.")]
    public string FormatString
    {
      get
      {
        return (string) this.GetValue(GridViewDataColumn.FormatStringProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.FormatStringProperty, (object) value);
        this.distinctValues = (GridViewColumnValuesCollection) null;
        this.distinctValuesWithFilter = (GridViewColumnValuesCollection) null;
      }
    }

    [Description("Gets or sets the cell display value corresponding to a cell value of System.DBNull or null")]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public object NullValue
    {
      get
      {
        return this.GetValue(GridViewDataColumn.NullValueProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.NullValueProperty, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value to the data source when the user enters a null value into a cell")]
    [Browsable(true)]
    [DefaultValue(null)]
    [Category("Appearance")]
    public object DataSourceNullValue
    {
      get
      {
        return this.GetValue(GridViewDataColumn.DataSourceNullValueProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDataColumn.DataSourceNullValueProperty, value);
      }
    }

    internal bool ContainsBlanks
    {
      get
      {
        return this.containsBlanks;
      }
    }

    public virtual GridViewColumnValuesCollection DistinctValues
    {
      get
      {
        if (this.distinctValues == null)
        {
          this.distinctValues = this.GetDistinctValues();
          if (this.distinctValues != null)
            this.EnsureColumnAsListener();
        }
        return this.distinctValues;
      }
    }

    public virtual GridViewColumnValuesCollection DistinctValuesWithFilter
    {
      get
      {
        int count = this.OwnerTemplate.ExcelFilteredColumns.Count;
        if (count > 0 && this == this.OwnerTemplate.ExcelFilteredColumns[count - 1] || this.OwnerTemplate.HierarchyLevel > 0)
        {
          if (count == 1 || this.OwnerTemplate.HierarchyLevel > 0)
            return this.DistinctValues;
          return this.distinctValuesWithFilterSnapshot;
        }
        this.distinctValuesWithFilter = this.GetDistinctValuesWithFilter();
        if (this.distinctValuesWithFilter != null)
          this.EnsureColumnAsListener();
        return this.distinctValuesWithFilter;
      }
    }

    private void EnsureColumnAsListener()
    {
      if (this.OwnerTemplate == null || this.OwnerTemplate.MasterTemplate == null || this.OwnerTemplate.MasterTemplate.SynchronizationService.ContainsListener((IGridViewEventListener) this))
        return;
      this.OwnerTemplate.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    public override string ToString()
    {
      return string.Format("{0} ({1})", (object) this.HeaderText, (object) this.GetType().Name);
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewTableHeaderRowInfo)
        return typeof (GridHeaderCellElement);
      if (row is GridViewFilteringRowInfo)
        return typeof (GridFilterCellElement);
      if (row is GridViewSummaryRowInfo)
        return typeof (GridSummaryCellElement);
      return typeof (GridDataCellElement);
    }

    protected virtual GridViewColumnValuesCollection GetDistinctValues()
    {
      int index1 = this.Index;
      if (index1 >= 0)
      {
        IList<GridViewRowInfo> gridViewRowInfoList = (IList<GridViewRowInfo>) this.OwnerTemplate.Rows;
        if (gridViewRowInfoList.Count == 0 && this.OwnerTemplate.Parent != null && this.OwnerTemplate.HierarchyLevel > 0)
        {
          gridViewRowInfoList = (IList<GridViewRowInfo>) new List<GridViewRowInfo>();
          GridViewInfo masterViewInfo = this.OwnerTemplate.MasterViewInfo;
          for (int index2 = 0; index2 < this.OwnerTemplate.Parent.Rows.Count; ++index2)
          {
            GridViewRowInfo row = this.OwnerTemplate.Parent.Rows[index2];
            ((List<GridViewRowInfo>) gridViewRowInfoList).AddRange((IEnumerable<GridViewRowInfo>) this.OwnerTemplate.HierarchyDataProvider.GetChildRows(row, masterViewInfo));
          }
        }
        GridViewColumnValuesCollection valuesCollection = new GridViewColumnValuesCollection();
        foreach (GridViewRowInfo gridViewRowInfo in (IEnumerable<GridViewRowInfo>) gridViewRowInfoList)
        {
          object obj = gridViewRowInfo.Cells[index1].Value;
          if (!valuesCollection.Contains(obj))
            valuesCollection.Add(obj);
        }
        if (valuesCollection.Count > 0)
          return valuesCollection;
      }
      return (GridViewColumnValuesCollection) null;
    }

    private void GetDistinctValuesFromGroupRows(
      GridViewRowInfo row,
      GridViewColumnValuesCollection distinctValues)
    {
      GridViewGroupRowInfo viewGroupRowInfo = row as GridViewGroupRowInfo;
      if (viewGroupRowInfo == null)
      {
        object obj = row.Cells[this.Index].Value;
        if (distinctValues.Contains(obj))
          return;
        distinctValues.Add(obj);
      }
      else
      {
        foreach (GridViewRowInfo childRow in viewGroupRowInfo.ChildRows)
          this.GetDistinctValuesFromGroupRows(childRow, distinctValues);
      }
    }

    protected virtual GridViewColumnValuesCollection GetDistinctValuesWithFilter()
    {
      int index = this.Index;
      this.containsBlanks = false;
      if (index >= 0)
      {
        GridViewColumnValuesCollection distinctValues = new GridViewColumnValuesCollection();
        if (this.OwnerTemplate.IsSelfReference)
        {
          foreach (GridViewRowInfo row in this.OwnerTemplate.Rows)
          {
            object obj = row.Cells[index].Value;
            if (!this.containsBlanks)
              this.containsBlanks = obj == null || obj == DBNull.Value;
            if (!distinctValues.Contains(obj))
              distinctValues.Add(obj);
          }
        }
        else
        {
          foreach (GridViewRowInfo row in !this.OwnerTemplate.MasterTemplate.EnablePaging ? (IEnumerable<GridViewRowInfo>) this.OwnerTemplate.ChildRows : (IEnumerable<GridViewRowInfo>) this.OwnerTemplate.Rows)
          {
            if (!this.OwnerTemplate.MasterTemplate.EnablePaging || this.OwnerTemplate.DataView.PassesFilter(row))
            {
              if (row is GridViewGroupRowInfo)
              {
                this.GetDistinctValuesFromGroupRows(row, distinctValues);
              }
              else
              {
                object obj = row.Cells[index].Value;
                if (!this.containsBlanks)
                  this.containsBlanks = obj == null || obj == DBNull.Value;
                if (!distinctValues.Contains(obj))
                  distinctValues.Add(obj);
              }
            }
          }
        }
        if (distinctValues.Count > 0)
          return distinctValues;
      }
      return (GridViewColumnValuesCollection) null;
    }

    protected virtual void UpdateDistinctValuesOnAdd(object value)
    {
      if (this.distinctValues == null || this.distinctValues.Contains(value))
        return;
      this.distinctValues.Add(value);
    }

    protected virtual void UpdateDistinctValues()
    {
      if (this.distinctValues == null)
        return;
      this.distinctValues.Clear();
      this.distinctValues = this.GetDistinctValues();
    }

    protected virtual void UpdateDistinctValuesWithFilter()
    {
      if (this.distinctValuesWithFilter == null)
        return;
      this.distinctValuesWithFilter.Clear();
      this.distinctValuesWithFilter = this.GetDistinctValuesWithFilter();
    }

    protected virtual void UpdateDistinctValuesOnAddWithFilter(object value)
    {
      if (this.distinctValuesWithFilter == null || this.distinctValuesWithFilter.Contains(value))
        return;
      this.distinctValuesWithFilter.Add(value);
    }

    protected override void OnNotifyPropertyChanging(PropertyChangingEventArgsEx e)
    {
      base.OnNotifyPropertyChanging(e);
      if (e.PropertyName != "OwnerTemplate" || this.OwnerTemplate == null || this.OwnerTemplate.MasterTemplate == null)
        return;
      this.OwnerTemplate.MasterTemplate.SynchronizationService.RemoveListener((IGridViewEventListener) this);
    }

    protected internal override void Initialize()
    {
      base.Initialize();
      if (this.OwnerTemplate == null)
        return;
      bool flag = false;
      if (!this.OwnerTemplate.Columns.IsUpdated)
      {
        this.OwnerTemplate.FilterDescriptors.BeginUpdate();
        flag = true;
      }
      if (this.filterDescriptor != null)
        this.FilterDescriptor = this.filterDescriptor;
      if (!flag)
        return;
      this.OwnerTemplate.FilterDescriptors.EndUpdate(false);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == GridViewDataColumn.DataTypeProperty)
      {
        object newValue = e.NewValue;
        int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeConverterProperty, (object) null);
      }
      if (this.filterDescriptor == null || e.Property != GridViewDataColumn.AllowFilteringProperty || ((bool) e.OldValue || !(bool) e.NewValue))
        return;
      this.OwnerTemplate.FilterDescriptors.Add(this.filterDescriptor);
    }

    protected override GridEventType GetEventInfo(
      RadProperty property,
      out GridEventDispatchMode dispatchMode)
    {
      if (property != GridViewDataColumn.DataTypeProperty)
        return base.GetEventInfo(property, out dispatchMode);
      dispatchMode = GridEventDispatchMode.Send;
      return GridEventType.Both;
    }

    public GridEventType DesiredEvents
    {
      get
      {
        return GridEventType.Data;
      }
    }

    public EventListenerPriority Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    public GridEventProcessMode DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.Process;
      }
    }

    public GridViewEventResult PreProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public GridViewEventResult ProcessEvent(GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.ViewChanged)
      {
        DataViewChangedEventArgs changedEventArgs = (DataViewChangedEventArgs) eventData.Arguments[0];
        switch (changedEventArgs.Action)
        {
          case ViewChangedAction.Add:
            if (changedEventArgs.NewItems[0] is GridViewDataRowInfo)
            {
              this.UpdateDistinctValues();
              this.UpdateDistinctValuesWithFilter();
              break;
            }
            break;
          case ViewChangedAction.Remove:
            if (changedEventArgs.NewItems[0] is GridViewDataRowInfo)
            {
              this.UpdateDistinctValues();
              this.UpdateDistinctValuesWithFilter();
              break;
            }
            break;
          case ViewChangedAction.Reset:
            this.distinctValues = (GridViewColumnValuesCollection) null;
            this.distinctValuesWithFilter = (GridViewColumnValuesCollection) null;
            break;
          case ViewChangedAction.ItemChanged:
            if (changedEventArgs.NewItems[0] is GridViewDataRowInfo)
            {
              this.UpdateDistinctValues();
              this.UpdateDistinctValuesWithFilter();
              break;
            }
            break;
        }
      }
      else if (eventData.Info.Id == KnownEvents.CollectionChanged)
      {
        NotifyCollectionChangedEventArgs changedEventArgs = (NotifyCollectionChangedEventArgs) eventData.Arguments[0];
        if (changedEventArgs.Action == NotifyCollectionChangedAction.Remove && changedEventArgs.NewItems[0] == this)
          this.OwnerTemplate.MasterTemplate.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      }
      return (GridViewEventResult) null;
    }

    public GridViewEventResult PostProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public bool AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    IContainer ITypeDescriptorContext.Container
    {
      get
      {
        if (this.OwnerTemplate == null || this.OwnerTemplate.MasterTemplate != null || this.OwnerTemplate.MasterTemplate.Owner == null)
          return (IContainer) null;
        IComponent owner = (IComponent) this.OwnerTemplate.MasterTemplate.Owner;
        if (owner != null)
        {
          ISite site = owner.Site;
          if (site != null)
            return site.Container;
        }
        return (IContainer) null;
      }
    }

    object ITypeDescriptorContext.Instance
    {
      get
      {
        return (object) this;
      }
    }

    void ITypeDescriptorContext.OnComponentChanged()
    {
    }

    bool ITypeDescriptorContext.OnComponentChanging()
    {
      return true;
    }

    PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
    {
      get
      {
        return (PropertyDescriptor) null;
      }
    }

    object IServiceProvider.GetService(Type serviceType)
    {
      if ((object) serviceType == (object) typeof (GridViewDataColumn))
        return (object) this;
      return (object) null;
    }

    public void CreateSnapshot()
    {
      if (this.distinctValuesWithFilter == null)
        return;
      this.distinctValuesWithFilterSnapshot = new GridViewColumnValuesCollection();
      foreach (object obj in this.distinctValuesWithFilter)
        this.distinctValuesWithFilterSnapshot.Add(obj);
    }
  }
}
