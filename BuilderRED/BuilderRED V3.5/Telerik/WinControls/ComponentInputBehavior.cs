// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ComponentInputBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls
{
  public class ComponentInputBehavior : ComponentBehavior
  {
    private bool enable = true;
    private List<RadElement> hoveredElements = new List<RadElement>();
    private ArrayList elementsUnderMouseToMonitor = new ArrayList();
    private bool disableMouseEvents;
    private MouseHoverTimer mouseHoverTimer;
    private Timer elementUnderMouseMonitorTimer;
    private RadItem gesturedItem;

    public ComponentInputBehavior(IComponentTreeHandler owner)
      : base(owner)
    {
      this.ShowItemToolTips = this.DefaultShowItemToolTips;
    }

    public virtual void OnMouseCaptureChanged(EventArgs e)
    {
      if (!this.enable)
        return;
      if (this.ItemCapture != null)
      {
        RadItem itemCapture = this.ItemCapture as RadItem;
        if (itemCapture != null)
        {
          Point position = Cursor.Position;
          itemCapture.CallOnLostMouseCapture(new MouseEventArgs(MouseButtons.None, 1, position.X, position.Y, 0));
        }
        this.ItemCapture = (RadElement) null;
      }
      this.Owner.CallOnMouseCaptureChanged(e);
    }

    public virtual bool OnMouseUp(MouseEventArgs e)
    {
      if (!this.enable || this.DisableMouseEvents)
        return false;
      if (this.OwnerControl.Capture && this.ItemCapture != null)
        this.ItemCapture.CallDoMouseUp(e);
      else if (this.selectedElement != null)
        this.selectedElement.CallDoMouseUp(e);
      else if (this.Owner != null)
      {
        ComponentThemableElementTree elementTree = this.Owner.ElementTree;
        elementTree.GetElementAtPoint((RadElement) elementTree.RootElement, e.Location, (List<RadElement>) null)?.CallDoMouseUp(e);
      }
      return false;
    }

    public virtual bool OnMouseDown(MouseEventArgs e)
    {
      if (!this.enable)
        return false;
      if (!this.DisableMouseEvents)
      {
        if (this.OwnerControl.Capture && this.ItemCapture != null)
        {
          this.ItemCapture.CallDoMouseDown(e);
          this.pressedElement = this.itemCapture;
        }
        else
        {
          this.SelectElementOnMouseOver(e);
          this.pressedElement = this.selectedElement;
          if (this.selectedElement != null)
            this.selectedElement.CallDoMouseDown(e);
        }
      }
      if (this.IsKeyMapActive)
        this.ResetKeyMap();
      return false;
    }

    public virtual bool OnDoubleClick(EventArgs e)
    {
      return false;
    }

    public virtual bool OnClick(EventArgs e)
    {
      return false;
    }

    public virtual bool OnMouseEnter(EventArgs e)
    {
      if (!this.enable || this.DisableMouseEvents)
        return false;
      this.MouseOver = true;
      this.Owner.ElementTree.RootElement.IsMouseOver = true;
      this.Owner.ElementTree.RootElement.IsMouseOverElement = true;
      return false;
    }

    public virtual bool OnMouseLeave(EventArgs e)
    {
      if (!this.enable)
        return false;
      this.MouseOver = false;
      if (!this.DisableMouseEvents)
      {
        if (this.selectedElement != null)
          this.HandleMouseLeave(e);
        this.Owner.ElementTree.RootElement.IsMouseOver = false;
        this.Owner.ElementTree.RootElement.IsMouseOverElement = false;
      }
      return false;
    }

    public virtual bool OnMouseMove(MouseEventArgs e)
    {
      if (!this.enable || this.DisableMouseEvents)
        return false;
      this.MouseOver = true;
      if (this.OwnerControl.Capture && this.ItemCapture != null)
      {
        this.ItemCapture.CallDoMouseMove(e);
        if (this.ItemCapture != null)
        {
          if (!this.ItemCaptureState && this.ItemCapture.HitTest(e.Location))
          {
            this.ItemCapture.CallDoMouseEnter((EventArgs) e);
            this.ItemCaptureState = true;
          }
          else if (this.ItemCaptureState && !this.ItemCapture.HitTest(e.Location))
          {
            this.ItemCapture.CallDoMouseLeave((EventArgs) e);
            this.ItemCaptureState = false;
          }
        }
      }
      else
        this.SelectElementOnMouseOver(e);
      return false;
    }

    public virtual bool OnMouseHover(EventArgs e)
    {
      if (!this.enable || this.DisableMouseEvents || (!this.OwnerControl.Capture || this.ItemCapture == null))
        return false;
      this.ItemCapture.CallDoMouseHover(e);
      return false;
    }

    public virtual bool OnMouseWheel(MouseEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null)
        return false;
      this.currentFocusedElement.CallRaiseMouseWheel(e);
      return false;
    }

    protected void HandleMouseLeave(EventArgs e)
    {
      this.HandleMouseLeave(e, (RadElement) null);
    }

    protected void HandleMouseLeave(EventArgs e, RadElement elementUnderMouse)
    {
      if (!this.enable)
        return;
      if (!this.Owner.IsDesignMode)
        this.MouseHoverTimer.Cancel(this.selectedElement);
      try
      {
        if (this.hoveredElements != null)
        {
          foreach (RadElement hoveredElement in this.hoveredElements)
          {
            hoveredElement.IsMouseOverElement = false;
            if (!this.MouseOver)
            {
              bool isMouseOver = hoveredElement.IsMouseOver;
              hoveredElement.IsMouseOver = false;
              if (!isMouseOver)
                hoveredElement.UpdateContainsMouse();
              else if (hoveredElement == this.selectedElement)
                this.selectedElement.CallDoMouseLeave(EventArgs.Empty);
            }
          }
          this.hoveredElements.Clear();
          this.hoveredElements = (List<RadElement>) null;
        }
        this.CheckAddParentElementsUnderMouseToMonitor(this.selectedElement);
        if (elementUnderMouse != null && elementUnderMouse.NotifyParentOnMouseInput)
        {
          bool flag = false;
          for (RadElement parent = elementUnderMouse.Parent; parent != null; parent = parent.Parent)
          {
            if (parent == this.selectedElement)
            {
              flag = true;
              this.AddElementsUnderMouseToMonitor(this.selectedElement);
              break;
            }
          }
          if (flag)
            return;
          this.selectedElement.CallDoMouseLeave(e);
        }
        else
        {
          if (!this.selectedElement.IsMouseOver && this.selectedElement.Enabled)
            return;
          this.selectedElement.CallDoMouseLeave(e);
        }
      }
      finally
      {
        this.selectedElement = (RadElement) null;
        this.pressedElement = (RadElement) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool OnKeyDown(KeyEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null || !(this.currentFocusedElement is RadItem))
        return false;
      ((RadItem) this.currentFocusedElement).CallRaiseKeyDown(e);
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void OnKeyDown(RadElement routedSender, KeyEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null || (routedSender == this.currentFocusedElement || !(this.currentFocusedElement is RadItem)))
        return;
      ((RadItem) this.currentFocusedElement).CallRaiseKeyDown(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool OnKeyPress(KeyPressEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null || !(this.currentFocusedElement is RadItem))
        return false;
      ((RadItem) this.currentFocusedElement).CallRaiseKeyPress(e);
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void OnKeyPress(RadElement routedSender, KeyPressEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null || (routedSender == this.currentFocusedElement || !(this.currentFocusedElement is RadItem)))
        return;
      ((RadItem) this.currentFocusedElement).CallRaiseKeyPress(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool OnKeyUp(KeyEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null || !(this.currentFocusedElement is RadItem))
        return false;
      ((RadItem) this.currentFocusedElement).CallRaiseKeyUp(e);
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void OnKeyUp(RadElement routedSender, KeyEventArgs e)
    {
      if (!this.enable || this.currentFocusedElement == null || (routedSender == this.currentFocusedElement || !(this.currentFocusedElement is RadItem)))
        return;
      ((RadItem) this.currentFocusedElement).CallRaiseKeyUp(e);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Enable
    {
      get
      {
        return this.enable;
      }
      set
      {
        this.enable = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool DisableMouseEvents
    {
      get
      {
        return this.disableMouseEvents;
      }
      set
      {
        this.disableMouseEvents = value;
      }
    }

    protected MouseHoverTimer MouseHoverTimer
    {
      get
      {
        if (this.mouseHoverTimer == null)
          this.mouseHoverTimer = new MouseHoverTimer();
        return this.mouseHoverTimer;
      }
    }

    protected internal virtual void OnGesture(GestureEventArgs e)
    {
      if (e.IsBegin)
      {
        this.gesturedItem = this.FindGesturedItem(e, (RadElement) this.Owner.RootElement);
      }
      else
      {
        if (this.gesturedItem != null)
          this.gesturedItem.CallOnGesture(e);
        if (!e.IsEnd)
          return;
        this.gesturedItem = (RadItem) null;
      }
    }

    private RadItem FindGesturedItem(GestureEventArgs args, RadElement current)
    {
      foreach (RadElement child in current.GetChildren(ChildrenListOptions.ZOrdered | ChildrenListOptions.ReverseOrder))
      {
        if (child.ControlBoundingRectangle.Contains(args.Location))
        {
          RadItem radItem = child as RadItem;
          if (radItem != null)
          {
            radItem.CallOnGesture(args);
            if (args.Handled)
              return radItem;
          }
          RadItem gesturedItem = this.FindGesturedItem(args, child);
          if (gesturedItem != null)
            return gesturedItem;
        }
      }
      return (RadItem) null;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.mouseHoverTimer != null)
        {
          this.mouseHoverTimer.Cancel();
          this.mouseHoverTimer.Dispose();
        }
        if (this.elementUnderMouseMonitorTimer != null)
        {
          this.elementUnderMouseMonitorTimer.Stop();
          this.elementUnderMouseMonitorTimer.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    private void AddElementsUnderMouseToMonitor(RadElement element)
    {
      this.elementsUnderMouseToMonitor.Add((object) element);
    }

    private void RemoveElementsUnderMouseToMonitor(RadElement element)
    {
      this.elementsUnderMouseToMonitor.Remove((object) element);
    }

    private void EnsureElementUnderMouseMonitorTimer()
    {
      if (this.elementUnderMouseMonitorTimer != null)
        return;
      this.elementUnderMouseMonitorTimer = new Timer();
      this.elementUnderMouseMonitorTimer.Interval = 50;
      this.elementUnderMouseMonitorTimer.Tick += new EventHandler(this.ElementUnderMouseMonitorTimer_Tick);
      this.elementUnderMouseMonitorTimer.Start();
    }

    private void ElementUnderMouseMonitorTimer_Tick(object sender, EventArgs e)
    {
      foreach (RadElement radElement in this.elementsUnderMouseToMonitor)
      {
        if (radElement.IsInValidState(true) && radElement.ElementTree.Control == this.Owner && radElement.IsMouseOver)
        {
          radElement.CallDoMouseLeave(EventArgs.Empty);
          radElement.IsMouseOverElement = false;
        }
      }
      this.elementsUnderMouseToMonitor.Clear();
    }

    private void CheckRemoveParentElementsUnderMouseToMonitor(RadElement elementUnderMouse)
    {
      if (!elementUnderMouse.NotifyParentOnMouseInput)
        return;
      for (RadElement parent = elementUnderMouse.Parent; parent != null; parent = parent.Parent)
      {
        if (parent.ShouldHandleMouseInput)
        {
          if (!parent.IsMouseOver)
            parent.CallDoMouseEnter(EventArgs.Empty);
          this.RemoveElementsUnderMouseToMonitor(parent);
          if (!parent.NotifyParentOnMouseInput)
            break;
        }
      }
    }

    private void CheckAddParentElementsUnderMouseToMonitor(RadElement elementUnderMouse)
    {
      if (!elementUnderMouse.NotifyParentOnMouseInput)
        return;
      for (RadElement parent = elementUnderMouse.Parent; parent != null; parent = parent.Parent)
      {
        if (parent.ShouldHandleMouseInput)
        {
          this.AddElementsUnderMouseToMonitor(parent);
          if (!parent.NotifyParentOnMouseInput)
            break;
        }
      }
    }

    private RadElement GetTopmostHoveredElementNoBoxElements(
      List<RadElement> hoveredElementList)
    {
      if (hoveredElementList == null)
        return (RadElement) null;
      RadElement radElement = (RadElement) null;
      for (int index = hoveredElementList.Count - 1; index >= 0; --index)
      {
        radElement = hoveredElementList[index];
        if (!(radElement is IBoxElement))
          break;
      }
      return radElement;
    }

    public RadElement GetHoveredRadElement()
    {
      return this.GetTopmostHoveredElementNoBoxElements(this.hoveredElements);
    }

    private void HandleHoveredElementsChanged(List<RadElement> newHoveredElements)
    {
      if (this.hoveredElements == null)
        this.hoveredElements = new List<RadElement>();
      if (newHoveredElements == null)
        newHoveredElements = new List<RadElement>();
      foreach (RadElement newHoveredElement in newHoveredElements)
      {
        bool flag = false;
        foreach (RadElement hoveredElement in this.hoveredElements)
        {
          if (newHoveredElement == hoveredElement)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          newHoveredElement.IsMouseOverElement = true;
      }
      foreach (RadElement hoveredElement in this.hoveredElements)
      {
        bool flag = false;
        foreach (RadElement newHoveredElement in newHoveredElements)
        {
          if (hoveredElement == newHoveredElement)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          hoveredElement.CallDoMouseLeave(EventArgs.Empty);
          hoveredElement.IsMouseOver = false;
          hoveredElement.IsMouseOverElement = false;
        }
      }
      RadElement elementNoBoxElements1 = this.GetTopmostHoveredElementNoBoxElements(this.hoveredElements);
      RadElement elementNoBoxElements2 = this.GetTopmostHoveredElementNoBoxElements(newHoveredElements);
      if (elementNoBoxElements1 != elementNoBoxElements2)
        this.OnHoveredElementChanged(new HoveredElementChangedEventArgs(elementNoBoxElements2));
      this.hoveredElements = newHoveredElements;
    }

    public event HoveredElementChangedEventHandler HoveredElementChanged;

    protected virtual void OnHoveredElementChanged(HoveredElementChangedEventArgs e)
    {
      if (this.HoveredElementChanged == null)
        return;
      this.HoveredElementChanged((object) this, e);
    }

    private void SelectElementOnMouseOver(MouseEventArgs e)
    {
      List<RadElement> radElementList = new List<RadElement>();
      RadElement elementAtPoint = this.Owner.ElementTree.GetElementAtPoint((RadElement) this.Owner.ElementTree.RootElement, e.Location, radElementList);
      this.HandleHoveredElementsChanged(radElementList);
      if (elementAtPoint != this.selectedElement)
      {
        this.EnsureElementUnderMouseMonitorTimer();
        if (this.selectedElement != null && elementAtPoint != null)
        {
          if (!elementAtPoint.ShouldHandleMouseInput || !this.selectedElement.ShouldHandleMouseInput || (!elementAtPoint.NotifyParentOnMouseInput || this.selectedElement.NotifyParentOnMouseInput))
            this.HandleMouseLeave((EventArgs) e, elementAtPoint);
          else
            this.AddElementsUnderMouseToMonitor(this.selectedElement);
        }
        else if (elementAtPoint == null)
          this.HandleMouseLeave((EventArgs) e);
        if (elementAtPoint != null)
        {
          this.RemoveElementsUnderMouseToMonitor(elementAtPoint);
          this.CheckRemoveParentElementsUnderMouseToMonitor(elementAtPoint);
          elementAtPoint.CallDoMouseEnter((EventArgs) e);
          if (!this.Owner.IsDesignMode)
            this.MouseHoverTimer.Start(elementAtPoint);
        }
      }
      this.selectedElement = elementAtPoint;
      if (this.selectedElement == null || !this.selectedElement.ShouldHandleMouseInput)
        return;
      this.selectedElement.CallDoMouseMove(e);
    }
  }
}
