// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class VirtualGridRowElement : LightVisualElement, IVirtualizedElement<int>
  {
    private int rowIndex = int.MinValue;
    public static RadProperty HotTrackingProperty = RadProperty.Register(nameof (HotTracking), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentProperty = RadProperty.Register(nameof (IsCurrent), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsOddProperty = RadProperty.Register(nameof (IsOdd), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsInEditModeProperty = RadProperty.Register(nameof (IsInEditMode), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsCurrentCellProperty = RadProperty.Register(nameof (ContainsCurrentCell), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsSelectedCellsProperty = RadProperty.Register(nameof (ContainsSelectedCells), typeof (bool), typeof (VirtualGridRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    private bool hasChildRows;
    private StackLayoutElement leftPinnedColumns;
    private StackLayoutElement rightPinnedColumns;
    private ScrollableVirtualCellsContainer scrollableColumns;
    private VirtualGridTableElement tableElement;
    private VirtualGridTableElement childTableElement;
    private VirtualGridDetailViewCellElement detailsElement;

    static VirtualGridRowElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new VirtualGridRowElementStateManagerFactory(), typeof (VirtualGridRowElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = false;
    }

    public VirtualGridRowElement()
    {
      this.leftPinnedColumns = this.CreateLeftPinnedColumnsContainer();
      this.leftPinnedColumns.StretchVertically = true;
      this.leftPinnedColumns.ElementSpacing = -1;
      this.Children.Add((RadElement) this.leftPinnedColumns);
      this.rightPinnedColumns = this.CreateRightPinnedColumnsContainer();
      this.rightPinnedColumns.StretchVertically = true;
      this.rightPinnedColumns.ElementSpacing = -1;
      this.Children.Add((RadElement) this.rightPinnedColumns);
      this.scrollableColumns = this.CreateScrollableColumnsContainer();
      this.scrollableColumns.RowElement = this;
      this.Children.Add((RadElement) this.scrollableColumns);
      this.detailsElement = this.CreateDetailViewCellElementContainer();
      this.detailsElement.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.detailsElement);
      this.HotTracking = false;
    }

    protected virtual StackLayoutElement CreateLeftPinnedColumnsContainer()
    {
      return new StackLayoutElement();
    }

    protected virtual StackLayoutElement CreateRightPinnedColumnsContainer()
    {
      return new StackLayoutElement();
    }

    protected virtual ScrollableVirtualCellsContainer CreateScrollableColumnsContainer()
    {
      return new ScrollableVirtualCellsContainer();
    }

    protected virtual VirtualGridDetailViewCellElement CreateDetailViewCellElementContainer()
    {
      return new VirtualGridDetailViewCellElement();
    }

    public VirtualGridDetailViewCellElement DetailsElement
    {
      get
      {
        return this.detailsElement;
      }
    }

    public virtual bool CanApplyAlternatingColor
    {
      get
      {
        return this.ViewInfo.EnableAlternatingRowColor;
      }
    }

    public StackLayoutElement LeftPinnedCellContainer
    {
      get
      {
        return this.leftPinnedColumns;
      }
    }

    public StackLayoutElement RightPinnedCellContainer
    {
      get
      {
        return this.rightPinnedColumns;
      }
    }

    public bool HasChildRows
    {
      get
      {
        return this.hasChildRows;
      }
    }

    public ScrollableVirtualCellsContainer CellContainer
    {
      get
      {
        return this.scrollableColumns;
      }
    }

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    public virtual bool HotTracking
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.HotTrackingProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.HotTrackingProperty, (object) value);
      }
    }

    public bool IsInEditMode
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.IsInEditModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.IsInEditModeProperty, (object) value);
      }
    }

    public virtual bool IsOdd
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.IsOddProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.IsOddProperty, (object) value);
      }
    }

    public virtual bool IsCurrent
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.IsCurrentProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.IsCurrentProperty, (object) value);
      }
    }

    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.IsSelectedProperty, (object) value);
      }
    }

    public virtual bool ContainsCurrentCell
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.ContainsCurrentCellProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.ContainsCurrentCellProperty, (object) value);
      }
    }

    public virtual bool ContainsSelectedCells
    {
      get
      {
        return (bool) this.GetValue(VirtualGridRowElement.ContainsSelectedCellsProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridRowElement.ContainsSelectedCellsProperty, (object) value);
      }
    }

    public bool IsChildViewInitialized
    {
      get
      {
        return this.childTableElement != null;
      }
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public int Data
    {
      get
      {
        return this.RowIndex;
      }
    }

    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.tableElement.ViewInfo;
      }
    }

    public bool IsChildViewVisible
    {
      get
      {
        if (this.childTableElement != null)
          return this.childTableElement.Parent != null;
        return false;
      }
    }

    public virtual void Initialize(VirtualGridTableElement tableElement)
    {
      this.tableElement = tableElement;
      this.scrollableColumns.DataProvider = (IEnumerable) tableElement.ColumnScroller;
      this.scrollableColumns.ElementProvider = tableElement.ColumnScroller.ElementProvider;
      this.UpdateCellSpacing();
    }

    public void InvalidatePinnedColumns()
    {
      int num1 = 1 + this.tableElement.ColumnsViewState.TopPinnedItems.Count;
      int count = this.tableElement.ColumnsViewState.BottomPinnedItems.Count;
      while (this.leftPinnedColumns.Children.Count > num1)
        this.leftPinnedColumns.Children[this.leftPinnedColumns.Children.Count - 1].Dispose();
      while (this.rightPinnedColumns.Children.Count > count)
        this.rightPinnedColumns.Children[this.rightPinnedColumns.Children.Count - 1].Dispose();
      if (this.leftPinnedColumns.Children.Count == 0 || !(this.leftPinnedColumns.Children[0] is VirtualGridIndentCellElement))
      {
        if (this.leftPinnedColumns.Children.Count > 0)
          this.leftPinnedColumns.Children[0].Dispose();
        this.leftPinnedColumns.Children.Insert(0, (RadElement) this.scrollableColumns.ElementProvider.GetElement(-1, (object) this));
      }
      while (this.leftPinnedColumns.Children.Count < num1)
        this.leftPinnedColumns.Children.Add((RadElement) this.scrollableColumns.ElementProvider.GetElement(this.ViewInfo.ColumnsViewState.TopPinnedItems[this.ViewInfo.ColumnsViewState.TopPinnedItems.Count - 1], (object) this));
      for (int index = 0; index < this.leftPinnedColumns.Children.Count - 1; ++index)
      {
        int topPinnedItem = this.ViewInfo.ColumnsViewState.TopPinnedItems[index];
        if (!((VirtualGridCellElement) this.leftPinnedColumns.Children[index + 1]).IsCompatible(topPinnedItem, (object) this))
        {
          this.leftPinnedColumns.Children.RemoveAt(index + 1);
          VirtualGridCellElement element = (VirtualGridCellElement) this.scrollableColumns.ElementProvider.GetElement(topPinnedItem, (object) this);
          this.leftPinnedColumns.Children.Insert(index + 1, (RadElement) element);
        }
      }
      while (this.rightPinnedColumns.Children.Count < count)
        this.rightPinnedColumns.Children.Add((RadElement) this.scrollableColumns.ElementProvider.GetElement(this.ViewInfo.ColumnsViewState.BottomPinnedItems[this.ViewInfo.ColumnsViewState.BottomPinnedItems.Count - 1], (object) this));
      for (int index = 0; index < this.rightPinnedColumns.Children.Count; ++index)
      {
        int bottomPinnedItem = this.ViewInfo.ColumnsViewState.BottomPinnedItems[index];
        if (!((VirtualGridCellElement) this.rightPinnedColumns.Children[index]).IsCompatible(bottomPinnedItem, (object) this))
        {
          this.rightPinnedColumns.Children.RemoveAt(index);
          VirtualGridCellElement element = (VirtualGridCellElement) this.scrollableColumns.ElementProvider.GetElement(bottomPinnedItem, (object) this);
          this.rightPinnedColumns.Children.Insert(index, (RadElement) element);
        }
      }
      VirtualGridIndentCellElement child = this.leftPinnedColumns.Children[0] as VirtualGridIndentCellElement;
      if (child.RowIndex != this.RowIndex)
      {
        child.Detach();
        child.Attach(-1, (object) this, false);
      }
      int num2 = 1;
      int num3 = 0;
      foreach (int topPinnedItem in this.tableElement.ColumnsViewState.TopPinnedItems)
        ((VirtualGridCellElement) this.leftPinnedColumns.Children[num2++]).Attach(topPinnedItem, (object) this, false);
      foreach (int bottomPinnedItem in this.tableElement.ColumnsViewState.BottomPinnedItems)
        ((VirtualGridCellElement) this.rightPinnedColumns.Children[num3++]).Attach(bottomPinnedItem, (object) this, false);
    }

    public virtual void Attach(int data, object context)
    {
      this.rowIndex = data;
      this.detailsElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.detailsElement_RadPropertyChanged);
      this.hasChildRows = this.TableElement.GridElement.OnQueryHasChildRows(this.rowIndex, this.ViewInfo);
      this.Synchronize();
    }

    public virtual void Detach()
    {
      this.rowIndex = int.MinValue;
      this.hasChildRows = false;
      this.detailsElement.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.detailsElement_RadPropertyChanged);
    }

    public virtual bool IsCompatible(int data, object context)
    {
      return data >= 0;
    }

    public virtual void Synchronize()
    {
      this.Synchronize(true);
    }

    public virtual void Synchronize(bool updateContent)
    {
      this.UpdateCellSpacing();
      this.IsCurrent = this.tableElement.GridElement.CurrentCell != null && this.tableElement.GridElement.CurrentCell.RowIndex == this.RowIndex && this.tableElement.GridElement.CurrentCell.ViewInfo == this.ViewInfo;
      this.ContainsSelectedCells = this.tableElement.GridElement.Selection.RowContainsSelection(this.rowIndex, this.ViewInfo);
      this.ContainsCurrentCell = this.IsCurrent;
      this.IsSelected = this.tableElement.GridElement.Selection.SelectionMode == VirtualGridSelectionMode.FullRowSelect && this.ContainsSelectedCells;
      this.IsOdd = this.rowIndex % 2 != 0;
      if (this.CanApplyAlternatingColor && this.IsOdd && (!this.IsCurrent && !this.IsSelected))
      {
        this.DrawFill = true;
        this.GradientStyle = GradientStyles.Solid;
        this.BackColor = this.TableElement.AlternatingRowColor;
      }
      else
      {
        int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
      }
      this.InvalidatePinnedColumns();
      this.InitializeChildTableView();
      this.SynchronizeCells(updateContent);
      this.tableElement.GridElement.OnRowFormatting(new VirtualGridRowElementEventArgs(this, this.ViewInfo));
    }

    public virtual void SynchronizeIndentCell()
    {
      foreach (VirtualGridCellElement child in this.LeftPinnedCellContainer.Children)
      {
        if (child is VirtualGridIndentCellElement)
        {
          child.Synchronize();
          break;
        }
      }
    }

    public virtual void SynchronizeCells()
    {
      this.SynchronizeCells(true);
    }

    public virtual void SynchronizeCells(bool updateContent)
    {
      foreach (VirtualGridCellElement cellElement in this.GetCellElements())
      {
        if (updateContent || cellElement is VirtualGridIndentCellElement)
          cellElement.Synchronize(this);
        else
          cellElement.Synchronize(false);
      }
    }

    protected override void DisposeManagedResources()
    {
      if (this.childTableElement != null && this.childTableElement.Parent == null)
        this.childTableElement.Dispose();
      base.DisposeManagedResources();
    }

    private void InitializeChildTableView()
    {
      if (this.tableElement.IsRowExpanded(this.rowIndex))
      {
        if (this.childTableElement == null)
          this.childTableElement = new VirtualGridTableElement(this.tableElement.GridElement, this.tableElement.GetChildViewInfo(this.rowIndex));
        else
          this.childTableElement.ViewInfo = this.tableElement.GetChildViewInfo(this.rowIndex);
        if (this.childTableElement.Parent != this.detailsElement)
          this.detailsElement.Children.Add((RadElement) this.childTableElement);
        this.detailsElement.Padding = this.childTableElement.ViewInfo.Padding;
        this.detailsElement.Visibility = ElementVisibility.Visible;
      }
      else
      {
        if (this.childTableElement == null || this.childTableElement.Parent != this.detailsElement)
          return;
        this.detailsElement.Children.Remove((RadElement) this.childTableElement);
        this.detailsElement.Visibility = ElementVisibility.Collapsed;
      }
    }

    private void UpdateCellSpacing()
    {
      if (this.tableElement == null)
        return;
      this.scrollableColumns.ItemSpacing = this.tableElement.CellSpacing;
      this.leftPinnedColumns.ElementSpacing = this.tableElement.CellSpacing;
      this.rightPinnedColumns.ElementSpacing = this.tableElement.CellSpacing;
    }

    public IEnumerable<VirtualGridCellElement> GetCellElements()
    {
      foreach (VirtualGridCellElement child in this.leftPinnedColumns.Children)
        yield return child;
      foreach (VirtualGridCellElement child in this.scrollableColumns.Children)
        yield return child;
      foreach (VirtualGridCellElement child in this.rightPinnedColumns.Children)
        yield return child;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.HotTracking = this.TableElement != null && this.TableElement.EnableHotTracking && this.IsMouseOverElement && (this.detailsElement == null || this.detailsElement.Visibility != ElementVisibility.Visible || !this.detailsElement.ControlBoundingRectangle.Contains(e.Location));
    }

    private void detailsElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.IsMouseOverElementProperty)
        return;
      this.HotTracking = this.IsMouseOverElement && !this.detailsElement.IsMouseOverElement;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.IsMouseOverElementProperty)
        return;
      this.HotTracking = this.TableElement != null && this.TableElement.EnableHotTracking && this.IsMouseOverElement && !this.detailsElement.IsMouseOverElement;
    }

    protected override void PostPaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale)
    {
      base.PostPaintChildren(graphics, clipRectange, angle, scale);
      this.TableElement.GridElement.OnRowPaint(this, graphics);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.scrollableColumns.ScrollOffset = new SizeF((float) -this.tableElement.ColumnScroller.ScrollOffset, 0.0f);
      int num = this.MeasureRowHeight(availableSize);
      return new SizeF(base.MeasureOverride(availableSize).Width, (float) num);
    }

    protected virtual int MeasureRowHeight(SizeF availableSize)
    {
      return this.TableElement.GetRowHeight(this.Data);
    }

    protected override SizeF MeasureElements(
      SizeF availableSize,
      SizeF clientSize,
      Padding borderThickness)
    {
      SizeF empty = SizeF.Empty;
      RadElement radElement1 = (RadElement) this.leftPinnedColumns;
      RadElement radElement2 = (RadElement) this.rightPinnedColumns;
      SizeF availableSize1 = availableSize;
      if (this.TableElement.IsRowPinned(this.RowIndex))
        availableSize1.Height = (float) this.TableElement.GetRowHeight(this.RowIndex);
      if (this.IsChildViewVisible)
        availableSize1.Height = (float) (this.tableElement.GetRowHeight(this.rowIndex) - this.childTableElement.ViewInfo.ExpandedHeight);
      if (this.RightToLeft)
      {
        radElement1 = (RadElement) this.rightPinnedColumns;
        radElement2 = (RadElement) this.leftPinnedColumns;
      }
      radElement1.Measure(availableSize1);
      float num1 = 0.0f;
      if ((double) radElement1.DesiredSize.Width > 0.0)
        num1 = radElement1.DesiredSize.Width + (float) this.tableElement.CellSpacing;
      availableSize1.Width -= num1;
      empty.Height = radElement1.DesiredSize.Height;
      empty.Width = num1;
      radElement2.Measure(availableSize1);
      float num2 = (double) radElement2.DesiredSize.Width <= 0.0 ? 0.0f : radElement2.DesiredSize.Width + (float) this.tableElement.CellSpacing;
      availableSize1.Width -= num2;
      empty.Height = Math.Max(empty.Height, radElement2.DesiredSize.Height);
      empty.Width += num2;
      this.scrollableColumns.Measure(availableSize1);
      empty.Height = Math.Max(empty.Height, this.scrollableColumns.DesiredSize.Height);
      empty.Width += this.scrollableColumns.DesiredSize.Width;
      if (float.IsInfinity(availableSize1.Height))
        empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      if (this.IsChildViewVisible)
      {
        this.childTableElement.ParentRowOffset = -this.GetParentRowOffset();
        float height = Math.Min((float) this.childTableElement.ViewInfo.ExpandedHeight, availableSize.Height - empty.Height);
        this.detailsElement.Measure(new SizeF(availableSize.Width, height));
        empty.Height += height;
      }
      return empty;
    }

    private float GetParentRowOffset()
    {
      float num1 = 0.0f;
      RadElement parent = this.Parent;
      int rowIndex = this.rowIndex;
      while (parent != null)
      {
        ScrollableVirtualRowsContainer virtualRowsContainer = parent as ScrollableVirtualRowsContainer;
        VirtualGridRowElement virtualGridRowElement = parent as VirtualGridRowElement;
        if (virtualRowsContainer != null)
        {
          int num2 = -1;
          IEnumerator enumerator = ((IEnumerable) virtualRowsContainer.TableElement.RowScroller).GetEnumerator();
          try
          {
            if (enumerator.MoveNext())
              num2 = (int) enumerator.Current;
          }
          finally
          {
            (enumerator as IDisposable)?.Dispose();
          }
          if (num2 != -1)
            num1 += virtualRowsContainer.ScrollOffset.Height + (float) virtualRowsContainer.TableElement.RowScroller.GetItemOffset(rowIndex) - (float) virtualRowsContainer.TableElement.RowScroller.GetItemOffset(num2) + (float) virtualRowsContainer.TableElement.RowsViewState.GetItemSize(rowIndex, true, false) + virtualRowsContainer.TableElement.ViewElement.TopPinnedRows.DesiredSize.Height;
          else
            continue;
        }
        if (virtualGridRowElement != null)
          rowIndex = virtualGridRowElement.rowIndex;
        parent = parent.Parent;
      }
      return num1;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.IsChildViewVisible)
        finalSize.Height -= (float) this.childTableElement.ViewInfo.ExpandedHeight;
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.Layout.Arrange(clientRectangle);
      RadElement radElement1 = (RadElement) this.leftPinnedColumns;
      RadElement radElement2 = (RadElement) this.rightPinnedColumns;
      if (this.RightToLeft)
      {
        radElement1 = (RadElement) this.rightPinnedColumns;
        radElement2 = (RadElement) this.leftPinnedColumns;
      }
      float x1 = 0.0f;
      radElement1.Arrange(new RectangleF(x1, 0.0f, radElement1.DesiredSize.Width, finalSize.Height));
      if ((double) radElement1.DesiredSize.Width > 0.0)
        x1 += radElement1.DesiredSize.Width + (float) this.tableElement.CellSpacing;
      this.scrollableColumns.Arrange(new RectangleF(x1, 0.0f, this.scrollableColumns.DesiredSize.Width, finalSize.Height));
      float x2 = x1 + (this.scrollableColumns.DesiredSize.Width + (float) this.tableElement.CellSpacing);
      radElement2.Arrange(new RectangleF(x2, 0.0f, radElement2.DesiredSize.Width, finalSize.Height));
      if (this.IsChildViewVisible)
      {
        this.detailsElement.Arrange(new RectangleF(clientRectangle.Left, clientRectangle.Bottom, clientRectangle.Width, (float) this.childTableElement.ViewInfo.ExpandedHeight));
        finalSize.Height += (float) this.childTableElement.ViewInfo.ExpandedHeight;
      }
      return finalSize;
    }
  }
}
