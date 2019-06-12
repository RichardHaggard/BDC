// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TileGroupElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.TileGroupElementDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(false)]
  public class TileGroupElement : LightVisualElement
  {
    public static RadProperty HeaderHeightProperty = RadProperty.Register(nameof (HeaderHeight), typeof (int), typeof (TileGroupElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private Size cellSize = new Size(100, 100);
    private GridLayout contentElement;
    private RadItemOwnerCollection items;
    private int minimumColumns;
    private int cachedHeaderHeight;

    [Browsable(true)]
    [DefaultValue(0)]
    [Category("Appearance")]
    [Description("Gets or sets the minimum number of columns that the view can be reduced to.")]
    public int MinimumColumns
    {
      get
      {
        return this.minimumColumns;
      }
      set
      {
        if (this.minimumColumns == value)
          return;
        this.minimumColumns = value;
        this.InvalidateMeasure(true);
      }
    }

    [Category("Appearance")]
    [VsbBrowsable(true)]
    [DefaultValue(0)]
    [Description("Gets or sets the height of the group title.")]
    [Browsable(true)]
    public int HeaderHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.cachedHeaderHeight, this.DpiScaleFactor);
      }
      set
      {
        if (value == 0)
        {
          int num = (int) this.ResetValue(TileGroupElement.HeaderHeightProperty);
          this.cachedHeaderHeight = 0;
        }
        else
        {
          if (this.cachedHeaderHeight == value)
            return;
          int num = (int) this.SetValue(TileGroupElement.HeaderHeightProperty, (object) value);
          this.cachedHeaderHeight = value;
        }
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Description("Gets the layout panel that arranges the tiles.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridLayout ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    [Description("Gets or sets the current number of columns.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int ColumnsCount
    {
      get
      {
        return this.contentElement.Columns.Count;
      }
      set
      {
        if (this.MinimumColumns != 0)
          value = Math.Max(value, this.MinimumColumns);
        if (this.contentElement.Columns.Count == value)
          return;
        List<GridLayoutColumn> gridLayoutColumnList = new List<GridLayoutColumn>(value);
        for (int index = 0; index < value; ++index)
        {
          GridLayoutColumn gridLayoutColumn = new GridLayoutColumn();
          gridLayoutColumn.FixedWidth = (float) this.CellSize.Width;
          gridLayoutColumn.SizingType = GridLayoutSizingType.Fixed;
          gridLayoutColumnList.Add(gridLayoutColumn);
        }
        this.contentElement.Columns = gridLayoutColumnList;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the number of rows.")]
    [Browsable(true)]
    [DefaultValue(1)]
    public int RowsCount
    {
      get
      {
        return this.contentElement.Rows.Count;
      }
      set
      {
        if (this.contentElement.Rows.Count == value)
          return;
        List<GridLayoutRow> gridLayoutRowList = new List<GridLayoutRow>(value);
        for (int index = 0; index < value; ++index)
        {
          GridLayoutRow gridLayoutRow = new GridLayoutRow();
          gridLayoutRow.FixedHeight = (float) this.CellSize.Height;
          gridLayoutRow.SizingType = GridLayoutSizingType.Fixed;
          gridLayoutRowList.Add(gridLayoutRow);
        }
        this.contentElement.Rows = gridLayoutRowList;
        this.InvalidateMeasure(true);
      }
    }

    [Description("Gets or sets the size of a single cell.")]
    [DefaultValue(typeof (Size), "100, 100")]
    [Category("Appearance")]
    [Browsable(true)]
    public Size CellSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.cellSize, this.DpiScaleFactor);
      }
      set
      {
        if (!(value != this.cellSize))
          return;
        this.cellSize = value;
        foreach (GridLayoutRow row in this.contentElement.Rows)
          row.FixedHeight = (float) this.cellSize.Height;
        foreach (GridLayoutColumn column in this.contentElement.Columns)
          column.FixedWidth = (float) this.cellSize.Width;
        this.InvalidateMeasure(true);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ForeColor = Color.White;
      this.Font = new Font("Segoe UI", 22f, FontStyle.Regular);
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.contentElement = new GridLayout();
      this.Children.Add((RadElement) this.contentElement);
      this.items = new RadItemOwnerCollection();
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
      this.items.ItemTypes = new System.Type[2]
      {
        typeof (RadTileElement),
        typeof (RadLiveTileElement)
      };
      this.items.Owner = (RadElement) this.contentElement;
      this.Margin = new Padding(0, 0, 25, 0);
      this.NotifyParentOnMouseInput = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      foreach (RadElement child in this.Children)
      {
        if (child != this.contentElement)
          child.Measure(availableSize);
      }
      SizeF panelSize = this.GetPanelSize();
      SizeF sizeF;
      if (this.GetValueSource(TileGroupElement.HeaderHeightProperty) > ValueSource.DefaultValue)
      {
        this.Layout.Measure(new SizeF(availableSize.Width, (float) this.HeaderHeight));
        sizeF = new SizeF(this.Layout.DesiredSize.Width, (float) this.HeaderHeight);
      }
      else
      {
        this.Layout.Measure(availableSize);
        sizeF = this.Layout.DesiredSize;
      }
      this.contentElement.Measure(panelSize);
      sizeF.Height += panelSize.Height;
      sizeF.Width = Math.Max(sizeF.Width, panelSize.Width);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      if (this.GetValueSource(TileGroupElement.HeaderHeightProperty) > ValueSource.DefaultValue)
      {
        this.Layout.Arrange(new RectangleF(PointF.Empty, new SizeF(finalSize.Width, (float) this.HeaderHeight)));
        this.contentElement.Arrange(new RectangleF(new PointF(0.0f, (float) this.HeaderHeight), new SizeF(finalSize.Width, this.contentElement.DesiredSize.Height)));
      }
      else
      {
        this.Layout.Arrange(new RectangleF(PointF.Empty, new SizeF(finalSize.Width, this.Layout.DesiredSize.Height)));
        this.contentElement.Arrange(new RectangleF(new PointF(0.0f, this.Layout.DesiredSize.Height), new SizeF(finalSize.Width, this.contentElement.DesiredSize.Height)));
      }
      return finalSize;
    }

    private SizeF GetPanelSize()
    {
      this.UpdateCellCount();
      return new SizeF((float) (this.ColumnsCount * this.CellSize.Width), (float) (this.RowsCount * this.CellSize.Height));
    }

    protected virtual void UpdateCellCount()
    {
      int num = 1;
      int val1 = 1;
      foreach (RadElement radElement in (RadItemCollection) this.Items)
      {
        int val2_1 = (int) radElement.GetValue(GridLayout.ColumnIndexProperty) + (int) radElement.GetValue(GridLayout.ColSpanProperty);
        int val2_2 = (int) radElement.GetValue(GridLayout.RowIndexProperty) + (int) radElement.GetValue(GridLayout.RowSpanProperty);
        val1 = Math.Max(val1, val2_2);
        num = Math.Max(num, val2_1);
      }
      if (this.MinimumColumns > 0)
        num = Math.Max(this.MinimumColumns, num);
      this.ColumnsCount = num;
      if (!this.IsDesignMode || this.RowsCount >= val1)
        return;
      this.RowsCount = val1;
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      RadTileElement tileElement = target as RadTileElement;
      RadPanoramaElement ancestor = this.FindAncestor<RadPanoramaElement>();
      RadControl radControl = this.ElementTree != null ? this.ElementTree.Control as RadControl : (RadControl) null;
      if (radControl == null || radControl.IsInitializing || this.DesignMode)
        return;
      if (ancestor != null && ancestor.AutoArrangeNewTiles && (tileElement != null && operation == ItemsChangeOperation.Inserted))
      {
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        if (tileElement.GetValueSource(GridLayout.ColumnIndexProperty) > ValueSource.DefaultValue || tileElement.GetValueSource(GridLayout.RowIndexProperty) > ValueSource.DefaultValue)
        {
          this.UpdateCellCount();
          this.InvalidateMeasure();
          return;
        }
        int col = 0;
        while (!flag)
        {
          for (int row = 0; !flag && row < this.RowsCount; ++row)
          {
            if (this.CanPlace(tileElement, row, col))
            {
              num1 = row;
              num2 = col;
              flag = true;
            }
          }
          ++col;
        }
        tileElement.Column = num2;
        tileElement.Row = num1;
      }
      this.UpdateCellCount();
      this.InvalidateMeasure();
    }

    private bool CanPlace(RadTileElement tileElement, int row, int col)
    {
      Rectangle rect = new Rectangle(col, row, tileElement.ColSpan, tileElement.RowSpan);
      foreach (RadTileElement radTileElement in (RadItemCollection) this.Items)
      {
        if (radTileElement != tileElement && new Rectangle(radTileElement.Column, radTileElement.Row, radTileElement.ColSpan, radTileElement.RowSpan).IntersectsWith(rect))
          return false;
      }
      return true;
    }
  }
}
