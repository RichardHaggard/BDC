// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewElement : GanttViewBaseViewElement
  {
    private int indent = 20;
    private GanttViewTextViewColumnCollection columns;
    private GanttViewTextViewColumnContainer columnContainer;
    private GanttViewTextViewColumnScroller columnScroller;
    private RadScrollBarElement columnsScrollBar;
    private GanttViewTextViewColumn firstVisibleColumn;
    private GanttViewTextViewColumn lastVisibleColumn;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.columnContainer = new GanttViewTextViewColumnContainer(this);
      this.columnContainer.StretchHorizontally = true;
      this.columnContainer.StretchVertically = true;
      this.columnContainer.ElementProvider = (IVirtualizedElementProvider<GanttViewTextViewColumn>) new GanttViewTextViewHeaderCellElementProvider();
      this.Children.Add((RadElement) this.columnContainer);
      this.columnsScrollBar = new RadScrollBarElement();
      this.columnsScrollBar.ScrollType = ScrollType.Horizontal;
      this.columnsScrollBar.MinSize = new Size(0, RadScrollBarElement.HorizontalScrollBarHeight);
      this.columnsScrollBar.ScrollTimerDelay = 1;
      this.Children.Add((RadElement) this.columnsScrollBar);
      this.columnScroller = new GanttViewTextViewColumnScroller();
      this.columnScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.columnScroller.ElementProvider = this.columnContainer.ElementProvider;
      this.columnScroller.Scrollbar = this.columnsScrollBar;
    }

    public GanttViewTextViewElement(RadGanttViewElement ganttView)
      : base(ganttView)
    {
      this.columns = new GanttViewTextViewColumnCollection(ganttView);
      this.columns.CollectionChanged += new NotifyCollectionChangedEventHandler(this.columns_CollectionChanged);
      this.columnScroller.Traverser = (ITraverser<GanttViewTextViewColumn>) new GanttViewTextViewColumnTraverser((IList<GanttViewTextViewColumn>) this.columns);
      this.columnContainer.DataProvider = (IEnumerable) this.columnScroller;
      this.columnScroller.ScrollerUpdated += new EventHandler(this.columnScroller_ScrollerUpdated);
      this.Scroller.ScrollState = ScrollState.AlwaysHide;
      this.Scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.Scroller.Traverser = (ITraverser<GanttViewDataItem>) new GanttViewTraverser(ganttView);
    }

    public GanttViewTextViewColumn FirstVisibleColumn
    {
      get
      {
        if (this.firstVisibleColumn == null)
          this.firstVisibleColumn = this.GetFirstVisibleColumn();
        return this.firstVisibleColumn;
      }
      internal set
      {
        this.firstVisibleColumn = value;
      }
    }

    public GanttViewTextViewColumn LastVisibleColumn
    {
      get
      {
        if (this.lastVisibleColumn == null)
          this.lastVisibleColumn = this.GetLastVisibleColumn();
        return this.lastVisibleColumn;
      }
      internal set
      {
        this.lastVisibleColumn = value;
      }
    }

    public GanttViewTextViewColumnContainer ColumnContainer
    {
      get
      {
        return this.columnContainer;
      }
    }

    public GanttViewTextViewColumnScroller ColumnScroller
    {
      get
      {
        return this.columnScroller;
      }
    }

    public GanttViewTextViewColumnCollection Columns
    {
      get
      {
        return this.columns;
      }
    }

    public int Indent
    {
      get
      {
        return this.indent;
      }
      set
      {
        this.indent = value;
      }
    }

    public GanttViewTextViewColumn GetFirstVisibleColumn()
    {
      for (int index = 0; index < this.Columns.Count; ++index)
      {
        if (this.Columns[index].Visible)
          return this.Columns[index];
      }
      return (GanttViewTextViewColumn) null;
    }

    public GanttViewTextViewColumn GetLastVisibleColumn()
    {
      for (int index = this.Columns.Count - 1; index >= 0; --index)
      {
        if (this.Columns[index].Visible)
          return this.Columns[index];
      }
      return (GanttViewTextViewColumn) null;
    }

    protected virtual void ColumnsCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.ItemChanged || e.Action == NotifyCollectionChangedAction.ItemChanging)
        return;
      this.firstVisibleColumn = (GanttViewTextViewColumn) null;
      this.lastVisibleColumn = (GanttViewTextViewColumn) null;
    }

    public void Update(RadGanttViewElement.UpdateActions updateAction)
    {
      if (updateAction == RadGanttViewElement.UpdateActions.Reset)
      {
        this.HScrollBar.Value = this.HScrollBar.Minimum;
        this.VScrollBar.Value = this.VScrollBar.Minimum;
        this.SuspendLayout();
        while (this.ViewElement.Children.Count > 0)
        {
          GanttViewTextItemElement child = (GanttViewTextItemElement) this.ViewElement.Children[0];
          this.ViewElement.Children.Remove((RadElement) child);
          child.Detach();
        }
        this.ResumeLayout(false);
        this.ViewElement.ElementProvider.ClearCache();
      }
      if (updateAction == RadGanttViewElement.UpdateActions.Reset || updateAction == RadGanttViewElement.UpdateActions.Resume)
        this.UpdateScrollers(updateAction);
      this.ViewElement.UpdateItems();
      if (updateAction != RadGanttViewElement.UpdateActions.StateChanged && updateAction != RadGanttViewElement.UpdateActions.Resume)
        return;
      this.SynchronizeItemElements();
    }

    protected internal virtual void SynchronizeItemElements()
    {
      foreach (GanttViewBaseItemElement child in this.ViewElement.Children)
        child.Synchronize();
      this.Invalidate();
    }

    private void columnScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.columnContainer.InvalidateMeasure();
      this.ViewElement.InvalidateArrange();
    }

    private void columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.ColumnsCollectionChanged(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location);
      if (!(elementAtPoint is RadScrollBarElement) && elementAtPoint.FindAncestor<RadScrollBarElement>() == null)
        return;
      this.GanttViewElement.EndEdit();
    }

    protected internal virtual void UpdateScrollers(RadGanttViewElement.UpdateActions updateAction)
    {
      if (this.GanttViewElement.Root.Items.Count > 0)
      {
        if (this.Scroller.Traverser.Current != null && this.Scroller.Traverser.Current.GanttViewElement != this.GanttViewElement)
        {
          this.Scroller.Traverser.Reset();
          this.Scroller.UpdateScrollRange();
          this.Scroller.UpdateScrollValue();
        }
        else
          this.Scroller.UpdateScrollRange();
      }
      else
      {
        this.HScrollBar.Value = this.HScrollBar.Minimum;
        this.HScrollBar.Maximum = this.HScrollBar.Minimum;
        this.VScrollBar.Value = this.VScrollBar.Minimum;
        this.VScrollBar.Maximum = this.VScrollBar.Minimum;
        this.UpdateHScrollbarVisibility();
      }
    }

    protected virtual void UpdateHScrollbarVisibility()
    {
      if (this.ColumnScroller.ScrollState == ScrollState.AlwaysShow)
        this.HScrollBar.Visibility = ElementVisibility.Visible;
      else if (this.ColumnScroller.ScrollState == ScrollState.AlwaysHide)
        this.ColumnScroller.Scrollbar.Visibility = ElementVisibility.Collapsed;
      else
        this.ColumnScroller.Scrollbar.Visibility = this.ColumnScroller.Scrollbar.LargeChange < this.ColumnScroller.Scrollbar.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    internal void UpdateHScrollbarMaximum(int newMaximum)
    {
      if (this.ColumnScroller.Scrollbar.Maximum == newMaximum)
        return;
      this.ColumnScroller.Scrollbar.Maximum = newMaximum;
      this.UpdateHScrollbarVisibility();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      this.MeasureChildElements(clientRectangle);
      ElementVisibility visibility = this.VScrollBar.Visibility;
      float num = 0.0f;
      if (this.columnsScrollBar.Visibility != ElementVisibility.Collapsed || this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Visibility != ElementVisibility.Collapsed)
        num = Math.Max(this.columnsScrollBar.DesiredSize.Height, this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.DesiredSize.Height);
      this.ViewElement.Measure(new SizeF(clientRectangle.Width - this.VScrollBar.DesiredSize.Width, availableSize.Height - num - (float) this.GanttViewElement.HeaderHeight));
      if (this.UpdateOnMeasure(availableSize) || visibility != this.VScrollBar.Visibility)
      {
        this.MeasureChildElements(clientRectangle);
        this.UpdateOnMeasure(availableSize);
        this.ViewElement.Measure(new SizeF(clientRectangle.Width - this.VScrollBar.DesiredSize.Width, availableSize.Height - num - (float) this.GanttViewElement.HeaderHeight));
      }
      this.columnContainer.Measure(new SizeF(clientRectangle.Width - this.VScrollBar.DesiredSize.Width, (float) this.GanttViewElement.HeaderHeight));
      SizeF desiredSize = this.ViewElement.DesiredSize;
      desiredSize.Height += (float) this.GanttViewElement.HeaderHeight + num;
      desiredSize.Width += this.VScrollBar.DesiredSize.Width;
      return desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.columnContainer.Visibility != ElementVisibility.Collapsed)
        this.columnContainer.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width - this.VScrollBar.DesiredSize.Width, (float) this.GanttViewElement.HeaderHeight));
      float num = 0.0f;
      if (this.columnsScrollBar.Visibility != ElementVisibility.Collapsed || this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Visibility != ElementVisibility.Collapsed)
        num = Math.Max(this.columnsScrollBar.DesiredSize.Height, this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.DesiredSize.Height);
      RectangleF viewElementRect = new RectangleF(clientRectangle.X, clientRectangle.Y + (float) this.GanttViewElement.HeaderHeight, clientRectangle.Width, clientRectangle.Height - num - (float) this.GanttViewElement.HeaderHeight);
      this.Layout.Arrange(clientRectangle);
      RectangleF hscrollBarRect = this.ArrangeHScrollBar(ref viewElementRect, clientRectangle);
      this.ArrangeVScrollBar(ref viewElementRect, hscrollBarRect, clientRectangle);
      viewElementRect.Width = Math.Max(1f, viewElementRect.Width);
      viewElementRect.Height = Math.Max(1f, viewElementRect.Height);
      this.ViewElement.Arrange(viewElementRect);
      return finalSize;
    }

    protected override RectangleF ArrangeHScrollBar(
      ref RectangleF viewElementRect,
      RectangleF clientRect)
    {
      RectangleF finalRect = RectangleF.Empty;
      if (this.columnsScrollBar.Visibility != ElementVisibility.Collapsed)
      {
        int num = (int) this.columnsScrollBar.DesiredSize.Height;
        if (num == 0)
          num = RadScrollBarElement.HorizontalScrollBarHeight;
        float y = clientRect.Bottom - (float) num;
        float width = clientRect.Width - this.VScrollBar.DesiredSize.Width;
        finalRect = new RectangleF(clientRect.X, y, width, (float) num);
        if (this.RightToLeft && this.VScrollBar.Visibility != ElementVisibility.Collapsed)
          finalRect.X += this.VScrollBar.DesiredSize.Width;
        this.columnsScrollBar.Arrange(finalRect);
        viewElementRect.Height -= this.columnsScrollBar.DesiredSize.Height;
      }
      return finalRect;
    }

    protected override bool UpdateOnMeasure(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      RadScrollBarElement columnsScrollBar = this.columnsScrollBar;
      RadScrollBarElement vscrollBar = this.VScrollBar;
      ElementVisibility visibility1 = columnsScrollBar.Visibility;
      ElementVisibility visibility2 = vscrollBar.Visibility;
      SizeF size = clientRectangle.Size;
      size.Height -= this.columnContainer.DesiredSize.Height;
      if (columnsScrollBar.Visibility != ElementVisibility.Collapsed || this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Visibility != ElementVisibility.Collapsed)
        size.Height -= columnsScrollBar.DesiredSize.Height;
      this.Scroller.ClientSize = size;
      this.columnScroller.ClientSize = size;
      this.columnScroller.UpdateScrollRange();
      this.Scroller.UpdateScrollRange();
      bool flag = false;
      if (vscrollBar.Visibility == ElementVisibility.Visible)
      {
        size.Width -= vscrollBar.DesiredSize.Width;
        this.Scroller.ClientSize = size;
        this.columnScroller.ClientSize = size;
        flag = true;
      }
      if (columnsScrollBar.Visibility != visibility1 && columnsScrollBar.Visibility == ElementVisibility.Visible)
      {
        size.Height -= columnsScrollBar.DesiredSize.Height;
        this.Scroller.ClientSize = size;
        this.columnScroller.ClientSize = size;
        flag = true;
      }
      if (flag)
      {
        this.columnScroller.UpdateScrollRange();
        this.Scroller.UpdateScrollRange();
      }
      if (visibility1 == columnsScrollBar.Visibility)
        return visibility2 != columnsScrollBar.Visibility;
      return true;
    }

    private void MeasureChildElements(RectangleF clientRect)
    {
      if (this.columnsScrollBar.Visibility != ElementVisibility.Collapsed)
        this.columnsScrollBar.Measure(clientRect.Size);
      if (this.VScrollBar.Visibility != ElementVisibility.Collapsed)
        this.VScrollBar.Measure(clientRect.Size);
      this.columnContainer.Visibility = ElementVisibility.Visible;
      this.columnContainer.Measure(new SizeF(clientRect.Width - this.VScrollBar.DesiredSize.Width, (float) this.GanttViewElement.HeaderHeight));
    }
  }
}
