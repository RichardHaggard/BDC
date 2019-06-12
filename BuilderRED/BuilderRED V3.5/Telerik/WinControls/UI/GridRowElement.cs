// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class GridRowElement : ConditionalFormattableGridVisualElement, IContextMenuProvider, IVirtualizedElement<GridViewRowInfo>
  {
    private bool selectionLost = true;
    private bool synchronizeSelectedState = true;
    public static RadProperty DrawBorderOnTopProperty = RadProperty.Register(nameof (DrawBorderOnTop), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HotTrackingProperty = RadProperty.Register(nameof (HotTracking), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GridBorderHorizontalColorProperty = RadProperty.Register(nameof (GridBorderHorizontalColor), typeof (Color), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GridBorderVerticalColorProperty = RadProperty.Register(nameof (GridBorderVerticalColor), typeof (Color), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GridBorderOnTopProperty = RadProperty.Register(nameof (GridBorderOnTop), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentProperty = RadProperty.Register(nameof (IsCurrent), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsOddProperty = RadProperty.Register(nameof (IsOdd), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RowVisualStateProperty = RadProperty.Register(nameof (RowVisualState), typeof (GridRowElement.RowVisualStates), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GridRowElement.RowVisualStates.None, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsCurrentCellProperty = RadProperty.Register(nameof (ContainsCurrentCell), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsSelectedCellsProperty = RadProperty.Register(nameof (ContainsSelectedCells), typeof (bool), typeof (GridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    private GridViewRowInfo rowInfo;
    private GridViewInfo viewInfo;
    private GridTableElement tableElement;
    private VisualCellsCollection visualCells;
    private RadDropDownMenu contextMenu;

    static GridRowElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridRowElementStateManagerFactory(), typeof (GridRowElement));
    }

    public GridRowElement()
    {
      this.visualCells = new VisualCellsCollection(this);
    }

    protected override void DisposeManagedResources()
    {
      this.Detach();
      if (this.contextMenu != null)
        this.contextMenu.Dispose();
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.AllowDrop = true;
      this.DrawBorder = false;
      this.BorderBoxStyle = BorderBoxStyle.SingleBorder;
      this.BorderWidth = 0.0f;
      this.Class = "RowFill";
    }

    public virtual void Initialize(GridViewRowInfo rowInfo)
    {
      if (rowInfo == null || this.rowInfo == rowInfo)
        return;
      bool flag = this.rowInfo == null;
      this.rowInfo = rowInfo;
      this.viewInfo = this.rowInfo.ViewInfo;
      this.HotTracking = false;
      this.WireEvents();
      this.UpdateSelectedState();
      this.UpdateCells();
      if (!flag)
        return;
      this.UpdateInfo();
    }

    public virtual void InitializeRowView(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    public virtual bool DrawBorderOnTop
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.DrawBorderOnTopProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.DrawBorderOnTopProperty, (object) value);
      }
    }

    public virtual bool HotTracking
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.HotTrackingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.HotTrackingProperty, (object) value);
      }
    }

    public virtual Color GridBorderHorizontalColor
    {
      get
      {
        return (Color) this.GetValue(GridRowElement.GridBorderHorizontalColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.GridBorderHorizontalColorProperty, (object) value);
      }
    }

    public virtual Color GridBorderVerticalColor
    {
      get
      {
        return (Color) this.GetValue(GridRowElement.GridBorderVerticalColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.GridBorderVerticalColorProperty, (object) value);
      }
    }

    public virtual bool GridBorderOnTop
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.GridBorderOnTopProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.GridBorderOnTopProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool IsCurrent
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.IsCurrentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.IsCurrentProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool IsOdd
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.IsOddProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.IsOddProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.IsSelectedProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual GridRowElement.RowVisualStates RowVisualState
    {
      get
      {
        return (GridRowElement.RowVisualStates) this.GetValue(GridRowElement.RowVisualStateProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.RowVisualStateProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool ContainsCurrentCell
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.ContainsCurrentCellProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.ContainsCurrentCellProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool ContainsSelectedCells
    {
      get
      {
        return (bool) this.GetValue(GridRowElement.ContainsSelectedCellsProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridRowElement.ContainsSelectedCellsProperty, (object) value);
      }
    }

    public GridViewRowInfo RowInfo
    {
      get
      {
        return this.rowInfo;
      }
      protected set
      {
        this.rowInfo = value;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        if (this.viewInfo != null)
          return this.viewInfo.ViewTemplate;
        return (GridViewTemplate) null;
      }
    }

    public MasterGridViewTemplate MasterTemplate
    {
      get
      {
        return this.ViewTemplate.MasterTemplate;
      }
    }

    public GridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.TableElement.GridViewElement;
      }
    }

    public VisualCellsCollection VisualCells
    {
      get
      {
        return this.visualCells;
      }
    }

    public virtual bool CanApplyFormatting
    {
      get
      {
        return true;
      }
    }

    protected virtual bool CanApplyAlternatingColor
    {
      get
      {
        return false;
      }
    }

    public virtual void UpdateInfo()
    {
      if (this.RowInfo == null || !this.IsInValidState(true))
        return;
      this.UpdateVisualState();
      this.ApplyCustomFormatting();
      this.UpdateSelectedState();
      this.GridViewElement.CallViewRowFormatting((object) this, new RowFormattingEventArgs(this));
      foreach (GridCellElement visualCell in this.VisualCells)
        visualCell.UpdateInfo();
    }

    public virtual void UpdateCells()
    {
    }

    public virtual void UpdateContent()
    {
      foreach (GridCellElement visualCell in this.VisualCells)
        visualCell.SetContent();
    }

    protected virtual void WireEvents()
    {
      this.ViewTemplate.PropertyChanged += new PropertyChangedEventHandler(this.ViewTemplate_PropertyChanged);
      this.RowInfo.PropertyChanged += new PropertyChangedEventHandler(this.RowInfo_PropertyChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.ViewTemplate.PropertyChanged -= new PropertyChangedEventHandler(this.ViewTemplate_PropertyChanged);
      this.RowInfo.PropertyChanged -= new PropertyChangedEventHandler(this.RowInfo_PropertyChanged);
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs e)
    {
      base.OnPropertyChanging(e);
      if (this.TableElement == null || e.Property != GridRowElement.HotTrackingProperty)
        return;
      bool newValue = (bool) e.NewValue;
      e.Cancel = newValue && !this.TableElement.EnableHotTracking;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.TableElement == null)
        return;
      if (e.Property == GridRowElement.IsSelectedProperty || e.Property == GridRowElement.IsCurrentProperty)
        this.UpdateInfo();
      else if (e.Property == RadElement.IsMouseOverElementProperty)
      {
        this.SetHoveredRow();
      }
      else
      {
        if (e.Property != GridRowElement.HotTrackingProperty)
          return;
        this.SetHoveredRow();
        this.UpdateVisualState();
      }
    }

    private void SetHoveredRow()
    {
      GridRowElement gridRowElement = (GridRowElement) null;
      if (this.IsMouseOverElement)
        gridRowElement = this;
      this.TableElement.HoveredRow = gridRowElement;
    }

    protected virtual void OnRowPropertyChanged(PropertyChangedEventArgs e)
    {
      if (!this.synchronizeSelectedState || this.MasterTemplate.SelectionMode == GridViewSelectionMode.None)
        return;
      if (e.PropertyName == "IsSelected")
      {
        this.IsSelected = this.RowInfo.IsSelected;
      }
      else
      {
        if (!(e.PropertyName == "IsCurrent"))
          return;
        this.IsCurrent = this.RowInfo.IsCurrent;
      }
    }

    protected virtual void OnTemplatePropertyChanged(PropertyChangedEventArgs e)
    {
    }

    private void RowInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnRowPropertyChanged(e);
    }

    private void ViewTemplate_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnTemplatePropertyChanged(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF elementSize = this.TableElement.RowScroller.ElementProvider.GetElementSize(this.RowInfo);
      if ((double) elementSize.Width != 0.0)
        availableSize.Width = Math.Min(availableSize.Width, elementSize.Width);
      if (!this.GridViewElement.AutoSizeRows)
        availableSize.Height = elementSize.Height;
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.TableElement.GridViewElement.AutoSizeRows && (this.TableElement.ViewElement.RowLayout is TableViewRowLayout || this.TableElement.ViewElement.RowLayout is ColumnGroupRowLayout) && (!this.RowInfo.IsCurrent || !this.GridViewElement.IsInEditMode))
      {
        sizeF.Height = Math.Max((float) this.RowInfo.MinHeight, sizeF.Height);
        if (this.RowInfo.MaxHeight >= 0 && !(this.TableElement.ViewElement.RowLayout is ColumnGroupRowLayout))
          sizeF.Height = Math.Min(sizeF.Height, (float) this.RowInfo.MaxHeight);
        if ((double) this.RowInfo.Height != (double) sizeF.Height)
        {
          int num = this.RowInfo.Height == -1 ? (int) elementSize.Height : this.RowInfo.Height;
          this.RowInfo.SuspendPropertyNotifications();
          this.RowInfo.Height = (int) sizeF.Height;
          this.RowInfo.ResumePropertyNotifications();
          if (!this.RowInfo.IsPinned)
            this.TableElement.RowScroller.UpdateScrollRange(this.TableElement.RowScroller.Scrollbar.Maximum + (this.RowInfo.Height - num), false);
        }
        return new SizeF(availableSize.Width, (float) this.RowInfo.Height);
      }
      if (this.TableElement == null || this.RowInfo == null)
        return sizeF;
      if (float.IsInfinity(availableSize.Height))
        availableSize.Height = !this.GridViewElement.AutoSizeRows || !(this.TableElement.ViewElement.RowLayout is TableViewRowLayout) && !(this.TableElement.ViewElement.RowLayout is ColumnGroupRowLayout) ? elementSize.Height : sizeF.Height;
      return availableSize;
    }

    protected virtual void ApplyCustomFormatting()
    {
    }

    private void UpdateVisualState()
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      if (this.TableElement.IsCurrentView)
      {
        if (this.IsCurrent && this.MasterTemplate.SelectionMode != GridViewSelectionMode.None)
          this.RowVisualState = this.IsSelected ? GridRowElement.RowVisualStates.CurrentSelected : GridRowElement.RowVisualStates.Current;
        else if (this.IsSelected && this.MasterTemplate.SelectionMode != GridViewSelectionMode.None)
          this.RowVisualState = GridRowElement.RowVisualStates.Selected;
        else
          this.RowVisualState = this.HotTracking ? GridRowElement.RowVisualStates.Hovered : GridRowElement.RowVisualStates.None;
      }
      else
        this.RowVisualState = this.HotTracking ? GridRowElement.RowVisualStates.Hovered : GridRowElement.RowVisualStates.None;
    }

    private void UpdateSelectedState()
    {
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.CellSelect)
      {
        this.synchronizeSelectedState = false;
        this.IsCurrent = false;
        this.IsSelected = false;
        this.selectionLost = true;
      }
      else
      {
        ComponentThemableElementTree elementTree = this.ElementTree;
        if (elementTree == null)
          return;
        Control control = elementTree.Control;
        if (control == null)
          return;
        if (this.GridViewElement.HideSelection && !control.Focused && !control.ContainsFocus)
        {
          if (this.selectionLost)
            return;
          this.selectionLost = true;
          this.synchronizeSelectedState = false;
          this.IsCurrent = false;
          this.IsSelected = false;
        }
        else
        {
          if (!this.selectionLost)
            return;
          this.selectionLost = false;
          this.synchronizeSelectedState = true;
          if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.None)
            return;
          this.IsCurrent = this.RowInfo.IsCurrent;
          this.IsSelected = this.RowInfo.IsSelected;
        }
      }
    }

    internal virtual void UpdateContainsCellsState()
    {
      if (this.MasterTemplate.SelectionMode != GridViewSelectionMode.CellSelect)
      {
        this.ContainsCurrentCell = false;
        this.ContainsSelectedCells = false;
      }
      else
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (GridCellElement visualCell in this.VisualCells)
        {
          if (visualCell.IsCurrent)
          {
            flag1 = true;
            flag2 = flag2 || visualCell.IsSelected;
          }
          else if (visualCell.IsSelected)
            flag2 = true;
          if (flag1)
          {
            if (flag2)
              break;
          }
        }
        this.ContainsCurrentCell = flag1;
        this.ContainsSelectedCells = flag2;
      }
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      return (VisualStyleElement) null;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      VisualStyleElement visualStyleElement = (VisualStyleElement) null;
      if (!this.Enabled)
      {
        visualStyleElement = VistaAeroTheme.ListView.Item.Disabled;
      }
      else
      {
        switch (this.RowVisualState)
        {
          case GridRowElement.RowVisualStates.Selected:
          case GridRowElement.RowVisualStates.Current:
          case GridRowElement.RowVisualStates.CurrentSelected:
            bool containsFocus = this.ElementTree.Control.ContainsFocus;
            visualStyleElement = this.HotTracking ? VistaAeroTheme.ListView.Item.HotSelected : (containsFocus ? VistaAeroTheme.ListView.Item.Selected : VistaAeroTheme.ListView.Item.SelectedNoFocus);
            break;
          case GridRowElement.RowVisualStates.Hovered:
            visualStyleElement = VistaAeroTheme.ListView.Item.Hot;
            break;
        }
      }
      return visualStyleElement;
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      if (!(dragObject is ColumnChooserItem) || this.ViewTemplate.ViewDefinition is ColumnGroupsViewDefinition)
        return;
      object dataContext = dragObject.GetDataContext();
      GridViewDataColumn gridViewDataColumn = dataContext as GridViewDataColumn;
      if (gridViewDataColumn != null)
      {
        gridViewDataColumn.IsVisible = true;
      }
      else
      {
        GridViewColumnGroup gridViewColumnGroup = dataContext as GridViewColumnGroup;
        if (gridViewColumnGroup == null)
          return;
        gridViewColumnGroup.IsVisible = true;
      }
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      if (dragObject is ColumnChooserItem)
      {
        object dataContext = dragObject.GetDataContext();
        GridViewDataColumn gridViewDataColumn = dataContext as GridViewDataColumn;
        if (gridViewDataColumn != null)
          return !gridViewDataColumn.IsVisible;
        GridViewColumnGroup gridViewColumnGroup = dataContext as GridViewColumnGroup;
        if (gridViewColumnGroup != null)
          return !gridViewColumnGroup.IsVisible;
      }
      return false;
    }

    public virtual GridCellElement CreateCell(GridViewColumn column)
    {
      GridViewCreateCellEventArgs e = new GridViewCreateCellEventArgs(this, column, this.GetCellType(column));
      this.GridViewElement.CallCreateCell(e);
      GridCellElement gridCellElement = (GridCellElement) null;
      if (e.CellElement != null)
        gridCellElement = e.CellElement;
      else if ((object) e.CellType != null)
        gridCellElement = Activator.CreateInstance(e.CellType, (object) column, (object) this) as GridCellElement;
      return gridCellElement;
    }

    public virtual System.Type GetCellType(GridViewColumn column)
    {
      return column.GetCellType(this.RowInfo);
    }

    protected override void PostPaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale)
    {
      base.PostPaintChildren(graphics, clipRectange, angle, scale);
      if (!this.GridViewElement.EnableCustomDrawing)
        return;
      GridViewRowPaintEventArgs args = new GridViewRowPaintEventArgs(this, (Graphics) graphics.UnderlayGraphics);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewRowPaintEventArgs>(EventDispatcher.RowPaint, (object) this, args);
    }

    public GridViewRowInfo Data
    {
      get
      {
        return this.RowInfo;
      }
    }

    public virtual void Attach(GridViewRowInfo row, object context)
    {
      this.Initialize(row);
    }

    public virtual void Detach()
    {
      if (this.rowInfo == null)
        return;
      this.UnwireEvents();
      this.synchronizeSelectedState = false;
      this.rowInfo = (GridViewRowInfo) null;
      this.viewInfo = (GridViewInfo) null;
      this.selectionLost = true;
    }

    public virtual void Synchronize()
    {
      this.UpdateInfo();
    }

    public virtual bool IsCompatible(GridViewRowInfo data, object context)
    {
      return false;
    }

    public RadDropDownMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        this.contextMenu = value;
      }
    }

    public virtual RadDropDownMenu MergeMenus(
      RadDropDownMenu contextMenu,
      params object[] parameters)
    {
      return (RadDropDownMenu) null;
    }

    public virtual RadDropDownMenu MergeMenus(
      IContextMenuProvider contextMenuProvider,
      params object[] parameters)
    {
      return (RadDropDownMenu) null;
    }

    public virtual RadDropDownMenu MergeMenus(
      IContextMenuManager contextMenuManager,
      params object[] parameters)
    {
      return (RadDropDownMenu) null;
    }

    public enum RowVisualStates
    {
      None,
      Selected,
      Current,
      Hovered,
      CurrentSelected,
      CurrentHovered,
    }
  }
}
