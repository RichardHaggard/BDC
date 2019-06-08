// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.GridLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Layouts
{
  public class GridLayout : LayoutPanel
  {
    public static readonly RadProperty ColumnIndexProperty = RadProperty.RegisterAttached("ColumnIndex", typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    public static readonly RadProperty RowIndexProperty = RadProperty.RegisterAttached("RowIndex", typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    public static readonly RadProperty RowSpanProperty = RadProperty.RegisterAttached("RowSpan", typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1));
    public static readonly RadProperty ColSpanProperty = RadProperty.RegisterAttached("ColSpan", typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1));
    public static readonly RadProperty CellPaddingProperty = RadProperty.RegisterAttached("CellPadding", typeof (Padding), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange));
    private List<GridLayoutColumn> columns;
    private List<GridLayoutRow> rows;
    private RadElement[,] elements;

    public GridLayout()
      : this(1, 1)
    {
      this.columns = new List<GridLayoutColumn>(1);
      this.columns.Add(new GridLayoutColumn());
      this.rows = new List<GridLayoutRow>(1);
      this.rows.Add(new GridLayoutRow());
    }

    public GridLayout(int columnsCount, int rowsCount)
    {
      this.columns = new List<GridLayoutColumn>(columnsCount);
      for (int index = 0; index < columnsCount; ++index)
        this.columns.Add(new GridLayoutColumn());
      this.rows = new List<GridLayoutRow>(rowsCount);
      for (int index = 0; index < rowsCount; ++index)
        this.rows.Add(new GridLayoutRow());
    }

    public List<GridLayoutColumn> Columns
    {
      get
      {
        return this.columns;
      }
      set
      {
        if (this.columns == value)
          return;
        this.columns = value;
        this.InvalidateMeasure(true);
      }
    }

    public List<GridLayoutRow> Rows
    {
      get
      {
        return this.rows;
      }
      set
      {
        if (this.rows == value)
          return;
        this.rows = value;
        this.InvalidateMeasure(true);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      availableSize.Width -= (float) (this.Padding.Horizontal + this.BorderThickness.Horizontal);
      availableSize.Height -= (float) (this.Padding.Vertical + this.BorderThickness.Vertical);
      SizeF empty = SizeF.Empty;
      this.elements = (RadElement[,]) null;
      if (this.StretchHorizontally)
      {
        this.SetStretchedWidths(availableSize);
        if (!float.IsInfinity(availableSize.Width))
          empty.Width = availableSize.Width;
      }
      else
        this.SetWidths(availableSize, ref empty);
      if (float.IsInfinity(availableSize.Width))
      {
        foreach (GridLayoutColumn column in this.columns)
          empty.Width += column.Width;
      }
      if (this.StretchVertically)
      {
        this.SetStretchedHeights(availableSize);
        if (!float.IsInfinity(availableSize.Height))
          empty.Height = availableSize.Height;
      }
      else
        this.SetHeights(availableSize, ref empty);
      if (float.IsInfinity(availableSize.Height))
      {
        foreach (GridLayoutRow row in this.rows)
          empty.Height += row.Height;
      }
      this.MeasureElements(availableSize, ref empty);
      return empty;
    }

    private void MeasureElements(SizeF availableSize, ref SizeF desiredSize)
    {
      int[] numArray1 = new int[this.columns.Count];
      int[] numArray2 = new int[this.rows.Count];
      foreach (RadElement child in this.Children)
      {
        int index1 = (int) child.GetValue(GridLayout.ColumnIndexProperty);
        int index2 = (int) child.GetValue(GridLayout.RowIndexProperty);
        int num1 = (int) child.GetValue(GridLayout.RowSpanProperty);
        int num2 = (int) child.GetValue(GridLayout.ColSpanProperty);
        Padding padding = (Padding) child.GetValue(GridLayout.CellPaddingProperty);
        float num3 = 0.0f;
        for (int index3 = 0; index3 < num2; ++index3)
          num3 += this.columns[index1 + index3].Width;
        float num4 = num3 - (float) padding.Horizontal;
        float num5 = 0.0f;
        for (int index3 = 0; index3 < num1 && index2 + index3 < this.rows.Count; ++index3)
          num5 += this.rows[index2 + index3].Height;
        float num6 = num5 - (float) padding.Vertical;
        SizeF availableSize1 = new SizeF(float.IsInfinity(availableSize.Width) ? float.PositiveInfinity : num4, float.IsInfinity(availableSize.Height) ? float.PositiveInfinity : num6);
        child.Measure(availableSize1);
        numArray1[index1] = this.columns[index1].SizingType != GridLayoutSizingType.Fixed ? Math.Max(numArray1[index1], (int) child.DesiredSize.Width + 1) : (int) this.columns[index1].Width;
        numArray2[index2] = this.rows[index2].SizingType != GridLayoutSizingType.Fixed ? Math.Max(numArray2[index2], (int) child.DesiredSize.Height) : (int) this.rows[index2].Height;
      }
      if (float.IsInfinity(availableSize.Width))
      {
        int num = 0;
        desiredSize.Width = 0.0f;
        foreach (GridLayoutColumn column in this.columns)
        {
          column.Width = (float) numArray1[num++];
          desiredSize.Width += column.Width;
        }
      }
      if (!float.IsInfinity(availableSize.Height))
        return;
      int num7 = 0;
      desiredSize.Height = 0.0f;
      foreach (GridLayoutRow row in this.rows)
      {
        if (num7 > numArray2.Length - 1)
          break;
        row.Height = (float) numArray2[num7++];
        desiredSize.Height += row.Height;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if ((double) finalSize.Width < 1.0 || (double) finalSize.Height < 1.0)
        return finalSize;
      foreach (RadElement child in this.Children)
      {
        int num1 = (int) child.GetValue(GridLayout.ColumnIndexProperty);
        int index1 = (int) child.GetValue(GridLayout.RowIndexProperty);
        int num2 = (int) child.GetValue(GridLayout.RowSpanProperty);
        int num3 = (int) child.GetValue(GridLayout.ColSpanProperty);
        Padding padding = (Padding) child.GetValue(GridLayout.CellPaddingProperty);
        float num4 = 0.0f;
        for (int index2 = 0; index2 < num1; ++index2)
          num4 += this.columns[index2].Width;
        double height1 = (double) this.rows[index1].Height;
        float num5 = 0.0f;
        for (int index2 = 0; index2 < index1; ++index2)
          num5 += this.rows[index2].Height;
        float num6 = 0.0f;
        for (int index2 = 0; index2 < num3; ++index2)
          num6 += this.columns[num1 + index2].Width;
        float width = num6 - (float) padding.Horizontal;
        float num7 = 0.0f;
        for (int index2 = 0; index2 < num2 && index1 + index2 < this.rows.Count; ++index2)
          num7 += this.rows[index1 + index2].Height;
        float height2 = num7 - (float) padding.Vertical;
        child.Arrange(new RectangleF(num4 + (float) padding.Left + (float) this.Padding.Left, num5 + (float) padding.Top + (float) this.Padding.Top, width, height2));
      }
      return finalSize;
    }

    private void SetStretchedWidths(SizeF availableSize)
    {
      if (float.IsInfinity(availableSize.Width))
        return;
      float num = availableSize.Width / (float) this.columns.Count;
      foreach (GridLayoutColumn column in this.columns)
        column.Width = num;
    }

    private void SetWidths(SizeF availableSize, ref SizeF desiredSize)
    {
      List<GridLayoutColumn> proportionalColumns = new List<GridLayoutColumn>();
      float remainingWidth = availableSize.Width;
      float num1 = 0.0f;
      for (int columnIndex = 0; columnIndex < this.columns.Count; ++columnIndex)
      {
        float num2 = 0.0f;
        switch (this.columns[columnIndex].SizingType)
        {
          case GridLayoutSizingType.Proportional:
            proportionalColumns.Add(this.columns[columnIndex]);
            break;
          case GridLayoutSizingType.Fixed:
            num2 = this.columns[columnIndex].FixedWidth;
            goto default;
          case GridLayoutSizingType.Auto:
            using (List<RadElement>.Enumerator enumerator = this.GetColumnElements(columnIndex).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                RadElement current = enumerator.Current;
                current.Measure(availableSize);
                if ((double) num2 < (double) current.DesiredSize.Width)
                  num2 = current.DesiredSize.Width;
              }
              goto default;
            }
          default:
            if ((double) remainingWidth < (double) num2)
            {
              num2 = remainingWidth;
              remainingWidth = 0.0f;
            }
            else
            {
              remainingWidth -= num2;
              num1 += num2;
            }
            this.columns[columnIndex].Width = num2;
            break;
        }
      }
      if (float.IsInfinity(availableSize.Width))
      {
        desiredSize.Width = num1;
      }
      else
      {
        float num2 = this.SetProportionalWidths(proportionalColumns, remainingWidth);
        desiredSize.Width = availableSize.Width - num2;
      }
    }

    private float SetProportionalWidths(
      List<GridLayoutColumn> proportionalColumns,
      float remainingWidth)
    {
      if (proportionalColumns.Count == 0)
        return remainingWidth;
      int num1 = 0;
      foreach (GridLayoutColumn proportionalColumn in proportionalColumns)
        num1 += proportionalColumn.ProportionalWidthWeight;
      foreach (GridLayoutColumn proportionalColumn in proportionalColumns)
      {
        float num2 = remainingWidth * (float) proportionalColumn.ProportionalWidthWeight / (float) num1;
        proportionalColumn.Width = num2;
      }
      return 0.0f;
    }

    private void SetStretchedHeights(SizeF availableSize)
    {
      if (float.IsInfinity(availableSize.Height))
        return;
      float num = availableSize.Height / (float) this.rows.Count;
      foreach (GridLayoutRow row in this.rows)
        row.Height = num;
    }

    private void SetHeights(SizeF availableSize, ref SizeF desiredSize)
    {
      List<GridLayoutRow> proportionalRows = new List<GridLayoutRow>();
      float remainingHeight = availableSize.Height;
      float num1 = 0.0f;
      for (int rowIndex = 0; rowIndex < this.rows.Count; ++rowIndex)
      {
        float num2 = 0.0f;
        switch (this.rows[rowIndex].SizingType)
        {
          case GridLayoutSizingType.Proportional:
            proportionalRows.Add(this.rows[rowIndex]);
            break;
          case GridLayoutSizingType.Fixed:
            num2 = this.rows[rowIndex].FixedHeight;
            goto default;
          case GridLayoutSizingType.Auto:
            using (List<RadElement>.Enumerator enumerator = this.GetRowElements(rowIndex).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                RadElement current = enumerator.Current;
                current.Measure(availableSize);
                Padding padding = (Padding) current.GetValue(GridLayout.CellPaddingProperty);
                if ((double) num2 < (double) current.DesiredSize.Height + (double) padding.Vertical)
                  num2 = current.DesiredSize.Height + (float) padding.Vertical;
              }
              goto default;
            }
          default:
            if ((double) remainingHeight < (double) num2)
            {
              num2 = remainingHeight;
              remainingHeight = 0.0f;
            }
            else
            {
              remainingHeight -= num2;
              num1 += num2;
            }
            this.rows[rowIndex].Height = num2;
            break;
        }
      }
      if (float.IsInfinity(availableSize.Height))
      {
        desiredSize.Height = num1;
      }
      else
      {
        float num2 = this.SetProportionalHeights(proportionalRows, remainingHeight);
        desiredSize.Height = availableSize.Height - num2;
      }
    }

    private float SetProportionalHeights(
      List<GridLayoutRow> proportionalRows,
      float remainingHeight)
    {
      if (proportionalRows.Count == 0)
        return remainingHeight;
      int num1 = 0;
      foreach (GridLayoutRow proportionalRow in proportionalRows)
        num1 += proportionalRow.ProportionalHeightWeight;
      foreach (GridLayoutRow proportionalRow in proportionalRows)
      {
        float num2 = remainingHeight * (float) proportionalRow.ProportionalHeightWeight / (float) num1;
        proportionalRow.Height = num2;
      }
      return 0.0f;
    }

    private void FillElementsMatrix()
    {
      this.elements = new RadElement[this.rows.Count, this.columns.Count];
      foreach (RadElement child in this.Children)
        this.elements[(int) child.GetValue(GridLayout.RowIndexProperty), (int) child.GetValue(GridLayout.ColumnIndexProperty)] = child;
    }

    private List<RadElement> GetColumnElements(int columnIndex)
    {
      if (this.elements == null)
        this.FillElementsMatrix();
      List<RadElement> radElementList = new List<RadElement>();
      for (int index = 0; index < this.rows.Count; ++index)
      {
        RadElement element = this.elements[index, columnIndex];
        if (element != null)
          radElementList.Add(element);
      }
      return radElementList;
    }

    private List<RadElement> GetRowElements(int rowIndex)
    {
      if (this.elements == null)
        this.FillElementsMatrix();
      List<RadElement> radElementList = new List<RadElement>();
      for (int index = 0; index < this.columns.Count; ++index)
      {
        RadElement element = this.elements[rowIndex, index];
        if (element != null)
          radElementList.Add(element);
      }
      return radElementList;
    }
  }
}
