// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewModeEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadPageViewModeEventArgs : EventArgs
  {
    private PageViewMode viewMode;

    public RadPageViewModeEventArgs(PageViewMode mode)
    {
      this.viewMode = mode;
    }

    public PageViewMode ViewMode
    {
      get
      {
        return this.viewMode;
      }
    }
  }
}
