// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewEventProcessEntity
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  internal class GridViewEventProcessEntity
  {
    private PriorityWeakReferenceList preProcessListeners;
    private PriorityWeakReferenceList processListeners;
    private PriorityWeakReferenceList postProcessListeners;
    private GridViewSynchronizationService owner;

    public GridViewEventProcessEntity(GridViewSynchronizationService owner)
    {
      this.preProcessListeners = new PriorityWeakReferenceList();
      this.processListeners = new PriorityWeakReferenceList();
      this.postProcessListeners = new PriorityWeakReferenceList();
      this.owner = owner;
    }

    public void Clear()
    {
      this.preProcessListeners.Clear();
      this.processListeners.Clear();
      this.postProcessListeners.Clear();
    }

    public void Add(IGridViewEventListener listener)
    {
      if (!this.VerifyAdd(listener))
        return;
      GridEventProcessMode desiredProcessMode = listener.DesiredProcessMode;
      if ((desiredProcessMode & GridEventProcessMode.PreProcess) == GridEventProcessMode.PreProcess)
        this.preProcessListeners.Add(listener);
      if ((desiredProcessMode & GridEventProcessMode.Process) == GridEventProcessMode.Process)
        this.processListeners.Add(listener);
      if ((desiredProcessMode & GridEventProcessMode.PostProcess) != GridEventProcessMode.PostProcess)
        return;
      this.postProcessListeners.Add(listener);
    }

    public void Remove(IGridViewEventListener listener)
    {
      GridEventProcessMode desiredProcessMode = listener.DesiredProcessMode;
      if ((desiredProcessMode & GridEventProcessMode.PreProcess) == GridEventProcessMode.PreProcess)
        this.preProcessListeners.Remove(listener);
      if ((desiredProcessMode & GridEventProcessMode.Process) == GridEventProcessMode.Process)
        this.processListeners.Remove(listener);
      if ((desiredProcessMode & GridEventProcessMode.PostProcess) != GridEventProcessMode.PostProcess)
        return;
      this.postProcessListeners.Remove(listener);
    }

    public bool Contains(IGridViewEventListener listener)
    {
      if (listener == null)
        return false;
      GridEventProcessMode desiredProcessMode = listener.DesiredProcessMode;
      return (desiredProcessMode & GridEventProcessMode.PreProcess) == GridEventProcessMode.PreProcess && this.preProcessListeners.IndexOf(listener) >= 0 || (desiredProcessMode & GridEventProcessMode.Process) == GridEventProcessMode.Process && this.processListeners.IndexOf(listener) >= 0 || (desiredProcessMode & GridEventProcessMode.PostProcess) == GridEventProcessMode.PostProcess && this.postProcessListeners.IndexOf(listener) >= 0;
    }

    public bool PreProcess(GridViewEvent gridEvent)
    {
      return this.ProcessCollection(gridEvent, this.preProcessListeners, GridEventProcessMode.PreProcess);
    }

    public bool Process(GridViewEvent gridEvent)
    {
      return this.ProcessCollection(gridEvent, this.processListeners, GridEventProcessMode.Process);
    }

    public bool PostProcess(GridViewEvent gridEvent)
    {
      return this.ProcessCollection(gridEvent, this.postProcessListeners, GridEventProcessMode.PostProcess);
    }

    public bool ProcessEvent(GridViewEvent gridEvent)
    {
      if (!this.PreProcess(gridEvent) || !this.Process(gridEvent))
        return false;
      return this.PostProcess(gridEvent);
    }

    protected virtual bool ProcessCollection(
      GridViewEvent gridEvent,
      PriorityWeakReferenceList list,
      GridEventProcessMode processMode)
    {
      bool flag = false;
      foreach (IGridViewEventListener viewEventListener in list.ReverseForEach())
      {
        switch (processMode)
        {
          case GridEventProcessMode.Process:
            GridViewEventResult gridViewEventResult1 = viewEventListener.ProcessEvent(gridEvent);
            flag = gridViewEventResult1 != null && gridViewEventResult1.StopDispatch;
            break;
          case GridEventProcessMode.PreProcess:
            GridViewEventResult gridViewEventResult2 = viewEventListener.PreProcessEvent(gridEvent);
            flag = gridViewEventResult2 != null && gridViewEventResult2.StopDispatch;
            break;
          default:
            GridViewEventResult gridViewEventResult3 = viewEventListener.PostProcessEvent(gridEvent);
            flag = gridViewEventResult3 != null && gridViewEventResult3.StopDispatch;
            break;
        }
        if (flag)
          break;
      }
      return !flag;
    }

    private bool VerifyAdd(IGridViewEventListener listener)
    {
      return !this.owner.IsDisposing && !this.owner.IsDisposed && !this.Contains(listener);
    }
  }
}
