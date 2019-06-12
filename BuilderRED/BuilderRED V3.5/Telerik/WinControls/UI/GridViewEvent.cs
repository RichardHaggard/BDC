// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewEvent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridViewEvent
  {
    private object sender;
    private object originator;
    private object[] arguments;
    private GridViewEventInfo info;
    private GridViewSynchronizationService service;

    public GridViewEvent(
      object sender,
      object originator,
      object[] arguments,
      GridViewEventInfo eventInfo)
    {
      this.sender = sender;
      this.originator = originator;
      this.arguments = arguments;
      this.info = eventInfo;
    }

    public object Originator
    {
      get
      {
        return this.originator;
      }
    }

    public object[] Arguments
    {
      get
      {
        return this.arguments;
      }
    }

    public GridViewEventInfo Info
    {
      get
      {
        return this.info;
      }
    }

    public object Sender
    {
      get
      {
        return this.sender;
      }
    }

    public GridViewSynchronizationService SynchronizationService
    {
      get
      {
        return this.service;
      }
      internal set
      {
        this.service = value;
      }
    }
  }
}
