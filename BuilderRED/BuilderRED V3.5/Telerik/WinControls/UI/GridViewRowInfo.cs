// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public abstract class GridViewRowInfo : IDataItem, IHierarchicalRow, INotifyPropertyChanged, INotifyPropertyChangingEx, IDisposable
  {
    protected BitVector32 state = new BitVector32();
    protected const int IsAttachedState = 1;
    protected const int IsModifiedState = 2;
    protected const int IsCurrentState = 4;
    protected const int IsSelectedState = 8;
    protected const int IsExpandedState = 16;
    protected const int IsVisibleState = 32;
    protected const int AllowResizeState = 64;
    protected const int SuspendNotificationsState = 128;
    protected const int IsInitializedState = 256;
    protected const int LastRowInfoState = 256;
    private object dataBoundItem;
    private GridViewInfo viewInfo;
    private GridViewRowInfo.GridViewRowInfoState rowState;
    private GridViewRowInfo parent;
    private Hashtable searchCache;
    internal bool AddingNewDataRow;

    public GridViewRowInfo(GridViewInfo viewInfo)
    {
      this.viewInfo = viewInfo;
      this.state[32] = true;
      this.state[64] = true;
      this.state[1] = false;
      this.parent = (GridViewRowInfo) null;
    }

    protected internal void SetParent(GridViewRowInfo parent)
    {
      this.parent = parent;
    }

    internal virtual bool IsValid
    {
      get
      {
        return true;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsInitialized
    {
      get
      {
        return this.state[256];
      }
      set
      {
        this.state[256] = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsAttached
    {
      get
      {
        return this.state[1];
      }
      set
      {
        this.state[1] = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal virtual object this[GridViewColumn column]
    {
      get
      {
        if (this.IsAttached)
          return column.Accessor[this];
        if (this.rowState == null)
          return (object) null;
        return this.Cache[column];
      }
      set
      {
        bool flag = this.ViewTemplate.MasterTemplate != null && this.ViewTemplate.MasterTemplate.VirtualMode;
        if (!flag && !this.AddingNewDataRow && !this.ViewTemplate.IsUpdating)
        {
          GridViewCollectionChangingEventArgs args = new GridViewCollectionChangingEventArgs(this.ViewTemplate, Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanging, (object) this, (object) this, this.Index, new PropertyChangingEventArgsEx(column.FieldName, this[column], value));
          this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.RowsChanging, (object) this, args);
          if (args.Cancel)
            return;
        }
        if (this.IsAttached)
        {
          column.Accessor[this] = value;
        }
        else
        {
          this.Cache[column] = value;
          if (flag || this.IsSystem)
            return;
          GridViewCollectionChangedEventArgs args = new GridViewCollectionChangedEventArgs(this.ViewTemplate, Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged, (object) this, (object) this, this.Index, column.FieldName);
          this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCollectionChangedEventArgs>(EventDispatcher.RowsChanged, (object) this, args);
        }
      }
    }

    internal GridViewRowInfoCache Cache
    {
      get
      {
        this.EnsureRowInfoState();
        if (this.rowState.Cache == null)
          this.rowState.Cache = new GridViewRowInfoCache();
        return this.rowState.Cache;
      }
      set
      {
        this.EnsureRowInfoState();
        this.rowState.Cache = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Hashtable SearchCache
    {
      get
      {
        if (this.searchCache == null)
          this.searchCache = new Hashtable();
        return this.searchCache;
      }
      internal set
      {
        this.searchCache = value;
      }
    }

    [Browsable(false)]
    public virtual bool IsSystem
    {
      get
      {
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsModified
    {
      get
      {
        return this.state[2];
      }
      internal set
      {
        this.state[2] = value;
      }
    }

    public string ErrorText
    {
      get
      {
        if (this.rowState != null && !string.IsNullOrEmpty(this.rowState.ErrorText))
          return this.rowState.ErrorText;
        DataRowView dataBoundItem1 = this.DataBoundItem as DataRowView;
        if (dataBoundItem1 != null && !dataBoundItem1.Row.HasErrors)
          return string.Empty;
        IDataErrorInfo dataBoundItem2 = this.DataBoundItem as IDataErrorInfo;
        if (dataBoundItem2 != null)
          return dataBoundItem2.Error;
        return string.Empty;
      }
      set
      {
        if (!(this.ErrorText != value))
          return;
        this.EnsureRowInfoState();
        this.SetRowStateProperty<string>(nameof (ErrorText), ref this.rowState.ErrorText, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual DataGroup Group
    {
      get
      {
        for (GridViewRowInfo parent = this.Parent as GridViewRowInfo; parent != null; parent = parent.Parent as GridViewRowInfo)
        {
          if (parent is GridViewGroupRowInfo)
            return parent.Group;
        }
        return (DataGroup) null;
      }
      internal set
      {
      }
    }

    public virtual int Index
    {
      get
      {
        if (this.Parent != null)
          return this.Parent.ChildRows.IndexOf(this);
        if (this.IsAttached)
          return this.ViewTemplate.ChildRows.IndexOf(this);
        return -1;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
      internal set
      {
        this.viewInfo = value;
      }
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        if (this.ViewInfo != null)
          return this.ViewInfo.ViewTemplate;
        return (GridViewTemplate) null;
      }
    }

    public GridViewCellInfoCollection Cells
    {
      get
      {
        if (this.rowState != null && this.rowState.Cells != null)
          return this.rowState.Cells;
        return new GridViewCellInfoCollection(this);
      }
    }

    public object Tag
    {
      get
      {
        if (this.rowState == null)
          return (object) null;
        return this.rowState.Tag;
      }
      set
      {
        if (this.Tag == value)
          return;
        this.EnsureRowInfoState();
        this.SetRowStateProperty<object>(nameof (Tag), ref this.rowState.Tag, value);
      }
    }

    public virtual object DataBoundItem
    {
      get
      {
        return ((IDataItem) this).DataBoundItem;
      }
    }

    [DefaultValue(5)]
    public int MinHeight
    {
      get
      {
        if (this.rowState == null)
          return 5;
        return this.rowState.MinHeight;
      }
      set
      {
        if (this.MinHeight == value)
          return;
        this.EnsureRowInfoState();
        this.SetRowStateProperty<int>(nameof (MinHeight), ref this.rowState.MinHeight, value);
        if (this.Height >= value || this.Height == -1)
          return;
        this.Height = value;
      }
    }

    [DefaultValue(-1)]
    public int MaxHeight
    {
      get
      {
        if (this.rowState == null)
          return -1;
        return this.rowState.MaxHeight;
      }
      set
      {
        if (this.MaxHeight == value)
          return;
        this.EnsureRowInfoState();
        this.SetRowStateProperty<int>(nameof (MaxHeight), ref this.rowState.MaxHeight, value);
        if (this.Height <= value)
          return;
        this.Height = value;
      }
    }

    public int Height
    {
      get
      {
        if (this.rowState == null)
          return -1;
        return this.rowState.Height;
      }
      set
      {
        if (this.Height == value)
          return;
        this.EnsureRowInfoState();
        this.SetRowStateProperty<int>(nameof (Height), ref this.rowState.Height, value);
      }
    }

    public bool IsCurrent
    {
      get
      {
        return this.state[4];
      }
      set
      {
        this.SetBooleanProperty(nameof (IsCurrent), 4, value);
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.state[8];
      }
      set
      {
        this.SetBooleanProperty(nameof (IsSelected), 8, value);
      }
    }

    public virtual bool IsExpanded
    {
      get
      {
        return this.state[16];
      }
      set
      {
        this.SetBooleanProperty(nameof (IsExpanded), 16, value);
      }
    }

    public bool IsVisible
    {
      get
      {
        return this.state[32];
      }
      set
      {
        this.SetBooleanProperty(nameof (IsVisible), 32, value);
      }
    }

    public virtual bool IsPinned
    {
      get
      {
        return this.PinPosition != PinnedRowPosition.None;
      }
      set
      {
        if (value)
          this.PinPosition = PinnedRowPosition.Top;
        else
          this.PinPosition = PinnedRowPosition.None;
      }
    }

    public virtual PinnedRowPosition PinPosition
    {
      get
      {
        if (this.rowState == null)
          return PinnedRowPosition.None;
        return this.rowState.PinPosition;
      }
      set
      {
        if (this.PinPosition == value)
          return;
        this.EnsureRowInfoState();
        this.SetRowStateProperty<PinnedRowPosition>(nameof (PinPosition), ref this.rowState.PinPosition, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool IsEditable
    {
      get
      {
        return true;
      }
    }

    public bool IsOdd
    {
      get
      {
        return this.Index % 2 == 1;
      }
    }

    public virtual bool AllowResize
    {
      get
      {
        return this.state[64];
      }
      set
      {
        this.SetBooleanProperty(nameof (AllowResize), 64, value);
      }
    }

    internal bool CanBeCurrent
    {
      get
      {
        return (this.AllowedStates & AllowedGridViewRowInfoStates.Current) == AllowedGridViewRowInfoStates.Current;
      }
    }

    internal bool CanBeSelected
    {
      get
      {
        return (this.AllowedStates & AllowedGridViewRowInfoStates.Selected) == AllowedGridViewRowInfoStates.Selected;
      }
    }

    internal bool CanBeExpanded
    {
      get
      {
        return (this.AllowedStates & AllowedGridViewRowInfoStates.Expanadable) == AllowedGridViewRowInfoStates.Expanadable;
      }
    }

    public virtual AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.Current | AllowedGridViewRowInfoStates.Selected;
      }
    }

    public virtual Type RowElementType
    {
      get
      {
        return typeof (GridDataRowElement);
      }
    }

    public virtual int GetActualHeight(IGridView gridView)
    {
      GridTableElement gridTableElement = gridView as GridTableElement;
      if (gridTableElement != null)
        return (int) gridTableElement.RowScroller.ElementProvider.GetElementSize(this).Height;
      return this.Height;
    }

    public virtual void InvalidateRow()
    {
      this.DispatchEvent(KnownEvents.RowInvalidated, GridEventType.UI, GridEventDispatchMode.Send, (object) this.ViewTemplate, (object[]) null);
    }

    public void EnsureVisible()
    {
      this.ViewTemplate.OnViewChanged((object) this.ViewTemplate, new DataViewChangedEventArgs(ViewChangedAction.EnsureRowVisible, (object) this));
    }

    public void EnsureVisible(bool expandParentRows)
    {
      if (expandParentRows)
      {
        for (GridViewRowInfo parent = this.Parent as GridViewRowInfo; parent != null; parent = parent.Parent as GridViewRowInfo)
          parent.IsExpanded = true;
      }
      this.EnsureVisible();
    }

    public virtual void Delete()
    {
      this.ViewTemplate.Rows.Remove(this);
    }

    public string GetErrorText(string fieldName)
    {
      if (!this.ViewTemplate.ListSource.IsDataBound || this.ViewTemplate.ListSource.BoundProperties.Find(fieldName, !this.ViewTemplate.ListSource.UseCaseSensitiveFieldNames) == null || this.rowState != null && !string.IsNullOrEmpty(this.rowState.ErrorText))
        return string.Empty;
      IDataErrorInfo dataBoundItem = this.DataBoundItem as IDataErrorInfo;
      if (!string.IsNullOrEmpty(fieldName) && dataBoundItem != null)
        return dataBoundItem[fieldName];
      return string.Empty;
    }

    public virtual bool HasChildRows()
    {
      return this.ChildRows.Count > 0;
    }

    public void SuspendPropertyNotifications()
    {
      this.state[128] = true;
    }

    public void ResumePropertyNotifications()
    {
      this.state[128] = false;
    }

    public T FindParent<T>() where T : IHierarchicalRow
    {
      for (IHierarchicalRow parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is T)
          return (T) parent;
      }
      return default (T);
    }

    internal void Attach()
    {
      this.IsAttached = true;
    }

    internal void Detach()
    {
      if (this.rowState != null)
      {
        for (int index = 0; index < this.ViewTemplate.ColumnCount; ++index)
          this.Cache[(GridViewColumn) this.ViewTemplate.Columns[index]] = this[(GridViewColumn) this.ViewTemplate.Columns[index]];
      }
      this.IsAttached = false;
    }

    internal bool CallOnBeginEdit()
    {
      return this.OnBeginEdit();
    }

    internal bool CallOnEndEdit()
    {
      return this.OnEndEdit();
    }

    object IDataItem.DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
      }
      set
      {
        if (value == this.dataBoundItem)
          return;
        this.dataBoundItem = value;
        if (value == null)
          return;
        this.Attach();
      }
    }

    int IDataItem.FieldCount
    {
      get
      {
        if (this.ViewTemplate == null)
          return 0;
        return this.ViewTemplate.Columns.Count;
      }
    }

    object IDataItem.this[int index]
    {
      get
      {
        return this[(GridViewColumn) this.ViewTemplate.Columns[index]];
      }
      set
      {
        this[(GridViewColumn) this.ViewTemplate.Columns[index]] = value;
      }
    }

    object IDataItem.this[string name]
    {
      get
      {
        if ((GridViewColumn) this.ViewTemplate.Columns[name] == null)
          throw new ArgumentException(string.Format("Field name: {0} does not exist in the template.", (object) name));
        return this[(GridViewColumn) this.ViewTemplate.Columns[name]];
      }
      set
      {
        this[(GridViewColumn) this.ViewTemplate.Columns[name]] = value;
      }
    }

    int IDataItem.IndexOf(string name)
    {
      return this.ViewTemplate.Columns.IndexOf(name);
    }

    public virtual int HierarchyLevel
    {
      get
      {
        int num = 0;
        for (IHierarchicalRow parent = this.Parent; parent != null && !(parent is MasterGridViewTemplate); parent = parent.Parent)
          ++num;
        return num;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IHierarchicalRow Parent
    {
      get
      {
        if (this.parent == null && this.ViewInfo != null)
          this.parent = this.ViewInfo.FindParent(this) as GridViewRowInfo;
        if (this.parent == null && this.ViewTemplate != null && this.ViewTemplate.HierarchyDataProvider != null)
          this.parent = (GridViewRowInfo) this.ViewTemplate.HierarchyDataProvider.GetParent(this);
        return (IHierarchicalRow) this.parent;
      }
    }

    public virtual GridViewChildRowCollection ChildRows
    {
      get
      {
        return GridViewChildRowCollection.Empty;
      }
    }

    public virtual bool HasChildViews
    {
      get
      {
        return false;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public event PropertyChangingEventHandlerEx PropertyChanging;

    public void Dispose()
    {
      this.SuspendPropertyNotifications();
      if (this.ViewInfo != null)
      {
        this.ViewTemplate.MasterTemplate.SelectedRows.BeginUpdate();
        this.ViewTemplate.MasterTemplate.SelectedRows.Remove(this);
        this.ViewTemplate.MasterTemplate.SelectedRows.EndUpdate(false);
        this.ViewInfo.PinnedRows.Remove(this);
      }
      this.ResumePropertyNotifications();
      this.parent = (GridViewRowInfo) null;
      this.rowState = (GridViewRowInfo.GridViewRowInfoState) null;
      this.viewInfo = (GridViewInfo) null;
      this.dataBoundItem = (object) null;
      GC.SuppressFinalize((object) this);
    }

    protected virtual void DispatchEvent(
      KnownEvents id,
      GridEventType type,
      GridEventDispatchMode dispatchMode,
      object originator,
      object[] arguments)
    {
      if (this.ViewTemplate == null)
        return;
      GridViewEventInfo eventInfo = new GridViewEventInfo(id, type, dispatchMode);
      GridViewSynchronizationService.DispatchEvent(this.ViewTemplate, new GridViewEvent((object) this, originator, arguments, eventInfo), false);
    }

    protected virtual GridEventType GetEventInfo(
      GridPropertyChangedEventArgs property,
      out GridEventDispatchMode dispatchMode)
    {
      dispatchMode = GridEventDispatchMode.Send;
      return GridEventType.UI;
    }

    protected virtual bool OnBeginEdit()
    {
      return true;
    }

    protected virtual bool OnEndEdit()
    {
      return true;
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgsEx args)
    {
      if (this.ViewTemplate != null && this.ViewTemplate.MasterTemplate != null)
      {
        MasterGridViewTemplate masterTemplate = this.ViewTemplate.MasterTemplate;
        if (args.PropertyName == "IsCurrent")
          args.Cancel = this.OnIsCurrentPropertyChanging(args);
        else if (args.PropertyName == "IsSelected")
        {
          if ((bool) args.NewValue && (!this.CanBeSelected || masterTemplate.SelectionMode != GridViewSelectionMode.FullRowSelect))
            args.Cancel = true;
        }
        else if (args.PropertyName == "Height")
          args.Cancel = this.OnHeightPropertyChanging(args);
      }
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, args);
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
    {
      if (args.PropertyName == "PinPosition")
        this.ViewInfo.PinnedRows.UpdateRow(this);
      else if (args.PropertyName == "IsSelected")
      {
        MasterGridViewTemplate masterTemplate = this.ViewTemplate.MasterTemplate;
        if (masterTemplate != null && masterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        {
          if (this.IsSelected)
          {
            masterTemplate.SelectedRows.BeginUpdate();
            if (!masterTemplate.MultiSelect)
              masterTemplate.SelectedRows.Clear();
            masterTemplate.SelectedRows.Add(this, true);
            masterTemplate.SelectedRows.EndUpdate(true);
          }
          else
            masterTemplate.SelectedRows.Remove(this);
        }
      }
      else if (args.PropertyName == "Height" || args.PropertyName == "MinHeight" || args.PropertyName == "MaxHeight")
      {
        RowHeightChangedEventArgs args1 = new RowHeightChangedEventArgs(this);
        this.ViewTemplate.MasterTemplate.EventDispatcher.RaiseEvent<RowHeightChangedEventArgs>(EventDispatcher.RowHeightChanged, (object) this, args1);
      }
      GridEventDispatchMode dispatchMode = GridEventDispatchMode.Send;
      this.DispatchEvent(KnownEvents.PropertyChanged, this.GetEventInfo(args as GridPropertyChangedEventArgs, out dispatchMode), dispatchMode, (object) null, new object[1]
      {
        (object) args
      });
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, args);
    }

    private bool OnIsCurrentPropertyChanging(PropertyChangingEventArgsEx args)
    {
      if (GridViewSynchronizationService.IsEventSuspended(this.ViewTemplate, KnownEvents.CurrentChanged))
        return false;
      bool newValue = (bool) args.NewValue;
      if (newValue && !this.CanBeCurrent)
        return true;
      GridViewRowInfo row = newValue ? this : (GridViewRowInfo) null;
      GridViewColumn column = this.ViewTemplate.CurrentColumn ?? MasterGridViewTemplate.GetColumnAllowingForCurrent(this.ViewTemplate);
      GridViewSynchronizationService.RaiseCurrentChanged(this.ViewTemplate, row, column, true);
      return this.ViewTemplate.MasterTemplate.CurrentRow != row;
    }

    private bool OnHeightPropertyChanging(PropertyChangingEventArgsEx args)
    {
      int newValue = (int) args.NewValue;
      if (this.MaxHeight > this.MinHeight && this.MaxHeight > 0 && (int) args.NewValue > this.MaxHeight)
        args.NewValue = (object) this.MaxHeight;
      if (this.MinHeight >= 0 && (int) args.NewValue < this.MinHeight)
        args.NewValue = (object) this.MinHeight;
      RowHeightChangingEventArgs args1 = new RowHeightChangingEventArgs(this, (int) args.NewValue);
      this.ViewTemplate.MasterTemplate.EventDispatcher.RaiseEvent<RowHeightChangingEventArgs>(EventDispatcher.RowHeightChanging, (object) this, args1);
      return args1.Cancel;
    }

    private void EnsureRowInfoState()
    {
      if (this.rowState != null)
        return;
      this.rowState = new GridViewRowInfo.GridViewRowInfoState();
    }

    protected internal virtual void ClearCache()
    {
      if (this.rowState == null || this.rowState.Cache == null)
        return;
      this.rowState.Cache.Clear();
    }

    internal void PersistCellInfoCollection(ref GridViewCellInfoCollection collection)
    {
      this.EnsureRowInfoState();
      if (this.rowState.Cells == null)
        this.rowState.Cells = collection;
      else
        collection = this.rowState.Cells;
    }

    protected virtual bool SetRowStateProperty<T>(
      string propertyName,
      ref T propertyField,
      T value)
    {
      T obj = propertyField;
      if (!this.state[128])
      {
        PropertyChangingEventArgsEx args = new PropertyChangingEventArgsEx(propertyName, (object) propertyField, (object) value);
        this.OnPropertyChanging(args);
        if (args.Cancel)
          return false;
      }
      propertyField = value;
      if (!this.state[128])
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(propertyName, (object) obj, (object) value));
      return true;
    }

    protected virtual bool SetBooleanProperty(string propertyName, int propertyKey, bool value)
    {
      bool flag = this.state[propertyKey];
      if (flag == value)
        return false;
      if (!this.state[128])
      {
        PropertyChangingEventArgsEx args = new PropertyChangingEventArgsEx(propertyName, (object) flag, (object) value);
        this.OnPropertyChanging(args);
        if (args.Cancel)
          return false;
      }
      this.state[propertyKey] = value;
      if (!this.state[128])
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(propertyName, (object) flag, (object) value));
      return true;
    }

    private class GridViewRowInfoState
    {
      public int MinHeight = 5;
      public int MaxHeight = -1;
      public int Height = -1;
      public PinnedRowPosition PinPosition = PinnedRowPosition.None;
      public GridViewRowInfoCache Cache;
      public string ErrorText;
      public object Tag;
      public GridViewCellInfoCollection Cells;
    }
  }
}
