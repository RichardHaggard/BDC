// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewColumnGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class GridViewColumnGroup : INotifyPropertyChanged
  {
    private bool visibleInColumnChooser = true;
    private bool isVisible = true;
    private bool allowHide = true;
    private bool showHeader = true;
    private bool allowReorder = true;
    private int rowSpan = 20;
    private PinnedColumnPosition pinPosition = PinnedColumnPosition.None;
    private GridViewColumnGroup parent;
    private ColumnGroupsViewDefinition parentDefinition;
    private GridViewColumnGroup rootParent;
    private ColumnGroupCollection groups;
    private ColumnGroupRowCollection rows;
    private string text;
    private int suspendedCount;
    private string name;
    private object tag;

    public GridViewColumnGroup()
      : this("")
    {
    }

    public GridViewColumnGroup(string text)
      : this(text, string.Empty)
    {
    }

    public GridViewColumnGroup(string text, string name)
    {
      this.groups = new ColumnGroupCollection(this);
      this.rows = new ColumnGroupRowCollection();
      this.rows.CollectionChanged += new NotifyCollectionChangedEventHandler(this.rows_CollectionChanged);
      this.text = text;
      this.name = name;
    }

    [DefaultValue(true)]
    public bool ShowHeader
    {
      get
      {
        return this.showHeader;
      }
      set
      {
        if (this.showHeader == value)
          return;
        if (!value && this.parent != null)
          throw new NotSupportedException("The Root Group can have invisible header only.");
        this.showHeader = value;
        this.CallViewChanged(nameof (ShowHeader));
        this.OnNotifyPropertyChanged(nameof (ShowHeader));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ColumnGroupCollection Groups
    {
      get
      {
        return this.groups;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ColumnGroupRowCollection Rows
    {
      get
      {
        return this.rows;
      }
    }

    [DefaultValue(null)]
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        if (!(value != this.text))
          return;
        this.text = value;
        this.CallViewChanged(nameof (Text));
        this.OnNotifyPropertyChanged(nameof (Text));
      }
    }

    [DefaultValue(20)]
    public int RowSpan
    {
      get
      {
        return this.rowSpan;
      }
      set
      {
        if (value == this.rowSpan)
          return;
        this.rowSpan = value;
        this.CallViewChanged(nameof (RowSpan));
        this.OnNotifyPropertyChanged(nameof (RowSpan));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewColumnGroup Parent
    {
      get
      {
        return this.parent;
      }
      internal set
      {
        if (this.parent == value)
          return;
        if (!this.ShowHeader && this.parent == null)
          throw new NotSupportedException("The Root Group can have invisible header only.");
        this.parent = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewColumnGroup RootColumnGroup
    {
      get
      {
        if (this.rootParent == null)
          return this;
        return this.rootParent;
      }
      internal set
      {
        if (this.rootParent == value)
          return;
        this.rootParent = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ColumnGroupsViewDefinition ParentViewDefinition
    {
      get
      {
        return this.parentDefinition;
      }
      internal set
      {
        this.parentDefinition = value;
      }
    }

    [DefaultValue(false)]
    public bool IsPinned
    {
      get
      {
        return this.PinPosition != PinnedColumnPosition.None;
      }
      set
      {
        if (value)
          this.PinPosition = PinnedColumnPosition.Left;
        else
          this.PinPosition = PinnedColumnPosition.None;
      }
    }

    [DefaultValue(PinnedColumnPosition.None)]
    public PinnedColumnPosition PinPosition
    {
      get
      {
        return this.pinPosition;
      }
      set
      {
        if (value == this.pinPosition)
          return;
        this.SetPinPosition(this, value, this.FindTemplate());
        this.OnNotifyPropertyChanged(nameof (PinPosition));
      }
    }

    [DefaultValue(true)]
    public bool IsVisible
    {
      get
      {
        return this.isVisible;
      }
      set
      {
        if (this.isVisible == value)
          return;
        this.isVisible = value;
        GridViewTemplate template = this.FindTemplate();
        if (!this.IsSuspended && template != null)
        {
          PropertyChangedEventArgs changedEventArgs = new PropertyChangedEventArgs(nameof (IsVisible));
          template.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ColumnGroupPropertyChanged, (object) changedEventArgs));
        }
        this.OnNotifyPropertyChanged(nameof (IsVisible));
      }
    }

    [DefaultValue(true)]
    public virtual bool AllowHide
    {
      get
      {
        return this.allowHide;
      }
      set
      {
        if (this.allowHide == value)
          return;
        this.allowHide = value;
        this.OnNotifyPropertyChanged(nameof (AllowHide));
      }
    }

    [DefaultValue(true)]
    public virtual bool VisibleInColumnChooser
    {
      get
      {
        return this.visibleInColumnChooser;
      }
      set
      {
        if (this.visibleInColumnChooser == value)
          return;
        this.visibleInColumnChooser = value;
        this.OnNotifyPropertyChanged(nameof (VisibleInColumnChooser));
      }
    }

    [DefaultValue(true)]
    public virtual bool AllowReorder
    {
      get
      {
        return this.allowReorder;
      }
      set
      {
        if (this.allowReorder == value)
          return;
        this.allowReorder = value;
        this.OnNotifyPropertyChanged(nameof (AllowReorder));
      }
    }

    [DefaultValue(null)]
    public string Name
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
        this.OnNotifyPropertyChanged(nameof (Name));
      }
    }

    [DefaultValue(null)]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.OnNotifyPropertyChanged(nameof (Tag));
      }
    }

    private void rows_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.CallViewChanged("Rows");
    }

    internal void SetPinPosition(
      GridViewColumnGroup group,
      PinnedColumnPosition position,
      GridViewTemplate template)
    {
      group.pinPosition = position;
      group.OnNotifyPropertyChanged("PinPosition");
      foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
        this.SetPinPosition(group1, position, template);
      if (template == null || group.Groups.Count != 0)
        return;
      foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
      {
        foreach (string columnName in (Collection<string>) row.ColumnNames)
        {
          GridViewColumn column = (GridViewColumn) template.Columns[columnName];
          column.SuspendPropertyNotifications();
          column.PinPosition = position;
          column.ResumePropertyNotifications();
        }
      }
      if (this.IsSuspended || template == null)
        return;
      PropertyChangedEventArgs changedEventArgs = new PropertyChangedEventArgs("PinPosition");
      template.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ColumnGroupPropertyChanged, (object) changedEventArgs));
    }

    public GridViewTemplate FindTemplate()
    {
      for (GridViewColumnGroup gridViewColumnGroup = this; gridViewColumnGroup != null; gridViewColumnGroup = gridViewColumnGroup.Parent)
      {
        if (gridViewColumnGroup.ParentViewDefinition != null)
          return gridViewColumnGroup.ParentViewDefinition.ViewTemplate;
      }
      return (GridViewTemplate) null;
    }

    private void CallViewChanged(string propertyName)
    {
      if (this.IsSuspended)
        return;
      GridViewTemplate template = this.FindTemplate();
      if (template == null)
        return;
      PropertyChangedEventArgs changedEventArgs = new PropertyChangedEventArgs(propertyName);
      template.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ColumnGroupPropertyChanged, (object) changedEventArgs));
    }

    public void SuspendNotifications()
    {
      ++this.suspendedCount;
    }

    public void ResumeNotifications()
    {
      if (this.suspendedCount <= 0)
        return;
      --this.suspendedCount;
    }

    public bool IsSuspended
    {
      get
      {
        return this.suspendedCount > 0;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
