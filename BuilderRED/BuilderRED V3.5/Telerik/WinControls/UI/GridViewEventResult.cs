// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewEventResult
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridViewEventResult
  {
    private bool handled;
    private bool stopDispatch;

    public GridViewEventResult()
    {
      this.handled = false;
      this.stopDispatch = false;
    }

    public GridViewEventResult(bool handled, bool stopDispatch)
    {
      this.handled = handled;
      this.stopDispatch = stopDispatch;
    }

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    public bool StopDispatch
    {
      get
      {
        return this.stopDispatch;
      }
      set
      {
        this.stopDispatch = value;
      }
    }
  }
}
