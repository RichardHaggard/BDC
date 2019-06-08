// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPanelScrollParametersEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadPanelScrollParametersEventArgs : EventArgs
  {
    private bool isHorizontalScrollBar;
    private ScrollBarParameters scrollBarParameters;

    public bool IsHorizontalScrollBar
    {
      get
      {
        return this.isHorizontalScrollBar;
      }
    }

    public ScrollBarParameters ScrollBarParameters
    {
      get
      {
        return this.scrollBarParameters;
      }
    }

    public RadPanelScrollParametersEventArgs(
      bool isHorizontalScrollBar,
      ScrollBarParameters scrollBarParameters)
    {
      this.isHorizontalScrollBar = isHorizontalScrollBar;
      this.scrollBarParameters = scrollBarParameters;
    }
  }
}
