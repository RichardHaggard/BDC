// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridVirtualizedCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridVirtualizedCellElement : GridCellElement, IVirtualizedElement<GridViewColumn>
  {
    private bool bindingsSuspended;
    private bool allowRowReorder;

    public GridVirtualizedCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      bool flag1 = column != null && column != this.ColumnInfo;
      bool flag2 = row != null && (row != this.RowElement || row.RowInfo != this.RowInfo);
      base.Initialize(column, row);
      if (flag1 || this.bindingsSuspended && column != null)
        this.BindColumnProperties();
      if (flag2 || this.bindingsSuspended && row != null && row.RowInfo != null)
        this.BindRowProperties();
      this.bindingsSuspended = false;
      if (flag1 || flag2)
        this.SetContent();
      this.UpdateInfo();
    }

    protected override void UpdateInfoCore()
    {
      this.DisableHTMLRendering = this.ColumnInfo.DisableHTMLRendering;
      base.UpdateInfoCore();
    }

    protected override void DisposeManagedResources()
    {
      this.Detach();
      base.DisposeManagedResources();
    }

    protected bool AllowRowReorder
    {
      get
      {
        return this.allowRowReorder;
      }
      set
      {
        if (value == this.allowRowReorder)
          return;
        this.allowRowReorder = value;
      }
    }

    public GridViewColumn Data
    {
      get
      {
        return this.ColumnInfo;
      }
    }

    public virtual void Attach(GridViewColumn data, object context)
    {
      this.Initialize(data, (GridRowElement) context);
      if (this.Style == null)
        return;
      this.Style.Apply((RadObject) this, false);
    }

    public virtual void Detach()
    {
      int num1 = (int) this.ResetValue(RadElement.ContainsMouseProperty);
      int num2 = (int) this.ResetValue(RadElement.IsMouseDownProperty);
      if (this.RowInfo != null)
      {
        this.UnbindRowProperties();
        this.RowElement = (GridRowElement) null;
        this.RowInfo = (GridViewRowInfo) null;
      }
      if (this.ColumnInfo != null)
      {
        this.UnbindColumnProperties();
        this.ColumnInfo = (GridViewColumn) null;
      }
      if (this.oldContextMenu == null)
        return;
      this.oldContextMenu.Dispose();
      this.oldContextMenu = (RadDropDownMenu) null;
    }

    public void Synchronize()
    {
      this.SetContent();
      this.UpdateInfo();
      this.Invalidate();
    }

    public virtual bool IsCompatible(GridViewColumn data, object context)
    {
      GridVirtualizedRowElement virtualizedRowElement = context as GridVirtualizedRowElement;
      if (virtualizedRowElement == null)
        return false;
      return data.GetCellType(virtualizedRowElement.RowInfo).IsAssignableFrom(this.GetType());
    }

    protected virtual void BindColumnProperties()
    {
      PropertyBindingOptions options = PropertyBindingOptions.OneWay | PropertyBindingOptions.PreserveAsLocalValue;
      int num = (int) this.BindProperty(GridCellElement.PinPositionProperty, (RadObject) this.ColumnInfo, GridViewColumn.PinPositionProperty, options);
      this.ColumnInfo.RadPropertyChanged += new RadPropertyChangedEventHandler(this.ColumnInfo_RadPropertyChanged);
    }

    protected virtual void UnbindColumnProperties()
    {
      int num = (int) this.UnbindProperty(GridCellElement.PinPositionProperty);
      this.ColumnInfo.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.ColumnInfo_RadPropertyChanged);
    }

    protected virtual void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == GridViewColumn.DisableHTMLRenderingProperty)
        this.DisableHTMLRendering = (bool) e.NewValue;
      if (e.Property == GridViewColumn.PinPositionProperty)
      {
        int num = (int) this.SetValue(GridCellElement.IsPinnedProperty, (object) ((PinnedColumnPosition) e.NewValue != PinnedColumnPosition.None));
      }
      this.UpdateInfo();
    }

    protected virtual void BindRowProperties()
    {
      int num = (int) this.BindProperty(GridCellElement.IsCurrentRowProperty, (RadObject) this.RowElement, GridRowElement.IsCurrentProperty, PropertyBindingOptions.OneWay);
      this.RowInfo.PropertyChanged += new PropertyChangedEventHandler(this.RowInfo_PropertyChanged);
    }

    protected virtual void UnbindRowProperties()
    {
      int num = (int) this.UnbindProperty(GridCellElement.IsCurrentRowProperty);
      this.RowInfo.PropertyChanged -= new PropertyChangedEventHandler(this.RowInfo_PropertyChanged);
    }

    protected virtual void OnRowPropertyChanged(PropertyChangedEventArgs e)
    {
    }

    internal void SuspendBindings()
    {
      if (this.ColumnInfo != null)
        this.UnbindColumnProperties();
      if (this.RowInfo != null)
        this.UnbindRowProperties();
      this.bindingsSuspended = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      bool flag = false;
      if (this.GridControl != null && this.RowInfo != null)
      {
        BaseGridBehavior gridBehavior = this.GridControl.GridBehavior as BaseGridBehavior;
        if (gridBehavior != null)
        {
          GridRowBehavior behavior = gridBehavior.GetBehavior(this.RowInfo.GetType()) as GridRowBehavior;
          if (behavior != null)
            flag = this.ViewTemplate.AllowRowReorder && behavior.CanResizeRow(e.Location, this.RowElement);
        }
      }
      if (e.Button != MouseButtons.Left || !this.AllowRowReorder || (!this.ViewTemplate.AllowRowReorder || flag))
        return;
      this.GridViewElement.GetService<RadDragDropService>()?.Start((object) new SnapshotDragItem((RadItem) this.RowElement));
    }

    private void ColumnInfo_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      this.OnColumnPropertyChanged(e);
    }

    private void RowInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnRowPropertyChanged(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == GridCellElement.FormatStringProperty)
      {
        this.SetContent();
      }
      else
      {
        if (e.Property != GridRowElement.IsCurrentProperty && e.Property != GridCellElement.IsCurrentColumnProperty)
          return;
        this.UpdateInfo();
      }
    }
  }
}
