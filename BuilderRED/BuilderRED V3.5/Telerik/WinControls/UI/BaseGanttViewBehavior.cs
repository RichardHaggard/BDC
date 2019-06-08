// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseGanttViewBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class BaseGanttViewBehavior
  {
    private Timer horizontalScrollTimer;
    private Timer verticalScrollTimer;
    private int horizontalScrollStep;
    private int verticalScrollStep;
    private RadGanttViewElement ganttViewElement;
    private Timer clickTimer;
    private List<System.Type> elementsStartingKineticScrolling;
    private GanttViewDefaultContextMenu defaultMenu;

    public BaseGanttViewBehavior()
    {
      this.clickTimer = new Timer();
      this.clickTimer.Tick += new EventHandler(this.clickTimer_Tick);
      this.horizontalScrollTimer = new Timer();
      this.horizontalScrollTimer.Interval = 20;
      this.horizontalScrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
      this.verticalScrollTimer = new Timer();
      this.verticalScrollTimer.Interval = 20;
      this.verticalScrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
      this.elementsStartingKineticScrolling = new List<System.Type>();
      this.elementsStartingKineticScrolling.Add(typeof (GanttViewTaskItemElement));
      this.elementsStartingKineticScrolling.Add(typeof (GanttViewSummaryItemElement));
      this.elementsStartingKineticScrolling.Add(typeof (GanttViewMilestoneItemElement));
      this.elementsStartingKineticScrolling.Add(typeof (GanttViewTextItemElement));
      this.elementsStartingKineticScrolling.Add(typeof (GanttViewTextViewCellElement));
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttViewElement;
      }
      internal set
      {
        this.ganttViewElement = value;
      }
    }

    protected GanttGraphicalViewBaseTaskElement EditedTaskElement { get; set; }

    protected GanttViewTextViewHeaderCellElement ColumnHeaderCell { get; set; }

    protected Point MouseDownLocation { get; set; }

    protected DateTime OriginalStart { get; set; }

    protected DateTime OriginalEnd { get; set; }

    protected int OriginalColumnWidth { get; set; }

    protected bool IsResizingColumn { get; set; }

    protected bool IsResizingStart { get; set; }

    protected bool IsResizingEnd { get; set; }

    protected bool IsCreatingLink { get; set; }

    protected bool IsEditingLink { get; set; }

    protected internal GanttViewLinkDataItem NewLink { get; set; }

    protected internal GanttViewLinkDataItem EditedLink { get; set; }

    protected internal GanttViewDataItem EditedLinkTaskCache { get; set; }

    protected internal TasksLinkType EditedLinkTypeCache { get; set; }

    protected bool WasSelectedOnMouseDown { get; set; }

    protected bool WasInEditModeOnMouseDown { get; set; }

    protected bool ShouldProcessKineticScrolling { get; set; }

    public Timer HorizontalScrollTimer
    {
      get
      {
        return this.horizontalScrollTimer;
      }
    }

    public Timer VerticalScrollTimer
    {
      get
      {
        return this.verticalScrollTimer;
      }
    }

    public int HorizontalScrollStep
    {
      get
      {
        return this.horizontalScrollStep;
      }
      set
      {
        this.horizontalScrollStep = value;
      }
    }

    public int VerticalScrollStep
    {
      get
      {
        return this.verticalScrollStep;
      }
      set
      {
        this.verticalScrollStep = value;
      }
    }

    protected internal Timer ClickTimer
    {
      get
      {
        return this.clickTimer;
      }
    }

    protected bool DoubleClick { get; set; }

    public List<System.Type> ElementsStartingKineticScrolling
    {
      get
      {
        return this.elementsStartingKineticScrolling;
      }
    }

    public virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      this.ShouldProcessKineticScrolling = this.ShouldHandleKineticScrolling(e);
      if (this.GanttViewElement.EnableKineticScrolling && this.ShouldProcessKineticScrolling)
      {
        bool isRunning = this.GanttViewElement.ScrollBehavior.IsRunning;
        this.GanttViewElement.ScrollBehavior.MouseDown(e.Location);
        if (isRunning && this.GanttViewElement.ScrollBehavior.IsRunning)
          return false;
      }
      RadElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location);
      if (elementAtPoint == null)
        return false;
      if (this.GanttViewElement.IsEditing && !(this.GanttViewElement.ActiveEditor as BaseInputEditor).EditorElement.IsMouseOverElement)
        this.GanttViewElement.EndEdit();
      if (this.GanttViewElement.TextViewElement.ControlBoundingRectangle.Contains(e.Location))
      {
        this.GanttViewElement.ProcessSelection((GanttViewLinkDataItem) null);
        GanttViewTextViewHeaderCellElement element = elementAtPoint as GanttViewTextViewHeaderCellElement;
        if (element != null)
          this.ProcessMouseDownOnCellHeaderElement(element, e);
        GanttViewTextViewCellElement cellElement = elementAtPoint as GanttViewTextViewCellElement;
        if (cellElement != null)
          this.ProcessMouseDownOnCellElement(cellElement, e);
      }
      else if (this.GanttViewElement.GraphicalViewElement.ControlBoundingRectangle.Contains(e.Location))
      {
        GanttGraphicalViewBaseTaskElement element1 = elementAtPoint as GanttGraphicalViewBaseTaskElement;
        if (element1 != null)
          this.ProcessMouseDownOnTaskElement(element1, e);
        GanttViewTaskLinkHandleElement element2 = elementAtPoint as GanttViewTaskLinkHandleElement;
        if (element2 != null)
        {
          if (this.GanttViewElement.SelectedLink == null || !this.ShouldStartLinkEditOperation(this.GanttViewElement.SelectedLink, element2))
            this.ProcessMouseDownOnTaskLinkHandleElement(element2, e);
          else
            this.ProcessMouseDownOnTaskLinkHandleElementWhenEditingLink(element2, e);
        }
        else
        {
          GanttViewLinkDataItem link = this.GanttViewElement.GraphicalViewElement.GetLink(e.Location);
          if (link != null && e.Button == MouseButtons.Left)
            this.GanttViewElement.ProcessSelection(link);
          else if (this.GanttViewElement.SelectedLink != null)
            this.GanttViewElement.ProcessSelection((GanttViewLinkDataItem) null);
        }
      }
      GanttViewTextViewCellElement textViewCellElement = elementAtPoint as GanttViewTextViewCellElement;
      if (textViewCellElement != null)
        textViewCellElement.Data.Current = true;
      GanttViewBaseItemElement viewBaseItemElement = elementAtPoint as GanttViewBaseItemElement;
      if (viewBaseItemElement == null)
      {
        viewBaseItemElement = elementAtPoint.FindAncestor<GanttViewBaseItemElement>();
        if (viewBaseItemElement == null)
          return false;
      }
      if (e.Button == MouseButtons.Left)
        this.GanttViewElement.ProcessSelection(viewBaseItemElement.Data);
      return false;
    }

    protected virtual void ProcessMouseDownOnCellHeaderElement(
      GanttViewTextViewHeaderCellElement element,
      MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      if (element.IsMouseOverResizeRectangle && element.CanBeResized())
      {
        this.OriginalColumnWidth = element.Column.Width;
        this.ColumnHeaderCell = element;
        this.GanttViewElement.ElementTree.Control.Capture = true;
        this.IsResizingColumn = true;
      }
      else
        this.GanttViewElement.DragDropService.Start((object) element);
    }

    protected virtual void ProcessMouseDownOnCellElement(
      GanttViewTextViewCellElement cellElement,
      MouseEventArgs e)
    {
      if (this.IsEditor(e.Location))
        return;
      GanttViewTextItemElement ancestor = cellElement.FindAncestor<GanttViewTextItemElement>();
      this.WasSelectedOnMouseDown = ancestor != null && ancestor.Data.Selected;
      this.WasInEditModeOnMouseDown = this.GanttViewElement.IsEditing;
      if (this.GanttViewElement.IsEditing && (!this.GanttViewElement.EndEdit() || ancestor != null && this.GanttViewElement.SelectedItem == ancestor.Data) || ancestor == null)
        return;
      this.GanttViewElement.ProcessSelection(ancestor.Data);
      this.MouseDownLocation = e.Location;
    }

    protected virtual void ProcessMouseDownOnTaskElement(
      GanttGraphicalViewBaseTaskElement element,
      MouseEventArgs e)
    {
      GanttGraphicalViewBaseItemElement parent = element.Parent as GanttGraphicalViewBaseItemElement;
      if (e.Button != MouseButtons.Left || this.GanttViewElement.ReadOnly || (parent == null || parent.Data.ReadOnly))
        return;
      if (element.IsMouseOverStartResizeRectangle && element.CanBeResized())
      {
        this.OriginalStart = parent.Data.Start;
        this.IsResizingStart = true;
        this.EditedTaskElement = element;
        this.GanttViewElement.ElementTree.Control.Capture = true;
      }
      else if (element.IsMouseOverEndResizeRectangle && element.CanBeResized())
      {
        this.OriginalEnd = parent.Data.End;
        this.IsResizingEnd = true;
        this.EditedTaskElement = element;
        this.GanttViewElement.ElementTree.Control.Capture = true;
      }
      else
        this.GanttViewElement.DragDropService.Start((object) element);
    }

    protected virtual void ProcessMouseDownOnTaskLinkHandleElement(
      GanttViewTaskLinkHandleElement element,
      MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left || this.GanttViewElement.ReadOnly)
        return;
      GanttGraphicalViewBaseItemElement parent = element.Parent as GanttGraphicalViewBaseItemElement;
      this.IsCreatingLink = true;
      this.GanttViewElement.ElementTree.Control.Capture = true;
      CreateGanttLinkDataItemEventArgs e1 = new CreateGanttLinkDataItemEventArgs();
      this.GanttViewElement.OnCreateLinkDataItem(e1);
      this.NewLink = e1.LinkDataItem == null ? new GanttViewLinkDataItem() : e1.LinkDataItem;
      this.NewLink.StartItem = parent.Data;
      this.NewLink.EndItem = new GanttViewDataItem();
      if (element == parent.LeftLinkHandleElement)
        this.NewLink.LinkType = TasksLinkType.StartToStart;
      else
        this.NewLink.LinkType = TasksLinkType.FinishToFinish;
    }

    protected virtual void ProcessMouseDownOnTaskLinkHandleElementWhenEditingLink(
      GanttViewTaskLinkHandleElement element,
      MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left || this.GanttViewElement.ReadOnly)
        return;
      GanttGraphicalViewBaseItemElement parent = element.Parent as GanttGraphicalViewBaseItemElement;
      this.IsEditingLink = true;
      this.GanttViewElement.ElementTree.Control.Capture = true;
      this.EditedLink = this.GanttViewElement.SelectedLink;
      this.EditedLinkTypeCache = this.EditedLink.LinkType;
      if (parent.Data == this.EditedLink.StartItem)
      {
        this.EditedLinkTaskCache = this.EditedLink.StartItem;
        this.EditedLink.StartItem = new GanttViewDataItem();
      }
      else
      {
        this.EditedLinkTaskCache = this.EditedLink.EndItem;
        this.EditedLink.EndItem = new GanttViewDataItem();
      }
    }

    public virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      if (this.IsResizingColumn)
        this.ProcessMouseMoveWhenResizingColumn(e);
      else if (this.IsResizingStart || this.IsResizingEnd)
        this.ProcessMouseMoveWhenResizingTask(this.EditedTaskElement, e);
      else if (this.IsCreatingLink)
        this.ProcessMouseMoveWhenCreatingLink(e);
      else if (this.IsEditingLink)
        this.ProcessMouseMoveWhenEditingLink(e);
      if (this.GanttViewElement.EnableKineticScrolling && this.ShouldProcessKineticScrolling)
        this.GanttViewElement.ScrollBehavior.MouseMove(e.Location);
      return false;
    }

    protected virtual void ProcessMouseMoveWhenResizingColumn(MouseEventArgs e)
    {
      int num = e.Location.X - this.ColumnHeaderCell.PointToControl(this.ColumnHeaderCell.Location).X;
      if (num <= this.GanttViewElement.MinimumColumnWidth)
        return;
      this.ColumnHeaderCell.Column.Width = num;
    }

    protected virtual void ProcessMouseMoveWhenResizingTask(
      GanttGraphicalViewBaseTaskElement element,
      MouseEventArgs e)
    {
      GanttViewDataItem data = ((GanttViewBaseItemElement) element.Parent).Data;
      if (this.IsResizingStart)
      {
        DateTime dateTime1 = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart.AddSeconds((double) (this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value + this.GanttViewElement.GraphicalViewElement.PointFromControl(e.Location).X) * this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
        if (dateTime1 == data.Start)
          return;
        DateTime dateTime2 = data.End.Subtract(new TimeSpan(this.GanttViewElement.GraphicalViewElement.OnePixelTime.Ticks * (long) this.GanttViewElement.MinimumTaskWidth));
        if (dateTime1 > dateTime2)
          dateTime1 = dateTime2;
        data.Start = dateTime1;
      }
      else if (this.IsResizingEnd)
      {
        DateTime dateTime1 = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart.AddSeconds((double) (this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value + this.GanttViewElement.GraphicalViewElement.PointFromControl(e.Location).X) * this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
        if (dateTime1 == data.End)
          return;
        DateTime dateTime2 = data.Start.Add(new TimeSpan(this.GanttViewElement.GraphicalViewElement.OnePixelTime.Ticks * (long) this.GanttViewElement.MinimumTaskWidth));
        if (dateTime1 < dateTime2)
          dateTime1 = dateTime2;
        data.End = dateTime1;
      }
      this.ProcessScrolling(this.GanttViewElement.ElementTree.Control.PointToScreen(e.Location), false);
      this.GanttViewElement.GraphicalViewElement.CalculateLinkLines(data);
      element.Parent.InvalidateMeasure(true);
      this.GanttViewElement.GraphicalViewElement.Invalidate();
    }

    protected virtual void ProcessMouseMoveWhenCreatingLink(MouseEventArgs e)
    {
      RadElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location);
      Point point = this.GanttViewElement.GraphicalViewElement.PointFromControl(e.Location);
      point.X += this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value;
      point.Y += this.GanttViewElement.GraphicalViewElement.VScrollBar.Value * this.GanttViewElement.ItemHeight;
      GanttGraphicalViewBaseItemElement viewBaseItemElement = elementAtPoint as GanttGraphicalViewBaseItemElement;
      if (viewBaseItemElement == null && elementAtPoint != null)
        viewBaseItemElement = elementAtPoint.FindAncestor<GanttGraphicalViewBaseItemElement>();
      if (viewBaseItemElement != null)
      {
        GanttViewDataItem data = viewBaseItemElement.Data;
        int num = (int) ((data.Start.AddSeconds((data.End - data.Start).TotalSeconds / 2.0) - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
        this.NewLink.LinkType = point.X >= num ? (this.NewLink.LinkType == TasksLinkType.StartToStart || this.NewLink.LinkType == TasksLinkType.StartToFinish ? TasksLinkType.StartToFinish : TasksLinkType.FinishToFinish) : (this.NewLink.LinkType == TasksLinkType.StartToStart || this.NewLink.LinkType == TasksLinkType.StartToFinish ? TasksLinkType.StartToStart : TasksLinkType.FinishToStart);
      }
      this.ProcessScrolling(this.GanttViewElement.ElementTree.Control.PointToScreen(e.Location), true);
      this.GanttViewElement.GraphicalViewElement.CalculateLinkLines(this.NewLink, new Point?(point));
      this.GanttViewElement.GraphicalViewElement.Invalidate();
    }

    protected virtual void ProcessMouseMoveWhenEditingLink(MouseEventArgs e)
    {
      RadElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location);
      Point point = this.GanttViewElement.GraphicalViewElement.PointFromControl(e.Location);
      point.X += this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value;
      point.Y += this.GanttViewElement.GraphicalViewElement.VScrollBar.Value * this.GanttViewElement.ItemHeight;
      GanttGraphicalViewBaseItemElement viewBaseItemElement = elementAtPoint as GanttGraphicalViewBaseItemElement;
      if (viewBaseItemElement == null && elementAtPoint != null)
        viewBaseItemElement = elementAtPoint.FindAncestor<GanttGraphicalViewBaseItemElement>();
      if (viewBaseItemElement != null)
      {
        GanttViewDataItem data = viewBaseItemElement.Data;
        int num = (int) ((data.Start.AddSeconds((data.End - data.Start).TotalSeconds / 2.0) - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
        if (point.X < num)
        {
          if (this.EditedLink.StartItem.GanttViewElement == null)
          {
            if (this.EditedLink.LinkType == TasksLinkType.FinishToFinish)
              this.EditedLink.LinkType = TasksLinkType.StartToFinish;
            else if (this.EditedLink.LinkType == TasksLinkType.FinishToStart)
              this.EditedLink.LinkType = TasksLinkType.StartToStart;
          }
          else if (this.EditedLink.LinkType == TasksLinkType.StartToFinish)
            this.EditedLink.LinkType = TasksLinkType.StartToStart;
          else if (this.EditedLink.LinkType == TasksLinkType.FinishToFinish)
            this.EditedLink.LinkType = TasksLinkType.FinishToStart;
        }
        else if (this.EditedLink.StartItem.GanttViewElement == null)
        {
          if (this.EditedLink.LinkType == TasksLinkType.StartToStart)
            this.EditedLink.LinkType = TasksLinkType.FinishToStart;
          else if (this.EditedLink.LinkType == TasksLinkType.StartToFinish)
            this.EditedLink.LinkType = TasksLinkType.FinishToFinish;
        }
        else if (this.EditedLink.LinkType == TasksLinkType.StartToStart)
          this.EditedLink.LinkType = TasksLinkType.StartToFinish;
        else if (this.EditedLink.LinkType == TasksLinkType.FinishToStart)
          this.EditedLink.LinkType = TasksLinkType.FinishToFinish;
      }
      this.ProcessScrolling(this.GanttViewElement.ElementTree.Control.PointToScreen(e.Location), true);
      this.GanttViewElement.GraphicalViewElement.CalculateLinkLines(this.EditedLink, new Point?(point));
      this.GanttViewElement.GraphicalViewElement.Invalidate();
    }

    public virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      if (this.GanttViewElement.EnableKineticScrolling && this.ShouldProcessKineticScrolling)
      {
        bool isRunning = this.GanttViewElement.ScrollBehavior.IsRunning;
        this.GanttViewElement.ScrollBehavior.MouseUp(e.Location);
        if (isRunning)
          return false;
      }
      if (this.GanttViewElement.TextViewElement.ControlBoundingRectangle.Contains(e.Location))
      {
        GanttViewTextViewCellElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location) as LightVisualElement as GanttViewTextViewCellElement;
        if (elementAtPoint != null)
          this.ProcessMouseUpOnCellElement(elementAtPoint, e);
      }
      if (this.IsResizingColumn)
        this.ProcessMouseUpWhenResizingColumn(e);
      else if (this.IsResizingStart || this.IsResizingEnd)
        this.ProcessMouseUpWhenResizingTask(this.EditedTaskElement, e);
      else if (this.IsCreatingLink)
        this.ProcessMouseUpWhenCreatingLink(e);
      else if (this.IsEditingLink)
        this.ProcessMouseUpWhenEditingLink(e);
      this.GanttViewElement.ElementTree.Control.Capture = false;
      if (this.HorizontalScrollTimer.Enabled)
        this.HorizontalScrollTimer.Stop();
      if (this.VerticalScrollTimer.Enabled)
        this.VerticalScrollTimer.Stop();
      this.GanttViewElement.GraphicalViewElement.InvalidateArrange(true);
      this.GanttViewElement.GraphicalViewElement.Invalidate();
      return false;
    }

    protected virtual void ProcessMouseUpOnCellElement(
      GanttViewTextViewCellElement cellElement,
      MouseEventArgs e)
    {
      GanttViewTextItemElement ancestor = cellElement.FindAncestor<GanttViewTextItemElement>();
      if (this.DoubleClick || this.IsEditor(e.Location) || ancestor == null || !ancestor.ControlBoundingRectangle.Contains(this.MouseDownLocation))
      {
        this.clickTimer.Stop();
        this.DoubleClick = false;
      }
      else if (this.IsScrollBar(e.Location))
      {
        if (!this.GanttViewElement.IsEditing)
          return;
        this.GanttViewElement.EndEdit();
      }
      else
      {
        if (e.Button != MouseButtons.Left)
          return;
        if (this.GanttViewElement.BeginEditMode == GanttViewBeginEditModes.BeginEditOnClick || this.WasInEditModeOnMouseDown)
        {
          this.GanttViewElement.BeginEdit();
        }
        else
        {
          if (this.GanttViewElement.BeginEditMode != GanttViewBeginEditModes.BeginEditOnSecondClick || !this.WasSelectedOnMouseDown && !this.WasInEditModeOnMouseDown)
            return;
          this.ClickTimer.Interval = SystemInformation.DoubleClickTime;
          this.ClickTimer.Start();
        }
      }
    }

    protected virtual void ProcessMouseUpWhenResizingColumn(MouseEventArgs e)
    {
      this.ColumnHeaderCell = (GanttViewTextViewHeaderCellElement) null;
      this.IsResizingColumn = false;
    }

    protected virtual void ProcessMouseUpWhenResizingTask(
      GanttGraphicalViewBaseTaskElement element,
      MouseEventArgs e)
    {
      this.EditedTaskElement.IsMouseDown = false;
      this.EditedTaskElement = (GanttGraphicalViewBaseTaskElement) null;
      this.IsResizingStart = false;
      this.IsResizingEnd = false;
    }

    protected virtual void ProcessMouseUpWhenCreatingLink(MouseEventArgs e)
    {
      GanttViewTaskLinkHandleElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location) as GanttViewTaskLinkHandleElement;
      if (elementAtPoint != null)
      {
        GanttGraphicalViewBaseItemElement parent = elementAtPoint.Parent as GanttGraphicalViewBaseItemElement;
        if (this.NewLink.StartItem != parent.Data)
        {
          this.NewLink.EndItem = parent.Data;
          this.NewLink.LinkType = elementAtPoint != parent.LeftLinkHandleElement ? (this.NewLink.LinkType == TasksLinkType.StartToFinish || this.NewLink.LinkType == TasksLinkType.StartToStart ? TasksLinkType.StartToFinish : TasksLinkType.FinishToFinish) : (this.NewLink.LinkType == TasksLinkType.StartToFinish || this.NewLink.LinkType == TasksLinkType.StartToStart ? TasksLinkType.StartToStart : TasksLinkType.FinishToStart);
          this.GanttViewElement.Links.Add(this.NewLink);
          this.GanttViewElement.GraphicalViewElement.CalculateLinkLines(this.NewLink, new Point?());
        }
      }
      this.IsCreatingLink = false;
      this.NewLink = (GanttViewLinkDataItem) null;
    }

    protected virtual void ProcessMouseUpWhenEditingLink(MouseEventArgs e)
    {
      GanttViewTaskLinkHandleElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location) as GanttViewTaskLinkHandleElement;
      if (elementAtPoint != null)
      {
        GanttGraphicalViewBaseItemElement parent = elementAtPoint.Parent as GanttGraphicalViewBaseItemElement;
        if (this.EditedLink.StartItem.GanttViewElement == null)
        {
          if (elementAtPoint == parent.LeftLinkHandleElement)
          {
            if (this.EditedLink.LinkType == TasksLinkType.FinishToFinish)
              this.EditedLink.LinkType = TasksLinkType.StartToFinish;
            else if (this.EditedLink.LinkType == TasksLinkType.FinishToStart)
              this.EditedLink.LinkType = TasksLinkType.StartToStart;
          }
          else if (elementAtPoint == parent.RightLinkHandleElement)
          {
            if (this.EditedLink.LinkType == TasksLinkType.StartToFinish)
              this.EditedLink.LinkType = TasksLinkType.FinishToFinish;
            else if (this.EditedLink.LinkType == TasksLinkType.StartToStart)
              this.EditedLink.LinkType = TasksLinkType.FinishToStart;
          }
          this.EditedLink.StartItem = parent.Data;
        }
        else if (this.EditedLink.EndItem.GanttViewElement == null)
        {
          if (elementAtPoint == parent.LeftLinkHandleElement)
          {
            if (this.EditedLink.LinkType == TasksLinkType.FinishToFinish)
              this.EditedLink.LinkType = TasksLinkType.FinishToStart;
            else if (this.EditedLink.LinkType == TasksLinkType.StartToFinish)
              this.EditedLink.LinkType = TasksLinkType.StartToStart;
          }
          else if (elementAtPoint == parent.RightLinkHandleElement)
          {
            if (this.EditedLink.LinkType == TasksLinkType.FinishToStart)
              this.EditedLink.LinkType = TasksLinkType.FinishToFinish;
            else if (this.EditedLink.LinkType == TasksLinkType.StartToStart)
              this.EditedLink.LinkType = TasksLinkType.StartToFinish;
          }
          this.EditedLink.EndItem = parent.Data;
        }
      }
      else
      {
        if (this.EditedLink.StartItem.GanttViewElement == null)
          this.EditedLink.StartItem = this.EditedLinkTaskCache;
        else if (this.EditedLink.EndItem.GanttViewElement == null)
          this.EditedLink.EndItem = this.EditedLinkTaskCache;
        this.EditedLink.LinkType = this.EditedLinkTypeCache;
        this.EditedLink.ShowItemsHandles();
      }
      this.GanttViewElement.GraphicalViewElement.CalculateLinkLines(this.EditedLink, new Point?());
      this.IsEditingLink = false;
      this.EditedLink = (GanttViewLinkDataItem) null;
      this.EditedLinkTaskCache = (GanttViewDataItem) null;
    }

    public virtual bool ProcessMouseClick(MouseEventArgs e)
    {
      return false;
    }

    public virtual bool ProcessDoubleClick(MouseEventArgs e)
    {
      return false;
    }

    public virtual bool ProcessMouseEnter(EventArgs e)
    {
      return false;
    }

    public virtual bool ProcessMouseLeave(EventArgs e)
    {
      return false;
    }

    public virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      if (this.GanttViewElement.IsEditing)
        return false;
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int num2 = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      RadScrollBarElement scrollBarElement = Control.ModifierKeys != Keys.Control ? this.GanttViewElement.GraphicalViewElement.VScrollBar : (!this.GanttViewElement.TextViewElement.ControlBoundingRectangle.Contains(e.Location) ? this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement : this.GanttViewElement.TextViewElement.ColumnScroller.Scrollbar);
      int num3 = scrollBarElement.Value - num2 * scrollBarElement.SmallChange;
      if (num3 > scrollBarElement.Maximum - scrollBarElement.LargeChange + 1)
        num3 = scrollBarElement.Maximum - scrollBarElement.LargeChange + 1;
      if (num3 < scrollBarElement.Minimum)
        num3 = scrollBarElement.Minimum;
      scrollBarElement.Value = num3;
      this.GanttViewElement.GraphicalViewElement.InvalidateArrange(true);
      this.GanttViewElement.GraphicalViewElement.Invalidate();
      return true;
    }

    public virtual bool ProcessMouseHover(EventArgs e)
    {
      return false;
    }

    protected virtual bool IsEditor(Point point)
    {
      RadElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(point);
      if (elementAtPoint == null)
        return false;
      if (elementAtPoint is RadEditorElement)
        return true;
      return elementAtPoint.FindAncestor<RadEditorElement>() != null;
    }

    protected virtual bool IsScrollBar(Point point)
    {
      RadElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(point);
      if (elementAtPoint == null)
        return false;
      if (elementAtPoint is RadScrollBarElement)
        return true;
      return elementAtPoint.FindAncestor<RadScrollBarElement>() != null;
    }

    protected virtual bool ShouldHandleKineticScrolling(MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return false;
      RadElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(e.Location);
      if (elementAtPoint == null)
        return false;
      return this.ElementsStartingKineticScrolling.Contains(elementAtPoint.GetType());
    }

    protected virtual bool ShouldStartLinkEditOperation(
      GanttViewLinkDataItem link,
      GanttViewTaskLinkHandleElement element)
    {
      if (link == null)
        return false;
      GanttGraphicalViewBaseItemElement element1 = this.GanttViewElement.GraphicalViewElement.GetElement(link.StartItem) as GanttGraphicalViewBaseItemElement;
      GanttGraphicalViewBaseItemElement element2 = this.GanttViewElement.GraphicalViewElement.GetElement(link.EndItem) as GanttGraphicalViewBaseItemElement;
      switch (link.LinkType)
      {
        case TasksLinkType.FinishToFinish:
          if (element1 != null && element == element1.LeftLinkHandleElement || element2 != null && element == element2.LeftLinkHandleElement)
            return false;
          break;
        case TasksLinkType.FinishToStart:
          if (element1 != null && element == element1.LeftLinkHandleElement || element2 != null && element == element2.RightLinkHandleElement)
            return false;
          break;
        case TasksLinkType.StartToFinish:
          if (element1 != null && element == element1.RightLinkHandleElement || element2 != null && element == element2.LeftLinkHandleElement)
            return false;
          break;
        case TasksLinkType.StartToStart:
          if (element1 != null && element == element1.RightLinkHandleElement || element2 != null && element == element2.RightLinkHandleElement)
            return false;
          break;
      }
      return true;
    }

    public virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      if (this.GanttViewElement.IsEditing)
        return false;
      switch (e.KeyCode)
      {
        case Keys.Return:
        case Keys.Space:
        case Keys.F2:
          this.ProcessF2Key(e);
          break;
        case Keys.End:
          this.ProcessEndKey(e);
          break;
        case Keys.Home:
          this.ProcessHomeKey(e);
          break;
        case Keys.Left:
          this.ProcessLeftKey(e);
          break;
        case Keys.Up:
          this.ProcessUpKey(e);
          break;
        case Keys.Right:
          this.ProcessRightKey(e);
          break;
        case Keys.Down:
          this.ProcessDownKey(e);
          break;
        case Keys.Delete:
          this.ProcessDeleteKey(e);
          break;
        case Keys.Add:
          this.ProcessAddKey(e);
          break;
        case Keys.Subtract:
          this.ProcessSubtractKey(e);
          break;
      }
      return false;
    }

    public virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      return false;
    }

    public virtual bool ProcessKeyUp(KeyEventArgs e)
    {
      return false;
    }

    public virtual bool ProcessDialogKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Return:
          this.ProcessF2Key(new KeyEventArgs(keyData));
          break;
        case Keys.Escape:
          this.ProcessEscapeKey(new KeyEventArgs(keyData));
          break;
      }
      return false;
    }

    protected virtual void ProcessEscapeKey(KeyEventArgs e)
    {
      this.GanttViewElement.ElementTree.Control.Capture = false;
      if (this.IsResizingColumn)
      {
        this.IsResizingColumn = false;
        this.ColumnHeaderCell.Column.Width = this.OriginalColumnWidth;
        this.ColumnHeaderCell = (GanttViewTextViewHeaderCellElement) null;
      }
      else if (this.IsResizingStart || this.IsResizingEnd)
      {
        GanttViewDataItem data = ((GanttViewBaseItemElement) this.EditedTaskElement.Parent).Data;
        this.EditedTaskElement = (GanttGraphicalViewBaseTaskElement) null;
        if (this.IsResizingStart)
        {
          this.IsResizingStart = false;
          data.Start = this.OriginalStart;
        }
        else
        {
          if (!this.IsResizingEnd)
            return;
          this.IsResizingEnd = false;
          data.End = this.OriginalEnd;
        }
      }
      else
      {
        if (!this.GanttViewElement.IsEditing)
          return;
        this.GanttViewElement.CancelEdit();
      }
    }

    public virtual bool ProcessSubtractKey(KeyEventArgs e)
    {
      if (this.GanttViewElement.SelectedItem != null)
        this.GanttViewElement.SelectedItem.Expanded = false;
      return false;
    }

    public virtual bool ProcessAddKey(KeyEventArgs e)
    {
      if (this.GanttViewElement.SelectedItem != null)
        this.GanttViewElement.SelectedItem.Expand();
      return false;
    }

    public virtual bool ProcessHomeKey(KeyEventArgs e)
    {
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      ganttViewTraverser.Reset();
      if (ganttViewTraverser.MoveNext())
        this.GanttViewElement.ProcessSelection(ganttViewTraverser.Current);
      return false;
    }

    public virtual bool ProcessEndKey(KeyEventArgs e)
    {
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      if (ganttViewTraverser.MoveToEnd())
        this.GanttViewElement.ProcessSelection(ganttViewTraverser.Current);
      return false;
    }

    public virtual bool ProcessLeftKey(KeyEventArgs e)
    {
      if (this.GanttViewElement.SelectedItem == null)
        return false;
      int num = this.GanttViewElement.Columns.IndexOf(this.GanttViewElement.CurrentColumn);
      if (num > 0)
      {
        this.GanttViewElement.CurrentColumn = this.GanttViewElement.Columns[num - 1];
        return false;
      }
      if (this.GanttViewElement.SelectedItem.Items.Count > 0 && this.GanttViewElement.SelectedItem.Expanded)
      {
        this.GanttViewElement.SelectedItem.Expanded = false;
        return false;
      }
      if (this.GanttViewElement.SelectedItem != this.GanttViewElement.Items[0] && this.GanttViewElement.Columns.Count > 0)
        this.GanttViewElement.CurrentColumn = this.GanttViewElement.Columns[this.GanttViewElement.Columns.Count - 1];
      return this.ProcessUpKey(e);
    }

    public virtual bool ProcessRightKey(KeyEventArgs e)
    {
      if (this.GanttViewElement.SelectedItem == null)
        return false;
      int num = this.GanttViewElement.Columns.IndexOf(this.GanttViewElement.CurrentColumn);
      if (num < this.GanttViewElement.Columns.Count - 1)
      {
        this.GanttViewElement.CurrentColumn = this.GanttViewElement.Columns[num + 1];
        return false;
      }
      if (this.GanttViewElement.SelectedItem.Items.Count > 0 && !this.GanttViewElement.SelectedItem.Expanded)
      {
        this.GanttViewElement.SelectedItem.Expanded = true;
        return false;
      }
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      if (ganttViewTraverser.MoveToEnd() && this.GanttViewElement.SelectedItem != ganttViewTraverser.Current && this.GanttViewElement.Columns.Count > 0)
        this.GanttViewElement.CurrentColumn = this.GanttViewElement.Columns[0];
      return this.ProcessDownKey(e);
    }

    public virtual bool ProcessUpKey(KeyEventArgs e)
    {
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      if (this.GanttViewElement.SelectedItem == null)
      {
        if (ganttViewTraverser.MoveNext())
          this.GanttViewElement.ProcessSelection(ganttViewTraverser.Current);
      }
      else
      {
        ganttViewTraverser.MoveTo(this.GanttViewElement.SelectedItem);
        if (ganttViewTraverser.MovePrevious())
          this.GanttViewElement.ProcessSelection(ganttViewTraverser.Current);
      }
      return false;
    }

    public virtual bool ProcessDownKey(KeyEventArgs e)
    {
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      if (this.GanttViewElement.SelectedItem == null)
      {
        if (ganttViewTraverser.MoveNext())
          this.GanttViewElement.ProcessSelection(ganttViewTraverser.Current);
      }
      else
      {
        ganttViewTraverser.MoveTo(this.GanttViewElement.SelectedItem);
        if (ganttViewTraverser.MoveNext())
          this.GanttViewElement.ProcessSelection(ganttViewTraverser.Current);
      }
      return false;
    }

    public virtual bool ProcessF2Key(KeyEventArgs e)
    {
      if (this.GanttViewElement.IsEditing)
        this.GanttViewElement.EndEdit();
      else
        this.GanttViewElement.BeginEdit();
      return false;
    }

    public virtual bool ProcessDeleteKey(KeyEventArgs e)
    {
      if (!this.GanttViewElement.ReadOnly && this.GanttViewElement.SelectedLink != null)
        this.GanttViewElement.Links.Remove(this.GanttViewElement.SelectedLink);
      return false;
    }

    public virtual bool ProcessContextMenu(Point location)
    {
      GanttViewVisualElement elementAtPoint = this.GanttViewElement.ElementTree.GetElementAtPoint(location) as GanttViewVisualElement;
      if (elementAtPoint == null)
        return false;
      GanttViewBaseItemElement viewBaseItemElement = elementAtPoint as GanttViewBaseItemElement ?? elementAtPoint.FindAncestor<GanttViewBaseItemElement>();
      if (viewBaseItemElement == null)
        return false;
      if (this.GanttViewElement.IsEditing)
        this.GanttViewElement.EndEdit();
      RadContextMenu menu = this.GanttViewElement.ContextMenu;
      GanttViewDataItem data = viewBaseItemElement.Data;
      if (data != null && data.ContextMenu != null)
        menu = data.ContextMenu;
      if (menu == null && data != null && this.GanttViewElement.AllowDefaultContextMenu)
      {
        if (this.defaultMenu != null)
          this.defaultMenu.Dispose();
        this.defaultMenu = new GanttViewDefaultContextMenu(this.GanttViewElement);
        this.defaultMenu.AddMenuItem.Text = LocalizationProvider<GanttViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuAdd");
        this.defaultMenu.AddChildMenuItem.Text = LocalizationProvider<GanttViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuAddChild");
        this.defaultMenu.AddSiblingMenuItem.Text = LocalizationProvider<GanttViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuAddSibling");
        this.defaultMenu.DeleteMenuItem.Text = LocalizationProvider<GanttViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuDelete");
        this.defaultMenu.ProgressMenuItem.Text = LocalizationProvider<GanttViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuProgress");
        this.defaultMenu.AddMenuItem.Name = "Add";
        this.defaultMenu.AddChildMenuItem.Name = "AddChild";
        this.defaultMenu.AddSiblingMenuItem.Name = "AddSibling";
        this.defaultMenu.DeleteMenuItem.Name = "Delete";
        this.defaultMenu.ProgressMenuItem.Name = "Progress";
        this.defaultMenu.AddMenuItem.Enabled = !this.GanttViewElement.ReadOnly;
        this.defaultMenu.ProgressMenuItem.Enabled = !data.ReadOnly && !this.GanttViewElement.ReadOnly;
        this.defaultMenu.DeleteMenuItem.Enabled = !this.GanttViewElement.ReadOnly;
        menu = (RadContextMenu) this.defaultMenu;
      }
      if (menu != null)
      {
        if (data != null)
          this.GanttViewElement.SelectedItem = data;
        GanttViewContextMenuOpeningEventArgs e = new GanttViewContextMenuOpeningEventArgs(data, menu);
        this.GanttViewElement.OnContextMenuOpening(e);
        if (!e.Cancel)
        {
          RadControl control = this.GanttViewElement.ElementTree.Control as RadControl;
          if (control != null)
            menu.ThemeName = control.ThemeName;
          menu.Show(this.GanttViewElement.ElementTree.Control, location);
          return true;
        }
      }
      return false;
    }

    private void clickTimer_Tick(object sender, EventArgs e)
    {
      this.clickTimer.Stop();
      this.GanttViewElement.BeginEdit();
    }

    public virtual void StopScrollTimers()
    {
      if (this.HorizontalScrollTimer.Enabled)
        this.HorizontalScrollTimer.Stop();
      if (!this.VerticalScrollTimer.Enabled)
        return;
      this.VerticalScrollTimer.Stop();
    }

    protected internal virtual void ProcessScrolling(Point mousePosition, bool scrollVertical)
    {
      Point point = this.GanttViewElement.GraphicalViewElement.PointFromScreen(mousePosition);
      if (point.X > this.GanttViewElement.GraphicalViewElement.Size.Width)
      {
        this.HorizontalScrollStep = point.X - this.GanttViewElement.GraphicalViewElement.Size.Width + 1;
        if (!this.HorizontalScrollTimer.Enabled)
          this.HorizontalScrollTimer.Start();
      }
      else if (point.X < 0)
      {
        this.HorizontalScrollStep = point.X + 1;
        if (!this.HorizontalScrollTimer.Enabled)
          this.HorizontalScrollTimer.Start();
      }
      else
        this.HorizontalScrollTimer.Stop();
      if (!scrollVertical)
      {
        if (!this.VerticalScrollTimer.Enabled)
          return;
        this.VerticalScrollTimer.Stop();
      }
      else if (point.Y < 0)
      {
        this.VerticalScrollStep = point.Y;
        if (this.VerticalScrollTimer.Enabled)
          return;
        this.VerticalScrollTimer.Start();
      }
      else if (point.Y > this.GanttViewElement.GraphicalViewElement.Size.Height)
      {
        this.VerticalScrollStep = point.Y - this.GanttViewElement.GraphicalViewElement.Size.Height + 1;
        if (this.VerticalScrollTimer.Enabled)
          return;
        this.VerticalScrollTimer.Start();
      }
      else
        this.VerticalScrollTimer.Stop();
    }

    protected virtual void OnHorizontalScrollTimerTick()
    {
      int num = this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value + this.HorizontalScrollStep;
      if (this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Maximum - this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.LargeChange + 1 <= num && this.horizontalScrollStep > 0)
      {
        this.GanttViewElement.GraphicalViewElement.TimelineEnd = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineEnd.AddDays(1.0);
        this.GanttViewElement.GraphicalViewElement.InvalidateMeasure(true);
      }
      else if (this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Minimum >= num && this.horizontalScrollStep < 0)
      {
        this.GanttViewElement.GraphicalViewElement.TimelineStart = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart.AddDays(-1.0);
        this.GanttViewElement.GraphicalViewElement.InvalidateMeasure(true);
      }
      else
        this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value = num;
    }

    protected virtual void OnVerticalScrollTimerTick()
    {
      RadScrollBarElement vscrollBar = this.GanttViewElement.GraphicalViewElement.VScrollBar;
      int num1 = vscrollBar.Value * this.GanttViewElement.ItemHeight + this.VerticalScrollStep;
      if ((vscrollBar.Maximum - vscrollBar.LargeChange + 1) * this.GanttViewElement.ItemHeight < num1)
        vscrollBar.Value = vscrollBar.Maximum - vscrollBar.LargeChange + 1;
      else if (vscrollBar.Minimum > num1)
      {
        vscrollBar.Value = vscrollBar.Minimum;
      }
      else
      {
        int num2 = this.VerticalScrollStep > 0 ? 1 : -1;
        int num3 = num1 / this.GanttViewElement.ItemHeight + num2;
        if (num3 <= vscrollBar.Minimum || num3 > vscrollBar.Maximum - vscrollBar.LargeChange + 1)
          return;
        vscrollBar.Value = num3;
      }
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      if (sender == this.HorizontalScrollTimer)
        this.OnHorizontalScrollTimerTick();
      else
        this.OnVerticalScrollTimerTick();
    }
  }
}
