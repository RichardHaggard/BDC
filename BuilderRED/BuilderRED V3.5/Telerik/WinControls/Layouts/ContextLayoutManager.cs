// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.ContextLayoutManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.Layouts
{
  public sealed class ContextLayoutManager : ILayoutManager, IDisposable
  {
    private static LayoutCallback _updateCallback = new LayoutCallback(ContextLayoutManager.UpdateLayoutCallback);
    private static LayoutCallback _updateLayoutBackground = new LayoutCallback(ContextLayoutManager.UpdateLayoutBackground);
    private const int s_LayoutRecursionLimit = 250;
    private ILayoutHandler layoutHandler;
    private ContextLayoutManager.InternalArrangeQueue _arrangeQueue;
    private int _arrangesOnStack;
    private bool _firePostLayoutEvents;
    private RadElement _forceLayoutElement;
    private bool _gotException;
    private bool _inFireLayoutUpdated;
    private bool _inFireSizeChanged;
    private bool _isDead;
    private bool _isInUpdateLayout;
    private bool _isUpdating;
    private List<EventHandler> _layoutEvents;
    private bool _layoutRequestPosted;
    private ContextLayoutManager.InternalMeasureQueue _measureQueue;
    private int _measuresOnStack;
    private SizeChangedInfo _sizeChangedChain;
    private int layoutCalls;

    public ContextLayoutManager(ILayoutHandler layoutHandler)
    {
      this.layoutHandler = layoutHandler;
    }

    public void Dispose()
    {
      if (this._layoutEvents != null)
        this._layoutEvents.Clear();
      this.layoutHandler = (ILayoutHandler) null;
      this._layoutEvents = (List<EventHandler>) null;
      this._measureQueue = (ContextLayoutManager.InternalMeasureQueue) null;
      this._arrangeQueue = (ContextLayoutManager.InternalArrangeQueue) null;
      this._sizeChangedChain = (SizeChangedInfo) null;
      this._forceLayoutElement = (RadElement) null;
      this._isDead = true;
    }

    internal void ClearSizeChangedChain()
    {
      this._sizeChangedChain = (SizeChangedInfo) null;
    }

    private void InvokeAsyncCallback(LayoutCallback callback)
    {
      if (this.layoutHandler == null)
        return;
      this.layoutHandler.InvokeLayoutCallback(callback);
    }

    public void InvokeUpdateLayoutAsync()
    {
      this.InvokeAsyncCallback(ContextLayoutManager._updateCallback);
    }

    private void setForceLayout(RadElement e)
    {
      this._forceLayoutElement = e;
    }

    public void RemoveElementFromQueues(RadElement e)
    {
      this.ArrangeQueueInternal.Remove(e);
      this.MeasureQueueInternal.Remove(e);
      if (!object.ReferenceEquals((object) e, (object) this._forceLayoutElement))
        return;
      this._forceLayoutElement = (RadElement) null;
    }

    private void NeedsRecalc()
    {
      if (this._isDead || this._layoutRequestPosted || this._isUpdating)
        return;
      this.InvokeAsyncCallback(ContextLayoutManager._updateCallback);
      this._layoutRequestPosted = true;
    }

    private static void UpdateLayoutBackground(ILayoutManager manager)
    {
      (manager as ContextLayoutManager)?.NeedsRecalc();
    }

    private static void UpdateLayoutCallback(ILayoutManager manager)
    {
      ContextLayoutManager contextLayoutManager = manager as ContextLayoutManager;
      if (contextLayoutManager == null || contextLayoutManager._isDead)
        return;
      contextLayoutManager.UpdateLayout();
    }

    public void AddToSizeChangedChain(SizeChangedInfo info)
    {
      info.Next = this._sizeChangedChain;
      this._sizeChangedChain = info;
    }

    public void EnterArrange()
    {
      ++this._arrangesOnStack;
      if (this._arrangesOnStack > 250)
        throw new InvalidOperationException(string.Format("Exceeded arrange recursion limit ({0})", (object) 250));
      this._firePostLayoutEvents = true;
    }

    public void EnterMeasure()
    {
      ++this._measuresOnStack;
      if (this._measuresOnStack > 250)
        throw new InvalidOperationException(string.Format("Exceeded measure recursion limit ({0})", (object) 250));
      this._firePostLayoutEvents = true;
    }

    public void ExitArrange()
    {
      --this._arrangesOnStack;
    }

    public void ExitMeasure()
    {
      --this._measuresOnStack;
    }

    private void fireLayoutUpdateEvent()
    {
      if (this._inFireLayoutUpdated)
        return;
      try
      {
        this._inFireLayoutUpdated = true;
        this._firePostLayoutEvents = false;
        EventHandler[] array = new EventHandler[this.LayoutEvents.Count];
        this.LayoutEvents.CopyTo(array);
        for (int index = 0; index < array.Length; ++index)
        {
          EventHandler eventHandler = array[index];
          if (eventHandler != null)
          {
            eventHandler((object) null, EventArgs.Empty);
            if (this.hasDirtiness)
              break;
          }
        }
      }
      finally
      {
        this._inFireLayoutUpdated = false;
      }
    }

    private void fireSizeChangedEvents()
    {
      if (this._inFireSizeChanged)
        return;
      try
      {
        this._inFireSizeChanged = true;
        while (this._sizeChangedChain != null)
        {
          SizeChangedInfo sizeChangedChain = this._sizeChangedChain;
          this._sizeChangedChain = sizeChangedChain.Next;
          sizeChangedChain.Element.sizeChangedInfo = (SizeChangedInfo) null;
          sizeChangedChain.Element.CallOnRenderSizeChanged(sizeChangedChain);
          if (this.hasDirtiness)
            break;
        }
      }
      finally
      {
        this._inFireSizeChanged = false;
      }
    }

    private void invalidateTreeIfRecovering()
    {
      if (this._forceLayoutElement == null && !this._gotException)
        return;
      if (this._forceLayoutElement != null)
        this.markTreeDirty(this._forceLayoutElement);
      this._forceLayoutElement = (RadElement) null;
      this._gotException = false;
    }

    private void markTreeDirty(RadElement e)
    {
      while (true)
      {
        RadElement parent = e.Parent;
        if (parent != null)
          e = parent;
        else
          break;
      }
      this.markTreeDirtyHelper(e);
      this.MeasureQueueInternal.Add(e);
      this.ArrangeQueueInternal.Add(e);
    }

    private void markTreeDirtyHelper(RadElement v)
    {
      if (v == null)
        return;
      v.MeasureIsDirty = true;
      v.ArrangeIsDirty = true;
      int count = v.Children.Count;
      for (int index = 0; index < count; ++index)
      {
        RadElement child = v.Children[index];
        if (child != null)
          this.markTreeDirtyHelper(child);
      }
    }

    public void UpdateLayout()
    {
      if (this._isInUpdateLayout || this._measuresOnStack > 0 || (this._arrangesOnStack > 0 || this._isDead))
        return;
      int num1 = 0;
      bool flag = true;
      RadElement radElement = (RadElement) null;
      try
      {
        this.invalidateTreeIfRecovering();
        while (this.hasDirtiness || this._firePostLayoutEvents)
        {
          if (++num1 > 153)
          {
            this.InvokeAsyncCallback(ContextLayoutManager._updateLayoutBackground);
            radElement = (RadElement) null;
            flag = false;
            return;
          }
          this._isUpdating = true;
          this._isInUpdateLayout = true;
          int num2 = 0;
          DateTime dateTime = new DateTime(0L);
          do
          {
            if (++num2 > 153)
            {
              num2 = 0;
              if (dateTime.Ticks == 0L)
                dateTime = DateTime.Now;
              else if ((DateTime.Now - dateTime).Milliseconds > 306)
              {
                this.InvokeAsyncCallback(ContextLayoutManager._updateLayoutBackground);
                radElement = (RadElement) null;
                flag = false;
                return;
              }
            }
            radElement = this.MeasureQueueInternal.GetTopMost();
            radElement?.Measure(radElement.PreviousConstraint);
          }
          while (radElement != null);
          int num3 = 0;
          dateTime = new DateTime(0L);
          while (this.MeasureQueueInternal.IsEmpty)
          {
            if (++num3 > 153)
            {
              num3 = 0;
              if (dateTime.Ticks == 0L)
                dateTime = DateTime.Now;
              else if ((DateTime.Now - dateTime).Milliseconds > 306)
              {
                this.InvokeAsyncCallback(ContextLayoutManager._updateLayoutBackground);
                radElement = (RadElement) null;
                flag = false;
                return;
              }
            }
            radElement = this.ArrangeQueueInternal.GetTopMost();
            if (radElement != null)
            {
              RectangleF arrangeRect = radElement.GetArrangeRect(radElement.PreviousArrangeRect);
              radElement.Arrange(arrangeRect);
            }
            else
              break;
          }
          if (this.MeasureQueueInternal.IsEmpty)
          {
            this._isInUpdateLayout = false;
            this.fireSizeChangedEvents();
            if (!this.hasDirtiness)
              this.fireLayoutUpdateEvent();
          }
        }
        radElement = (RadElement) null;
        flag = false;
      }
      finally
      {
        this._isUpdating = false;
        this._layoutRequestPosted = false;
        this._isInUpdateLayout = false;
        if (flag)
        {
          this._gotException = true;
          this._forceLayoutElement = radElement;
          this.InvokeAsyncCallback(ContextLayoutManager._updateLayoutBackground);
        }
      }
    }

    private ContextLayoutManager.LayoutQueue ArrangeQueueInternal
    {
      get
      {
        if (this._arrangeQueue == null)
          this._arrangeQueue = new ContextLayoutManager.InternalArrangeQueue();
        return (ContextLayoutManager.LayoutQueue) this._arrangeQueue;
      }
    }

    public ILayoutQueue ArrangeQueue
    {
      get
      {
        return (ILayoutQueue) this.ArrangeQueueInternal;
      }
    }

    private bool hasDirtiness
    {
      get
      {
        if (this.MeasureQueueInternal.IsEmpty)
          return !this.ArrangeQueueInternal.IsEmpty;
        return true;
      }
    }

    public List<EventHandler> LayoutEvents
    {
      get
      {
        if (this._layoutEvents == null)
          this._layoutEvents = new List<EventHandler>();
        return this._layoutEvents;
      }
    }

    private ContextLayoutManager.LayoutQueue MeasureQueueInternal
    {
      get
      {
        if (this._measureQueue == null)
          this._measureQueue = new ContextLayoutManager.InternalMeasureQueue();
        return (ContextLayoutManager.LayoutQueue) this._measureQueue;
      }
    }

    public ILayoutQueue MeasureQueue
    {
      get
      {
        return (ILayoutQueue) this.MeasureQueueInternal;
      }
    }

    public bool IsUpdating
    {
      get
      {
        if (!this._isUpdating)
          return this.layoutCalls > 0;
        return true;
      }
    }

    public void IncrementLayoutCalls()
    {
      ++this.layoutCalls;
    }

    public void DecrementLayoutCalls()
    {
      if (this.layoutCalls <= 0)
        return;
      --this.layoutCalls;
    }

    internal abstract class LayoutQueue : ILayoutQueue
    {
      internal const int PocketCapacity = 153;
      private const int PocketReserve = 8;
      private ContextLayoutManager.LayoutQueue.Request _head;
      private ContextLayoutManager.LayoutQueue.Request _pocket;
      private int _pocketSize;

      internal LayoutQueue()
      {
        for (int index = 0; index < 153; ++index)
          this._pocket = new ContextLayoutManager.LayoutQueue.Request()
          {
            Next = this._pocket
          };
        this._pocketSize = 153;
      }

      private void _addRequest(RadElement e)
      {
        ContextLayoutManager.LayoutQueue.Request newRequest = this._getNewRequest(e);
        if (newRequest == null)
          return;
        newRequest.Next = this._head;
        if (this._head != null)
          this._head.Prev = newRequest;
        this._head = newRequest;
        this.setRequest(e, newRequest);
      }

      private ContextLayoutManager.LayoutQueue.Request _getNewRequest(
        RadElement e)
      {
        ContextLayoutManager.LayoutQueue.Request request1;
        if (this._pocket != null)
        {
          request1 = this._pocket;
          this._pocket = request1.Next;
          --this._pocketSize;
          ContextLayoutManager.LayoutQueue.Request request2;
          request1.Prev = request2 = (ContextLayoutManager.LayoutQueue.Request) null;
          request1.Next = request2;
        }
        else
        {
          ContextLayoutManager layoutManager = e.LayoutManager as ContextLayoutManager;
          try
          {
            request1 = new ContextLayoutManager.LayoutQueue.Request();
          }
          catch (OutOfMemoryException ex)
          {
            layoutManager?.setForceLayout(e);
            throw ex;
          }
        }
        request1.Target = e;
        return request1;
      }

      private void _removeRequest(ContextLayoutManager.LayoutQueue.Request entry)
      {
        if (entry.Prev == null)
          this._head = entry.Next;
        else
          entry.Prev.Next = entry.Next;
        if (entry.Next != null)
          entry.Next.Prev = entry.Prev;
        this.ReuseRequest(entry);
      }

      public void Add(RadElement e)
      {
        if (this.getRequest(e) != null || e.IsLayoutSuspended)
          return;
        this.RemoveOrphans(e);
        RadElement parent1 = e.Parent;
        if (parent1 != null && this.canRelyOnParentRecalc(parent1))
          return;
        ContextLayoutManager layoutManager = e.LayoutManager as ContextLayoutManager;
        if (layoutManager._isDead)
          return;
        RadElement parent2;
        if (this._pocketSize <= 8)
        {
          for (; e != null; e = parent2)
          {
            parent2 = e.Parent;
            this.invalidate(e);
            if (parent2 != null)
              this.Remove(e);
            else if (this.getRequest(e) == null)
            {
              this.RemoveOrphans(e);
              this._addRequest(e);
            }
          }
        }
        else
          this._addRequest(e);
        layoutManager.NeedsRecalc();
      }

      internal abstract bool canRelyOnParentRecalc(RadElement parent);

      internal abstract ContextLayoutManager.LayoutQueue.Request getRequest(
        RadElement e);

      internal RadElement GetTopMost()
      {
        RadElement radElement = (RadElement) null;
        ulong num = ulong.MaxValue;
        for (ContextLayoutManager.LayoutQueue.Request request = this._head; request != null; request = request.Next)
        {
          ulong treeLevel = (ulong) request.Target.TreeLevel;
          if (treeLevel < num)
          {
            num = treeLevel;
            radElement = request.Target;
          }
        }
        return radElement;
      }

      internal abstract void invalidate(RadElement e);

      public void Remove(RadElement e)
      {
        ContextLayoutManager.LayoutQueue.Request request = this.getRequest(e);
        if (request == null)
          return;
        this._removeRequest(request);
        this.setRequest(e, (ContextLayoutManager.LayoutQueue.Request) null);
      }

      internal void RemoveOrphans(RadElement parent)
      {
        ContextLayoutManager.LayoutQueue.Request next;
        for (ContextLayoutManager.LayoutQueue.Request request = this._head; request != null; request = next)
        {
          RadElement target = request.Target;
          next = request.Next;
          ulong treeLevel = (ulong) parent.TreeLevel;
          if ((long) target.TreeLevel == (long) treeLevel + 1L && target.Parent == parent)
          {
            this._removeRequest(this.getRequest(target));
            this.setRequest(target, (ContextLayoutManager.LayoutQueue.Request) null);
          }
        }
      }

      private void ReuseRequest(ContextLayoutManager.LayoutQueue.Request r)
      {
        r.Target = (RadElement) null;
        if (this._pocketSize >= 153)
          return;
        r.Next = this._pocket;
        this._pocket = r;
        ++this._pocketSize;
      }

      internal abstract void setRequest(RadElement e, ContextLayoutManager.LayoutQueue.Request r);

      internal bool IsEmpty
      {
        get
        {
          return this._head == null;
        }
      }

      public int Count
      {
        get
        {
          int num = 0;
          for (ContextLayoutManager.LayoutQueue.Request request = this._head; request != null; request = request.Next)
            ++num;
          return num;
        }
      }

      internal class Request
      {
        internal ContextLayoutManager.LayoutQueue.Request Next;
        internal ContextLayoutManager.LayoutQueue.Request Prev;
        internal RadElement Target;
      }
    }

    internal class InternalArrangeQueue : ContextLayoutManager.LayoutQueue
    {
      internal override bool canRelyOnParentRecalc(RadElement parent)
      {
        if (!parent.IsArrangeValid)
          return !parent.ArrangeInProgress;
        return false;
      }

      internal override ContextLayoutManager.LayoutQueue.Request getRequest(
        RadElement e)
      {
        return e.ArrangeRequest;
      }

      internal override void invalidate(RadElement e)
      {
        e.ArrangeIsDirty = true;
      }

      internal override void setRequest(RadElement e, ContextLayoutManager.LayoutQueue.Request r)
      {
        e.ArrangeRequest = r;
      }
    }

    internal class InternalMeasureQueue : ContextLayoutManager.LayoutQueue
    {
      internal override bool canRelyOnParentRecalc(RadElement parent)
      {
        if (!parent.IsMeasureValid)
          return !parent.MeasureInProgress;
        return false;
      }

      internal override ContextLayoutManager.LayoutQueue.Request getRequest(
        RadElement e)
      {
        return e.MeasureRequest;
      }

      internal override void invalidate(RadElement e)
      {
        e.MeasureIsDirty = true;
      }

      internal override void setRequest(RadElement e, ContextLayoutManager.LayoutQueue.Request r)
      {
        e.MeasureRequest = r;
      }
    }
  }
}
