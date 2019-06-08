// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSynchronizationService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Threading;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSynchronizationService : DisposableObject
  {
    private GridViewEventProcessEntity dataEntity;
    private GridViewEventProcessEntity uiEntity;
    private PriorityWeakReferenceList queueAnalyzers;
    private LinkedList<GridViewEvent> eventsQueue;
    private Dictionary<KnownEvents, int> suspendedEvents;
    private GridViewEvent currentEvent;
    private byte beginDispatchCount;
    private byte dispatchingCount;
    private SynchronizationContext syncContext;
    private byte uiEventSuspendDispatchCount;
    private byte dataEventSuspendDispatchCount;

    public GridViewSynchronizationService()
    {
      this.dataEntity = new GridViewEventProcessEntity(this);
      this.uiEntity = new GridViewEventProcessEntity(this);
      this.queueAnalyzers = new PriorityWeakReferenceList();
      this.eventsQueue = new LinkedList<GridViewEvent>();
      this.suspendedEvents = new Dictionary<KnownEvents, int>();
      this.syncContext = SynchronizationContext.Current;
    }

    protected override void DisposeManagedResources()
    {
      this.SuspendDispatch();
      this.eventsQueue.Clear();
      this.dataEntity.Clear();
      this.uiEntity.Clear();
      base.DisposeManagedResources();
    }

    public GridViewEvent DispatchingEvent
    {
      get
      {
        return this.currentEvent;
      }
    }

    public bool IsDispatching
    {
      get
      {
        return this.dispatchingCount > (byte) 0;
      }
    }

    public bool IsInBeginDispatchBlock
    {
      get
      {
        return this.beginDispatchCount > (byte) 0;
      }
    }

    public bool IsDispatchSuspended
    {
      get
      {
        if (this.IsUIEventDispatchSuspended)
          return this.IsDataEventDispatchSuspended;
        return false;
      }
    }

    public bool IsUIEventDispatchSuspended
    {
      get
      {
        return this.uiEventSuspendDispatchCount > (byte) 0;
      }
    }

    public bool IsDataEventDispatchSuspended
    {
      get
      {
        return this.dataEventSuspendDispatchCount > (byte) 0;
      }
    }

    public bool IsEventSuspended(KnownEvents eventId)
    {
      if (this.suspendedEvents.ContainsKey(eventId))
        return this.suspendedEvents[eventId] > 0;
      return false;
    }

    public bool ContainsEvent(Predicate<GridViewEvent> predicate)
    {
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      foreach (GridViewEvent events in this.eventsQueue)
      {
        if (predicate(events))
          return true;
      }
      return false;
    }

    public void DispatchEvent(GridViewEvent gridEvent)
    {
      if (!this.VerifyDispatch(gridEvent))
        return;
      gridEvent.SynchronizationService = this;
      if (gridEvent.Info.Critical)
        this.eventsQueue.AddFirst(gridEvent);
      else
        this.eventsQueue.AddLast(gridEvent);
      if (this.beginDispatchCount != (byte) 0)
        return;
      this.BeginDispatch();
      this.FlushEvents();
      this.EndDispatch();
    }

    protected virtual bool VerifyDispatch(GridViewEvent gridEvent)
    {
      if (this.IsDisposing || this.IsDisposed)
        throw new ObjectDisposedException(nameof (GridViewSynchronizationService));
      GridViewEventInfo info = gridEvent.Info;
      if (this.IsDispatchSuspended || this.IsUIEventDispatchSuspended && info.Type == GridEventType.UI || this.IsDataEventDispatchSuspended && info.Type == GridEventType.Data)
        return false;
      return !this.IsEventSuspended(info.Id);
    }

    public void BeginDispatch()
    {
      if (this.beginDispatchCount == byte.MaxValue)
        throw new InvalidOperationException("Synchronization service suspended more that 255 times.");
      ++this.beginDispatchCount;
    }

    public void EndDispatch()
    {
      this.EndDispatch(false);
    }

    public void EndDispatch(bool flushEvents)
    {
      if (this.beginDispatchCount > (byte) 0)
        --this.beginDispatchCount;
      if (!flushEvents || this.beginDispatchCount != (byte) 0)
        return;
      this.FlushEvents();
    }

    public void SuspendDispatch()
    {
      this.SuspendDispatch(GridEventType.Both);
    }

    public void ResumeDispatch()
    {
      this.ResumeDispatch(GridEventType.Both);
    }

    public void SuspendDispatch(GridEventType type)
    {
      if ((type & GridEventType.UI) == GridEventType.UI)
        ++this.uiEventSuspendDispatchCount;
      if ((type & GridEventType.Data) != GridEventType.Data)
        return;
      ++this.dataEventSuspendDispatchCount;
    }

    public void ResumeDispatch(GridEventType type)
    {
      if ((type & GridEventType.UI) == GridEventType.UI && this.uiEventSuspendDispatchCount > (byte) 0)
        --this.uiEventSuspendDispatchCount;
      if ((type & GridEventType.Data) != GridEventType.Data || this.dataEventSuspendDispatchCount <= (byte) 0)
        return;
      --this.dataEventSuspendDispatchCount;
    }

    public void FlushEvents()
    {
      if (this.IsDisposing || this.IsDisposed)
        throw new ObjectDisposedException(nameof (GridViewSynchronizationService));
      ++this.dispatchingCount;
      do
      {
        this.AnalyzeQueue();
        if (this.eventsQueue.Count != 0)
        {
          this.currentEvent = this.eventsQueue.First.Value;
          this.eventsQueue.RemoveFirst();
          if (this.currentEvent.Info.DispatchMode == GridEventDispatchMode.Post)
          {
            this.syncContext.Post(new SendOrPostCallback(this.OnPostCallback), (object) this.currentEvent);
          }
          else
          {
            GridViewEvent currentEvent = this.currentEvent;
            GridViewEventInfo info = currentEvent.Info;
            bool flag = this.CanBeSuspended(info);
            if (flag)
              this.SuspendEvent(info.Id);
            this.NotifyListeners(currentEvent);
            this.currentEvent = currentEvent;
            if (flag)
              this.ResumeEvent(info.Id);
          }
          this.currentEvent = (GridViewEvent) null;
        }
        else
          break;
      }
      while (this.eventsQueue.Count > 0);
      --this.dispatchingCount;
    }

    protected virtual bool CanBeSuspended(GridViewEventInfo eventInfo)
    {
      return eventInfo.Id != KnownEvents.PropertyChanged;
    }

    private void AnalyzeQueue()
    {
      List<GridViewEvent> events = new List<GridViewEvent>((IEnumerable<GridViewEvent>) this.eventsQueue);
      bool flag = false;
      foreach (IGridViewEventListener viewEventListener in this.queueAnalyzers.ReverseForEach())
        flag = viewEventListener.AnalyzeQueue(events) || flag;
      if (!flag)
        return;
      this.eventsQueue.Clear();
      foreach (GridViewEvent gridViewEvent in events)
        this.eventsQueue.AddLast(gridViewEvent);
    }

    public void SuspendEvent(KnownEvents eventId)
    {
      if (!this.suspendedEvents.ContainsKey(eventId))
        this.suspendedEvents.Add(eventId, 0);
      Dictionary<KnownEvents, int> suspendedEvents;
      KnownEvents index;
      (suspendedEvents = this.suspendedEvents)[index = eventId] = suspendedEvents[index] + 1;
    }

    public void ResumeEvent(KnownEvents eventId)
    {
      if (!this.suspendedEvents.ContainsKey(eventId))
        return;
      int suspendedEvent = this.suspendedEvents[eventId];
      if (suspendedEvent == 0)
        return;
      int num = suspendedEvent - 1;
      this.suspendedEvents[eventId] = num;
    }

    private void OnPostCallback(object state)
    {
      GridViewEvent gridEvent = state as GridViewEvent;
      if (gridEvent == null || this.IsDisposing || this.IsDisposed)
        return;
      GridViewEventInfo info = gridEvent.Info;
      bool flag = this.CanBeSuspended(info);
      if (flag)
        this.SuspendEvent(info.Id);
      this.NotifyListeners(gridEvent);
      if (!flag)
        return;
      this.ResumeEvent(info.Id);
    }

    protected virtual void NotifyListeners(GridViewEvent gridEvent)
    {
      GridViewEventInfo info = gridEvent.Info;
      bool flag = (info.Type & GridEventType.Data) == GridEventType.Data;
      if (flag && !this.IsDataEventDispatchSuspended && (!this.dataEntity.PreProcess(gridEvent) || !this.dataEntity.Process(gridEvent)) || (info.Type & GridEventType.UI) == GridEventType.UI && !this.IsUIEventDispatchSuspended && !this.uiEntity.ProcessEvent(gridEvent) || (!flag || this.IsDataEventDispatchSuspended))
        return;
      this.dataEntity.PostProcess(gridEvent);
    }

    public void AddListener(IGridViewEventListener listener)
    {
      GridEventType desiredEvents = listener.DesiredEvents;
      if ((desiredEvents & GridEventType.Data) == GridEventType.Data)
        this.dataEntity.Add(listener);
      if ((desiredEvents & GridEventType.UI) == GridEventType.UI)
        this.uiEntity.Add(listener);
      if ((listener.DesiredProcessMode & GridEventProcessMode.AnalyzeQueue) != GridEventProcessMode.AnalyzeQueue)
        return;
      this.queueAnalyzers.Add(listener);
    }

    public void RemoveListener(IGridViewEventListener listener)
    {
      GridEventType desiredEvents = listener.DesiredEvents;
      if ((desiredEvents & GridEventType.Data) == GridEventType.Data && this.dataEntity.Contains(listener))
        this.dataEntity.Remove(listener);
      if ((desiredEvents & GridEventType.UI) == GridEventType.UI && this.uiEntity.Contains(listener))
        this.uiEntity.Remove(listener);
      if ((listener.DesiredProcessMode & GridEventProcessMode.AnalyzeQueue) != GridEventProcessMode.AnalyzeQueue || this.queueAnalyzers.IndexOf(listener) < 0)
        return;
      this.queueAnalyzers.Remove(listener);
    }

    public bool ContainsListener(IGridViewEventListener listener)
    {
      if (this.IsDisposing || this.IsDisposed)
        throw new ObjectDisposedException(nameof (GridViewSynchronizationService));
      GridEventType desiredEvents = listener.DesiredEvents;
      return (desiredEvents & GridEventType.Data) == GridEventType.Data && this.dataEntity.Contains(listener) || (desiredEvents & GridEventType.UI) == GridEventType.UI && this.uiEntity.Contains(listener);
    }

    public static bool IsEventSuspended(GridViewTemplate template, KnownEvents eventId)
    {
      if (template == null)
        throw new ArgumentNullException("Template");
      MasterGridViewTemplate masterTemplate = template.MasterTemplate;
      if (masterTemplate != null)
        return masterTemplate.SynchronizationService.IsEventSuspended(eventId);
      return false;
    }

    public static void RaiseCurrentChanged(
      GridViewTemplate template,
      GridViewRowInfo row,
      GridViewColumn column,
      bool user)
    {
      if (template == null)
        throw new ArgumentNullException("Template");
      MasterGridViewTemplate masterTemplate = template.MasterTemplate;
      if (masterTemplate == null)
        return;
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.CurrentChanged, GridEventType.Both, GridEventDispatchMode.Send);
      object originator = user ? (object) (RadCollectionView<GridViewRowInfo>) null : (object) template.DataView;
      masterTemplate.SynchronizationService.DispatchEvent(new GridViewEvent((object) template, originator, new object[2]
      {
        (object) row,
        (object) column
      }, eventInfo));
    }

    public static void SuspendEvent(GridViewTemplate template, KnownEvents eventId)
    {
      if (template == null)
        throw new ArgumentNullException("Template");
      template.MasterTemplate?.SynchronizationService.SuspendEvent(eventId);
    }

    public static void ResumeEvent(GridViewTemplate template, KnownEvents eventId)
    {
      if (template == null)
        throw new ArgumentNullException("Template");
      template.MasterTemplate?.SynchronizationService.ResumeEvent(eventId);
    }

    public static void DispatchEvent(
      GridViewTemplate template,
      GridViewEvent eventData,
      bool postUI)
    {
      if (template == null)
        throw new ArgumentNullException("Template");
      MasterGridViewTemplate masterTemplate = template.MasterTemplate;
      if (masterTemplate == null)
        return;
      GridViewSynchronizationService synchronizationService = masterTemplate.SynchronizationService;
      if (synchronizationService == null)
        return;
      if (eventData.Info.Type == GridEventType.Both && postUI)
      {
        KnownEvents id = eventData.Info.Id;
        bool critical = eventData.Info.Critical;
        object[] arguments = eventData.Arguments;
        object sender = eventData.Sender;
        object originator = eventData.Originator;
        GridViewEventInfo eventInfo1 = new GridViewEventInfo(id, GridEventType.Data, GridEventDispatchMode.Send, critical);
        GridViewEvent gridEvent1 = new GridViewEvent(sender, originator, arguments, eventInfo1);
        GridViewEventInfo eventInfo2 = new GridViewEventInfo(id, GridEventType.UI, GridEventDispatchMode.Post, critical);
        GridViewEvent gridEvent2 = new GridViewEvent(sender, originator, arguments, eventInfo2);
        synchronizationService.DispatchEvent(gridEvent1);
        synchronizationService.DispatchEvent(gridEvent2);
      }
      else
        synchronizationService.DispatchEvent(eventData);
    }

    public static bool IsGroupCollectionChangedEvent(GridViewEvent gridViewEvent)
    {
      if (gridViewEvent.Info.Id == KnownEvents.CollectionChanged)
        return gridViewEvent.Sender is GridViewGroupDescriptorCollection;
      return false;
    }

    public static bool IsColumnsCollectionChangedEvent(GridViewEvent gridViewEvent)
    {
      GridViewTemplate sender = gridViewEvent.Sender as GridViewTemplate;
      if (gridViewEvent.Info.Id == KnownEvents.CollectionChanged && sender != null)
        return gridViewEvent.Originator == sender.Columns;
      return false;
    }

    public static bool IsConditionalFormattingCollectionChangedEvent(GridViewEvent gridViewEvent)
    {
      if (gridViewEvent.Originator is ConditionalFormattingObjectCollection && gridViewEvent.Sender is GridViewColumn)
        return gridViewEvent.Info.Id == KnownEvents.CollectionChanged;
      return false;
    }

    public static bool IsTemplatePropertyChangedEvent(GridViewEvent gridViewEvent)
    {
      if (gridViewEvent.Info.Id == KnownEvents.PropertyChanged)
        return gridViewEvent.Sender is GridViewTemplate;
      return false;
    }

    public static bool IsTemplatePropertyChangingEvent(GridViewEvent gridViewEvent)
    {
      if (gridViewEvent.Info.Id == KnownEvents.PropertyChanging)
        return gridViewEvent.Sender is GridViewTemplate;
      return false;
    }

    public static bool IsRowPropertyChangedEvent(GridViewEvent gridViewEvent)
    {
      if (gridViewEvent.Info.Id == KnownEvents.PropertyChanged)
        return gridViewEvent.Sender is GridViewRowInfo;
      return false;
    }
  }
}
