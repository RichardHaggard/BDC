// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandTabSelectingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class CommandTabSelectingEventArgs : CancelEventArgs
  {
    private RibbonTab oldCommandTab;
    private RibbonTab newCommandTab;

    public CommandTabSelectingEventArgs(RibbonTab oldCommandTab, RibbonTab newCommandTab)
    {
      this.oldCommandTab = oldCommandTab;
      this.newCommandTab = newCommandTab;
    }

    public RibbonTab OldCommandTab
    {
      get
      {
        return this.oldCommandTab;
      }
    }

    public RibbonTab NewCommandTab
    {
      get
      {
        return this.newCommandTab;
      }
    }
  }
}
