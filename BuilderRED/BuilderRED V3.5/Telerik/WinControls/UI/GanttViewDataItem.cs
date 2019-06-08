// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GanttViewDataItem : RadObject, IDataItem
  {
    public static RadProperty IsMouseOverProperty = RadProperty.Register("IsMouseOver", typeof (bool), typeof (GanttViewDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (GanttViewDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CurrentProperty = RadProperty.Register(nameof (Current), typeof (bool), typeof (GanttViewDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnabledProperty = RadProperty.Register(nameof (Enabled), typeof (bool), typeof (GanttViewDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty VisibleProperty = RadProperty.Register("IsVisible", typeof (bool), typeof (GanttViewDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ReadOnlyProperty = RadProperty.Register(nameof (ReadOnly), typeof (bool), typeof (GanttViewDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    protected BitVector32 state = new BitVector32();
    protected const int SuspendNotificationsState = 1;
    protected const int IsExpandedState = 2;
    protected const int IsAllowDropState = 4;
    private GanttViewDataItemCach cache;
    private object tag;
    private object dataBoundItem;
    private int boundIndex;
    private string title;
    private DateTime start;
    private DateTime end;
    private Decimal progress;
    internal GanttViewDataItem parent;
    internal GanttViewDataItemCollection items;
    private RadGanttViewElement ganttView;
    private RadContextMenu contextMenu;

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        if (this.ganttView == null)
          this.ganttView = this.FindGanttView();
        return this.ganttView;
      }
      internal set
      {
        if (value == this.ganttView)
          return;
        this.ganttView = value;
        foreach (GanttViewDataItem ganttViewDataItem in (Collection<GanttViewDataItem>) this.Items)
          ganttViewDataItem.GanttViewElement = value;
      }
    }

    public GanttViewDataItem()
    {
      this.state[2] = true;
    }

    [DefaultValue("")]
    [Description("Gets or sets the title.")]
    public string Title
    {
      get
      {
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.TitleDescriptor != null)
            return Convert.ToString(descriptor.GetTitle(this));
        }
        return this.title;
      }
      set
      {
        if (!(this.Title != value))
          return;
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.TitleDescriptor != null)
            descriptor.SetTitle(this, (object) value);
        }
        else
          this.title = value;
        this.OnNotifyPropertyChanged(nameof (Title));
        this.UpdateParent();
        this.Update(RadGanttViewElement.UpdateActions.ItemEdited);
        if (this.GanttViewElement == null)
          return;
        this.GanttViewElement.OnItemChanged(new GanttViewItemChangedEventArgs(this, nameof (Title)));
      }
    }

    [Description("Gets or sets the start.")]
    public DateTime Start
    {
      get
      {
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.StartDescriptor != null)
            return Convert.ToDateTime(descriptor.GetStart(this));
        }
        return this.start;
      }
      set
      {
        if (!(this.Start != value))
          return;
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.StartDescriptor != null)
            descriptor.SetStart(this, (object) value);
        }
        else
          this.start = value;
        this.OnNotifyPropertyChanged(nameof (Start));
        this.UpdateParent();
        this.Update(RadGanttViewElement.UpdateActions.ItemEdited);
        if (this.GanttViewElement != null)
          this.GanttViewElement.OnItemChanged(new GanttViewItemChangedEventArgs(this, nameof (Start)));
        if (!(value > this.End) || this.GanttViewElement == null)
          return;
        this.End = value.Add(new TimeSpan(this.GanttViewElement.GraphicalViewElement.OnePixelTime.Ticks * 20L));
      }
    }

    [Description("Gets or sets the end.")]
    public DateTime End
    {
      get
      {
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.EndDescriptor != null)
            return Convert.ToDateTime(descriptor.GetEnd(this));
        }
        return this.end;
      }
      set
      {
        if (!(this.End != value) || !(value != DateTime.MinValue))
          return;
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.EndDescriptor != null)
            descriptor.SetEnd(this, (object) value);
        }
        else
          this.end = value;
        this.OnNotifyPropertyChanged(nameof (End));
        this.UpdateParent();
        this.Update(RadGanttViewElement.UpdateActions.ItemEdited);
        if (this.GanttViewElement != null)
          this.GanttViewElement.OnItemChanged(new GanttViewItemChangedEventArgs(this, nameof (End)));
        if (!(value < this.Start) || this.GanttViewElement == null)
          return;
        this.Start = value.Subtract(new TimeSpan(this.GanttViewElement.GraphicalViewElement.OnePixelTime.Ticks * 20L));
      }
    }

    [DefaultValue(0)]
    [Description("Gets or sets the progress.")]
    public Decimal Progress
    {
      get
      {
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.ProgressDescriptor != null)
            return Convert.ToDecimal(descriptor.GetProgress(this));
        }
        return this.progress;
      }
      set
      {
        if (!(this.Progress != value))
          return;
        if (this.dataBoundItem != null && this.parent != null)
        {
          GanttTaskDescriptor descriptor = this.GanttViewElement.BindingProvider.Descriptor;
          if (descriptor.ProgressDescriptor != null)
            descriptor.SetProgress(this, (object) value);
        }
        else
          this.progress = value;
        this.OnNotifyPropertyChanged(nameof (Progress));
        this.UpdateParent();
        this.Update(RadGanttViewElement.UpdateActions.ItemEdited);
        if (this.GanttViewElement == null)
          return;
        this.GanttViewElement.OnItemChanged(new GanttViewItemChangedEventArgs(this, nameof (Progress)));
      }
    }

    [Description("Gets or sets a value indicating whether this GanttViewDataItem is visible.")]
    [DefaultValue(null)]
    public bool Visible
    {
      get
      {
        return (bool) this.GetValue(GanttViewDataItem.VisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewDataItem.VisibleProperty, (object) value);
      }
    }

    internal int BoundIndex
    {
      get
      {
        return this.boundIndex;
      }
      set
      {
        this.boundIndex = value;
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the tag.")]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        this.tag = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public GanttViewDataItemCollection Items
    {
      get
      {
        if (this.items == null)
          this.items = this.CreateChildItemsCollection();
        if (this.ganttView != null && this.items.NeedsRefresh)
        {
          this.GanttViewElement.BeginUpdate();
          this.GanttViewElement.BindingProvider.SuspendUpdate();
          this.items = this.CreateChildItemsCollection();
          this.items.BeginUpdate();
          IList<GanttViewDataItem> items = this.ganttView.BindingProvider.GetItems(this);
          if (items != null)
            this.items.AddRange((IEnumerable<GanttViewDataItem>) items);
          this.items.UpdateView();
          this.items.EndUpdate(false);
          this.items.SyncVersion();
          this.GanttViewElement.BindingProvider.ResumeUpdate();
          this.GanttViewElement.EndUpdate(false, RadGanttViewElement.UpdateActions.Resume);
        }
        return this.items;
      }
    }

    [Description("Gets or sets a value indicating whether the item is current.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(false)]
    public bool Current
    {
      get
      {
        return (bool) this.GetValue(GanttViewDataItem.CurrentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewDataItem.CurrentProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets a value indicating whether the item is selected.")]
    [DefaultValue(false)]
    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(GanttViewDataItem.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewDataItem.SelectedProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the item is enabled.")]
    public bool Enabled
    {
      get
      {
        return (bool) this.GetValue(GanttViewDataItem.EnabledProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewDataItem.EnabledProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int Level
    {
      get
      {
        int num = 1;
        for (GanttViewDataItem parent = this.Parent; parent != null; parent = parent.Parent)
        {
          if (num > (int) short.MaxValue)
            throw new OverflowException(string.Format("GanttViewDataItem can contain {0} levels of items.", (object) short.MaxValue));
          ++num;
        }
        return num;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GanttViewDataItem Parent
    {
      get
      {
        if (this.parent is RadGanttViewElement.RootDataItem)
          return (GanttViewDataItem) null;
        return this.parent;
      }
      internal set
      {
        if (this.parent == value)
          return;
        if (value == null)
        {
          this.Current = false;
          this.Selected = false;
          if (this.parent != null)
            this.parent.OnChildRemoved(this);
        }
        this.parent = value;
        if (this.parent == null)
          this.ClearChildrenState();
        else
          this.parent.OnChildAdded(this);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int Index
    {
      get
      {
        if (this.parent == null)
          return -1;
        return this.parent.Items.IndexOf(this);
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether this item is expanded.")]
    public bool Expanded
    {
      get
      {
        return this.state[2];
      }
      set
      {
        if (this.Expanded == value || this.GanttViewElement != null && !this.GanttViewElement.OnItemExpandedChanging(this))
          return;
        this.SetBooleanProperty(nameof (Expanded), 2, value);
        if (this.GanttViewElement != null)
          this.GanttViewElement.OnItemExpandedChanged(new GanttViewExpandedChangedEventArgs(this));
        this.NotifyExpandedChanged(this);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GanttViewDataItem NextItem
    {
      get
      {
        if (this.parent == null)
          return (GanttViewDataItem) null;
        int num = this.parent.Items.IndexOf(this);
        if (num < 0)
          return (GanttViewDataItem) null;
        int index = num + 1;
        if (index < this.parent.Items.Count)
          return this.parent.Items[index];
        return (GanttViewDataItem) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GanttViewDataItem PrevVisibleItem
    {
      get
      {
        if (this.parent == null)
          return (GanttViewDataItem) null;
        for (int index = this.parent.Items.IndexOf(this) - 1; index >= 0; --index)
        {
          GanttViewDataItem parent = this.parent.Items[index];
          if (parent.Visible)
          {
            if (parent.Expanded)
            {
              GanttViewDataItem lastVisibleItem = this.GetLastVisibleItem(parent);
              if (lastVisibleItem != null)
                return lastVisibleItem;
            }
            return parent;
          }
        }
        if (this.parent == this.GanttViewElement.Root)
          return (GanttViewDataItem) null;
        return this.parent;
      }
    }

    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return (bool) this.GetValue(GanttViewDataItem.ReadOnlyProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewDataItem.ReadOnlyProperty, (object) value);
      }
    }

    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the shortcut menu associated to the node.")]
    public virtual RadContextMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        this.contextMenu = value;
        this.OnNotifyPropertyChanged(nameof (ContextMenu));
      }
    }

    public bool IsAttached
    {
      get
      {
        return this.GanttViewElement != null;
      }
    }

    public object DataBoundItem
    {
      get
      {
        return ((IDataItem) this).DataBoundItem;
      }
    }

    object IDataItem.DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
      }
      set
      {
        this.SetDataBoundItem(false, value);
      }
    }

    int IDataItem.FieldCount
    {
      get
      {
        return this.GanttViewElement.Columns.Count;
      }
    }

    object IDataItem.this[string name]
    {
      get
      {
        return this[this.GanttViewElement.Columns[name]];
      }
      set
      {
        this[this.GanttViewElement.Columns[name]] = value;
      }
    }

    object IDataItem.this[int index]
    {
      get
      {
        return this[this.GanttViewElement.Columns[index]];
      }
      set
      {
        this[this.GanttViewElement.Columns[index]] = value;
      }
    }

    public object this[GanttViewTextViewColumn column]
    {
      get
      {
        return column.Accessor[this];
      }
      set
      {
        if (column.FieldName == this.GanttViewElement.StartMember)
          this.Start = Convert.ToDateTime(value);
        else if (column.FieldName == this.GanttViewElement.EndMember)
          this.End = Convert.ToDateTime(value);
        else if (column.FieldName == this.GanttViewElement.TitleMember)
          this.Title = Convert.ToString(value);
        else if (column.FieldName == this.GanttViewElement.ProgressMember)
        {
          this.Progress = Convert.ToDecimal(value);
        }
        else
        {
          column.Accessor[this] = value;
          if (this.GanttViewElement != null)
            this.GanttViewElement.OnItemChanged(new GanttViewItemChangedEventArgs(this, column.FieldName));
        }
        if (this.GanttViewElement.AllowSummaryEditing || this.Parent == null)
          return;
        if (column.FieldName == this.GanttViewElement.StartMember)
          this.Parent.OnChildStartChanged(this);
        else if (column.FieldName == this.GanttViewElement.EndMember)
        {
          this.Parent.OnChildEndChanged(this);
        }
        else
        {
          if (!(column.FieldName == this.GanttViewElement.ProgressMember))
            return;
          this.Parent.OnChildProgressChanged(this);
        }
      }
    }

    int IDataItem.IndexOf(string name)
    {
      return this.GanttViewElement.Columns.IndexOf(name);
    }

    internal GanttViewDataItemCach Cache
    {
      get
      {
        if (this.cache == null)
          this.cache = new GanttViewDataItemCach();
        return this.cache;
      }
      set
      {
        this.cache = value;
      }
    }

    protected virtual bool SetBooleanProperty(string propertyName, int propertyKey, bool value)
    {
      if (this.state[propertyKey] == value)
        return false;
      this.state[propertyKey] = value;
      if (!this.state[1])
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
      return true;
    }

    protected virtual void NotifyExpandedChanged(GanttViewDataItem item)
    {
      if (this.GanttViewElement == null)
        return;
      if (item != null)
      {
        this.GanttViewElement.Update(RadGanttViewElement.UpdateActions.ExpandedChanged, item);
        ControlTraceMonitor.TrackAtomicFeature((RadElement) this.GanttViewElement, item.Expanded ? "Expanded" : "Collapsed", (object) item.Title);
      }
      else
        this.GanttViewElement.Update(RadGanttViewElement.UpdateActions.ExpandedChanged);
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (!this.IsAttached)
        return;
      if (this.DataBoundItem == null && this.GanttViewElement != null)
      {
        foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.GanttViewElement.Columns)
        {
          if (e.PropertyName == column.FieldName)
            this.Cache.Remove(column);
        }
      }
      base.OnNotifyPropertyChanged(e);
      if (e.PropertyName == "Selected" && this.GanttViewElement != null)
      {
        this.GanttViewElement.ProcessSelection(this);
        ControlTraceMonitor.TrackAtomicFeature((RadElement) this.GanttViewElement, "SelectionChanged");
      }
      if (this.Parent == null || this.GanttViewElement.AllowSummaryEditing)
        return;
      if (e.PropertyName == "Start")
        this.Parent.OnChildStartChanged(this);
      else if (e.PropertyName == "End")
      {
        this.Parent.OnChildEndChanged(this);
      }
      else
      {
        if (!(e.PropertyName == "Progress"))
          return;
        this.Parent.OnChildProgressChanged(this);
      }
    }

    public void Expand()
    {
      this.Expanded = true;
      this.NotifyExpandedChanged((GanttViewDataItem) null);
    }

    private void Update(RadGanttViewElement.UpdateActions updateActions)
    {
      if (this.state[1] || this.GanttViewElement == null)
        return;
      this.GanttViewElement.Update(updateActions, this);
    }

    private void UpdateParent()
    {
      if (this.parent == null || this.parent.items == null || (this.GanttViewElement == null || this.GanttViewElement.BindingProvider.IsDataBound) || this.GanttViewElement.FilterDescriptors.Count <= 0)
        return;
      this.parent.items.UpdateView();
    }

    protected internal virtual void SetDataBoundItem(bool dataBinding, object value)
    {
      this.dataBoundItem = value;
      if (this.GanttViewElement != null && this.GanttViewElement.DataSource != null)
        this.GanttViewElement.OnItemDataBound(new GanttViewItemDataBoundEventArgs(this));
      this.OnNotifyPropertyChanged("DataBoundItem");
    }

    private RadGanttViewElement FindGanttView()
    {
      GanttViewDataItem ganttViewDataItem = this;
      while (ganttViewDataItem.parent != null)
        ganttViewDataItem = ganttViewDataItem.parent;
      return ganttViewDataItem.ganttView;
    }

    private void ClearChildrenState()
    {
      Queue<GanttViewDataItemCollection> dataItemCollectionQueue = new Queue<GanttViewDataItemCollection>();
      if (this.items != null)
        dataItemCollectionQueue.Enqueue(this.items);
      while (dataItemCollectionQueue.Count > 0)
      {
        foreach (GanttViewDataItem ganttViewDataItem in (Collection<GanttViewDataItem>) dataItemCollectionQueue.Dequeue())
        {
          ganttViewDataItem.Current = false;
          ganttViewDataItem.Selected = false;
          ganttViewDataItem.GanttViewElement = (RadGanttViewElement) null;
          if (ganttViewDataItem.items != null && ganttViewDataItem.items.Count > 0)
            dataItemCollectionQueue.Enqueue(ganttViewDataItem.items);
        }
      }
    }

    private GanttViewDataItem GetLastVisibleItem(GanttViewDataItem parent)
    {
      for (int index = parent.Items.Count - 1; index >= 0; --index)
      {
        GanttViewDataItem parent1 = parent.Items[index];
        if (parent1.Visible)
        {
          if (parent1.Expanded && parent1.Items.Count > 0)
            return this.GetLastVisibleItem(parent1);
          return parent1;
        }
      }
      return (GanttViewDataItem) null;
    }

    public override string ToString()
    {
      return string.Format("{0}", (object) this.Title);
    }

    protected virtual void OnChildStartChanged(GanttViewDataItem child)
    {
      this.InvalidateStartAndEnd();
    }

    protected virtual void OnChildEndChanged(GanttViewDataItem child)
    {
      this.InvalidateStartAndEnd();
    }

    protected virtual void OnChildProgressChanged(GanttViewDataItem child)
    {
      this.InvalidateProgress();
    }

    protected virtual void OnChildAdded(GanttViewDataItem child)
    {
      if (this.Parent == null || this.GanttViewElement.AllowSummaryEditing)
        return;
      this.InvalidateStartAndEnd();
      this.InvalidateProgress();
    }

    protected virtual void OnChildRemoved(GanttViewDataItem child)
    {
      if (this.Parent == null || this.GanttViewElement.AllowSummaryEditing)
        return;
      this.InvalidateStartAndEnd();
      this.InvalidateProgress();
    }

    protected virtual GanttViewDataItemCollection CreateChildItemsCollection()
    {
      return new GanttViewDataItemCollection(this);
    }

    private void InvalidateStartAndEnd()
    {
      DateTime dateTime1 = DateTime.MaxValue.AddDays(-2.0);
      DateTime dateTime2 = DateTime.MinValue.AddDays(2.0);
      foreach (GanttViewDataItem ganttViewDataItem in (Collection<GanttViewDataItem>) this.Items)
      {
        if (ganttViewDataItem.Start < dateTime1)
          dateTime1 = ganttViewDataItem.Start;
        if (ganttViewDataItem.End > dateTime2)
          dateTime2 = ganttViewDataItem.End;
      }
      if (this.Start != dateTime1)
        this.Start = dateTime1;
      if (!(this.End != dateTime2))
        return;
      this.End = dateTime2;
    }

    private void InvalidateProgress()
    {
      if (this.Items.Count == 0)
        return;
      Decimal num = new Decimal(0);
      foreach (GanttViewDataItem ganttViewDataItem in (Collection<GanttViewDataItem>) this.Items)
        num += ganttViewDataItem.Progress;
      this.Progress = num / (Decimal) this.Items.Count;
    }
  }
}
