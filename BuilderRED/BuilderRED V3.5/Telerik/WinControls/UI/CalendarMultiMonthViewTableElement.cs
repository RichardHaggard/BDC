// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarMultiMonthViewTableElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class CalendarMultiMonthViewTableElement : CalendarTableElement
  {
    private int horizontalCellSpacing = 5;
    private int verticalCellSpacing = 5;

    protected internal CalendarMultiMonthViewTableElement(
      CalendarVisualElement owner,
      RadCalendar calendar,
      CalendarView view)
      : base(owner, calendar, view)
    {
      this.cachedView = view;
      this.View.ShowColumnHeaders = false;
      this.View.ShowRowHeaders = false;
      this.View.ShowSelector = false;
    }

    protected internal CalendarMultiMonthViewTableElement(CalendarVisualElement owner)
      : this(owner, (RadCalendar) null, (CalendarView) null)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowFadeAndAnimation = false;
    }

    public override int Columns
    {
      get
      {
        return this.View.MultiViewColumns;
      }
    }

    public override int Rows
    {
      get
      {
        return this.View.MultiViewRows;
      }
    }

    public override CalendarView View
    {
      get
      {
        return this.cachedView;
      }
      set
      {
        if (this.cachedView == value)
          return;
        this.cachedView = value;
        this.ResetVisuals();
        this.View.ShowColumnHeaders = false;
        this.View.ShowRowHeaders = false;
        this.View.ShowSelector = false;
      }
    }

    public override void ResetVisuals()
    {
      this.cachedView = this.View;
      int index = 0;
      foreach (MonthViewElement child in this.Children)
      {
        child.TableElement.View = this.View.Children[index];
        child.TitleElement.View = this.View.Children[index];
        ++index;
      }
    }

    public override void Initialize(int rows, int columns)
    {
      this.visualElements = new List<LightVisualElement>(this.View.Children.Count);
      this.visualElements.Clear();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.View.Children.Count; ++index)
      {
        CalendarView child = this.View.Children[index];
        MonthViewElement monthViewElement = new MonthViewElement(this.Calendar, child);
        child.ShowHeader = true;
        monthViewElement.TitleElement.Visibility = ElementVisibility.Visible;
        monthViewElement.Row = num1;
        monthViewElement.Column = num2;
        ++num2;
        if (num2 == columns)
        {
          num2 = 0;
          ++num1;
        }
        this.visualElements.Add((LightVisualElement) monthViewElement);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.View == null || this.visualElements.Count == 0 || (this.Columns == 0 || this.Rows == 0))
        return finalSize;
      this.ArrangeContentCells(!this.StretchHorizontally ? (int) this.visualElements[0].DesiredSize.Width : (int) ((double) finalSize.Width - (double) (this.horizontalCellSpacing * this.Columns)) / this.Columns, !this.StretchHorizontally ? (int) this.visualElements[0].DesiredSize.Height : ((int) finalSize.Height - this.verticalCellSpacing * this.Rows) / this.Rows, 0, 0);
      return finalSize;
    }

    protected override void ArrangeHeaders(int xCellSize, int yCellSize, int xOffset, int yOffset)
    {
    }

    protected override void ArrangeContentCells(
      int xCellSize,
      int yCellSize,
      int xOffset,
      int yOffset)
    {
      PointF empty = PointF.Empty;
      for (int row = 0; row < this.Rows; ++row)
      {
        empty.Y += row > 0 ? (float) yCellSize : (float) yOffset;
        empty.Y += (float) this.verticalCellSpacing;
        empty.X = 0.0f;
        for (int column = 0; column < this.Columns; ++column)
        {
          LightVisualElement element = this.GetElement(row, column);
          if (element != null)
          {
            empty.X += column > 0 ? (float) xCellSize : (float) xOffset;
            empty.X += (float) this.horizontalCellSpacing;
            element.Arrange(new RectangleF(empty, new SizeF((float) xCellSize, (float) yCellSize)));
          }
        }
      }
    }

    protected internal override void CreateVisuals()
    {
      this.Initialize(this.Rows, this.Columns);
    }

    public override void RefreshVisuals(bool unconditional)
    {
      foreach (CalendarVisualElement visualElement in this.VisualElements)
        visualElement.RefreshVisuals(unconditional);
    }

    protected internal override LightVisualElement GetElement(int row, int column)
    {
      for (int index = 0; index < this.visualElements.Count; ++index)
      {
        MonthViewElement visualElement = this.visualElements[index] as MonthViewElement;
        if (visualElement.Column == column && visualElement.Row == row)
          return (LightVisualElement) visualElement;
      }
      return (LightVisualElement) null;
    }

    protected internal override void Recreate()
    {
      this.SuspendLayout();
      this.VisualElements.Clear();
      this.cachedView = this.View;
      if (this.Calendar != null)
      {
        this.Initialize(this.Calendar.MultiViewRows, this.Calendar.MultiViewColumns);
        this.InitializeChildren();
      }
      this.ResumeLayout(true);
    }

    protected override void InitializeChildren()
    {
      this.DisposeChildren();
      for (int index = 0; index < this.VisualElements.Count; ++index)
        this.Children.Add((RadElement) this.visualElements[index]);
    }

    protected override void SetBehaviorOnPropertyChange(string propertyName)
    {
    }
  }
}
