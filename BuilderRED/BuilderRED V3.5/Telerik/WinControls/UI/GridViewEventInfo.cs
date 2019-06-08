// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewEventInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public struct GridViewEventInfo
  {
    private KnownEvents id;
    private GridEventDispatchMode dispatchMode;
    private GridEventType type;
    private bool critical;

    public GridViewEventInfo(
      KnownEvents id,
      GridEventType type,
      GridEventDispatchMode dispatchMode)
    {
      this = new GridViewEventInfo(id, type, dispatchMode, false);
    }

    public GridViewEventInfo(
      KnownEvents id,
      GridEventType type,
      GridEventDispatchMode dispatchMode,
      bool critical)
    {
      this.id = id;
      this.type = type;
      this.dispatchMode = dispatchMode;
      this.critical = critical;
    }

    public KnownEvents Id
    {
      get
      {
        return this.id;
      }
    }

    public GridEventType Type
    {
      get
      {
        return this.type;
      }
    }

    public GridEventDispatchMode DispatchMode
    {
      get
      {
        return this.dispatchMode;
      }
    }

    public bool Critical
    {
      get
      {
        return this.critical;
      }
    }
  }
}
