// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridCheckBoxHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridCheckBoxHeaderCellElement : GridHeaderCellElement
  {
    public static RoutedEvent OnHeaderCellToggleStateChanged = RadElement.RegisterRoutedEvent(nameof (OnHeaderCellToggleStateChanged), typeof (GridCheckBoxHeaderCellElement));
    private bool shouldCheckDataRows = true;
    private RadCheckBoxElement checkBox;
    private bool suspendProcessingToggleStateChanged;
    private bool suspendHeaderSynchronization;
    private bool raiseEvent;

    public GridCheckBoxHeaderCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
      this.checkBox.IsThreeState = ((GridViewCheckBoxColumn) column).ThreeState;
    }

    protected override void DisposeManagedResources()
    {
      this.checkBox.ToggleStateChanged -= new StateChangedEventHandler(this.checkbox_ToggleStateChanged);
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBox = new RadCheckBoxElement();
      this.checkBox.ToggleStateChanged += new StateChangedEventHandler(this.checkbox_ToggleStateChanged);
      this.Children.Add((RadElement) this.checkBox);
    }

    public override void Attach(GridViewColumn data, object context)
    {
      base.Attach(data, context);
      if (((GridViewCheckBoxColumn) data).EnableHeaderCheckBox)
      {
        this.GridControl.CellValueChanged += new GridViewCellEventHandler(this.GridControl_CellValueChanged);
        this.GridControl.FilterChanged += new GridViewCollectionChangedEventHandler(this.GridControl_FilterChanged);
        this.GridControl.RowsChanged += new GridViewCollectionChangedEventHandler(this.GridControl_RowsChanged);
        this.checkBox.Visibility = ElementVisibility.Visible;
        this.checkBox.TextWrap = this.ColumnInfo.WrapText;
        this.SetCheckBoxState();
        this.SetContent();
      }
      else
      {
        this.GridControl.CellValueChanged -= new GridViewCellEventHandler(this.GridControl_CellValueChanged);
        this.GridControl.FilterChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_FilterChanged);
        this.GridControl.RowsChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_RowsChanged);
        this.checkBox.Visibility = ElementVisibility.Collapsed;
      }
    }

    public override void Detach()
    {
      if (this.GridControl != null)
      {
        this.GridControl.CellValueChanged -= new GridViewCellEventHandler(this.GridControl_CellValueChanged);
        this.GridControl.FilterChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_FilterChanged);
        this.GridControl.RowsChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_RowsChanged);
      }
      base.Detach();
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (GridHeaderCellElement);
      }
    }

    public RadCheckBoxElement CheckBox
    {
      get
      {
        return this.checkBox;
      }
    }

    private void GridControl_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      if (!this.shouldCheckDataRows || this.ViewInfo == null || this.ColumnIndex == -1)
        return;
      this.SetCheckBoxState();
    }

    private void GridControl_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      if (e.Action != NotifyCollectionChangedAction.Add || this.ColumnInfo == null || !((GridViewCheckBoxColumn) this.ColumnInfo).EnableHeaderCheckBox)
        return;
      this.SetCheckBoxState();
    }

    protected virtual void checkbox_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (this.suspendProcessingToggleStateChanged)
        return;
      if (this.ViewTemplate != null && !this.ViewTemplate.IsSelfReference && !this.MasterTemplate.IsSelfReference)
        this.MasterTemplate.BeginUpdate();
      else
        this.TableElement.BeginUpdate();
      object state = (object) DBNull.Value;
      if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        state = (object) true;
      else if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off)
        state = (object) false;
      else if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
        state = (object) null;
      this.RaiseToggleStateEvent();
      if (!this.shouldCheckDataRows)
      {
        if (this.ViewTemplate == null || this.ViewTemplate.IsSelfReference || this.MasterTemplate.IsSelfReference)
          return;
        this.MasterTemplate.EndUpdate(true, new DataViewChangedEventArgs(ViewChangedAction.DataChanged));
      }
      else
      {
        this.GridViewElement.EditorManager.EndEdit();
        this.TableElement.BeginUpdate();
        this.MasterTemplate.MasterViewInfo.TableSearchRow.SuspendSearch();
        GridViewColumn columnInfo = this.ColumnInfo;
        bool shouldCheckDataRows = this.shouldCheckDataRows;
        this.shouldCheckDataRows = false;
        List<FilterDescriptor> filterDescriptorList = new List<FilterDescriptor>();
        for (int index = 0; index < this.ViewTemplate.Columns.Count; ++index)
        {
          filterDescriptorList.Add(this.ViewTemplate.Columns[index].FilterDescriptor);
          this.ViewTemplate.Columns[index].FilterDescriptor = (FilterDescriptor) null;
        }
        List<GridViewRowInfo> rowsToIterateOver = this.GetRowsToIterateOver();
        this.suspendHeaderSynchronization = true;
        foreach (GridViewRowInfo gridViewRowInfo in rowsToIterateOver)
        {
          GridViewGroupRowInfo row = gridViewRowInfo as GridViewGroupRowInfo;
          if (row != null)
            this.CheckAllCheckBoxInChildRows(row, state);
          else
            gridViewRowInfo.Cells[this.ColumnIndex].Value = state;
        }
        this.suspendHeaderSynchronization = false;
        this.MasterTemplate.MasterViewInfo.TableSearchRow.ResumeSearch();
        this.TableElement.EndUpdate(false);
        this.TableElement.Update(GridUINotifyAction.DataChanged);
        if (this.ViewTemplate != null && !this.ViewTemplate.IsSelfReference && !this.MasterTemplate.IsSelfReference)
          this.MasterTemplate.EndUpdate(true, new DataViewChangedEventArgs(ViewChangedAction.DataChanged));
        else
          this.TableElement.EndUpdate(false);
        if (this.ViewTemplate != null)
        {
          for (int index = 0; index < filterDescriptorList.Count; ++index)
            this.ViewTemplate.Columns[index].FilterDescriptor = filterDescriptorList[index];
        }
        this.shouldCheckDataRows = shouldCheckDataRows;
      }
    }

    private List<GridViewRowInfo> GetRowsToIterateOver()
    {
      if (this.ViewTemplate != null && this.ViewTemplate.IsSelfReference)
      {
        PrintGridTraverser printGridTraverser = new PrintGridTraverser(this.ViewInfo);
        List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
        while (printGridTraverser.MoveNext())
        {
          if (printGridTraverser.Current is GridViewDataRowInfo)
            gridViewRowInfoList.Add(printGridTraverser.Current);
        }
        return gridViewRowInfoList;
      }
      ++this.ViewInfo.Version;
      List<GridViewRowInfo> gridViewRowInfoList1 = new List<GridViewRowInfo>((IEnumerable<GridViewRowInfo>) this.ViewInfo.Rows);
      --this.ViewInfo.Version;
      return gridViewRowInfoList1;
    }

    protected internal virtual void SetCheckBoxState()
    {
      if ((this.ColumnInfo as GridViewCheckBoxColumn).ThreeState)
        this.SetThreeStateCheckBoxState();
      else
        this.SetTwoStateCheckBoxState();
    }

    protected virtual void SetThreeStateCheckBoxState()
    {
      bool foundMatchingRow1 = false;
      bool foundMatchingRow2 = false;
      bool foundMatchingRow3 = false;
      foreach (GridViewRowInfo row in this.GetRowsToIterateOver())
      {
        foundMatchingRow1 = this.CheckCheckBoxValueForAllRows(row, foundMatchingRow1, Telerik.WinControls.Enumerations.ToggleState.On);
        foundMatchingRow2 = this.CheckCheckBoxValueForAllRows(row, foundMatchingRow2, Telerik.WinControls.Enumerations.ToggleState.Off);
        foundMatchingRow3 = this.CheckCheckBoxValueForAllRows(row, foundMatchingRow3, Telerik.WinControls.Enumerations.ToggleState.Indeterminate);
        if (foundMatchingRow1)
        {
          if (foundMatchingRow2)
            break;
        }
        if (foundMatchingRow3)
          break;
      }
      if (foundMatchingRow1 && foundMatchingRow2 || foundMatchingRow3)
        this.SetCheckBoxState(Telerik.WinControls.Enumerations.ToggleState.Indeterminate);
      else if (foundMatchingRow2)
      {
        this.SetCheckBoxState(Telerik.WinControls.Enumerations.ToggleState.Off);
      }
      else
      {
        if (this.GetRowsToIterateOver().Count <= 0)
          return;
        this.SetCheckBoxState(Telerik.WinControls.Enumerations.ToggleState.On);
      }
    }

    protected virtual void SetTwoStateCheckBoxState()
    {
      bool foundMatchingRow = false;
      foreach (GridViewRowInfo row in this.GetRowsToIterateOver())
      {
        foundMatchingRow = this.CheckCheckBoxValueForAllRows(row, foundMatchingRow, Telerik.WinControls.Enumerations.ToggleState.Off);
        if (foundMatchingRow)
        {
          this.SetCheckBoxState(Telerik.WinControls.Enumerations.ToggleState.Off);
          break;
        }
      }
      if (foundMatchingRow || this.GetRowsToIterateOver().Count <= 0)
        return;
      this.SetCheckBoxState(Telerik.WinControls.Enumerations.ToggleState.On);
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      if (e.Property == GridViewColumn.WrapTextProperty)
        this.checkBox.TextWrap = this.ColumnInfo.WrapText;
      if (e.Property == GridViewColumn.HeaderTextProperty)
        this.checkBox.Text = this.ColumnInfo.HeaderText;
      if (e.Property == GridViewCheckBoxColumn.ShouldCheckDataRowsProperty)
        this.shouldCheckDataRows = (bool) e.NewValue;
      if (e.Property == GridViewCheckBoxColumn.CheckedProperty)
        this.checkBox.ToggleState = ((GridViewCheckBoxColumn) this.ColumnInfo).Checked;
      if (e.Property == GridViewCheckBoxColumn.ThreeStateProperty)
        this.checkBox.IsThreeState = ((GridViewCheckBoxColumn) this.ColumnInfo).ThreeState;
      if (e.Property != GridViewCheckBoxColumn.EnableHeaderCheckBoxProperty)
        return;
      bool newValue = (bool) e.NewValue;
      this.checkBox.Visibility = newValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.SetContent();
      if (newValue)
      {
        this.GridControl.CellValueChanged -= new GridViewCellEventHandler(this.GridControl_CellValueChanged);
        this.GridControl.FilterChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_FilterChanged);
        this.GridControl.RowsChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_RowsChanged);
        this.GridControl.CellValueChanged += new GridViewCellEventHandler(this.GridControl_CellValueChanged);
        this.GridControl.FilterChanged += new GridViewCollectionChangedEventHandler(this.GridControl_FilterChanged);
        this.GridControl.RowsChanged += new GridViewCollectionChangedEventHandler(this.GridControl_RowsChanged);
      }
      else
      {
        this.GridControl.CellValueChanged -= new GridViewCellEventHandler(this.GridControl_CellValueChanged);
        this.GridControl.FilterChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_FilterChanged);
        this.GridControl.RowsChanged -= new GridViewCollectionChangedEventHandler(this.GridControl_RowsChanged);
      }
    }

    private void GridControl_CellValueChanged(object sender, GridViewCellEventArgs e)
    {
      if (!this.shouldCheckDataRows || this.ViewInfo == null || (!((GridViewCheckBoxColumn) this.Data).EnableHeaderCheckBox || this.MasterTemplate.CurrentRow == null) || (this.MasterTemplate.CurrentRow is GridViewNewRowInfo || this.MasterTemplate.CurrentRow.ViewInfo != this.ViewInfo || (e.Row.ViewTemplate != this.ViewTemplate || this.suspendHeaderSynchronization)))
        return;
      this.SetCheckBoxState();
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewCheckBoxColumn)
        return base.IsCompatible(data, context);
      return false;
    }

    protected override void UpdateInfoCore()
    {
      base.UpdateInfoCore();
      GridViewCheckBoxColumn columnInfo = this.ColumnInfo as GridViewCheckBoxColumn;
      if (columnInfo == null)
        return;
      this.shouldCheckDataRows = columnInfo.ShouldCheckDataRows;
    }

    public override void SetContent()
    {
      base.SetContent();
      if (((GridViewCheckBoxColumn) this.ColumnInfo).EnableHeaderCheckBox)
      {
        this.DrawText = false;
        this.checkBox.Text = this.ApplyFormatString(this.Value);
        this.checkBox.CheckAlignment = ((GridViewCheckBoxColumn) this.ColumnInfo).HeaderCheckBoxAlignment;
      }
      else
      {
        this.DrawText = true;
        this.checkBox.Text = "";
      }
      this.suspendProcessingToggleStateChanged = true;
      if (!this.raiseEvent)
        this.CheckBox.ToggleState = ((GridViewCheckBoxColumn) this.ColumnInfo).Checked;
      this.suspendProcessingToggleStateChanged = false;
    }

    protected virtual void SetCheckBoxState(Telerik.WinControls.Enumerations.ToggleState state)
    {
      if (!((GridViewCheckBoxColumn) this.Data).EnableHeaderCheckBox)
        return;
      this.suspendProcessingToggleStateChanged = true;
      this.raiseEvent = false;
      if (this.checkBox.ToggleState != state)
        this.raiseEvent = true;
      this.checkBox.ToggleState = state;
      if (this.raiseEvent)
        this.RaiseToggleStateEvent();
      this.raiseEvent = true;
      this.suspendProcessingToggleStateChanged = false;
    }

    protected virtual void RaiseToggleStateEvent()
    {
      if (!this.raiseEvent && this.ViewInfo.ParentRow != null)
        return;
      if (this.ViewTemplate != null && ((GridViewCheckBoxColumn) this.Data).EnableHeaderCheckBox && !this.raiseEvent)
        ((GridViewCheckBoxColumn) this.ColumnInfo).Checked = this.CheckBox.ToggleState;
      if (this.ViewTemplate.EventDispatcher.IsSuspended)
        this.ViewTemplate.EventDispatcher.ResumeNotifications();
      GridViewHeaderCellEventArgs args = new GridViewHeaderCellEventArgs(this.RowInfo, this.ColumnInfo, this.CheckBox.ToggleState);
      this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewHeaderCellEventArgs>(EventDispatcher.HeaderCellToggleStateChanged, (object) this, args);
      if (!this.ViewTemplate.EventDispatcher.IsSuspended)
        return;
      this.ViewTemplate.EventDispatcher.SuspendNotifications();
    }

    private bool CheckCheckBoxValueForAllRows(
      GridViewRowInfo row,
      bool foundMatchingRow,
      Telerik.WinControls.Enumerations.ToggleState togglestate)
    {
      if (foundMatchingRow)
        return foundMatchingRow;
      foreach (GridViewRowInfo gridViewRowInfo in !(row is GridViewGroupRowInfo) || !row.HasChildRows() ? row.ViewInfo.Rows : row.ChildRows)
      {
        GridViewGroupRowInfo viewGroupRowInfo = gridViewRowInfo as GridViewGroupRowInfo;
        if (viewGroupRowInfo != null)
        {
          foundMatchingRow = this.CheckCheckBoxValueForAllRows((GridViewRowInfo) viewGroupRowInfo, foundMatchingRow, togglestate);
        }
        else
        {
          object obj = gridViewRowInfo.Cells[this.ColumnIndex].Value;
          bool? nullable = new bool?();
          if (this.ColumnInfo is GridViewDataColumn)
          {
            TypeConverter dataTypeConverter = ((GridViewDataColumn) this.ColumnInfo).DataTypeConverter;
            if (dataTypeConverter != null)
            {
              if (!dataTypeConverter.CanConvertTo(typeof (bool)))
              {
                if (!dataTypeConverter.CanConvertTo(typeof (Telerik.WinControls.Enumerations.ToggleState)))
                  goto label_20;
              }
              try
              {
                if (dataTypeConverter.CanConvertTo(typeof (Telerik.WinControls.Enumerations.ToggleState)))
                {
                  Telerik.WinControls.Enumerations.ToggleState toggleState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
                  if (!(obj is DBNull) && obj != null)
                    toggleState = (Telerik.WinControls.Enumerations.ToggleState) dataTypeConverter.ConvertTo(obj, typeof (Telerik.WinControls.Enumerations.ToggleState));
                  switch (toggleState)
                  {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                      nullable = new bool?(true);
                      goto label_22;
                    case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
                      nullable = new bool?();
                      goto label_22;
                    default:
                      nullable = new bool?(false);
                      goto label_22;
                  }
                }
                else
                {
                  nullable = new bool?(Convert.ToBoolean(dataTypeConverter.ConvertTo(obj, typeof (bool))));
                  goto label_22;
                }
              }
              catch (FormatException ex)
              {
                goto label_22;
              }
              catch (NotSupportedException ex)
              {
                goto label_22;
              }
            }
label_20:
            bool result = false;
            if (bool.TryParse(string.Concat(obj), out result))
              nullable = new bool?(result);
          }
label_22:
          try
          {
            if (!this.checkBox.IsThreeState)
            {
              if ((gridViewRowInfo == this.RowInfo || obj != null && obj != DBNull.Value) && (!nullable.HasValue || nullable.Value))
              {
                if (!nullable.HasValue)
                {
                  if (Convert.ToBoolean(obj))
                    continue;
                }
                else
                  continue;
              }
              foundMatchingRow = true;
            }
            else
            {
              if ((obj == null || obj == DBNull.Value || !nullable.HasValue) && togglestate == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
                foundMatchingRow = true;
              if (gridViewRowInfo != this.RowInfo && (obj == null || obj == DBNull.Value) || nullable.HasValue && nullable.Value && togglestate == Telerik.WinControls.Enumerations.ToggleState.On || !nullable.HasValue && Convert.ToBoolean(obj) && togglestate == Telerik.WinControls.Enumerations.ToggleState.On)
                foundMatchingRow = true;
              if ((gridViewRowInfo == this.RowInfo || obj != null && obj != DBNull.Value) && (!nullable.HasValue || nullable.Value || togglestate != Telerik.WinControls.Enumerations.ToggleState.Off))
              {
                if (!nullable.HasValue)
                {
                  if (!Convert.ToBoolean(obj))
                  {
                    if (togglestate != Telerik.WinControls.Enumerations.ToggleState.Off)
                      continue;
                  }
                  else
                    continue;
                }
                else
                  continue;
              }
              foundMatchingRow = true;
            }
          }
          catch (FormatException ex)
          {
          }
          catch (InvalidCastException ex)
          {
          }
        }
      }
      return foundMatchingRow;
    }

    private void CheckAllCheckBoxInChildRows(GridViewGroupRowInfo row, object state)
    {
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      foreach (GridViewRowInfo childRow in row.ChildRows)
        gridViewRowInfoList.Add(childRow);
      foreach (GridViewRowInfo gridViewRowInfo in gridViewRowInfoList)
      {
        GridViewGroupRowInfo row1 = gridViewRowInfo as GridViewGroupRowInfo;
        if (row1 != null)
          this.CheckAllCheckBoxInChildRows(row1, state);
        else
          gridViewRowInfo.Cells[this.ColumnIndex].Value = state;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size1 = this.TableElement.ViewElement.RowLayout.ArrangeCell(new RectangleF((PointF) Point.Empty, availableSize), (GridCellElement) this).Size;
      SizeF empty = SizeF.Empty;
      SizeF size2 = this.GetClientRectangle(availableSize).Size;
      Padding borderThickness = this.GetBorderThickness(false);
      if (this.Arrow != null && this.Arrow.Visibility == ElementVisibility.Visible)
      {
        this.Arrow.Measure(size2);
        empty.Width += (float) (2.0 * (double) this.Arrow.DesiredSize.Width + 2.0);
        empty.Height = this.Arrow.DesiredSize.Height;
        if (this.Arrow.Alignment != ContentAlignment.TopCenter)
          size2.Width -= empty.Width;
      }
      if (this.StackLayout != null && this.StackLayout.Visibility == ElementVisibility.Visible && this.FilterButton.Visibility == ElementVisibility.Visible)
      {
        this.StackLayout.Measure(size2);
        empty.Width += this.StackLayout.DesiredSize.Width;
        empty.Height = Math.Max(empty.Height, this.StackLayout.DesiredSize.Height);
        size2.Width -= this.StackLayout.DesiredSize.Width;
      }
      if (this.checkBox != null && this.checkBox.Visibility == ElementVisibility.Visible)
      {
        this.checkBox.Measure(size2);
        size2.Width -= this.checkBox.DesiredSize.Width;
      }
      this.Layout.Measure(size2);
      empty.Width += this.Layout.DesiredSize.Width + this.checkBox.DesiredSize.Width;
      empty.Height = Math.Max(this.checkBox.DesiredSize.Height, Math.Max(empty.Height, this.Layout.DesiredSize.Height));
      empty.Width += (float) (this.Padding.Horizontal + borderThickness.Horizontal + 2);
      empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      if (!float.IsInfinity(availableSize.Width))
        empty.Width = size1.Width;
      if (!float.IsInfinity(availableSize.Height))
        empty.Height = size1.Height;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.ArrowPosition = this.SetArrowPosition();
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      double width = (double) clientRectangle.Width;
      if (this.StackLayout.Visibility == ElementVisibility.Visible && this.FilterButton.Visibility == ElementVisibility.Visible)
      {
        clientRectangle.Width -= this.StackLayout.DesiredSize.Width;
        if (this.RightToLeft)
          clientRectangle.X += this.StackLayout.DesiredSize.Width;
      }
      if (this.ArrowPosition != GridHeaderCellElement.ArrowPositionEnum.Top && this.ArrowPosition != GridHeaderCellElement.ArrowPositionEnum.Bottom)
        clientRectangle.Width -= (float) (2.0 * (double) this.Arrow.DesiredSize.Width + 2.0);
      if (this.checkBox.Visibility == ElementVisibility.Visible)
      {
        clientRectangle.Width -= this.checkBox.DesiredSize.Width;
        switch (((GridViewCheckBoxColumn) this.ColumnInfo).HeaderCheckBoxPosition)
        {
          case HorizontalAlignment.Right:
            clientRectangle.X = 2f;
            break;
          case HorizontalAlignment.Center:
            clientRectangle.X = (float) ((double) clientRectangle.Width / 2.0 + (double) this.checkBox.DesiredSize.Width / 2.0);
            break;
        }
      }
      this.Layout.Arrange(clientRectangle);
      foreach (RadElement child in this.Children)
      {
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          if (child == this.Arrow)
          {
            if (!this.ViewTemplate.EnableSorting)
              child.Visibility = ElementVisibility.Hidden;
            else if ((double) finalSize.Width - 2.0 * (double) this.Arrow.DesiredSize.Width > 0.0 && 2.0 * (double) this.Arrow.DesiredSize.Width < (double) finalSize.Width)
            {
              if (this.SortOrder != RadSortOrder.None && this.ColumnInfo.SortOrder != RadSortOrder.None)
                child.Visibility = ElementVisibility.Visible;
              this.ArrangeArrow(finalSize, child);
            }
            else
              child.Visibility = ElementVisibility.Hidden;
          }
          else if (child == this.StackLayout)
          {
            if (this.RightToLeft)
              this.StackLayout.Arrange(new RectangleF(0.0f, (float) (((double) finalSize.Height - (double) this.StackLayout.DesiredSize.Height) / 2.0), this.StackLayout.DesiredSize.Width, this.StackLayout.DesiredSize.Height));
            else
              this.StackLayout.Arrange(new RectangleF(finalSize.Width - this.StackLayout.DesiredSize.Width, (float) (((double) finalSize.Height - (double) this.StackLayout.DesiredSize.Height) / 2.0), this.StackLayout.DesiredSize.Width, this.StackLayout.DesiredSize.Height));
          }
          else if (child == this.checkBox)
          {
            GridViewCheckBoxColumn columnInfo = (GridViewCheckBoxColumn) this.ColumnInfo;
            RectangleF rectangleF = new RectangleF(0.0f, 0.0f, clientRectangle.Width + this.checkBox.DesiredSize.Width, finalSize.Height);
            switch (columnInfo.HeaderCheckBoxPosition)
            {
              case HorizontalAlignment.Left:
                rectangleF.X = 2f;
                break;
              case HorizontalAlignment.Right:
                rectangleF.X = rectangleF.Width - this.checkBox.DesiredSize.Width;
                break;
              case HorizontalAlignment.Center:
                rectangleF.X = (float) ((double) rectangleF.Width / 2.0 - (double) this.checkBox.DesiredSize.Width / 2.0);
                break;
            }
            rectangleF.Y = (float) ((double) finalSize.Height / 2.0 - (double) this.checkBox.DesiredSize.Height / 2.0);
            this.ArrangeElement(child, finalSize, new RectangleF(rectangleF.Location, this.checkBox.DesiredSize));
          }
          else
            this.ArrangeElement(child, finalSize, clientRectangle);
        }
      }
      return finalSize;
    }
  }
}
