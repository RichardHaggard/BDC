// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StretchVirtualGridColumnLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class StretchVirtualGridColumnLayout : BaseVirtualGridColumnLayout
  {
    private int lastModifiedStrechableColumn = -1;
    private int lastModifiedColumn = -1;
    private int availableWidth;
    private int nonStretchableWidth;
    private List<int> nonStretchableColumns;
    private List<int> stretchableColumns;
    private int offsetX;
    private int resizedColumn;
    private int stretchableColumnResizeIndex;
    private Dictionary<int, int> resizeStartWidths;
    private Dictionary<int, double> resizeStartScaleFactors;

    public override void CalculateColumnWidths(SizeF availableSize)
    {
      if (this.TableElement.ColumnCount == 0)
        return;
      this.nonStretchableColumns = new List<int>();
      this.nonStretchableColumns.AddRange((IEnumerable<int>) this.TableElement.ColumnsViewState.TopPinnedItems);
      this.nonStretchableColumns.AddRange((IEnumerable<int>) this.TableElement.ColumnsViewState.BottomPinnedItems);
      this.stretchableColumns = new List<int>();
      for (int columnIndex = 0; columnIndex < this.TableElement.ColumnCount; ++columnIndex)
      {
        if (!this.TableElement.IsColumnPinned(columnIndex))
          this.stretchableColumns.Add(columnIndex);
      }
      int cellSpacing = this.TableElement.CellSpacing;
      int columnWidth1 = this.TableElement.GetColumnWidth(-1);
      int num1 = (int) availableSize.Width - columnWidth1;
      if (cellSpacing > 0)
        num1 -= cellSpacing * (this.TableElement.ColumnCount - (columnWidth1 > 0 ? 0 : -1));
      if (this.TableElement.VScrollBar.Visibility == ElementVisibility.Visible)
        num1 -= (int) this.TableElement.VScrollBar.DesiredSize.Width;
      if (this.availableWidth == num1)
        return;
      this.availableWidth = num1;
      this.nonStretchableWidth = 0;
      for (int index = 0; index < this.nonStretchableColumns.Count; ++index)
      {
        int columnWidth2 = this.TableElement.GetColumnWidth(this.nonStretchableColumns[index]);
        if (index < this.nonStretchableColumns.Count - 1)
          columnWidth2 += cellSpacing;
        this.nonStretchableWidth += columnWidth2;
        this.availableWidth -= columnWidth2;
      }
      int num2 = 0;
      for (int index = 0; index < this.stretchableColumns.Count; ++index)
        num2 += this.TableElement.GetColumnWidth(this.stretchableColumns[index]);
      int num3 = 0;
      int[] numArray = new int[this.stretchableColumns.Count];
      for (int index = 0; index < this.stretchableColumns.Count; ++index)
      {
        double num4 = (double) this.TableElement.GetColumnWidth(this.stretchableColumns[index]) / (double) num2;
        numArray[index] = (int) Math.Round(num4 * (double) this.availableWidth);
        num3 += numArray[index];
      }
      int num5 = this.availableWidth - num3 - (this.stretchableColumns.Count - 1) * this.TableElement.CellSpacing;
      int index1 = 0;
      while (num5 > 0)
      {
        ++numArray[index1];
        --num5;
        index1 = (index1 + 1) % numArray.Length;
      }
      this.TableElement.BeginUpdate();
      for (int index2 = 0; index2 < this.stretchableColumns.Count; ++index2)
        this.TableElement.ColumnsViewState.SetItemSize(this.stretchableColumns[index2], numArray[index2]);
      this.TableElement.EndUpdate();
      int num6 = num3 + this.nonStretchableWidth;
      for (int index2 = this.stretchableColumns.Count - 1; num6 != this.availableWidth && index2 >= 0; --index2)
      {
        int stretchableColumn = this.stretchableColumns[index2];
        int columnWidth2 = this.TableElement.GetColumnWidth(stretchableColumn);
        int size = columnWidth2 - (num6 - this.availableWidth);
        if (size >= this.TableElement.ViewInfo.MinColumnWidth)
        {
          int num4 = num6 + (size - columnWidth2);
          this.TableElement.ColumnsViewState.SetItemSize(stretchableColumn, size);
          break;
        }
      }
    }

    public override void StartColumnResize(int column)
    {
      if (!this.stretchableColumns.Contains(column))
        return;
      this.resizedColumn = column;
      this.resizeStartWidths = new Dictionary<int, int>();
      this.resizeStartScaleFactors = new Dictionary<int, double>();
      foreach (int stretchableColumn in this.stretchableColumns)
        this.resizeStartWidths.Add(stretchableColumn, this.TableElement.GetColumnWidth(stretchableColumn));
      this.stretchableColumnResizeIndex = this.stretchableColumns.IndexOf(this.resizedColumn);
      this.offsetX = 0;
      foreach (int topPinnedItem in this.TableElement.ViewInfo.ColumnsViewState.TopPinnedItems)
        this.offsetX += this.TableElement.GetColumnWidth(topPinnedItem);
      foreach (int stretchableColumn in this.stretchableColumns)
      {
        if (stretchableColumn < this.resizedColumn)
          this.offsetX += this.TableElement.GetColumnWidth(stretchableColumn) + this.TableElement.CellSpacing;
        else
          break;
      }
      int num = this.availableWidth - this.offsetX - this.TableElement.GetColumnWidth(this.resizedColumn);
      if (this.stretchableColumnResizeIndex + 1 == this.stretchableColumns.Count - 1)
      {
        this.resizeStartScaleFactors[this.stretchableColumns[this.stretchableColumnResizeIndex + 1]] = 1.0;
      }
      else
      {
        for (int index = this.stretchableColumnResizeIndex + 1; index < this.stretchableColumns.Count; ++index)
        {
          int stretchableColumn = this.stretchableColumns[index];
          this.resizeStartScaleFactors[stretchableColumn] = (double) this.resizeStartWidths[stretchableColumn] / (double) num;
        }
      }
    }

    public override bool ResizeColumn(int delta)
    {
      if (this.stretchableColumns.Count == 0)
        return false;
      int width = this.ClampWidth(this.TableElement.GetColumnWidth(this.resizedColumn) + delta);
      if (width == this.TableElement.GetColumnWidth(this.resizedColumn))
        return false;
      int length = this.TableElement.ColumnCount - this.resizedColumn - 1;
      if (length == 0 || this.resizedColumn < 0)
        return false;
      int[] numArray = new int[length];
      Dictionary<int, int> dictionary = new Dictionary<int, int>();
      if (this.resizedColumn != this.lastModifiedColumn)
      {
        this.lastModifiedColumn = this.resizedColumn;
        this.lastModifiedStrechableColumn = 0;
      }
      for (int index1 = this.resizedColumn + 1; index1 < this.TableElement.ColumnCount; ++index1)
      {
        int index2 = index1 - (this.resizedColumn + 1);
        numArray[index2] = index1;
        dictionary.Add(index1, this.TableElement.GetColumnWidth(index1));
      }
      int num1 = Math.Abs(delta);
      int num2 = delta > 0 ? -1 : 1;
      int num3 = length;
      while (num1 > 0 && num3 > 0)
      {
        int num4 = dictionary[numArray[this.lastModifiedStrechableColumn]] + num2;
        if (num4 < this.TableElement.ViewInfo.MinColumnWidth)
        {
          --num3;
        }
        else
        {
          dictionary[numArray[this.lastModifiedStrechableColumn]] = num4;
          --num1;
        }
        ++this.lastModifiedStrechableColumn;
        this.lastModifiedStrechableColumn %= length;
      }
      this.TableElement.BeginUpdate();
      foreach (KeyValuePair<int, int> keyValuePair in dictionary)
        this.TableElement.SetColumnWidth(keyValuePair.Key, keyValuePair.Value);
      this.TableElement.GetColumnWidth(this.resizedColumn);
      this.TableElement.SetColumnWidth(this.resizedColumn, width);
      this.TableElement.EndUpdate();
      return true;
    }

    public override void EndResizeColumn()
    {
      this.resizedColumn = int.MinValue;
      this.offsetX = 0;
      this.stretchableColumnResizeIndex = int.MinValue;
      this.resizeStartWidths.Clear();
      this.resizeStartScaleFactors.Clear();
    }

    private int ClampWidth(int width)
    {
      width = Math.Max(this.TableElement.ViewInfo.MinColumnWidth, width);
      return width;
    }

    public override void ResetCache()
    {
      this.availableWidth = int.MinValue;
    }
  }
}
