// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowInfoComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewRowInfoComparer : IComparer<GridViewRowInfo>
  {
    private SortDescriptorCollection sortContext;

    public GridViewRowInfoComparer(SortDescriptorCollection context)
    {
      this.sortContext = context;
    }

    public SortDescriptorCollection Context
    {
      get
      {
        return this.sortContext;
      }
    }

    public static int CompareRows(
      GridViewRowInfo x,
      GridViewRowInfo y,
      SortDescriptorCollection context)
    {
      GridViewDataOperation type = GridViewDataOperation.Sorting;
      for (int index = 0; index < context.Count; ++index)
      {
        if (context[index].PropertyIndex >= 0)
        {
          GridViewDataColumn column = x.ViewTemplate.Columns[context[index].PropertyIndex];
          object xValue = column.GetValue(x, type);
          object yValue = column.GetValue(y, type);
          if (column.UseDataTypeConverterWhenSorting && column.DataTypeConverter != null && column.DataTypeConverter.CanConvertTo((ITypeDescriptorContext) column, column.DataType))
          {
            GridViewComboBoxColumn viewComboBoxColumn = column as GridViewComboBoxColumn;
            if (viewComboBoxColumn == null || !viewComboBoxColumn.DisplayMemberSort)
            {
              CultureInfo formatterCulture = GridViewRowInfoComparer.GetFormatterCulture((IFormatProvider) column.FormatInfo);
              xValue = column.DataTypeConverter.ConvertTo((ITypeDescriptorContext) column, formatterCulture, xValue, column.DataType);
              yValue = column.DataTypeConverter.ConvertTo((ITypeDescriptorContext) column, formatterCulture, yValue, column.DataType);
            }
          }
          GridViewRowInfoComparer.GetBlobData(ref xValue, ref yValue);
          int num = !(xValue is IComparable) || yValue == null || (object) yValue.GetType() != (object) xValue.GetType() ? DataUtils.CompareNulls(xValue, yValue) : ((IComparable) xValue).CompareTo(yValue);
          if (num != 0)
          {
            if (context[index].Direction == ListSortDirection.Descending)
              return -num;
            return num;
          }
        }
      }
      return 0;
    }

    private static CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
    {
      return formatInfo as CultureInfo ?? CultureInfo.CurrentCulture;
    }

    public static void GetBlobData(ref object xValue, ref object yValue)
    {
      if (xValue is Image && yValue is Image)
      {
        ImageConverter imageConverter = new ImageConverter();
        xValue = (object) (byte[]) imageConverter.ConvertTo(xValue, typeof (byte[]));
        yValue = (object) (byte[]) imageConverter.ConvertTo(yValue, typeof (byte[]));
      }
      if (xValue is Color && yValue is Color)
      {
        xValue = (object) ((Color) xValue).ToArgb();
        yValue = (object) ((Color) yValue).ToArgb();
      }
      if (!(xValue is Array))
        return;
      xValue = (object) ((Array) xValue).Length;
      if (!(yValue is Array))
        return;
      yValue = (object) ((Array) yValue).Length;
    }

    public virtual int Compare(GridViewRowInfo x, GridViewRowInfo y)
    {
      return GridViewRowInfoComparer.CompareRows(x, y, this.sortContext);
    }
  }
}
