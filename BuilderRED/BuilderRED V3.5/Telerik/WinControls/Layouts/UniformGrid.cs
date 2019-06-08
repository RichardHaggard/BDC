// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.UniformGrid
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.Layouts
{
  public class UniformGrid : LayoutPanel
  {
    public static readonly RadProperty ColumnsProperty = RadProperty.Register(nameof (Columns), typeof (int), typeof (UniformGrid), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty FirstColumnProperty = RadProperty.Register(nameof (FirstColumn), typeof (int), typeof (UniformGrid), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty RowsProperty = RadProperty.Register(nameof (Rows), typeof (int), typeof (UniformGrid), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty UniformGridColumnIndexProperty = RadProperty.RegisterAttached("UniformGridColumnIndex", typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    public static readonly RadProperty UniformGridRowIndexProperty = RadProperty.RegisterAttached("UniformGridRowIndex", typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    private int columns;
    private int rows;

    public int Columns
    {
      get
      {
        return (int) this.GetValue(UniformGrid.ColumnsProperty);
      }
      set
      {
        int num = (int) this.SetValue(UniformGrid.ColumnsProperty, (object) value);
      }
    }

    public int Rows
    {
      get
      {
        return (int) this.GetValue(UniformGrid.RowsProperty);
      }
      set
      {
        int num = (int) this.SetValue(UniformGrid.RowsProperty, (object) value);
      }
    }

    public int FirstColumn
    {
      get
      {
        return (int) this.GetValue(UniformGrid.FirstColumnProperty);
      }
      set
      {
        int num = (int) this.SetValue(UniformGrid.FirstColumnProperty, (object) value);
      }
    }

    public static void SetColumnIndex(RadElement element, int index)
    {
      int num = (int) element.SetValue(UniformGrid.UniformGridColumnIndexProperty, (object) index);
    }

    public static int GetColumnIndex(RadElement element)
    {
      return (int) element.GetValue(UniformGrid.UniformGridColumnIndexProperty);
    }

    public static void SetRowIndex(RadElement element, int index)
    {
      int num = (int) element.SetValue(UniformGrid.UniformGridRowIndexProperty, (object) index);
    }

    public static int GetRowIndex(RadElement element)
    {
      return (int) element.GetValue(UniformGrid.UniformGridRowIndexProperty);
    }

    private void UpdateComputedValues()
    {
      this.columns = this.Columns;
      this.rows = this.Rows;
      if (this.FirstColumn >= this.columns)
        this.FirstColumn = 0;
      if (this.rows != 0 && this.columns != 0)
        return;
      int num = 0;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index].Visibility != ElementVisibility.Collapsed)
          ++num;
      }
      if (num == 0)
        num = 1;
      if (this.rows == 0)
      {
        if (this.columns > 0)
        {
          this.rows = num + this.FirstColumn + (this.columns - 1) / this.columns;
        }
        else
        {
          this.rows = (int) Math.Sqrt((double) num);
          if (this.rows * this.rows < num)
            ++this.rows;
          this.columns = this.rows;
        }
      }
      else
      {
        if (this.columns != 0)
          return;
        this.columns = (num + this.rows - 1) / this.rows;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.UpdateComputedValues();
      availableSize = new SizeF(availableSize.Width / (float) this.columns, availableSize.Height / (float) this.rows);
      float num1 = 0.0f;
      float num2 = 0.0f;
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        SizeF desiredSize = child.DesiredSize;
        if ((double) num1 < (double) desiredSize.Width)
          num1 = desiredSize.Width;
        if ((double) num2 < (double) desiredSize.Height)
          num2 = desiredSize.Height;
      }
      return new SizeF(num1 * (float) this.columns, num2 * (float) this.rows);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      float width = finalSize.Width / (float) this.columns;
      float height = finalSize.Height / (float) this.rows;
      RectangleF finalRect = new RectangleF(PointF.Empty, new SizeF(width, height));
      double num = (double) finalSize.Width - 1.0;
      finalRect.X += finalRect.Width * (float) this.FirstColumn;
      int index1 = 0;
      int index2 = 0;
      foreach (RadElement child in this.Children)
      {
        child.Arrange(finalRect);
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          finalRect.X += width;
          UniformGrid.SetRowIndex(child, index1);
          UniformGrid.SetColumnIndex(child, index2);
          ++index2;
          if ((double) finalRect.X >= num)
          {
            finalRect.Y += finalRect.Height;
            finalRect.X = 0.0f;
            ++index1;
            index2 = 0;
          }
        }
      }
      return finalSize;
    }
  }
}
