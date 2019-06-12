// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridRatingCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridRatingCellElement : GridDataCellElement
  {
    private RadRatingElement rating;

    public GridRatingCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
      foreach (RadElement radElement in (RadItemCollection) this.rating.Items)
        radElement.Margin = new Padding(0);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.rating = new RadRatingElement();
      this.rating.StretchHorizontally = true;
      this.rating.StretchVertically = true;
      this.rating.ShouldHandleMouseInput = true;
      this.rating.CaptionElement.Visibility = ElementVisibility.Collapsed;
      this.rating.DescriptionElement.Visibility = ElementVisibility.Collapsed;
      this.rating.SubCaptionElement.Visibility = ElementVisibility.Collapsed;
      this.rating.ValueChanged += new EventHandler(this.rating_ValueChanged);
      for (int index = 0; index < 5; ++index)
      {
        RatingStarVisualElement starVisualElement = new RatingStarVisualElement();
        starVisualElement.MinSize = new Size(10, 10);
        this.rating.Items.Add((RadItem) starVisualElement);
      }
      this.rating.IsInRadGridView = true;
      this.Children.Add((RadElement) this.rating);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.rating = (RadRatingElement) null;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
      this.ClipDrawing = true;
    }

    public override bool IsEditable
    {
      get
      {
        return false;
      }
    }

    public RadRatingElement RatingElement
    {
      get
      {
        return this.rating;
      }
    }

    private void rating_ValueChanged(object sender, EventArgs e)
    {
      this.Value = (object) this.rating.Value;
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      GridViewRatingColumn columnInfo = (GridViewRatingColumn) this.ColumnInfo;
      if (e.Property == GridViewRatingColumn.MaximumProperty)
        this.rating.Maximum = columnInfo.Maximum;
      else if (e.Property == GridViewRatingColumn.MinimumProperty)
        this.rating.Minimum = columnInfo.Minimum;
      else if (e.Property == GridViewRatingColumn.ShouldPaintHoverProperty)
        this.rating.ShouldPaintHover = columnInfo.ShouldPaintHover;
      else if (e.Property == GridViewRatingColumn.SelectionModeProperty)
        this.rating.SelectionMode = columnInfo.SelectionMode;
      else if (e.Property == GridViewRatingColumn.ReadOnlyProperty)
        this.rating.ReadOnly = columnInfo.ReadOnly;
      else if (e.Property == GridViewRatingColumn.DirectionProperty)
        this.rating.Direction = columnInfo.Direction;
      else if (e.Property == GridViewRatingColumn.ToolTipPrecisionProperty)
        this.rating.ToolTipPrecision = columnInfo.ToolTipPrecision;
      else if (e.Property == GridViewRatingColumn.PercentageRoundingProperty)
      {
        this.rating.PercentageRounding = columnInfo.PercentageRounding;
      }
      else
      {
        if (e.Property != GridViewRatingColumn.ToolTipFormatStringProperty)
          return;
        this.rating.ToolTipFormatString = columnInfo.ToolTipFormatString;
      }
    }

    public override void Attach(GridViewColumn data, object context)
    {
      base.Attach(data, context);
      if (this.RowElement == null)
        return;
      this.GridViewElement.EditorManager.RegisterPermanentEditorType(typeof (RadRatingElement));
    }

    protected override void UpdateInfoCore()
    {
      base.UpdateInfoCore();
      GridViewRatingColumn columnInfo = this.ColumnInfo as GridViewRatingColumn;
      if (columnInfo == null)
        return;
      this.rating.Maximum = columnInfo.Maximum;
      this.rating.Minimum = columnInfo.Minimum;
      this.rating.ShouldPaintHover = columnInfo.ShouldPaintHover;
      this.rating.SelectionMode = columnInfo.SelectionMode;
      this.rating.ReadOnly = columnInfo.ReadOnly;
      this.rating.Direction = columnInfo.Direction;
      this.rating.ToolTipPrecision = columnInfo.ToolTipPrecision;
      this.rating.PercentageRounding = columnInfo.PercentageRounding;
      this.rating.ToolTipFormatString = columnInfo.ToolTipFormatString;
    }

    protected override void SetContentCore(object value)
    {
      GridViewRatingColumn columnInfo = this.ColumnInfo as GridViewRatingColumn;
      if (columnInfo == null)
        return;
      double result = columnInfo.Minimum;
      if (value == null)
      {
        this.rating.Value = new double?();
      }
      else
      {
        if (!double.TryParse(value.ToString(), out result))
          return;
        this.rating.Value = new double?(result);
      }
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewRatingColumn;
    }
  }
}
