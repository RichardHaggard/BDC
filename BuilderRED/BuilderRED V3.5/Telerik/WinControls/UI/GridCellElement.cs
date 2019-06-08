// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class GridCellElement : ConditionalFormattableGridVisualElement, IContextMenuProvider
  {
    public static RadProperty IsCurrentProperty = RadProperty.Register(nameof (IsCurrent), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentColumnProperty = RadProperty.Register(nameof (IsCurrentColumn), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentRowProperty = RadProperty.Register(nameof (IsCurrentRow), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSortedProperty = RadProperty.Register(nameof (IsSorted), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsRowHoveredProperty = RadProperty.Register(nameof (IsRowHovered), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsPinnedProperty = RadProperty.Register(nameof (IsPinned), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty PinPositionProperty = RadProperty.Register(nameof (PinPosition), typeof (PinnedColumnPosition), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PinnedColumnPosition.None, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsReadOnlyProperty = RadProperty.Register("IsReadOnly", typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsOddRowProperty = RadProperty.Register(nameof (IsOddRow), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FormatStringProperty = RadProperty.Register(nameof (FormatString), typeof (string), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) "", ElementPropertyOptions.AffectsDisplay));
    private GridViewRowInfo row;
    private GridViewColumn column;
    private GridRowElement rowElement;
    private bool updatingInfo;
    private RadDropDownMenu contextMenu;
    protected RadDropDownMenu oldContextMenu;

    static GridCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridCellElementStateManagerFactory(), typeof (GridCellElement));
      RadItem.TextProperty.OverrideMetadata(typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.Cancelable));
    }

    public GridCellElement(GridViewColumn column, GridRowElement row)
    {
      this.Initialize(column, row);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "Cell";
      this.NotifyParentOnMouseInput = false;
      this.StretchVertically = true;
    }

    protected override void DisposeManagedResources()
    {
      if (this.contextMenu != null)
      {
        this.contextMenu.Items.Clear();
        this.contextMenu.Dispose();
      }
      if (this.oldContextMenu != null)
      {
        this.oldContextMenu.Items.Clear();
        this.oldContextMenu.Dispose();
      }
      base.DisposeManagedResources();
    }

    [Description("Gets or sets a value indicating that this is the current cell in the grid")]
    [Category("Behavior")]
    public virtual bool IsCurrent
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsCurrentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsCurrentProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating that this cell belongs to the current column in grid")]
    public virtual bool IsCurrentColumn
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsCurrentColumnProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsCurrentColumnProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating that this cell belongs to the current column in grid")]
    [Category("Behavior")]
    public virtual bool IsCurrentRow
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsCurrentRowProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsCurrentRowProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating that the column containing this cell is sorted")]
    public virtual bool IsSorted
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsSortedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsSortedProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating that the row containing this cell is hovered")]
    [Category("Appearance")]
    public virtual bool IsRowHovered
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsRowHoveredProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsRowHoveredProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating that the cell is pinned")]
    public virtual bool IsPinned
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsPinnedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsPinnedProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating that the cell is selected")]
    [Category("Appearance")]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsSelectedProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets a value indicating that this cell is owned by an odd row")]
    public virtual bool IsOddRow
    {
      get
      {
        return (bool) this.GetValue(GridCellElement.IsOddRowProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.IsOddRowProperty, (object) value);
      }
    }

    public PinnedColumnPosition PinPosition
    {
      get
      {
        return (PinnedColumnPosition) this.GetValue(GridCellElement.PinPositionProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridCellElement.PinPositionProperty, (object) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("Appearance")]
    [Description("Gets or sets the format string to be used for the cell's value.")]
    public virtual string FormatString
    {
      get
      {
        return (string) this.GetValue(GridCellElement.FormatStringProperty);
      }
      set
      {
        int num1 = (int) this.SetValue(GridCellElement.FormatStringProperty, (object) value);
        int num2 = (int) this.ResetValue(GridCellElement.FormatStringProperty, ValueResetFlags.Binding);
      }
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        if (this.TableElement != null)
          return this.TableElement.GridViewElement;
        return (RadGridViewElement) null;
      }
    }

    public GridTableElement TableElement
    {
      get
      {
        if (this.RowElement != null)
          return this.RowElement.TableElement;
        return (GridTableElement) null;
      }
    }

    public GridRowElement RowElement
    {
      get
      {
        return this.rowElement;
      }
      protected set
      {
        this.rowElement = value;
      }
    }

    public virtual GridViewRowInfo RowInfo
    {
      get
      {
        return this.row;
      }
      protected set
      {
        this.row = value;
      }
    }

    public virtual GridViewColumn ColumnInfo
    {
      get
      {
        return this.column;
      }
      protected set
      {
        this.column = value;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        if (this.RowInfo != null)
          return this.RowInfo.ViewInfo;
        return (GridViewInfo) null;
      }
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        if (this.RowInfo != null)
          return this.RowInfo.ViewTemplate;
        return (GridViewTemplate) null;
      }
    }

    public MasterGridViewTemplate MasterTemplate
    {
      get
      {
        return this.GridViewElement.Template;
      }
    }

    protected bool UpdatingInfo
    {
      get
      {
        return this.updatingInfo;
      }
      set
      {
        this.updatingInfo = value;
      }
    }

    public int RowIndex
    {
      get
      {
        if (this.RowInfo != null)
          return this.RowInfo.Index;
        return -1;
      }
    }

    public int ColumnIndex
    {
      get
      {
        if (this.ViewTemplate != null)
          return this.ViewTemplate.Columns.IndexOf(this.ColumnInfo as GridViewDataColumn);
        return -1;
      }
    }

    public virtual object Value
    {
      get
      {
        return (object) string.Empty;
      }
      set
      {
      }
    }

    internal virtual bool CanBestFit(BestFitColumnMode bestFitMode)
    {
      return true;
    }

    public virtual void Initialize(GridViewColumn column, GridRowElement row)
    {
      this.column = column;
      this.rowElement = row;
      if (row == null)
        return;
      this.row = row.RowInfo;
    }

    public virtual void UpdateInfo()
    {
      if (!this.CanUpdateInfo)
        return;
      this.updatingInfo = true;
      this.UpdateInfoCore();
      this.OnViewCellFormatting(new CellFormattingEventArgs(this));
      this.updatingInfo = false;
    }

    protected virtual void OnViewCellFormatting(CellFormattingEventArgs e)
    {
      if (this.GridViewElement == null)
        return;
      this.GridViewElement.OnViewCellFormatting((object) this, e);
    }

    protected virtual bool CanUpdateInfo
    {
      get
      {
        if (!this.updatingInfo && this.column != null && this.rowElement != null)
          return this.rowElement.RowInfo != null;
        return false;
      }
    }

    public virtual void SetContent()
    {
      this.SetContentCore(this.Value);
    }

    protected override TextParams CreateTextParams()
    {
      TextParams textParams = base.CreateTextParams();
      textParams.ClipText = true;
      if (this.MasterTemplate.MasterViewInfo.TableSearchRow.HighlightResults)
      {
        List<CharacterRange> searchHighlightRanges = this.GetSearchHighlightRanges();
        if (searchHighlightRanges.Count > 0)
        {
          textParams.highlightRanges = searchHighlightRanges.ToArray();
          textParams.highlightColor = this.TableElement.SearchHighlightColor;
        }
      }
      return textParams;
    }

    protected virtual List<CharacterRange> GetSearchHighlightRanges()
    {
      List<CharacterRange> characterRangeList = new List<CharacterRange>();
      if (this.ColumnInfo == null || !this.RowInfo.SearchCache.Contains((object) this.ColumnInfo))
        return characterRangeList;
      string str = this.RowInfo.SearchCache[(object) this.ColumnInfo] as string;
      int First = -1;
      CompareOptions options = !this.MasterTemplate.MasterViewInfo.TableSearchRow.CaseSensitive ? this.MasterTemplate.MasterViewInfo.TableSearchRow.CompareOptions : CompareOptions.Ordinal;
      while (First + 1 < this.Text.Length)
      {
        First = this.MasterTemplate.MasterViewInfo.TableSearchRow.Culture.CompareInfo.IndexOf(this.Text, str, First + 1, options);
        if (First >= 0)
        {
          if ((options & CompareOptions.IgnoreSymbols) == CompareOptions.IgnoreSymbols)
          {
            int Length = 0;
            int length = str.Length;
            for (int index = First; length > 0 && index < this.Text.Length; ++index)
            {
              if (char.IsLetterOrDigit(this.Text[index]))
                --length;
              ++Length;
            }
            characterRangeList.Add(new CharacterRange(First, Length));
          }
          else
            characterRangeList.Add(new CharacterRange(First, str.Length));
        }
        if (First < 0 || characterRangeList.Count >= 32)
          break;
      }
      return characterRangeList;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.rowElement == null)
        return;
      base.PaintElement(graphics, angle, scale);
      if (!this.GridViewElement.EnableCustomDrawing)
        return;
      GridViewCellPaintEventArgs args = new GridViewCellPaintEventArgs(this, (Graphics) graphics.UnderlayGraphics);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCellPaintEventArgs>(EventDispatcher.CellPaint, (object) this, args);
    }

    protected override void PaintElementSkin(IGraphics graphics)
    {
      base.PaintElementSkin(graphics);
      this.PaintContent(graphics);
    }

    internal void CallPaintOverride(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      this.PaintOverride(graphics, clipRectange, angle, scale, useRelativeTransformation);
    }

    protected internal virtual void ShowContextMenu()
    {
      this.GridViewElement.ContextMenuManager.ShowContextMenu((IContextMenuProvider) this);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.MasterTemplate.EventDispatcher.RaiseEvent<MouseEventArgs>(EventDispatcher.CellMouseMove, (object) this, e);
      this.MasterTemplate.EventDispatcher.RaiseEvent<MouseEventArgs>(EventDispatcher.RowMouseMove, (object) this.RowElement, e);
    }

    public virtual RadDropDownMenu ContextMenu
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
      if (this.oldContextMenu != null)
      {
        this.oldContextMenu.Items.Clear();
        this.oldContextMenu.Dispose();
      }
      RadDropDownMenu radDropDownMenu = new RadDropDownMenu();
      this.TableElement.GetCellElement((GridViewRowInfo) this.ViewInfo.TableHeaderRow, this.ColumnInfo)?.MergeMenus(radDropDownMenu);
      this.CreateContextMenuItems(radDropDownMenu);
      this.RowElement?.MergeMenus(radDropDownMenu);
      this.oldContextMenu = radDropDownMenu;
      return radDropDownMenu;
    }

    protected virtual void CreateContextMenuItems(RadDropDownMenu menu)
    {
    }

    protected virtual void UpdateInfoCore()
    {
      if (this.ColumnInfo == null)
        return;
      bool flag1 = this.GridViewElement.CurrentColumn == this.ColumnInfo && this.ColumnInfo.IsCurrent;
      bool flag2 = false;
      if (this.ElementTree != null)
      {
        Control control = this.ElementTree.Control;
        if (control != null)
          flag2 = this.GridViewElement.HideSelection && !control.Focused && !control.ContainsFocus;
      }
      if (this.MasterTemplate.SelectionMode != GridViewSelectionMode.None)
      {
        this.IsCurrentColumn = flag1;
        this.IsCurrent = flag1 && this.RowInfo.IsCurrent && this.MasterTemplate.CurrentView == this.ViewInfo && !flag2;
      }
      this.IsSorted = this.ColumnInfo.IsSorted;
      this.TextWrap = this.ColumnInfo.WrapText;
      this.AutoEllipsis = this.ColumnInfo.AutoEllipsis;
      this.IsPinned = this.ColumnInfo.PinPosition != PinnedColumnPosition.None;
    }

    protected virtual void SetContentCore(object value)
    {
      this.Text = this.ApplyFormatString(value);
    }

    protected virtual string ApplyFormatString(object value)
    {
      if (!string.IsNullOrEmpty(this.FormatString))
      {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        GridViewDataColumn columnInfo = this.ColumnInfo as GridViewDataColumn;
        if (columnInfo != null && columnInfo.FormatInfo != null)
          cultureInfo = columnInfo.FormatInfo;
        return string.Format((IFormatProvider) cultureInfo, this.FormatString, value);
      }
      if (value != null)
        return value.ToString();
      return string.Empty;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.ColumnInfo != null)
      {
        SizeF size = this.TableElement.ViewElement.RowLayout.ArrangeCell(new RectangleF((PointF) Point.Empty, availableSize), this).Size;
        if (!float.IsInfinity(availableSize.Width))
          sizeF.Width = size.Width;
        if (!float.IsInfinity(availableSize.Height))
          sizeF.Height = size.Height;
      }
      return sizeF;
    }
  }
}
